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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelStorekeeper = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.dataGridViewStore = new System.Windows.Forms.DataGridView();
            this.menuStripStorekeeper = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.Articul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameStorekeeper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelStorekeeper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStore)).BeginInit();
            this.menuStripStorekeeper.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStorekeeper
            // 
            this.panelStorekeeper.Controls.Add(this.buttonExit);
            this.panelStorekeeper.Controls.Add(this.dataGridViewStore);
            this.panelStorekeeper.Controls.Add(this.menuStripStorekeeper);
            this.panelStorekeeper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStorekeeper.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelStorekeeper.Location = new System.Drawing.Point(0, 0);
            this.panelStorekeeper.Name = "panelStorekeeper";
            this.panelStorekeeper.Size = new System.Drawing.Size(1286, 937);
            this.panelStorekeeper.TabIndex = 0;
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(1098, 0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(185, 49);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Выйти";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // dataGridViewStore
            // 
            this.dataGridViewStore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Articul,
            this.NameStorekeeper,
            this.Category,
            this.Unit,
            this.Price,
            this.Balance});
            this.dataGridViewStore.Location = new System.Drawing.Point(0, 55);
            this.dataGridViewStore.Name = "dataGridViewStore";
            this.dataGridViewStore.RowHeadersVisible = false;
            this.dataGridViewStore.RowHeadersWidth = 82;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewStore.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewStore.RowTemplate.Height = 33;
            this.dataGridViewStore.Size = new System.Drawing.Size(1286, 882);
            this.dataGridViewStore.TabIndex = 0;
            // 
            // menuStripStorekeeper
            // 
            this.menuStripStorekeeper.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStripStorekeeper.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripStorekeeper.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripStorekeeper.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripTextBox2});
            this.menuStripStorekeeper.Location = new System.Drawing.Point(0, 0);
            this.menuStripStorekeeper.Name = "menuStripStorekeeper";
            this.menuStripStorekeeper.Size = new System.Drawing.Size(1286, 54);
            this.menuStripStorekeeper.TabIndex = 1;
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(300, 50);
            this.toolStripTextBox1.Text = "Оформить отгрузку";
            this.toolStripTextBox1.Click += new System.EventHandler(this.ToolStripTextBox1_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(700, 50);
            this.toolStripTextBox2.Text = "Кладовщик:";
            this.toolStripTextBox2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Articul
            // 
            this.Articul.HeaderText = "Артикул";
            this.Articul.MinimumWidth = 10;
            this.Articul.Name = "Articul";
            this.Articul.Width = 150;
            // 
            // NameStorekeeper
            // 
            this.NameStorekeeper.HeaderText = "Название";
            this.NameStorekeeper.MinimumWidth = 10;
            this.NameStorekeeper.Name = "NameStorekeeper";
            this.NameStorekeeper.Width = 350;
            // 
            // Category
            // 
            this.Category.HeaderText = "Категория";
            this.Category.MinimumWidth = 10;
            this.Category.Name = "Category";
            this.Category.Width = 250;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Единица измерения";
            this.Unit.MinimumWidth = 10;
            this.Unit.Name = "Unit";
            this.Unit.Width = 150;
            // 
            // Price
            // 
            this.Price.HeaderText = "Цена";
            this.Price.MinimumWidth = 10;
            this.Price.Name = "Price";
            this.Price.Width = 200;
            // 
            // Balance
            // 
            this.Balance.HeaderText = "Остаток";
            this.Balance.MinimumWidth = 10;
            this.Balance.Name = "Balance";
            this.Balance.Width = 200;
            // 
            // StorekeeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 937);
            this.Controls.Add(this.panelStorekeeper);
            this.MainMenuStrip = this.menuStripStorekeeper;
            this.Name = "StorekeeperForm";
            this.Text = "Кладовщик";
            this.panelStorekeeper.ResumeLayout(false);
            this.panelStorekeeper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStore)).EndInit();
            this.menuStripStorekeeper.ResumeLayout(false);
            this.menuStripStorekeeper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStorekeeper;
        private System.Windows.Forms.DataGridView dataGridViewStore;
        private System.Windows.Forms.MenuStrip menuStripStorekeeper;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articul;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameStorekeeper;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
    }
}