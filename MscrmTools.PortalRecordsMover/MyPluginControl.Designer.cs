using MscrmTools.PortalRecordsMover.Controls;

namespace MscrmTools.PortalRecordsMover
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRetrieveRecords = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbImportRecords = new System.Windows.Forms.ToolStripButton();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbTransferData = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiTansferToNewOrg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scRight = new System.Windows.Forms.SplitContainer();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbRemoveFormattedValues = new System.Windows.Forms.CheckBox();
            this.cbZipFolderStructure = new System.Windows.Forms.CheckBox();
            this.cbExportAsFolderStructure = new System.Windows.Forms.CheckBox();
            this.chkActiveOnly = new System.Windows.Forms.CheckBox();
            this.wpcWebsiteFilter = new MscrmTools.PortalRecordsMover.Controls.WebsitePickerControl();
            this.docModifyFilter = new MscrmTools.PortalRecordsMover.Controls.DateOptionControl();
            this.docCreateFilter = new MscrmTools.PortalRecordsMover.Controls.DateOptionControl();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.pnlTabSelection = new System.Windows.Forms.Panel();
            this.cbbTabSelection = new System.Windows.Forms.ComboBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.ecpEntities = new MscrmTools.PortalRecordsMover.Controls.EntityPickerControl();
            this.pnlImport = new System.Windows.Forms.Panel();
            this.pnlImportMain = new System.Windows.Forms.Panel();
            this.lvProgress = new System.Windows.Forms.ListView();
            this.chEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSuccessFirst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErrorFirst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSuccessSecond = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErrorSecond = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSuccessSetState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErrorSetState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlProgressTiles = new System.Windows.Forms.Panel();
            this.pbImport = new MscrmTools.PortalRecordsMover.Controls.CustomProgressBar();
            this.pnlImportFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.llOpenLogFile = new System.Windows.Forms.LinkLabel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnImportClose = new System.Windows.Forms.Button();
            this.pnlImportFile = new System.Windows.Forms.Panel();
            this.txtImportFilePath = new System.Windows.Forms.TextBox();
            this.rdbSelectFile = new System.Windows.Forms.RadioButton();
            this.rdbSelectFolder = new System.Windows.Forms.RadioButton();
            this.btnBrowseImportFile = new System.Windows.Forms.Button();
            this.lblImportFileLabel = new System.Windows.Forms.Label();
            this.pnlImportHeader = new System.Windows.Forms.Panel();
            this.lblImportHeader = new System.Windows.Forms.Label();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scRight)).BeginInit();
            this.scRight.Panel1.SuspendLayout();
            this.scRight.Panel2.SuspendLayout();
            this.scRight.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.pnlTabSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pnlImport.SuspendLayout();
            this.pnlImportMain.SuspendLayout();
            this.pnlImportFooter.SuspendLayout();
            this.pnlImportFile.SuspendLayout();
            this.pnlImportHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbLoad
            // 
            this.tsbLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoad.Image")));
            this.tsbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoad.Name = "tsbLoad";
            this.tsbLoad.Size = new System.Drawing.Size(167, 36);
            this.tsbLoad.Text = "Load items";
            this.tsbLoad.ToolTipText = "Load entities and website from the connected organization.\r\n\r\nThis step is mandat" +
    "ory before executing other actions";
            this.tsbLoad.Click += new System.EventHandler(this.tsbLoad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbRetrieveRecords
            // 
            this.tsbRetrieveRecords.Enabled = false;
            this.tsbRetrieveRecords.Image = ((System.Drawing.Image)(resources.GetObject("tsbRetrieveRecords.Image")));
            this.tsbRetrieveRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRetrieveRecords.Name = "tsbRetrieveRecords";
            this.tsbRetrieveRecords.Size = new System.Drawing.Size(222, 36);
            this.tsbRetrieveRecords.Text = "Retrieve records";
            this.tsbRetrieveRecords.ToolTipText = "Retrieve records based on options defined";
            this.tsbRetrieveRecords.Click += new System.EventHandler(this.tsbRetrieveRecords_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbExport
            // 
            this.tsbExport.Enabled = false;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(203, 36);
            this.tsbExport.Text = "Export records";
            this.tsbExport.ToolTipText = "Export records in a file";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbImportRecords
            // 
            this.tsbImportRecords.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportRecords.Image")));
            this.tsbImportRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportRecords.Name = "tsbImportRecords";
            this.tsbImportRecords.Size = new System.Drawing.Size(207, 36);
            this.tsbImportRecords.Text = "Import records";
            this.tsbImportRecords.ToolTipText = "Import records from a file";
            this.tsbImportRecords.Click += new System.EventHandler(this.tsbImportRecords_Click);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator3,
            this.tsbLoad,
            this.toolStripSeparator1,
            this.tsbRetrieveRecords,
            this.toolStripSeparator2,
            this.tsbExport,
            this.tsbImportRecords,
            this.tssbTransferData,
            this.toolStripSeparator4,
            this.tsddSettings});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.tsMain.Size = new System.Drawing.Size(1866, 39);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "tsMain";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(36, 36);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tssbTransferData
            // 
            this.tssbTransferData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTansferToNewOrg});
            this.tssbTransferData.Enabled = false;
            this.tssbTransferData.Image = global::MscrmTools.PortalRecordsMover.Properties.Resources.database;
            this.tssbTransferData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbTransferData.Name = "tssbTransferData";
            this.tssbTransferData.Size = new System.Drawing.Size(243, 36);
            this.tssbTransferData.Text = "Transfer records";
            this.tssbTransferData.ToolTipText = "Transfer records between the connected organization and another organization";
            this.tssbTransferData.ButtonClick += new System.EventHandler(this.tsbTransferData_Click);
            // 
            // tsmiTansferToNewOrg
            // 
            this.tsmiTansferToNewOrg.Name = "tsmiTansferToNewOrg";
            this.tsmiTansferToNewOrg.Size = new System.Drawing.Size(445, 38);
            this.tsmiTansferToNewOrg.Text = "Transfert to a new organization";
            this.tsmiTansferToNewOrg.Visible = false;
            this.tsmiTansferToNewOrg.Click += new System.EventHandler(this.tsmiTansferToNewOrg_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tsddSettings
            // 
            this.tsddSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.tsddSettings.Enabled = false;
            this.tsddSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsddSettings.Image")));
            this.tsddSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddSettings.Name = "tsddSettings";
            this.tsddSettings.Size = new System.Drawing.Size(155, 36);
            this.tsddSettings.Text = "Settings";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(258, 38);
            this.loadSettingsToolStripMenuItem.Text = "Load Settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(258, 38);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // scRight
            // 
            this.scRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRight.Location = new System.Drawing.Point(0, 0);
            this.scRight.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.scRight.Name = "scRight";
            this.scRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRight.Panel1
            // 
            this.scRight.Panel1.Controls.Add(this.gbOptions);
            // 
            // scRight.Panel2
            // 
            this.scRight.Panel2.Controls.Add(this.tabCtrl);
            this.scRight.Panel2.Controls.Add(this.pnlTabSelection);
            this.scRight.Size = new System.Drawing.Size(1554, 1211);
            this.scRight.SplitterDistance = 421;
            this.scRight.SplitterWidth = 12;
            this.scRight.TabIndex = 0;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbRemoveFormattedValues);
            this.gbOptions.Controls.Add(this.cbZipFolderStructure);
            this.gbOptions.Controls.Add(this.cbExportAsFolderStructure);
            this.gbOptions.Controls.Add(this.chkActiveOnly);
            this.gbOptions.Controls.Add(this.wpcWebsiteFilter);
            this.gbOptions.Controls.Add(this.docModifyFilter);
            this.gbOptions.Controls.Add(this.docCreateFilter);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOptions.Location = new System.Drawing.Point(0, 0);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.gbOptions.Size = new System.Drawing.Size(1554, 421);
            this.gbOptions.TabIndex = 0;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbRemoveFormattedValues
            // 
            this.cbRemoveFormattedValues.AutoSize = true;
            this.cbRemoveFormattedValues.Location = new System.Drawing.Point(614, 301);
            this.cbRemoveFormattedValues.Margin = new System.Windows.Forms.Padding(4);
            this.cbRemoveFormattedValues.Name = "cbRemoveFormattedValues";
            this.cbRemoveFormattedValues.Size = new System.Drawing.Size(471, 29);
            this.cbRemoveFormattedValues.TabIndex = 6;
            this.cbRemoveFormattedValues.Text = "Remove formatted values from exported files";
            this.cbRemoveFormattedValues.UseVisualStyleBackColor = true;
            // 
            // cbZipFolderStructure
            // 
            this.cbZipFolderStructure.AutoSize = true;
            this.cbZipFolderStructure.Enabled = false;
            this.cbZipFolderStructure.Location = new System.Drawing.Point(348, 301);
            this.cbZipFolderStructure.Margin = new System.Windows.Forms.Padding(4);
            this.cbZipFolderStructure.Name = "cbZipFolderStructure";
            this.cbZipFolderStructure.Size = new System.Drawing.Size(224, 29);
            this.cbZipFolderStructure.TabIndex = 5;
            this.cbZipFolderStructure.Text = "Zip folder structure";
            this.cbZipFolderStructure.UseVisualStyleBackColor = true;
            // 
            // cbExportAsFolderStructure
            // 
            this.cbExportAsFolderStructure.AutoSize = true;
            this.cbExportAsFolderStructure.Location = new System.Drawing.Point(8, 301);
            this.cbExportAsFolderStructure.Margin = new System.Windows.Forms.Padding(4);
            this.cbExportAsFolderStructure.Name = "cbExportAsFolderStructure";
            this.cbExportAsFolderStructure.Size = new System.Drawing.Size(285, 29);
            this.cbExportAsFolderStructure.TabIndex = 4;
            this.cbExportAsFolderStructure.Text = "Export as folder structure";
            this.cbExportAsFolderStructure.UseVisualStyleBackColor = true;
            this.cbExportAsFolderStructure.CheckedChanged += new System.EventHandler(this.cbExportAsFolderStructure_CheckedChanged);
            // 
            // chkActiveOnly
            // 
            this.chkActiveOnly.AutoSize = true;
            this.chkActiveOnly.Location = new System.Drawing.Point(8, 242);
            this.chkActiveOnly.Margin = new System.Windows.Forms.Padding(4);
            this.chkActiveOnly.Name = "chkActiveOnly";
            this.chkActiveOnly.Size = new System.Drawing.Size(265, 29);
            this.chkActiveOnly.TabIndex = 3;
            this.chkActiveOnly.Text = "Get only active records";
            this.chkActiveOnly.UseVisualStyleBackColor = true;
            // 
            // wpcWebsiteFilter
            // 
            this.wpcWebsiteFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.wpcWebsiteFilter.IsEnabled = false;
            this.wpcWebsiteFilter.Location = new System.Drawing.Point(8, 164);
            this.wpcWebsiteFilter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.wpcWebsiteFilter.Name = "wpcWebsiteFilter";
            this.wpcWebsiteFilter.SelectedWebSiteId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.wpcWebsiteFilter.Size = new System.Drawing.Size(1538, 65);
            this.wpcWebsiteFilter.TabIndex = 2;
            // 
            // docModifyFilter
            // 
            this.docModifyFilter.Attribute = null;
            this.docModifyFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.docModifyFilter.IsEnabled = false;
            this.docModifyFilter.Label = "Get records modified on or after";
            this.docModifyFilter.Location = new System.Drawing.Point(8, 99);
            this.docModifyFilter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.docModifyFilter.Name = "docModifyFilter";
            this.docModifyFilter.SelectedDate = new System.DateTime(2017, 6, 13, 13, 59, 42, 271);
            this.docModifyFilter.Size = new System.Drawing.Size(1538, 65);
            this.docModifyFilter.TabIndex = 1;
            // 
            // docCreateFilter
            // 
            this.docCreateFilter.Attribute = null;
            this.docCreateFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.docCreateFilter.IsEnabled = false;
            this.docCreateFilter.Label = "Get records created on or after";
            this.docCreateFilter.Location = new System.Drawing.Point(8, 34);
            this.docCreateFilter.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.docCreateFilter.Name = "docCreateFilter";
            this.docCreateFilter.SelectedDate = new System.DateTime(2017, 6, 13, 13, 59, 42, 288);
            this.docCreateFilter.Size = new System.Drawing.Size(1538, 65);
            this.docCreateFilter.TabIndex = 0;
            // 
            // tabCtrl
            // 
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(0, 52);
            this.tabCtrl.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(1554, 726);
            this.tabCtrl.TabIndex = 5;
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.tabCtrl_SelectedIndexChanged);
            // 
            // pnlTabSelection
            // 
            this.pnlTabSelection.Controls.Add(this.cbbTabSelection);
            this.pnlTabSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabSelection.Location = new System.Drawing.Point(0, 0);
            this.pnlTabSelection.Margin = new System.Windows.Forms.Padding(6);
            this.pnlTabSelection.Name = "pnlTabSelection";
            this.pnlTabSelection.Size = new System.Drawing.Size(1554, 52);
            this.pnlTabSelection.TabIndex = 4;
            this.pnlTabSelection.Visible = false;
            // 
            // cbbTabSelection
            // 
            this.cbbTabSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTabSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTabSelection.FormattingEnabled = true;
            this.cbbTabSelection.Location = new System.Drawing.Point(1002, 6);
            this.cbbTabSelection.Margin = new System.Windows.Forms.Padding(6);
            this.cbbTabSelection.Name = "cbbTabSelection";
            this.cbbTabSelection.Size = new System.Drawing.Size(542, 33);
            this.cbbTabSelection.TabIndex = 0;
            this.cbbTabSelection.SelectedIndexChanged += new System.EventHandler(this.cbbTabSelection_SelectedIndexChanged);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 39);
            this.scMain.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.ecpEntities);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scRight);
            this.scMain.Size = new System.Drawing.Size(1866, 1211);
            this.scMain.SplitterDistance = 300;
            this.scMain.SplitterWidth = 12;
            this.scMain.TabIndex = 4;
            // 
            // ecpEntities
            // 
            this.ecpEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecpEntities.Location = new System.Drawing.Point(0, 0);
            this.ecpEntities.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.ecpEntities.Name = "ecpEntities";
            this.ecpEntities.Service = null;
            this.ecpEntities.Size = new System.Drawing.Size(300, 1211);
            this.ecpEntities.TabIndex = 0;
            // 
            // pnlImport
            // 
            this.pnlImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImport.BackColor = System.Drawing.Color.White;
            this.pnlImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImport.Controls.Add(this.pnlImportMain);
            this.pnlImport.Controls.Add(this.pnlImportFooter);
            this.pnlImport.Controls.Add(this.pnlImportFile);
            this.pnlImport.Controls.Add(this.pnlImportHeader);
            this.pnlImport.Location = new System.Drawing.Point(300, 80);
            this.pnlImport.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.pnlImport.Name = "pnlImport";
            this.pnlImport.Size = new System.Drawing.Size(1332, 1000);
            this.pnlImport.TabIndex = 5;
            this.pnlImport.Visible = false;
            // 
            // pnlImportMain
            // 
            this.pnlImportMain.Controls.Add(this.lvProgress);
            this.pnlImportMain.Controls.Add(this.lblProgress);
            this.pnlImportMain.Controls.Add(this.pnlProgressTiles);
            this.pnlImportMain.Controls.Add(this.pbImport);
            this.pnlImportMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImportMain.Location = new System.Drawing.Point(0, 169);
            this.pnlImportMain.Margin = new System.Windows.Forms.Padding(6);
            this.pnlImportMain.Name = "pnlImportMain";
            this.pnlImportMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlImportMain.Size = new System.Drawing.Size(1330, 739);
            this.pnlImportMain.TabIndex = 9;
            this.pnlImportMain.Visible = false;
            // 
            // lvProgress
            // 
            this.lvProgress.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEntity,
            this.chSuccessFirst,
            this.chErrorFirst,
            this.chSuccessSecond,
            this.chErrorSecond,
            this.chSuccessSetState,
            this.chErrorSetState});
            this.lvProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProgress.HideSelection = false;
            this.lvProgress.Location = new System.Drawing.Point(10, 178);
            this.lvProgress.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.lvProgress.Name = "lvProgress";
            this.lvProgress.Size = new System.Drawing.Size(1310, 551);
            this.lvProgress.TabIndex = 12;
            this.lvProgress.UseCompatibleStateImageBehavior = false;
            this.lvProgress.View = System.Windows.Forms.View.Details;
            // 
            // chEntity
            // 
            this.chEntity.Text = "Entity";
            this.chEntity.Width = 200;
            // 
            // chSuccessFirst
            // 
            this.chSuccessFirst.Text = "Success (1st phase)";
            this.chSuccessFirst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chSuccessFirst.Width = 120;
            // 
            // chErrorFirst
            // 
            this.chErrorFirst.Text = "Error (1st phase)";
            this.chErrorFirst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chErrorFirst.Width = 120;
            // 
            // chSuccessSecond
            // 
            this.chSuccessSecond.Text = "Success (2nd phase)";
            this.chSuccessSecond.Width = 120;
            // 
            // chErrorSecond
            // 
            this.chErrorSecond.Text = "Error (2nd phase)";
            this.chErrorSecond.Width = 120;
            // 
            // chSuccessSetState
            // 
            this.chSuccessSetState.Text = "Success (Deactivation)";
            this.chSuccessSetState.Width = 120;
            // 
            // chErrorSetState
            // 
            this.chErrorSetState.Text = "Error (Deactivation)";
            this.chErrorSetState.Width = 120;
            // 
            // lblProgress
            // 
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProgress.Location = new System.Drawing.Point(10, 126);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(1310, 52);
            this.lblProgress.TabIndex = 11;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlProgressTiles
            // 
            this.pnlProgressTiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProgressTiles.Location = new System.Drawing.Point(10, 45);
            this.pnlProgressTiles.Name = "pnlProgressTiles";
            this.pnlProgressTiles.Padding = new System.Windows.Forms.Padding(10);
            this.pnlProgressTiles.Size = new System.Drawing.Size(1310, 81);
            this.pnlProgressTiles.TabIndex = 10;
            // 
            // pbImport
            // 
            this.pbImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbImport.IsOnError = false;
            this.pbImport.Location = new System.Drawing.Point(10, 10);
            this.pbImport.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.pbImport.Name = "pbImport";
            this.pbImport.Size = new System.Drawing.Size(1310, 35);
            this.pbImport.Step = 1;
            this.pbImport.TabIndex = 7;
            // 
            // pnlImportFooter
            // 
            this.pnlImportFooter.Controls.Add(this.btnCancel);
            this.pnlImportFooter.Controls.Add(this.llOpenLogFile);
            this.pnlImportFooter.Controls.Add(this.btnImport);
            this.pnlImportFooter.Controls.Add(this.btnImportClose);
            this.pnlImportFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlImportFooter.Location = new System.Drawing.Point(0, 908);
            this.pnlImportFooter.Margin = new System.Windows.Forms.Padding(6);
            this.pnlImportFooter.Name = "pnlImportFooter";
            this.pnlImportFooter.Padding = new System.Windows.Forms.Padding(20);
            this.pnlImportFooter.Size = new System.Drawing.Size(1330, 90);
            this.pnlImportFooter.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(860, 20);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // llOpenLogFile
            // 
            this.llOpenLogFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.llOpenLogFile.Location = new System.Drawing.Point(20, 20);
            this.llOpenLogFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llOpenLogFile.Name = "llOpenLogFile";
            this.llOpenLogFile.Size = new System.Drawing.Size(216, 50);
            this.llOpenLogFile.TabIndex = 2;
            this.llOpenLogFile.TabStop = true;
            this.llOpenLogFile.Text = "Open log file";
            this.llOpenLogFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llOpenLogFile.Visible = false;
            this.llOpenLogFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenLogFile_LinkClicked);
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImport.Location = new System.Drawing.Point(1010, 20);
            this.btnImport.Margin = new System.Windows.Forms.Padding(6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(150, 50);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnImportClose
            // 
            this.btnImportClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImportClose.Location = new System.Drawing.Point(1160, 20);
            this.btnImportClose.Margin = new System.Windows.Forms.Padding(6);
            this.btnImportClose.Name = "btnImportClose";
            this.btnImportClose.Size = new System.Drawing.Size(150, 50);
            this.btnImportClose.TabIndex = 0;
            this.btnImportClose.Text = "Close";
            this.btnImportClose.UseVisualStyleBackColor = true;
            this.btnImportClose.Click += new System.EventHandler(this.btnImportClose_Click);
            // 
            // pnlImportFile
            // 
            this.pnlImportFile.Controls.Add(this.txtImportFilePath);
            this.pnlImportFile.Controls.Add(this.rdbSelectFile);
            this.pnlImportFile.Controls.Add(this.rdbSelectFolder);
            this.pnlImportFile.Controls.Add(this.btnBrowseImportFile);
            this.pnlImportFile.Controls.Add(this.lblImportFileLabel);
            this.pnlImportFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImportFile.Location = new System.Drawing.Point(0, 125);
            this.pnlImportFile.Margin = new System.Windows.Forms.Padding(6);
            this.pnlImportFile.Name = "pnlImportFile";
            this.pnlImportFile.Padding = new System.Windows.Forms.Padding(22, 0, 22, 0);
            this.pnlImportFile.Size = new System.Drawing.Size(1330, 44);
            this.pnlImportFile.TabIndex = 3;
            // 
            // txtImportFilePath
            // 
            this.txtImportFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtImportFilePath.Location = new System.Drawing.Point(127, 0);
            this.txtImportFilePath.Margin = new System.Windows.Forms.Padding(6);
            this.txtImportFilePath.Name = "txtImportFilePath";
            this.txtImportFilePath.Size = new System.Drawing.Size(722, 31);
            this.txtImportFilePath.TabIndex = 5;
            this.txtImportFilePath.TextChanged += new System.EventHandler(this.txtImportFilePath_TextChanged);
            // 
            // rdbSelectFile
            // 
            this.rdbSelectFile.AutoSize = true;
            this.rdbSelectFile.Checked = true;
            this.rdbSelectFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.rdbSelectFile.Location = new System.Drawing.Point(849, 0);
            this.rdbSelectFile.Name = "rdbSelectFile";
            this.rdbSelectFile.Size = new System.Drawing.Size(205, 44);
            this.rdbSelectFile.TabIndex = 3;
            this.rdbSelectFile.TabStop = true;
            this.rdbSelectFile.Text = "File (XML or ZIP)";
            this.rdbSelectFile.UseVisualStyleBackColor = true;
            // 
            // rdbSelectFolder
            // 
            this.rdbSelectFolder.AutoSize = true;
            this.rdbSelectFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.rdbSelectFolder.Location = new System.Drawing.Point(1054, 0);
            this.rdbSelectFolder.Name = "rdbSelectFolder";
            this.rdbSelectFolder.Size = new System.Drawing.Size(104, 44);
            this.rdbSelectFolder.TabIndex = 4;
            this.rdbSelectFolder.Text = "Folder";
            this.rdbSelectFolder.UseVisualStyleBackColor = true;
            // 
            // btnBrowseImportFile
            // 
            this.btnBrowseImportFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseImportFile.Location = new System.Drawing.Point(1158, 0);
            this.btnBrowseImportFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnBrowseImportFile.Name = "btnBrowseImportFile";
            this.btnBrowseImportFile.Size = new System.Drawing.Size(150, 44);
            this.btnBrowseImportFile.TabIndex = 1;
            this.btnBrowseImportFile.Text = "Browse";
            this.btnBrowseImportFile.UseVisualStyleBackColor = true;
            this.btnBrowseImportFile.Click += new System.EventHandler(this.btnBrowseImportFile_Click);
            // 
            // lblImportFileLabel
            // 
            this.lblImportFileLabel.AutoSize = true;
            this.lblImportFileLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblImportFileLabel.Location = new System.Drawing.Point(22, 0);
            this.lblImportFileLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblImportFileLabel.Name = "lblImportFileLabel";
            this.lblImportFileLabel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblImportFileLabel.Size = new System.Drawing.Size(105, 33);
            this.lblImportFileLabel.TabIndex = 0;
            this.lblImportFileLabel.Text = "Import file";
            // 
            // pnlImportHeader
            // 
            this.pnlImportHeader.Controls.Add(this.lblImportHeader);
            this.pnlImportHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImportHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlImportHeader.Margin = new System.Windows.Forms.Padding(6);
            this.pnlImportHeader.Name = "pnlImportHeader";
            this.pnlImportHeader.Size = new System.Drawing.Size(1330, 125);
            this.pnlImportHeader.TabIndex = 2;
            // 
            // lblImportHeader
            // 
            this.lblImportHeader.AutoSize = true;
            this.lblImportHeader.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportHeader.Location = new System.Drawing.Point(6, 23);
            this.lblImportHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblImportHeader.Name = "lblImportHeader";
            this.lblImportHeader.Size = new System.Drawing.Size(418, 59);
            this.lblImportHeader.TabIndex = 0;
            this.lblImportHeader.Text = "Portal Records Import";
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.pnlImport);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1866, 1250);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.scRight.Panel1.ResumeLayout(false);
            this.scRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scRight)).EndInit();
            this.scRight.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.pnlTabSelection.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.pnlImport.ResumeLayout(false);
            this.pnlImportMain.ResumeLayout(false);
            this.pnlImportFooter.ResumeLayout(false);
            this.pnlImportFile.ResumeLayout(false);
            this.pnlImportFile.PerformLayout();
            this.pnlImportHeader.ResumeLayout(false);
            this.pnlImportHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRetrieveRecords;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbImportRecords;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.SplitContainer scRight;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Panel pnlImport;
        private Controls.EntityPickerControl ecpEntities;
        private Controls.DateOptionControl docCreateFilter;
        private Controls.WebsitePickerControl wpcWebsiteFilter;
        private Controls.DateOptionControl docModifyFilter;
        private System.Windows.Forms.Panel pnlImportFile;
        private System.Windows.Forms.Button btnBrowseImportFile;
        private System.Windows.Forms.Label lblImportFileLabel;
        private System.Windows.Forms.Panel pnlImportHeader;
        private System.Windows.Forms.Label lblImportHeader;
        private CustomProgressBar pbImport;
        private System.Windows.Forms.Panel pnlImportFooter;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnImportClose;
        private System.Windows.Forms.Panel pnlImportMain;
        private System.Windows.Forms.LinkLabel llOpenLogFile;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox chkActiveOnly;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.Panel pnlTabSelection;
        private System.Windows.Forms.ComboBox cbbTabSelection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton tsddSettings;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbZipFolderStructure;
        private System.Windows.Forms.CheckBox cbExportAsFolderStructure;
        private System.Windows.Forms.ListView lvProgress;
        private System.Windows.Forms.ColumnHeader chEntity;
        private System.Windows.Forms.ColumnHeader chSuccessFirst;
        private System.Windows.Forms.ColumnHeader chErrorFirst;
        private System.Windows.Forms.ColumnHeader chSuccessSecond;
        private System.Windows.Forms.ColumnHeader chErrorSecond;
        private System.Windows.Forms.ColumnHeader chSuccessSetState;
        private System.Windows.Forms.ColumnHeader chErrorSetState;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlProgressTiles;
        private System.Windows.Forms.ToolStripSplitButton tssbTransferData;
        private System.Windows.Forms.ToolStripMenuItem tsmiTansferToNewOrg;
        private System.Windows.Forms.TextBox txtImportFilePath;
        private System.Windows.Forms.RadioButton rdbSelectFile;
        private System.Windows.Forms.RadioButton rdbSelectFolder;
        private System.Windows.Forms.CheckBox cbRemoveFormattedValues;
    }
}
