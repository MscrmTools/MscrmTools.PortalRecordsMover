using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.PortalRecordsMover.AppCode;
using MscrmTools.PortalRecordsMover.Controls;
using MscrmTools.PortalRecordsMover.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MscrmTools.PortalRecordsMover
{
    public partial class MyPluginControl : PluginControlBase, IGitHubPlugin, IHelpPlugin, IStatusBarMessenger
    {
        private ExportSettings settings;

        private List<EntityMetadata> emds = new List<EntityMetadata>();

        public MyPluginControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            settings = new ExportSettings();
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public string RepositoryName => "MscrmTools.PortalRecordsMover";
        public string UserName => "MscrmTools";
        public string HelpUrl => "https://github.com/MscrmTools/MscrmTools.PortalRecordsMover/wiki";

        #region Events

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbLoad_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadItems);
        }

        private void tsbRetrieveRecords_Click(object sender, EventArgs e)
        {
            if (ecpEntities.SelectedMetadatas.Count == 0)
            {
                MessageBox.Show(this, "No entity selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComputeSettings();

            tabCtrl.SelectedIndexChanged -= tabCtrl_SelectedIndexChanged;
            cbbTabSelection.SelectedIndexChanged -= cbbTabSelection_SelectedIndexChanged;

            tabCtrl.TabPages.Clear();
            cbbTabSelection.Items.Clear();

            tabCtrl.SelectedIndexChanged += tabCtrl_SelectedIndexChanged;
            cbbTabSelection.SelectedIndexChanged += cbbTabSelection_SelectedIndexChanged;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                AsyncArgument = settings,
                IsCancelable = true,
                Work = (bw, evt) =>
                {
                    var exSettings = (ExportSettings)evt.Argument;

                    if (bw.CancellationPending)
                    {
                        bw.ReportProgress(0, "Cancelling...");
                        bw.CancelAsync();
                    }

                    var results = new ExportResults { Settings = exSettings };

                    bw.ReportProgress(0, "Retrieving selected entities views layout...");

                    results.Views = RetrieveViews(exSettings.Entities);

                    foreach (var entity in exSettings.Entities)
                    {
                        bw.ReportProgress(0, $"Retrieving records for entity {entity.DisplayName?.UserLocalizedLabel?.Label ?? entity.LogicalName}...");

                        var er = new EntityResult
                        {
                            Records = RetrieveRecords(entity, exSettings)
                        };

                        results.Entities.Add(er);
                    }

                    bw.ReportProgress(0, "Retrieving many to many relationships records...");
                    results.NnRecords = RetrieveNnRecords(exSettings, results.Entities.SelectMany(a => a.Records.Entities).ToList());

                    evt.Result = results;
                },
                PostWorkCallBack = evt =>
                {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(string.Empty));

                    if (evt.Cancelled)
                    {
                        return;
                    }

                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $"An error occured: {evt.Error.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    tsbExport.Enabled = true;
                    pnlTabSelection.Visible = true;

                    var results = (ExportResults)evt.Result;

                    foreach (var entity in results.Entities)
                    {
                        var emd = results.Settings.AllEntities.First(ent => ent.LogicalName == entity.Records.EntityName);
                        var tabPage = new TabPage($"{emd.DisplayName?.UserLocalizedLabel?.Label ?? emd.SchemaName} ({entity.Records.Entities.Count})");
                        tabCtrl.TabPages.Add(tabPage);

                        cbbTabSelection.Items.Add(tabPage.Text);

                        var layoutxml = results.Views.First(
                            v => v.GetAttributeValue<string>("returnedtypecode") == emd.LogicalName)
                            .GetAttributeValue<string>("layoutxml");

                        var rl = new RecordsListerControl(entity.Records.Entities.ToList(), emd, layoutxml)
                        {
                            Dock = DockStyle.Fill
                        };

                        tabPage.Controls.Add(rl);
                    }

                    foreach (var entity in results.NnRecords)
                    {
                        var emd = results.Settings.AllEntities.First(ent => ent.LogicalName == entity.Records.EntityName);
                        var rel = results.Settings.AllEntities
                            .SelectMany(ent => ent.ManyToManyRelationships)
                            .First(r => r.IntersectEntityName == emd.LogicalName);

                        var name = $"{results.Settings.AllEntities.First(ent => ent.LogicalName == rel.Entity1LogicalName).DisplayName?.UserLocalizedLabel?.Label} / {results.Settings.AllEntities.First(ent => ent.LogicalName == rel.Entity2LogicalName).DisplayName?.UserLocalizedLabel?.Label}";

                        var tabPage = new TabPage(name);
                        tabCtrl.TabPages.Add(tabPage);

                        cbbTabSelection.Items.Add(name);

                        var rl = new RecordsListerControl(entity.Records.Entities.ToList(), results.Entities.SelectMany(rs => rs.Records.Entities).ToList(), rel, results.Settings.AllEntities)
                        {
                            Dock = DockStyle.Fill
                        };

                        tabPage.Controls.Add(rl);
                    }

                    if (cbbTabSelection.Items.Count > 0)
                    {
                        cbbTabSelection.SelectedIndex = 0;
                    }
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
            });
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            var ec = new EntityCollection();

            // Gathering records to export
            foreach (TabPage tp in tabCtrl.TabPages)
            {
                var rlc = (RecordsListerControl)tp.Controls[0];
                ec.Entities.AddRange(rlc.Records);
            }

            if (ec.Entities.Count == 0)
            {
                MessageBox.Show(this, "No record selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WorkAsync(new WorkAsyncInfo
            {
                AsyncArgument = ec,
                Work = (bw, evt) =>
                {
                    var list = (EntityCollection)evt.Argument;

                    var webFiles = list.Entities.Where(ent => ent.LogicalName == "adx_webfile").ToList();
                    if (webFiles.Any())
                    {
                        bw.ReportProgress(0, "Retrieving web files annotation records...");
                        var records = RetrieveWebfileAnnotations(webFiles.Select(w => w.Id).ToList());
                        foreach (var record in records)
                        {
                            ec.Entities.Insert(0, record);
                        }
                    }

                    bw.ReportProgress(0, "Exporting records...");

                    MemoryStream ms = new MemoryStream();

                    var serializer = new DataContractSerializer(typeof(EntityCollection), new List<Type> { typeof(Entity) });
                    serializer.WriteObject(ms, list);

                    evt.Result = ms;
                },
                PostWorkCallBack = evt =>
                {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(string.Empty));

                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $"An error occured: {evt.Error.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    var ms = (MemoryStream)evt.Result;

                    var sfd = new SaveFileDialog
                    {
                        Filter = "XML document (*.xml)|*.xml",
                        AddExtension = true
                    };

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        using (StreamWriter fs = new StreamWriter(sfd.FileName, false))
                        {
                            ms.WriteTo(fs.BaseStream);
                        }

                        MessageBox.Show(this, $"Records exported to {sfd.FileName}!", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
            });
        }

        private void tsbImportRecords_Click(object sender, EventArgs e)
        {
            pnlImport.BringToFront();
            tsMain.Enabled = false;
            pnlImport.Visible = true;
            btnImport.Enabled = false;
        }

        private void btnBrowseImportFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "XML File (*.xml)|*.xml",
                Title = "Select the file containing portal records to import"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                btnImport.Enabled = true;
                txtImportFilePath.Text = ofd.FileName;
            }
            else if (txtImportFilePath.Text.Length == 0)
            {
                btnImport.Enabled = false;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ExecuteMethod(Import);
        }

        private void llOpenLogFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Path.Combine(Paths.LogsPath, $"{GetType().Assembly.FullName.Split(',')[0]}.log");
            Process.Start("notepad.exe", path);
        }

        private void btnImportClose_Click(object sender, EventArgs e)
        {
            tsMain.Enabled = true;
            txtImportFilePath.Text = string.Empty;
            btnImport.Enabled = false;
            pnlImport.Visible = false;
            pbImport.Value = 0;
            lblProgress.Text = string.Empty;
            lvProgress.Items.Clear();
            pnlImportMain.Visible = false;
            llOpenLogFile.Visible = false;
        }

        private void cbbTabSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTabSelection.SelectedItem.ToString() != tabCtrl.SelectedTab.Text)
            {
                foreach (TabPage tab in tabCtrl.TabPages)
                {
                    if (tab.Text == cbbTabSelection.Text)
                    {
                        tabCtrl.SelectTab(tab);
                        break;
                    }
                }
            }
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTabSelection.SelectedItem?.ToString() != tabCtrl.SelectedTab.Text)
            {
                cbbTabSelection.SelectedItem = tabCtrl.SelectedTab.Text;
            }
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofDialog = new OpenFileDialog { Filter = "XML Document (*.xml)|*.xml" };
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                var xdoc = new XmlDocument();
                xdoc.Load(ofDialog.FileName);
                try
                {
                    settings = (ExportSettings)XmlSerializerHelper.Deserialize(xdoc.OuterXml, typeof(ExportSettings));

                    chkActiveOnly.Checked = settings.ActiveItemsOnly;
                    docCreateFilter.IsEnabled = settings.CreateFilter.HasValue;
                    docCreateFilter.SelectedDate = settings.CreateFilter ?? DateTime.Today;
                    docModifyFilter.IsEnabled = settings.ModifyFilter.HasValue;
                    docModifyFilter.SelectedDate = settings.ModifyFilter ?? DateTime.Today;
                    wpcWebsiteFilter.SelectedWebSiteId = settings.WebsiteFilter;
                    wpcWebsiteFilter.IsEnabled = settings.WebsiteFilter != Guid.Empty;
                    ecpEntities.SelectItems(settings.SelectedEntities);
                }
                catch (Exception error)
                {
                    MessageBox.Show(this, $"An error occured: {error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfDialog = new SaveFileDialog { Filter = "XML Document (*.xml)|*.xml" };
            if (sfDialog.ShowDialog(this) == DialogResult.OK)
            {
                ComputeSettings();
                XmlSerializerHelper.SerializeToFile(settings, sfDialog.FileName);
                MessageBox.Show(this, $"Settings saved to {sfDialog.FileName}", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Methods

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            ecpEntities.Service = newService;

            base.UpdateConnection(newService, detail, actionName, parameter);
        }

        private void ComputeSettings()
        {
            settings.CreateFilter = docCreateFilter.IsEnabled ? docCreateFilter.SelectedDate : new DateTime?();
            settings.ModifyFilter = docModifyFilter.IsEnabled ? docCreateFilter.SelectedDate : new DateTime?();
            settings.WebsiteFilter = wpcWebsiteFilter.SelectedWebSiteId;
            settings.SelectedEntities = ecpEntities.SelectedMetadatas.Select(emd => emd.LogicalName).ToList();
            settings.AllEntities = ecpEntities.Metadata;
            settings.ActiveItemsOnly = chkActiveOnly.Checked;
        }

        public void LoadItems()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                Work = (bw, evt) =>
                {
                    ecpEntities.LoadEntities(settings);
                    emds = ecpEntities.Metadata;

                    bw.ReportProgress(0, "Loading Web sites");

                    evt.Result = Service.RetrieveMultiple(new QueryExpression("adx_website")
                    {
                        ColumnSet = new ColumnSet(true)
                    }).Entities.Select(record => new Website(record)).ToList();
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $"An error occured: {evt.Error.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    ecpEntities.FillList();
                    wpcWebsiteFilter.Websites = (List<Website>)evt.Result;

                    tsbRetrieveRecords.Enabled = true;
                    tsddSettings.Enabled = true;
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
            });
        }

        private List<Entity> RetrieveViews(List<EntityMetadata> entities)
        {
            var query = new QueryExpression("savedquery")
            {
                ColumnSet = new ColumnSet("returnedtypecode", "layoutxml"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("isquickfindquery", ConditionOperator.Equal, true),
                        new ConditionExpression("returnedtypecode", ConditionOperator.In, entities.Select(e=>e.LogicalName).ToArray())
                    }
                }
            };

            return Service.RetrieveMultiple(query).Entities.ToList();
        }

        private EntityCollection RetrieveRecords(EntityMetadata emd, ExportSettings settings)
        {
            var validAttributes = emd.Attributes
                .Where(a => a.IsValidForCreate.Value || a.IsValidForUpdate.Value)
                .Select(a => a.LogicalName)
                .ToArray();

            var query = new QueryExpression(emd.LogicalName)
            {
                ColumnSet = new ColumnSet(validAttributes),
                Criteria = new FilterExpression()
            };

            if (settings.CreateFilter.HasValue)
            {
                query.Criteria.AddCondition("createdon", ConditionOperator.OnOrAfter, settings.CreateFilter.Value.ToString("yyyy-MM-dd"));
            }

            if (settings.ModifyFilter.HasValue)
            {
                query.Criteria.AddCondition("modifiedon", ConditionOperator.OnOrAfter, settings.ModifyFilter.Value.ToString("yyyy-MM-dd"));
            }

            if (settings.WebsiteFilter != null && emd.Attributes.Any(a => a is LookupAttributeMetadata && ((LookupAttributeMetadata)a).Targets[0] == "adx_website"))
            {
                query.Criteria.AddCondition("adx_websiteid", ConditionOperator.Equal, settings.WebsiteFilter);
            }

            if (settings.ActiveItemsOnly)
            {
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            }

            return Service.RetrieveMultiple(query);
        }

        private List<Entity> RetrieveWebfileAnnotations(List<Guid> ids)
        {
            return Service.RetrieveMultiple(new QueryExpression("annotation")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("objectid", ConditionOperator.In, ids.ToArray())
                    }
                }
            }).Entities.ToList();
        }

        private List<EntityResult> RetrieveNnRecords(ExportSettings settings, List<Entity> records)
        {
            var ers = new List<EntityResult>();
            var rels = new List<ManyToManyRelationshipMetadata>();

            foreach (var emd in settings.Entities)
            {
                foreach (var mm in emd.ManyToManyRelationships)
                {
                    var e1 = mm.Entity1LogicalName;
                    var e2 = mm.Entity2LogicalName;
                    var isValid = false;

                    if (e1 == emd.LogicalName)
                    {
                        if (settings.Entities.Any(e => e.LogicalName == e2))
                        {
                            isValid = true;
                        }
                    }
                    else
                    {
                        if (settings.Entities.Any(e => e.LogicalName == e1))
                        {
                            isValid = true;
                        }
                    }

                    if (isValid && rels.All(r => r.IntersectEntityName != mm.IntersectEntityName))
                    {
                        rels.Add(mm);
                    }
                }
            }

            foreach (var mm in rels)
            {
                var ids = records.Where(r => r.LogicalName == mm.Entity1LogicalName).Select(r => r.Id).ToList();
                if (!ids.Any())
                {
                    continue;
                }

                var query = new QueryExpression(mm.IntersectEntityName)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression(mm.Entity1IntersectAttribute, ConditionOperator.In, ids.ToArray())
                        }
                    }
                };

                ers.Add(new EntityResult { Records = Service.RetrieveMultiple(query) });
            }

            return ers;
        }

        private void Import()
        {
            if (txtImportFilePath.Text.Length == 0)
            {
                return;
            }

            if (!File.Exists(txtImportFilePath.Text))
            {
                MessageBox.Show(this, $"The file {txtImportFilePath.Text} does not exist!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblProgress.Text = "Deserializing file...";
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs("Deserializing file..."));

            EntityCollection ec;
            using (var reader = new StreamReader(txtImportFilePath.Text))
            {
                var serializer = new DataContractSerializer(typeof(EntityCollection), new List<Type> { typeof(Entity) });
                ec = (EntityCollection)serializer.ReadObject(reader.BaseStream);
            }

            var webSitesId = ec.Entities.SelectMany(e => e.Attributes)
                .Where(a => a.Value is EntityReference && ((EntityReference)a.Value).LogicalName == "adx_website")
                .Select(a => ((EntityReference) a.Value).Id)
                .Distinct()
                .ToList();

            var targetWebSites = Service.RetrieveMultiple(new QueryExpression("adx_website")
            {
                ColumnSet = new ColumnSet("adx_name")
            }).Entities;

            if (!webSitesId.All(id => targetWebSites.Select(w => w.Id).Contains(id)))
            {
                var wsmDialog =new WebSiteMapper(ec, targetWebSites.Select(t => new Website(t)).ToList());
                if (wsmDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
            }

            btnImport.Enabled = false;
            pnlImportMain.Visible = true;
            pbImport.IsOnError = false;

            var worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += (s, evt) =>
            {
                var bw = (BackgroundWorker)s;
               
                if (emds.Count == 0)
                {
                    bw.ReportProgress(0, "Retrieving metadata...");
                    emds = MetadataManager.GetEntitiesList(Service);
                }

                bw.ReportProgress(0, "Processing records...");

                var rm = new RecordManager(Service);
                rm.ProcessRecords((EntityCollection)evt.Argument, emds, bw);
            };
            worker.RunWorkerCompleted += (s, evt) =>
            {
                SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(string.Empty));
                lblProgress.Text = "Records imported!";
                llOpenLogFile.Visible = true;

                btnImportClose.Enabled = true;
                btnImport.Enabled = true;

                if (evt.Cancelled)
                {
                    lblProgress.Text = "Canceled!";
                    return;
                }

                if (evt.Error != null)
                {
                    MessageBox.Show(this, $"An error occured: {evt.Error.Message}", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (pbImport.IsOnError)
                {
                    MessageBox.Show(this, "Import complete with errors\n\nPlease review the logs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(this, "Import complete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            worker.ProgressChanged += (s, evt) =>
            {
                if (evt.UserState is string)
                {
                    lblProgress.Text = evt.UserState.ToString();
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
                else
                {
                    var progress = evt.UserState as ImportProgress;
                    if (progress != null)
                    {
                        foreach (var ep in progress.Entities)
                        {
                            var item = lvProgress.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Text == ep.Entity);
                            if (item == null)
                            {
                                item = new ListViewItem(ep.Entity);
                                item.SubItems.Add(ep.Success.ToString());
                                item.SubItems.Add(ep.Error.ToString());
                                lvProgress.Items.Add(item);
                            }
                            else
                            {
                                item.SubItems[1].Text = ep.Success.ToString();
                                item.SubItems[2].Text = ep.Error.ToString();
                            }

                            if (ep.Error > 0)
                            {
                                pbImport.IsOnError = true;
                            }

                            pbImport.Value = progress.Entities.Sum(ent => ent.Processed)*100/progress.Count;
                        }
                    }
                    else if (evt.UserState is bool)
                    {
                        if ((bool) evt.UserState == false)
                        {
                            pbImport.IsOnError = true;
                        }
                        pbImport.Value = evt.ProgressPercentage;

                    }
                }
            };
            worker.RunWorkerAsync(ec);
        }

        #endregion
    }
}
