namespace AutomechanicsProject.Formes
{
    partial class ShipmentHistoryForm
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
            this.panelHistory = new System.Windows.Forms.Panel();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.textBoxHistory = new System.Windows.Forms.TextBox();
            this.panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHistory
            // 
            this.panelHistory.BackColor = System.Drawing.SystemColors.Window;
            this.panelHistory.Controls.Add(this.dataGridViewHistory);
            this.panelHistory.Controls.Add(this.textBoxHistory);
            this.panelHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelHistory.Location = new System.Drawing.Point(0, 0);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Size = new System.Drawing.Size(1254, 854);
            this.panelHistory.TabIndex = 0;
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.AllowUserToDeleteRows = false;
            this.dataGridViewHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHistory.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewHistory.Location = new System.Drawing.Point(0, 67);
            this.dataGridViewHistory.MultiSelect = false;
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.ReadOnly = true;
            this.dataGridViewHistory.RowHeadersVisible = false;
            this.dataGridViewHistory.RowHeadersWidth = 82;
            this.dataGridViewHistory.RowTemplate.Height = 33;
            this.dataGridViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistory.Size = new System.Drawing.Size(1254, 787);
            this.dataGridViewHistory.TabIndex = 1;
            // 
            // textBoxHistory
            // 
            this.textBoxHistory.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxHistory.Font = new System.Drawing.Font("Jost", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxHistory.Location = new System.Drawing.Point(0, 0);
            this.textBoxHistory.Name = "textBoxHistory";
            this.textBoxHistory.ReadOnly = true;
            this.textBoxHistory.Size = new System.Drawing.Size(1254, 67);
            this.textBoxHistory.TabIndex = 0;
            this.textBoxHistory.Text = "История отгрузок";
            this.textBoxHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ShipmentHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 854);
            this.Controls.Add(this.panelHistory);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ShipmentHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "История отгрузок";
            this.Load += new System.EventHandler(this.ShipmentHistoryForm_Load);
            this.panelHistory.ResumeLayout(false);
            this.panelHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.TextBox textBoxHistory;
    }
}