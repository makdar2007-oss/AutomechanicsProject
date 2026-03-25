namespace AutomechanicsProject.Formes
{
    partial class StorekeeperForm
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
            this.panelStorekeeper = new System.Windows.Forms.Panel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.buttonShipment = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelData = new System.Windows.Forms.Panel();
            this.dataGridViewStore = new System.Windows.Forms.DataGridView();
            this.buttonExit = new System.Windows.Forms.Button();
            this.menuStripStorekeeper = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBoxStorekeeper = new System.Windows.Forms.ToolStripTextBox();
            this.panelStorekeeper.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStore)).BeginInit();
            this.menuStripStorekeeper.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStorekeeper
            // 
            this.panelStorekeeper.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelStorekeeper.Controls.Add(this.panelSearch);
            this.panelStorekeeper.Controls.Add(this.panelData);
            this.panelStorekeeper.Controls.Add(this.buttonExit);
            this.panelStorekeeper.Controls.Add(this.menuStripStorekeeper);
            this.panelStorekeeper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStorekeeper.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelStorekeeper.Location = new System.Drawing.Point(0, 0);
            this.panelStorekeeper.Name = "panelStorekeeper";
            this.panelStorekeeper.Size = new System.Drawing.Size(1322, 1154);
            this.panelStorekeeper.TabIndex = 0;
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.buttonShipment);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 57);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1322, 115);
            this.panelSearch.TabIndex = 6;
            // 
            // buttonShipment
            // 
            this.buttonShipment.BackColor = System.Drawing.SystemColors.Control;
            this.buttonShipment.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonShipment.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShipment.Location = new System.Drawing.Point(999, 0);
            this.buttonShipment.Name = "buttonShipment";
            this.buttonShipment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonShipment.Size = new System.Drawing.Size(323, 115);
            this.buttonShipment.TabIndex = 4;
            this.buttonShipment.Text = "Оформить отгрузку";
            this.buttonShipment.UseVisualStyleBackColor = false;
            this.buttonShipment.Click += new System.EventHandler(this.ButtonShipment_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(28, 19);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(484, 54);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.Text = "Поиск:";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.dataGridViewStore);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 57);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(1322, 1097);
            this.panelData.TabIndex = 5;
            // 
            // dataGridViewStore
            // 
            this.dataGridViewStore.AllowUserToResizeRows = false;
            this.dataGridViewStore.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStore.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridViewStore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewStore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewStore.Location = new System.Drawing.Point(0, 108);
            this.dataGridViewStore.Name = "dataGridViewStore";
            this.dataGridViewStore.RowHeadersVisible = false;
            this.dataGridViewStore.RowHeadersWidth = 82;
            this.dataGridViewStore.RowTemplate.Height = 35;
            this.dataGridViewStore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStore.Size = new System.Drawing.Size(1322, 989);
            this.dataGridViewStore.TabIndex = 1;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Location = new System.Drawing.Point(938, 0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(350, 62);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Выйти";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // menuStripStorekeeper
            // 
            this.menuStripStorekeeper.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStripStorekeeper.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripStorekeeper.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripStorekeeper.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxStorekeeper});
            this.menuStripStorekeeper.Location = new System.Drawing.Point(0, 0);
            this.menuStripStorekeeper.Name = "menuStripStorekeeper";
            this.menuStripStorekeeper.Size = new System.Drawing.Size(1322, 57);
            this.menuStripStorekeeper.TabIndex = 1;
            // 
            // toolStripTextBoxStorekeeper
            // 
            this.toolStripTextBoxStorekeeper.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxStorekeeper.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBoxStorekeeper.Name = "toolStripTextBoxStorekeeper";
            this.toolStripTextBoxStorekeeper.ReadOnly = true;
            this.toolStripTextBoxStorekeeper.Size = new System.Drawing.Size(500, 49);
            this.toolStripTextBoxStorekeeper.Text = "Кладовщик";
            // 
            // StorekeeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 1154);
            this.Controls.Add(this.panelStorekeeper);
            this.MainMenuStrip = this.menuStripStorekeeper;
            this.Name = "StorekeeperForm";
            this.Text = "Кладовщик - Управление складом";
            this.Load += new System.EventHandler(this.StorekeeperForm_Load);
            this.panelStorekeeper.ResumeLayout(false);
            this.panelStorekeeper.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStore)).EndInit();
            this.menuStripStorekeeper.ResumeLayout(false);
            this.menuStripStorekeeper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStorekeeper;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button buttonShipment;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.DataGridView dataGridViewStore;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.MenuStrip menuStripStorekeeper;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxStorekeeper;
    }
}