namespace AutomechanicsProject.Formes
{
    partial class AdminForm
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
            this.panelAdmin = new System.Windows.Forms.Panel();
            this.buttonhistory = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dataGridViewMainForm = new System.Windows.Forms.DataGridView();
            this.Articul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.toolStripComboBoxAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.категориюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.категориюToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.категориюToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxAdmin = new System.Windows.Forms.ToolStripTextBox();
            this.panelAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).BeginInit();
            this.menuStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAdmin
            // 
            this.panelAdmin.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelAdmin.Controls.Add(this.buttonhistory);
            this.panelAdmin.Controls.Add(this.buttonExit);
            this.panelAdmin.Controls.Add(this.textBoxSearch);
            this.panelAdmin.Controls.Add(this.dataGridViewMainForm);
            this.panelAdmin.Controls.Add(this.menuStripMainForm);
            this.panelAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdmin.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelAdmin.Location = new System.Drawing.Point(0, 0);
            this.panelAdmin.Name = "panelAdmin";
            this.panelAdmin.Size = new System.Drawing.Size(1327, 969);
            this.panelAdmin.TabIndex = 0;
            // 
            // buttonhistory
            // 
            this.buttonhistory.BackColor = System.Drawing.SystemColors.Control;
            this.buttonhistory.Location = new System.Drawing.Point(992, 60);
            this.buttonhistory.Name = "buttonhistory";
            this.buttonhistory.Size = new System.Drawing.Size(323, 70);
            this.buttonhistory.TabIndex = 4;
            this.buttonhistory.Text = "История отгрузок";
            this.buttonhistory.UseVisualStyleBackColor = false;
            this.buttonhistory.Click += new System.EventHandler(this.ButtonHistory_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Location = new System.Drawing.Point(1024, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(303, 46);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Выйти";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(3, 69);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(484, 54);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.Text = "Поиск:";
            this.textBoxSearch.Click += new System.EventHandler(this.TextBoxSearch_TextChanged);
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // dataGridViewMainForm
            // 
            this.dataGridViewMainForm.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridViewMainForm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMainForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Articul,
            this.Name,
            this.Category,
            this.Unit,
            this.Price,
            this.Balance});
            this.dataGridViewMainForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewMainForm.Location = new System.Drawing.Point(0, 131);
            this.dataGridViewMainForm.Name = "dataGridViewMainForm";
            this.dataGridViewMainForm.RowHeadersVisible = false;
            this.dataGridViewMainForm.RowHeadersWidth = 82;
            this.dataGridViewMainForm.RowTemplate.Height = 33;
            this.dataGridViewMainForm.Size = new System.Drawing.Size(1327, 838);
            this.dataGridViewMainForm.TabIndex = 0;
            // 
            // Articul
            // 
            this.Articul.HeaderText = "Артикул";
            this.Articul.MinimumWidth = 10;
            this.Articul.Name = "Articul";
            this.Articul.Width = 150;
            // 
            // Name
            // 
            this.Name.HeaderText = "Название";
            this.Name.MinimumWidth = 10;
            this.Name.Name = "Name";
            this.Name.Width = 300;
            // 
            // Category
            // 
            this.Category.HeaderText = "Категория";
            this.Category.MinimumWidth = 10;
            this.Category.Name = "Category";
            this.Category.Width = 270;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Ед. измерения";
            this.Unit.MinimumWidth = 10;
            this.Unit.Name = "Unit";
            this.Unit.Width = 200;
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
            // menuStripMainForm
            // 
            this.menuStripMainForm.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStripMainForm.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripMainForm.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxAdd,
            this.toolStripComboBox2,
            this.toolStripComboBox3,
            this.toolStripTextBoxAdmin});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(1327, 53);
            this.menuStripMainForm.TabIndex = 1;
            // 
            // toolStripComboBoxAdd
            // 
            this.toolStripComboBoxAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.товарToolStripMenuItem,
            this.категориюToolStripMenuItem});
            this.toolStripComboBoxAdd.Name = "toolStripComboBoxAdd";
            this.toolStripComboBoxAdd.Size = new System.Drawing.Size(158, 49);
            this.toolStripComboBoxAdd.Text = "Добавить";
            // 
            // товарToolStripMenuItem
            // 
            this.товарToolStripMenuItem.Name = "товарToolStripMenuItem";
            this.товарToolStripMenuItem.Size = new System.Drawing.Size(289, 50);
            this.товарToolStripMenuItem.Text = "Товар";
            // 
            // категориюToolStripMenuItem
            // 
            this.категориюToolStripMenuItem.Name = "категориюToolStripMenuItem";
            this.категориюToolStripMenuItem.Size = new System.Drawing.Size(289, 50);
            this.категориюToolStripMenuItem.Text = "Категорию";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.товарToolStripMenuItem1,
            this.категориюToolStripMenuItem1});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(221, 49);
            this.toolStripComboBox2.Text = "Редактировать";
            // 
            // товарToolStripMenuItem1
            // 
            this.товарToolStripMenuItem1.Name = "товарToolStripMenuItem1";
            this.товарToolStripMenuItem1.Size = new System.Drawing.Size(359, 50);
            this.товарToolStripMenuItem1.Text = "Товар";
            // 
            // категориюToolStripMenuItem1
            // 
            this.категориюToolStripMenuItem1.Name = "категориюToolStripMenuItem1";
            this.категориюToolStripMenuItem1.Size = new System.Drawing.Size(359, 50);
            this.категориюToolStripMenuItem1.Text = "Категорию";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.товарToolStripMenuItem2,
            this.категориюToolStripMenuItem2});
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(141, 49);
            this.toolStripComboBox3.Text = "Удалить";
            // 
            // товарToolStripMenuItem2
            // 
            this.товарToolStripMenuItem2.Name = "товарToolStripMenuItem2";
            this.товарToolStripMenuItem2.Size = new System.Drawing.Size(359, 50);
            this.товарToolStripMenuItem2.Text = "Товар";
            // 
            // категориюToolStripMenuItem2
            // 
            this.категориюToolStripMenuItem2.Name = "категориюToolStripMenuItem2";
            this.категориюToolStripMenuItem2.Size = new System.Drawing.Size(359, 50);
            this.категориюToolStripMenuItem2.Text = "Категорию";
            // 
            // toolStripTextBoxAdmin
            // 
            this.toolStripTextBoxAdmin.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxAdmin.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBoxAdmin.Name = "toolStripTextBoxAdmin";
            this.toolStripTextBoxAdmin.Size = new System.Drawing.Size(500, 49);
            this.toolStripTextBoxAdmin.Text = "Администратор: Фамилия И.О.";
            this.toolStripTextBoxAdmin.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 969);
            this.Controls.Add(this.panelAdmin);
            this.MainMenuStrip = this.menuStripMainForm;
            this.Text = "Главная форма";
            this.panelAdmin.ResumeLayout(false);
            this.panelAdmin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).EndInit();
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAdmin;
        private System.Windows.Forms.DataGridView dataGridViewMainForm;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBoxAdd;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem категориюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem категориюToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBox3;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem категориюToolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxAdmin;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonhistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articul;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
    }
}