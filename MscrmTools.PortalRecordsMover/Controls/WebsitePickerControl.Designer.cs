namespace MscrmTools.PortalRecordsMover.Controls
{
    partial class WebsitePickerControl
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
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.cbbWebsite = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            this.chkEnabled.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkEnabled.Location = new System.Drawing.Point(0, 0);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(200, 22);
            this.chkEnabled.TabIndex = 1;
            this.chkEnabled.Text = "Get records from website";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // cbbWebsite
            // 
            this.cbbWebsite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbWebsite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWebsite.FormattingEnabled = true;
            this.cbbWebsite.Location = new System.Drawing.Point(200, 0);
            this.cbbWebsite.Name = "cbbWebsite";
            this.cbbWebsite.Size = new System.Drawing.Size(315, 21);
            this.cbbWebsite.TabIndex = 2;
            // 
            // WebsitePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbWebsite);
            this.Controls.Add(this.chkEnabled);
            this.Name = "WebsitePickerControl";
            this.Size = new System.Drawing.Size(515, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.ComboBox cbbWebsite;
    }
}
