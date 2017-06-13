namespace MscrmTools.PortalRecordsMover.Controls
{
    partial class RecordsListerControl
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.llClear = new System.Windows.Forms.LinkLabel();
            this.llSelectAll = new System.Windows.Forms.LinkLabel();
            this.lvRecords = new System.Windows.Forms.ListView();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.llClear);
            this.pnlHeader.Controls.Add(this.llSelectAll);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(435, 30);
            this.pnlHeader.TabIndex = 2;
            // 
            // llClear
            // 
            this.llClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClear.AutoSize = true;
            this.llClear.Location = new System.Drawing.Point(299, 5);
            this.llClear.Name = "llClear";
            this.llClear.Size = new System.Drawing.Size(76, 13);
            this.llClear.TabIndex = 1;
            this.llClear.TabStop = true;
            this.llClear.Text = "Clear selection";
            this.llClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClear_LinkClicked);
            // 
            // llSelectAll
            // 
            this.llSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llSelectAll.AutoSize = true;
            this.llSelectAll.Location = new System.Drawing.Point(381, 5);
            this.llSelectAll.Name = "llSelectAll";
            this.llSelectAll.Size = new System.Drawing.Size(50, 13);
            this.llSelectAll.TabIndex = 0;
            this.llSelectAll.TabStop = true;
            this.llSelectAll.Text = "Select all";
            this.llSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectAll_LinkClicked);
            // 
            // lvRecords
            // 
            this.lvRecords.CheckBoxes = true;
            this.lvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRecords.Location = new System.Drawing.Point(0, 30);
            this.lvRecords.Name = "lvRecords";
            this.lvRecords.Size = new System.Drawing.Size(435, 358);
            this.lvRecords.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRecords.TabIndex = 3;
            this.lvRecords.UseCompatibleStateImageBehavior = false;
            this.lvRecords.View = System.Windows.Forms.View.Details;
            // 
            // RecordsListerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvRecords);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RecordsListerControl";
            this.Size = new System.Drawing.Size(435, 388);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.LinkLabel llClear;
        private System.Windows.Forms.LinkLabel llSelectAll;
        private System.Windows.Forms.ListView lvRecords;
    }
}
