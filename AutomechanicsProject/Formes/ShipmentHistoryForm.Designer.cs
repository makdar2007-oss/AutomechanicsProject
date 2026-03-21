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
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Articul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameHistory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Storekeeper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHistory
            // 
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
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Articul,
            this.NameHistory,
            this.User,
            this.Storekeeper});
            this.dataGridViewHistory.Location = new System.Drawing.Point(0, 73);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.RowHeadersVisible = false;
            this.dataGridViewHistory.RowHeadersWidth = 82;
            this.dataGridViewHistory.RowTemplate.Height = 33;
            this.dataGridViewHistory.Size = new System.Drawing.Size(1254, 778);
            this.dataGridViewHistory.TabIndex = 1;
            // 
            // textBoxHistory
            // 
            this.textBoxHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHistory.Location = new System.Drawing.Point(0, 0);
            this.textBoxHistory.Name = "textBoxHistory";
            this.textBoxHistory.Size = new System.Drawing.Size(1254, 67);
            this.textBoxHistory.TabIndex = 0;
            this.textBoxHistory.Text = "История отгрузок";
            this.textBoxHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Number
            // 
            this.Number.HeaderText = "Номер";
            this.Number.MinimumWidth = 10;
            this.Number.Name = "Number";
            this.Number.Width = 150;
            // 
            // Articul
            // 
            this.Articul.HeaderText = "Артикул";
            this.Articul.MinimumWidth = 10;
            this.Articul.Name = "Articul";
            this.Articul.Width = 200;
            // 
            // NameHistory
            // 
            this.NameHistory.HeaderText = "Название";
            this.NameHistory.MinimumWidth = 10;
            this.NameHistory.Name = "NameHistory";
            this.NameHistory.Width = 300;
            // 
            // User
            // 
            this.User.HeaderText = "Кому";
            this.User.MinimumWidth = 10;
            this.User.Name = "User";
            this.User.Width = 300;
            // 
            // Storekeeper
            // 
            this.Storekeeper.HeaderText = "ФИО Кладовщика";
            this.Storekeeper.MinimumWidth = 10;
            this.Storekeeper.Name = "Storekeeper";
            this.Storekeeper.Width = 300;
            // 
            // ShipmentHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 854);
            this.Controls.Add(this.panelHistory);
            this.Name = "ShipmentHistoryForm";
            this.Text = "ShipmentHistoryForm";
            this.panelHistory.ResumeLayout(false);
            this.panelHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.TextBox textBoxHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articul;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Storekeeper;
    }
}