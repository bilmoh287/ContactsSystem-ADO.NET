namespace ContactsPresentationLayer
{
    partial class frmListContacts
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
            this.dgvAllContacts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAllContacts
            // 
            this.dgvAllContacts.AllowUserToAddRows = false;
            this.dgvAllContacts.AllowUserToDeleteRows = false;
            this.dgvAllContacts.AllowUserToOrderColumns = true;
            this.dgvAllContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllContacts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvAllContacts.Location = new System.Drawing.Point(0, 113);
            this.dgvAllContacts.Name = "dgvAllContacts";
            this.dgvAllContacts.ReadOnly = true;
            this.dgvAllContacts.RowHeadersWidth = 51;
            this.dgvAllContacts.RowTemplate.Height = 24;
            this.dgvAllContacts.Size = new System.Drawing.Size(984, 488);
            this.dgvAllContacts.TabIndex = 0;
            this.dgvAllContacts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllContacts_CellContentClick);
            // 
            // frmListContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 601);
            this.Controls.Add(this.dgvAllContacts);
            this.Name = "frmListContacts";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmListContacts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllContacts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllContacts;
    }
}

