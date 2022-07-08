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
            this.llOpenItem = new System.Windows.Forms.LinkLabel();
            this.llClear = new System.Windows.Forms.LinkLabel();
            this.llSelectAll = new System.Windows.Forms.LinkLabel();
            this.lvRecords = new System.Windows.Forms.ListView();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.llOpenItem);
            this.pnlHeader.Controls.Add(this.llClear);
            this.pnlHeader.Controls.Add(this.llSelectAll);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(803, 46);
            this.pnlHeader.TabIndex = 2;
            // 
            // llOpenItem
            // 
            this.llOpenItem.AutoSize = true;
            this.llOpenItem.Location = new System.Drawing.Point(4, 8);
            this.llOpenItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llOpenItem.Name = "llOpenItem";
            this.llOpenItem.Size = new System.Drawing.Size(146, 20);
            this.llOpenItem.TabIndex = 2;
            this.llOpenItem.TabStop = true;
            this.llOpenItem.Text = "Open selected item";
            this.llOpenItem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenItem_LinkClicked);
            // 
            // llClear
            // 
            this.llClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.llClear.Location = new System.Drawing.Point(545, 0);
            this.llClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llClear.Name = "llClear";
            this.llClear.Size = new System.Drawing.Size(175, 46);
            this.llClear.TabIndex = 1;
            this.llClear.TabStop = true;
            this.llClear.Text = "Clear selection";
            this.llClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClear_LinkClicked);
            // 
            // llSelectAll
            // 
            this.llSelectAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.llSelectAll.Location = new System.Drawing.Point(720, 0);
            this.llSelectAll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llSelectAll.Name = "llSelectAll";
            this.llSelectAll.Size = new System.Drawing.Size(83, 46);
            this.llSelectAll.TabIndex = 0;
            this.llSelectAll.TabStop = true;
            this.llSelectAll.Text = "Select all";
            this.llSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectAll_LinkClicked);
            // 
            // lvRecords
            // 
            this.lvRecords.CheckBoxes = true;
            this.lvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRecords.FullRowSelect = true;
            this.lvRecords.HideSelection = false;
            this.lvRecords.Location = new System.Drawing.Point(0, 46);
            this.lvRecords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvRecords.Name = "lvRecords";
            this.lvRecords.Size = new System.Drawing.Size(803, 582);
            this.lvRecords.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRecords.TabIndex = 3;
            this.lvRecords.UseCompatibleStateImageBehavior = false;
            this.lvRecords.View = System.Windows.Forms.View.Details;
            this.lvRecords.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvRecords_ItemChecked);
            // 
            // RecordsListerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvRecords);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RecordsListerControl";
            this.Size = new System.Drawing.Size(803, 628);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.LinkLabel llClear;
        private System.Windows.Forms.LinkLabel llSelectAll;
        private System.Windows.Forms.ListView lvRecords;
        private System.Windows.Forms.LinkLabel llOpenItem;
    }
}
