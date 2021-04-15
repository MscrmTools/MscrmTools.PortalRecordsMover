namespace MscrmTools.PortalRecordsMover.Forms
{
    partial class PreImportWarningDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlWebFile = new System.Windows.Forms.Panel();
            this.lblWebFileDesc = new System.Windows.Forms.Label();
            this.pnlWebFileConfirmation = new System.Windows.Forms.Panel();
            this.rdbWebFileCleaningNo = new System.Windows.Forms.RadioButton();
            this.rdbWebFileCleaningYes = new System.Windows.Forms.RadioButton();
            this.lblWebFileHeader = new System.Windows.Forms.Label();
            this.pnlJavaScriptRestriction = new System.Windows.Forms.Panel();
            this.lblJavaScriptRestrictionDesc = new System.Windows.Forms.Label();
            this.lblJavaScriptRestrictionHeader = new System.Windows.Forms.Label();
            this.pnlPagePlugin = new System.Windows.Forms.Panel();
            this.lblWebPagePluginDesc = new System.Windows.Forms.Label();
            this.lblPagePluginHeader = new System.Windows.Forms.Label();
            this.pnlSiteSettings = new System.Windows.Forms.Panel();
            this.lblSiteSettingsHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbCreateOnlyNewSettingsNo = new System.Windows.Forms.RadioButton();
            this.rdbCreateOnlyNewSettingsYes = new System.Windows.Forms.RadioButton();
            this.lblSiteSettingsDesc = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlWebFile.SuspendLayout();
            this.pnlWebFileConfirmation.SuspendLayout();
            this.pnlJavaScriptRestriction.SuspendLayout();
            this.pnlPagePlugin.SuspendLayout();
            this.pnlSiteSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(8, 535);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(600, 50);
            this.pnlBottom.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(483, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(361, 10);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 35);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.pnlSiteSettings);
            this.pnlMain.Controls.Add(this.pnlWebFile);
            this.pnlMain.Controls.Add(this.pnlJavaScriptRestriction);
            this.pnlMain.Controls.Add(this.pnlPagePlugin);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(8, 8);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(600, 527);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlWebFile
            // 
            this.pnlWebFile.Controls.Add(this.lblWebFileDesc);
            this.pnlWebFile.Controls.Add(this.pnlWebFileConfirmation);
            this.pnlWebFile.Controls.Add(this.lblWebFileHeader);
            this.pnlWebFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWebFile.Location = new System.Drawing.Point(0, 236);
            this.pnlWebFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlWebFile.Name = "pnlWebFile";
            this.pnlWebFile.Size = new System.Drawing.Size(600, 118);
            this.pnlWebFile.TabIndex = 3;
            // 
            // lblWebFileDesc
            // 
            this.lblWebFileDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWebFileDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebFileDesc.Location = new System.Drawing.Point(0, 40);
            this.lblWebFileDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWebFileDesc.Name = "lblWebFileDesc";
            this.lblWebFileDesc.Size = new System.Drawing.Size(506, 78);
            this.lblWebFileDesc.TabIndex = 4;
            this.lblWebFileDesc.Text = "You are trying to import web files. Would you like to clean target organization a" +
    "nnotation for web file so that only the annotation you import is kept in the web" +
    " file?";
            // 
            // pnlWebFileConfirmation
            // 
            this.pnlWebFileConfirmation.Controls.Add(this.rdbWebFileCleaningNo);
            this.pnlWebFileConfirmation.Controls.Add(this.rdbWebFileCleaningYes);
            this.pnlWebFileConfirmation.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlWebFileConfirmation.Location = new System.Drawing.Point(506, 40);
            this.pnlWebFileConfirmation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlWebFileConfirmation.Name = "pnlWebFileConfirmation";
            this.pnlWebFileConfirmation.Size = new System.Drawing.Size(94, 78);
            this.pnlWebFileConfirmation.TabIndex = 2;
            // 
            // rdbWebFileCleaningNo
            // 
            this.rdbWebFileCleaningNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbWebFileCleaningNo.Location = new System.Drawing.Point(0, 40);
            this.rdbWebFileCleaningNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbWebFileCleaningNo.Name = "rdbWebFileCleaningNo";
            this.rdbWebFileCleaningNo.Size = new System.Drawing.Size(94, 40);
            this.rdbWebFileCleaningNo.TabIndex = 1;
            this.rdbWebFileCleaningNo.TabStop = true;
            this.rdbWebFileCleaningNo.Text = "No";
            this.rdbWebFileCleaningNo.UseVisualStyleBackColor = true;
            // 
            // rdbWebFileCleaningYes
            // 
            this.rdbWebFileCleaningYes.Checked = true;
            this.rdbWebFileCleaningYes.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbWebFileCleaningYes.Location = new System.Drawing.Point(0, 0);
            this.rdbWebFileCleaningYes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbWebFileCleaningYes.Name = "rdbWebFileCleaningYes";
            this.rdbWebFileCleaningYes.Size = new System.Drawing.Size(94, 40);
            this.rdbWebFileCleaningYes.TabIndex = 0;
            this.rdbWebFileCleaningYes.TabStop = true;
            this.rdbWebFileCleaningYes.Text = "Yes";
            this.rdbWebFileCleaningYes.UseVisualStyleBackColor = true;
            // 
            // lblWebFileHeader
            // 
            this.lblWebFileHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWebFileHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebFileHeader.Location = new System.Drawing.Point(0, 0);
            this.lblWebFileHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWebFileHeader.Name = "lblWebFileHeader";
            this.lblWebFileHeader.Size = new System.Drawing.Size(600, 40);
            this.lblWebFileHeader.TabIndex = 0;
            this.lblWebFileHeader.Text = "Web file cleaning";
            // 
            // pnlJavaScriptRestriction
            // 
            this.pnlJavaScriptRestriction.Controls.Add(this.lblJavaScriptRestrictionDesc);
            this.pnlJavaScriptRestriction.Controls.Add(this.lblJavaScriptRestrictionHeader);
            this.pnlJavaScriptRestriction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlJavaScriptRestriction.Location = new System.Drawing.Point(0, 118);
            this.pnlJavaScriptRestriction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlJavaScriptRestriction.Name = "pnlJavaScriptRestriction";
            this.pnlJavaScriptRestriction.Size = new System.Drawing.Size(600, 118);
            this.pnlJavaScriptRestriction.TabIndex = 2;
            // 
            // lblJavaScriptRestrictionDesc
            // 
            this.lblJavaScriptRestrictionDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJavaScriptRestrictionDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJavaScriptRestrictionDesc.Location = new System.Drawing.Point(0, 40);
            this.lblJavaScriptRestrictionDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJavaScriptRestrictionDesc.Name = "lblJavaScriptRestrictionDesc";
            this.lblJavaScriptRestrictionDesc.Size = new System.Drawing.Size(600, 78);
            this.lblJavaScriptRestrictionDesc.TabIndex = 1;
            this.lblJavaScriptRestrictionDesc.Text = "You are trying to import JavaScript web file. We will remove JavaScript file type" +
    " restriction to ensure successful import\r\nNote: The restriction will be added ba" +
    "ck at the end of the import process";
            // 
            // lblJavaScriptRestrictionHeader
            // 
            this.lblJavaScriptRestrictionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblJavaScriptRestrictionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJavaScriptRestrictionHeader.Location = new System.Drawing.Point(0, 0);
            this.lblJavaScriptRestrictionHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJavaScriptRestrictionHeader.Name = "lblJavaScriptRestrictionHeader";
            this.lblJavaScriptRestrictionHeader.Size = new System.Drawing.Size(600, 40);
            this.lblJavaScriptRestrictionHeader.TabIndex = 0;
            this.lblJavaScriptRestrictionHeader.Text = "JavaScript file type restriction";
            // 
            // pnlPagePlugin
            // 
            this.pnlPagePlugin.Controls.Add(this.lblWebPagePluginDesc);
            this.pnlPagePlugin.Controls.Add(this.lblPagePluginHeader);
            this.pnlPagePlugin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPagePlugin.Location = new System.Drawing.Point(0, 0);
            this.pnlPagePlugin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlPagePlugin.Name = "pnlPagePlugin";
            this.pnlPagePlugin.Size = new System.Drawing.Size(600, 118);
            this.pnlPagePlugin.TabIndex = 1;
            // 
            // lblWebPagePluginDesc
            // 
            this.lblWebPagePluginDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWebPagePluginDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebPagePluginDesc.Location = new System.Drawing.Point(0, 40);
            this.lblWebPagePluginDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWebPagePluginDesc.Name = "lblWebPagePluginDesc";
            this.lblWebPagePluginDesc.Size = new System.Drawing.Size(600, 78);
            this.lblWebPagePluginDesc.TabIndex = 1;
            this.lblWebPagePluginDesc.Text = "You are trying to import web pages. We will disable plugins steps related to this" +
    " entity to ensure successful import. \r\nNote: The plugin steps will be enabled at" +
    " the end of the import process";
            // 
            // lblPagePluginHeader
            // 
            this.lblPagePluginHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPagePluginHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagePluginHeader.Location = new System.Drawing.Point(0, 0);
            this.lblPagePluginHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPagePluginHeader.Name = "lblPagePluginHeader";
            this.lblPagePluginHeader.Size = new System.Drawing.Size(600, 40);
            this.lblPagePluginHeader.TabIndex = 0;
            this.lblPagePluginHeader.Text = "Web Page plugins";
            // 
            // pnlSiteSettings
            // 
            this.pnlSiteSettings.Controls.Add(this.lblSiteSettingsDesc);
            this.pnlSiteSettings.Controls.Add(this.panel1);
            this.pnlSiteSettings.Controls.Add(this.lblSiteSettingsHeader);
            this.pnlSiteSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSiteSettings.Location = new System.Drawing.Point(0, 354);
            this.pnlSiteSettings.Name = "pnlSiteSettings";
            this.pnlSiteSettings.Size = new System.Drawing.Size(600, 120);
            this.pnlSiteSettings.TabIndex = 5;
            // 
            // lblSiteSettingsHeader
            // 
            this.lblSiteSettingsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSiteSettingsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSiteSettingsHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSiteSettingsHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteSettingsHeader.Name = "lblSiteSettingsHeader";
            this.lblSiteSettingsHeader.Size = new System.Drawing.Size(600, 34);
            this.lblSiteSettingsHeader.TabIndex = 6;
            this.lblSiteSettingsHeader.Text = "Site Settings import";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbCreateOnlyNewSettingsNo);
            this.panel1.Controls.Add(this.rdbCreateOnlyNewSettingsYes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(506, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(94, 86);
            this.panel1.TabIndex = 8;
            // 
            // rdbCreateOnlyNewSettingsNo
            // 
            this.rdbCreateOnlyNewSettingsNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbCreateOnlyNewSettingsNo.Location = new System.Drawing.Point(0, 40);
            this.rdbCreateOnlyNewSettingsNo.Margin = new System.Windows.Forms.Padding(2);
            this.rdbCreateOnlyNewSettingsNo.Name = "rdbCreateOnlyNewSettingsNo";
            this.rdbCreateOnlyNewSettingsNo.Size = new System.Drawing.Size(94, 40);
            this.rdbCreateOnlyNewSettingsNo.TabIndex = 1;
            this.rdbCreateOnlyNewSettingsNo.TabStop = true;
            this.rdbCreateOnlyNewSettingsNo.Text = "No";
            this.rdbCreateOnlyNewSettingsNo.UseVisualStyleBackColor = true;
            // 
            // rdbCreateOnlyNewSettingsYes
            // 
            this.rdbCreateOnlyNewSettingsYes.Checked = true;
            this.rdbCreateOnlyNewSettingsYes.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbCreateOnlyNewSettingsYes.Location = new System.Drawing.Point(0, 0);
            this.rdbCreateOnlyNewSettingsYes.Margin = new System.Windows.Forms.Padding(2);
            this.rdbCreateOnlyNewSettingsYes.Name = "rdbCreateOnlyNewSettingsYes";
            this.rdbCreateOnlyNewSettingsYes.Size = new System.Drawing.Size(94, 40);
            this.rdbCreateOnlyNewSettingsYes.TabIndex = 0;
            this.rdbCreateOnlyNewSettingsYes.TabStop = true;
            this.rdbCreateOnlyNewSettingsYes.Text = "Yes";
            this.rdbCreateOnlyNewSettingsYes.UseVisualStyleBackColor = true;
            // 
            // lblSiteSettingsDesc
            // 
            this.lblSiteSettingsDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSiteSettingsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSiteSettingsDesc.Location = new System.Drawing.Point(0, 34);
            this.lblSiteSettingsDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteSettingsDesc.Name = "lblSiteSettingsDesc";
            this.lblSiteSettingsDesc.Size = new System.Drawing.Size(506, 86);
            this.lblSiteSettingsDesc.TabIndex = 9;
            this.lblSiteSettingsDesc.Text = "You are trying to import Site Settings. Would you like to create only new Site Se" +
    "ttings and don\'t overwrite existing settings so that you don\'t update the config" +
    "uration of the targeted environment";
            // 
            // PreImportWarningDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(616, 593);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PreImportWarningDialog";
            this.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Warning";
            this.pnlBottom.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlWebFile.ResumeLayout(false);
            this.pnlWebFileConfirmation.ResumeLayout(false);
            this.pnlJavaScriptRestriction.ResumeLayout(false);
            this.pnlPagePlugin.ResumeLayout(false);
            this.pnlSiteSettings.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlPagePlugin;
        private System.Windows.Forms.Label lblWebPagePluginDesc;
        private System.Windows.Forms.Label lblPagePluginHeader;
        private System.Windows.Forms.Panel pnlJavaScriptRestriction;
        private System.Windows.Forms.Label lblJavaScriptRestrictionDesc;
        private System.Windows.Forms.Label lblJavaScriptRestrictionHeader;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlWebFile;
        private System.Windows.Forms.Label lblWebFileDesc;
        private System.Windows.Forms.Panel pnlWebFileConfirmation;
        private System.Windows.Forms.RadioButton rdbWebFileCleaningNo;
        private System.Windows.Forms.RadioButton rdbWebFileCleaningYes;
        private System.Windows.Forms.Label lblWebFileHeader;
        private System.Windows.Forms.Panel pnlSiteSettings;
        private System.Windows.Forms.Label lblSiteSettingsDesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbCreateOnlyNewSettingsNo;
        private System.Windows.Forms.RadioButton rdbCreateOnlyNewSettingsYes;
        private System.Windows.Forms.Label lblSiteSettingsHeader;
    }
}