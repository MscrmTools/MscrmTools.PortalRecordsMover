namespace MscrmTools.PortalRecordsMover.Controls
{
    partial class WebsiteMapperControl
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
            this.cbbTargetWebsites = new System.Windows.Forms.ComboBox();
            this.lblSourceWebSite = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbbTargetWebsites
            // 
            this.cbbTargetWebsites.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbbTargetWebsites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTargetWebsites.FormattingEnabled = true;
            this.cbbTargetWebsites.Location = new System.Drawing.Point(240, 0);
            this.cbbTargetWebsites.Name = "cbbTargetWebsites";
            this.cbbTargetWebsites.Size = new System.Drawing.Size(298, 21);
            this.cbbTargetWebsites.TabIndex = 1;
            this.cbbTargetWebsites.SelectedIndexChanged += new System.EventHandler(this.cbbTargetWebsites_SelectedIndexChanged);
            // 
            // lblSourceWebSite
            // 
            this.lblSourceWebSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSourceWebSite.Location = new System.Drawing.Point(0, 0);
            this.lblSourceWebSite.Name = "lblSourceWebSite";
            this.lblSourceWebSite.Size = new System.Drawing.Size(240, 21);
            this.lblSourceWebSite.TabIndex = 2;
            this.lblSourceWebSite.Text = "[Label]";
            // 
            // WebsiteMapperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSourceWebSite);
            this.Controls.Add(this.cbbTargetWebsites);
            this.Name = "WebsiteMapperControl";
            this.Size = new System.Drawing.Size(538, 21);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbbTargetWebsites;
        private System.Windows.Forms.Label lblSourceWebSite;
    }
}
