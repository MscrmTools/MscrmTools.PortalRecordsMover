using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.PortalRecordsMover.AppCode;
using MscrmTools.PortalRecordsMover.Controls;
using MscrmTools.PortalRecordsMover.Forms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MscrmTools.PortalRecordsMover
{
    public partial class MyPluginControl : MultipleConnectionsPluginControlBase, IGitHubPlugin, IHelpPlugin, IStatusBarMessenger
    {
        private readonly ImportSettings iSettings;
        private readonly LogManager logger;
        private List<EntityMetadata> emds = new List<EntityMetadata>();
        private BackgroundWorker importWorker;
        private NoteManager nManager;
        private PluginManager pManager;
        private RecordManager rManager;
        private ExportSettings settings;
        private IOrganizationService targetService;

        public MyPluginControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            settings = new ExportSettings();
            iSettings = new ImportSettings();

            logger = new LogManager(GetType());

            docCreateFilter.SelectedDate = DateTime.Now;
            docModifyFilter.SelectedDate = DateTime.Now;
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public string HelpUrl => "https://github.com/MscrmTools/MscrmTools.PortalRecordsMover/wiki";
        public string RepositoryName => "MscrmTools.PortalRecordsMover";
        public string UserName => "MscrmTools";

        #region Events

        private void btnBrowseImportFile_Click(object sender, EventArgs e)
        {
            if (rdbSelectFile.Checked)
            {
                var ofd = new OpenFileDialog
                {
                    Filter = @"XML or Zip File (*.xml,*.zip)|*.xml;*.zip",
                    Title = @"Select the file containing portal records to import"
                };

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtImportFilePath.Text = ofd.FileName;
                }
            }
            else
            {
                var fbd = new FolderBrowserDialog
                {
                    Description = @"Folder with portal data to import"
                };

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    txtImportFilePath.Text = fbd.SelectedPath;
                }
            }

            btnImport.Enabled = txtImportFilePath.Text.Length > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            importWorker?.CancelAsync();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ExecuteMethod(Import);
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

        private void cbExportAsFolderStructure_CheckedChanged(object sender, EventArgs e)
        {
            cbZipFolderStructure.Enabled = cbExportAsFolderStructure.Checked;
            if (!cbZipFolderStructure.Enabled)
            {
                cbZipFolderStructure.Checked = false;
            }
        }

        private void llOpenLogFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Path.Combine(Paths.LogsPath, $"{GetType().Assembly.FullName.Split(',')[0]}.log");
            Process.Start("notepad.exe", path);
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofDialog = new OpenFileDialog { Filter = @"XML Document (*.xml)|*.xml" };
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
                    cbExportAsFolderStructure.Checked = settings.ExportInFolderStructure;
                    cbZipFolderStructure.Checked = settings.ZipFolderStructure;
                }
                catch (Exception error)
                {
                    MessageBox.Show(this, $@"An error occured: {error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            try
            {
                var assemblies = (from a in AppDomain.CurrentDomain.GetAssemblies()
                                  where a.CustomAttributes.Any(ca => ca.AttributeType == typeof(ProxyTypesAssemblyAttribute))
                                      && !a.GetName().Name.StartsWith("Microsoft.")
                                  select a).ToList();
                if (assemblies.Count > 0)
                {
                    MessageBox.Show(this,
                        $@"Potential conflict detected!

The following files are not respecting XrmToolBox best practices and might prevent this tool from transfering records correctly:

{string.Join(Environment.NewLine, assemblies.Select(a => "- " + a.GetName().Name))}

If you experience issue when transfering some records, especially annotations, please try to remove the corresponding tools from XrmToolBox", @"Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch
            {
                // Do nothing. We don't want to fail here
            }
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfDialog = new SaveFileDialog { Filter = @"XML Document (*.xml)|*.xml" };
            if (sfDialog.ShowDialog(this) == DialogResult.OK)
            {
                ComputeSettings();
                XmlSerializerHelper.SerializeToFile(settings, sfDialog.FileName);
                MessageBox.Show(this, $@"Settings saved to {sfDialog.FileName}", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTabSelection.SelectedItem?.ToString() != tabCtrl.SelectedTab.Text)
            {
                cbbTabSelection.SelectedItem = tabCtrl.SelectedTab.Text;
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            ExportData(true);
        }

        private void tsbImportRecords_Click(object sender, EventArgs e)
        {
            lblImportHeader.Text = @"Portal Records Import";
            pnlImportFile.Visible = true;
            pnlImport.BringToFront();
            tsMain.Enabled = false;
            pnlImport.Visible = true;
            btnImport.Visible = true;
            btnImport.Enabled = false;
        }

        private void tsbLoad_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadItems);
        }

        private void tsbRetrieveRecords_Click(object sender, EventArgs e)
        {
            if (ecpEntities.SelectedMetadatas.Count == 0)
            {
                MessageBox.Show(this, @"No entity selected!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComputeSettings();

            tabCtrl.SelectedIndexChanged -= tabCtrl_SelectedIndexChanged;
            cbbTabSelection.SelectedIndexChanged -= cbbTabSelection_SelectedIndexChanged;

            tabCtrl.TabPages.Clear();
            cbbTabSelection.Items.Clear();

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

                    results.Views = rManager.RetrieveViews(exSettings.Entities);

                    foreach (var entity in exSettings.Entities)
                    {
                        bw.ReportProgress(0, $"Retrieving records for entity {entity.DisplayName?.UserLocalizedLabel?.Label ?? entity.LogicalName}...");

                        var er = new EntityResult
                        {
                            Records = rManager.RetrieveRecords(entity, exSettings)
                        };

                        results.Entities.Add(er);
                    }

                    bw.ReportProgress(0, "Retrieving many to many relationships records...");
                    results.NnRecords = rManager.RetrieveNnRecords(exSettings, results.Entities.SelectMany(a => a.Records.Entities).ToList());

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
                        MessageBox.Show(this, $@"An error occured: {evt.Error.Message}", @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    tsbExport.Enabled = true;
                    tssbTransferData.Enabled = true;
                    pnlTabSelection.Visible = true;

                    var results = (ExportResults)evt.Result;

                    var tabs = new List<TabPage>();

                    foreach (var entity in results.Entities)
                    {
                        var emd = results.Settings.AllEntities.First(ent => ent.LogicalName == entity.Records.EntityName);
                        var tabPage = new TabPage($"{emd.DisplayName?.UserLocalizedLabel?.Label ?? emd.SchemaName} ({entity.Records.Entities.Count})");
                        tabs.Add(tabPage);

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
                        tabs.Add(tabPage);

                        var rl = new RecordsListerControl(entity.Records.Entities.ToList(), results.Entities.SelectMany(rs => rs.Records.Entities).ToList(), rel, results.Settings.AllEntities)
                        {
                            Dock = DockStyle.Fill
                        };

                        tabPage.Controls.Add(rl);
                    }

                    tabCtrl.TabPages.AddRange(tabs.OrderBy(t => t.Text).ToArray());
                    cbbTabSelection.Items.AddRange(tabs.OrderBy(t => t.Text).Select(t => t.Text).Cast<object>().ToArray());

                    if (cbbTabSelection.Items.Count > 0)
                    {
                        cbbTabSelection.SelectedIndex = 0;
                    }

                    tabCtrl.SelectedIndexChanged += tabCtrl_SelectedIndexChanged;
                    cbbTabSelection.SelectedIndexChanged += cbbTabSelection_SelectedIndexChanged;
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
            });
        }

        private void tsbTransferData_Click(object sender, EventArgs e)
        {
            if (targetService == null)
            {
                AddAdditionalOrganization();
            }
            else
            {
                pnlImportFile.Visible = false;
                pnlImport.BringToFront();
                tsMain.Enabled = false;
                pnlImport.Visible = true;
                btnImport.Visible = false;

                ExportData(false);
            }
        }

        private void tsmiTansferToNewOrg_Click(object sender, EventArgs e)
        {
            AddAdditionalOrganization();
        }

        private void txtImportFilePath_TextChanged(object sender, EventArgs e)
        {
            btnImport.Enabled = txtImportFilePath.Text.Length > 0;
        }

        #endregion Events

        #region Methods

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
                        MessageBox.Show(this, $@"An error occured: {evt.Error.Message}", @"Error", MessageBoxButtons.OK,
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

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            ecpEntities.Service = newService;

            nManager = new NoteManager(newService);
            rManager = new RecordManager(newService);

            base.UpdateConnection(newService, detail, actionName, parameter);
        }

        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var detail = AdditionalConnectionDetails.Last();
                targetService = detail.GetCrmServiceClient();

                tssbTransferData.Text = $@"Transfer records to {detail.OrganizationFriendlyName}";
                tsmiTansferToNewOrg.Visible = true;

                pnlImportFile.Visible = false;
                pnlImport.BringToFront();
                tsMain.Enabled = false;
                pnlImport.Visible = true;
                btnImport.Visible = false;

                ExportData(false);
            }
        }

        private void AddTile(string text, string description)
        {
            Invoke(new Action(() =>
            {
                var ctrl = new ProgressTile(text) { Dock = DockStyle.Left };
                pnlProgressTiles.Controls.Add(ctrl);
                pnlProgressTiles.Controls.SetChildIndex(ctrl, 0);

                lblProgress.Text = description;
            }));
        }

        private void CancelTile()
        {
            Invoke(new Action(() =>
            {
                pnlProgressTiles.Controls.OfType<ProgressTile>().FirstOrDefault()?.Cancel();
            }));
        }

        private void CompleteTile()
        {
            Invoke(new Action(() =>
            {
                pnlProgressTiles.Controls.OfType<ProgressTile>().FirstOrDefault()?.Complete();
            }));
        }

        private void ComputeSettings()
        {
            settings.CreateFilter = docCreateFilter.IsEnabled ? docCreateFilter.SelectedDate : (DateTime?)null;
            settings.ModifyFilter = docModifyFilter.IsEnabled ? docModifyFilter.SelectedDate : (DateTime?)null;
            settings.WebsiteFilter = wpcWebsiteFilter.IsEnabled ? wpcWebsiteFilter.SelectedWebSiteId : Guid.Empty;
            settings.SelectedEntities = ecpEntities.SelectedMetadatas.Select(emd => emd.LogicalName).ToList();
            settings.AllEntities = ecpEntities.Metadata;
            settings.ActiveItemsOnly = chkActiveOnly.Checked;
            settings.ExportInFolderStructure = cbExportAsFolderStructure.Checked;
            settings.ZipFolderStructure = cbZipFolderStructure.Checked;
        }

        private void ExportData(bool isFileExport)
        {
            ComputeSettings();

            var ec = new EntityCollection();

            // Gathering records to export
            foreach (TabPage tp in tabCtrl.TabPages)
            {
                var rlc = (RecordsListerControl)tp.Controls[0];
                ec.Entities.AddRange(rlc.Records);
            }

            if (ec.Entities.Count == 0)
            {
                MessageBox.Show(this, @"No record selected!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var record in ec.Entities)
            {
                var emd = settings.AllEntities.First(ent => ent.LogicalName == record.LogicalName);
                if (!(emd.IsIntersect ?? false))
                {
                    var validAttributes = emd.Attributes
                        .Where(a => (a.IsValidForCreate ?? false) || (a.IsValidForUpdate ?? false))
                        .Select(a => a.LogicalName)
                        .ToArray();

                    for (int i = record.Attributes.Count - 1; i >= 0; i--)
                    {
                        if (!validAttributes.Contains(record.Attributes.ElementAt(i).Key))
                        {
                            record.Attributes.Remove(record.Attributes.ElementAt(i));
                        }
                        else
                        {
                            if (record[record.Attributes.ElementAt(i).Key] is EntityReference er
                                && er.LogicalName == "contact")
                            {
                                record.Attributes.Remove(record.Attributes.ElementAt(i));
                            }
                        }
                    }

                    foreach (var va in validAttributes)
                    {
                        //add any null attributes to force them to update to null.
                        if (!record.Contains(va))
                            record[va] = null;
                    }
                }
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "",
                AsyncArgument = ec,
                Work = (bw, evt) =>
                {
                    var list = (EntityCollection)evt.Argument;

                    var webFiles = list.Entities.Where(ent => ent.LogicalName == "adx_webfile").ToList();
                    if (webFiles.Any())
                    {
                        if (isFileExport)
                        {
                            bw.ReportProgress(0, "Retrieving web files annotation records...");
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                pnlImportMain.Visible = true;
                                pnlImport.BringToFront();
                            }));
                            AddTile("Retrieve notes", "We need to retrieve notes associated to the web files you want to transfer");
                        }

                        var records = rManager.RetrieveWebfileAnnotations(webFiles.Select(w => w.Id).ToList());
                        foreach (var record in records)
                        {
                            ec.Entities.Insert(0, record);
                        }

                        CompleteTile();
                    }

                    evt.Result = list;
                },
                PostWorkCallBack = evt =>
                {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(string.Empty));

                    if (evt.Error != null)
                    {
                        CancelTile();
                        MessageBox.Show(this, $@"An error occured: {evt.Error.Message}", @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    var list = (EntityCollection)evt.Result;

                    if (isFileExport)
                    {
                        ExportToDisk(list);
                    }
                    else
                    {
                        lblImportHeader.Text = @"Portal Records Transfer";
                        TransferData(list, targetService);
                    }
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
            });
        }

        private void ExportToDisk(EntityCollection list)
        {
            if (settings.ExportInFolderStructure)
            {
                var fbd = new FolderBrowserDialog
                {
                    Description = @"Folder where to save exported records"
                };
                if (fbd.ShowDialog(this) != DialogResult.OK) return;

                var timestampExport = $"Export_{DateTime.Now:yyyyMMdd_hhmmss}";
                var rootPath = Path.Combine(fbd.SelectedPath, timestampExport);
                var entities = list.Entities.GroupBy(ent => ent.LogicalName);
                foreach (var entity in entities)
                {
                    var directory = Path.Combine(rootPath, entity.Key);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    foreach (var record in entity)
                    {
                        var filePath = Path.Combine(directory, $"{record.Id:B}.xml");
                        var xwSettings = new XmlWriterSettings { Indent = true };
                        var serializer = new DataContractSerializer(typeof(Entity));

                        using (var w = XmlWriter.Create(filePath, xwSettings))
                        {
                            serializer.WriteObject(w, record);
                        }
                    }
                }

                if (settings.ZipFolderStructure)
                {
                    var filename = Path.Combine(fbd.SelectedPath, $"{timestampExport}.zip");
                    ZipFile.CreateFromDirectory(rootPath, filename);
                    Directory.Delete(rootPath, true);

                    MessageBox.Show(this, $@"Records exported to {filename}!", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, $@"Records exported to {rootPath}!", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                return;
            }

            var sfd = new SaveFileDialog
            {
                Filter = @"XML document (*.xml)|*.xml",
                AddExtension = true
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                var xwSettings = new XmlWriterSettings { Indent = true };
                var serializer = new DataContractSerializer(typeof(EntityCollection), new List<Type> { typeof(Entity) });

                using (var w = XmlWriter.Create(sfd.FileName, xwSettings))
                {
                    serializer.WriteObject(w, list);
                }

                MessageBox.Show(this, $@"Records exported to {sfd.FileName}!", @"Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void Import()
        {
            if (txtImportFilePath.Text.Length == 0)
            {
                return;
            }

            if (!File.Exists(txtImportFilePath.Text) && !Directory.Exists(txtImportFilePath.Text))
            {
                MessageBox.Show(this, $@"The path ""{txtImportFilePath.Text}"" does not exist!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EntityCollection ec = FileManager.GetRecordsFromDisk(txtImportFilePath.Text);

            TransferData(ec, Service);
        }

        private void TransferData(EntityCollection ec, IOrganizationService service)
        {
            // References to websites
            var webSitesRefId = ec.Entities.SelectMany(e => e.Attributes)
                .Where(a => a.Value is EntityReference reference && reference.LogicalName == "adx_website")
                .Select(a => ((EntityReference)a.Value).Id)
                .Distinct()
                .ToList();

            // Websites included in records to process
            var webSitesIds = ec.Entities.Where(e => e.LogicalName == "adx_website")
                .Select(e => e.Id)
                .ToList();

            // If some references are not found in websites included in records
            // to process, ask the user to map to the appropriate website
            if (!webSitesRefId.All(id => webSitesIds.Contains(id)))
            {
                var targetWebSites = service.RetrieveMultiple(new QueryExpression("adx_website")
                {
                    ColumnSet = new ColumnSet("adx_name")
                }).Entities;

                if (!webSitesRefId.All(id => targetWebSites.Select(w => w.Id).Contains(id)))
                {
                    var wsmDialog = new WebSiteMapper(ec, targetWebSites.Select(t => new Website(t)).ToList());
                    if (wsmDialog.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            var pluginCheck = ec.Entities.Any(e => e.LogicalName == "adx_webpage");
            var javascriptCheck =
                ec.Entities.Any(e =>
                    e.LogicalName == "annotation" &&
                    (e.GetAttributeValue<string>("filename")?.ToLower().EndsWith(".js") ?? false)) &&
                nManager.HasJsRestriction;
            var webFileCleaning = ec.Entities.Any(e =>
                e.LogicalName == "annotation" &&
                e.GetAttributeValue<EntityReference>("objectid")?.LogicalName == "adx_webfile");

            if (pluginCheck || javascriptCheck)
            {
                var dialog = new PreImportWarningDialog(pluginCheck, javascriptCheck, webFileCleaning);
                var result = dialog.ShowDialog(this);
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                iSettings.DeactivateWebPagePlugins = true;
                iSettings.RemoveJavaScriptFileRestriction = true;
                iSettings.CleanWebFiles = dialog.CleanWebFiles;
            }

            var lm = new LogManager(GetType());
            if (File.Exists(lm.FilePath))
            {
                if (MessageBox.Show(this, @"A log file already exists. Would you like to create a new log file?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Copy(lm.FilePath, $"{lm.FilePath.Substring(0, lm.FilePath.Length - 4)}-{DateTime.Now:yyyyMMdd_HHmmss}.txt", true);
                    File.Delete(lm.FilePath);
                }
            }

            btnCancel.Visible = true;
            btnImport.Enabled = false;
            pnlImportMain.Visible = true;
            pbImport.IsOnError = false;
            lvProgress.Items.Clear();

            var worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += (s, evt) =>
            {
                importWorker = (BackgroundWorker)s;

                if (emds.Count == 0)
                {
                    AddTile("Retrieve metadata", "We need entities metadata to process your records");

                    emds = MetadataManager.GetEntitiesList(service);

                    CompleteTile();
                }

                if (iSettings.DeactivateWebPagePlugins)
                {
                    AddTile("Disable plugins", "We need to disable web page plugins to ensure we don't create duplicates");

                    logger.LogInfo("Deactivating Webpage plugins steps");

                    pManager = new PluginManager(service);
                    pManager.DeactivateWebpagePlugins();

                    CompleteTile();

                    logger.LogInfo("Webpage plugins steps deactivated");
                }

                if (iSettings.RemoveJavaScriptFileRestriction && nManager.HasJsRestriction)
                {
                    AddTile("Remove file restriction", "We need to authorize JavaScript file type to create Web file correctly");

                    logger.LogInfo("Removing JavaScript file restriction");

                    nManager.RemoveRestriction();

                    // Wait 2 seconds to be sure the settings is updated
                    Thread.Sleep(2000);

                    CompleteTile();

                    logger.LogInfo("JavaScript file restriction removed");
                }

                AddTile("Process records", "Records are processed in three phases : one phase without lookup being populated. A second phase with lookup being populated. This ensure all relationships between records can be created on the second phase. And a third phase to deactivate records that need it.");

                var rm = new RecordManager(service);
                evt.Cancel = rm.ProcessRecords((EntityCollection)evt.Argument, emds, ConnectionDetail.OrganizationMajorVersion, importWorker, iSettings);

                if (evt.Cancel)
                {
                    CancelTile();
                }
                else
                {
                    CompleteTile();
                }

                if (iSettings.DeactivateWebPagePlugins)
                {
                    AddTile("Enable plugins", "We are enabling web page plugins so that your portal work smoothly");
                    logger.LogInfo("Reactivating Webpage plugins steps");

                    pManager.ActivateWebpagePlugins();

                    logger.LogInfo("Webpage plugins steps activated");
                    CompleteTile();
                }

                if (iSettings.RemoveJavaScriptFileRestriction && nManager.HasJsRestriction)
                {
                    AddTile("Add file restriction", "We are adding back restriction for JavaScript files.");
                    logger.LogInfo("Adding back JavaScript file restriction");

                    importWorker.ReportProgress(0, "Adding back JavaScript file restriction...");
                    nManager.AddRestriction();

                    logger.LogInfo("JavaScript file restriction added back");
                    CompleteTile();
                }
            };
            worker.RunWorkerCompleted += (s, evt) =>
            {
                llOpenLogFile.Visible = true;
                btnImportClose.Enabled = true;
                btnImport.Enabled = true;
                btnCancel.Visible = false;

                if (evt.Cancelled)
                {
                    CancelTile();
                    lblProgress.Text = @"Import was canceled!";
                    return;
                }

                if (evt.Error != null)
                {
                    CancelTile();

                    logger.LogError(evt.Error.ToString());

                    MessageBox.Show(this, $@"An error occured: {evt.Error.Message}", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(string.Empty));
                lblProgress.Text = @"Records imported!";

                if (pbImport.IsOnError)
                {
                    MessageBox.Show(this, @"Import complete with errors

Please review the logs", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(this, @"Import complete", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            worker.ProgressChanged += (s, evt) =>
            {
                if (evt.UserState is string)
                {
                    lblProgress.Text = evt.UserState.ToString();
                    logger.LogInfo(evt.UserState.ToString());

                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(evt.UserState.ToString()));
                }
                else
                {
                    if (evt.UserState is ImportProgress progress)
                    {
                        foreach (var ep in progress.Entities)
                        {
                            var item = lvProgress.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag.ToString() == ep.LogicalName);
                            if (item == null)
                            {
                                item = new ListViewItem($"{ep.Entity} ({ep.Count})") { Tag = ep.LogicalName };
                                item.SubItems.Add(ep.SuccessFirstPhase.ToString());
                                item.SubItems.Add(ep.ErrorFirstPhase.ToString());
                                item.SubItems.AddRange(new[] { "", "", "", "" });

                                if (ep.SuccessSecondPhase.HasValue || ep.ErrorSecondPhase.HasValue)
                                {
                                    item.SubItems[3].Text = (ep.SuccessSecondPhase ?? 0).ToString();
                                    item.SubItems[4].Text = (ep.ErrorSecondPhase ?? 0).ToString();
                                }

                                if (ep.SuccessSetStatePhase.HasValue || ep.ErrorSetState.HasValue)
                                {
                                    item.SubItems[5].Text = (ep.SuccessSetStatePhase ?? 0).ToString();
                                    item.SubItems[6].Text = (ep.ErrorSetState ?? 0).ToString();
                                }

                                lvProgress.Items.Add(item);
                            }
                            else
                            {
                                item.SubItems[1].Text = ep.SuccessFirstPhase.ToString();
                                item.SubItems[2].Text = ep.ErrorFirstPhase.ToString();

                                if (ep.SuccessSecondPhase.HasValue || ep.ErrorSecondPhase.HasValue)
                                {
                                    item.SubItems[3].Text = (ep.SuccessSecondPhase ?? 0).ToString();
                                    item.SubItems[4].Text = (ep.ErrorSecondPhase ?? 0).ToString();
                                }

                                if (ep.SuccessSetStatePhase.HasValue || ep.ErrorSetState.HasValue)
                                {
                                    item.SubItems[5].Text = (ep.SuccessSetStatePhase ?? 0).ToString();
                                    item.SubItems[6].Text = (ep.ErrorSetState ?? 0).ToString();
                                }
                            }

                            item.ForeColor = ep.ErrorFirstPhase > 0 || ep.ErrorSecondPhase > 0 || ep.ErrorSetState > 0
                                ? Color.Red
                                : Color.Green;

                            if (ep.ErrorFirstPhase > 0 || ep.ErrorSecondPhase > 0 || ep.ErrorSetState > 0)
                            {
                                pbImport.IsOnError = true;
                            }

                            pbImport.Value = progress.Entities.Sum(ent => ent.Processed) * 100 / progress.Count;
                        }
                    }
                }
            };
            worker.RunWorkerAsync(ec);
        }

        #endregion Methods
    }
}