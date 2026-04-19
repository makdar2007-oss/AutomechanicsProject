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
            this.buttonExit = new System.Windows.Forms.Button();
            this.panelSearchHIstory = new System.Windows.Forms.Panel();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonCurrency = new System.Windows.Forms.Button();
            this.buttonSupply = new System.Windows.Forms.Button();
            this.buttonhistory = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelAdminData = new System.Windows.Forms.Panel();
            this.dataGridViewMainForm = new System.Windows.Forms.DataGridView();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.toolStripComboBoxAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CategoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.CategoryToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxAdmin = new System.Windows.Forms.ToolStripTextBox();
            this.panelAdmin.SuspendLayout();
            this.panelSearchHIstory.SuspendLayout();
            this.panelAdminData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).BeginInit();
            this.menuStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAdmin
            // 
            this.panelAdmin.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelAdmin.Controls.Add(this.buttonExit);
            this.panelAdmin.Controls.Add(this.panelSearchHIstory);
            this.panelAdmin.Controls.Add(this.panelAdminData);
            this.panelAdmin.Controls.Add(this.menuStripMainForm);
            this.panelAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelAdmin.Location = new System.Drawing.Point(0, 0);
            this.panelAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.panelAdmin.Name = "panelAdmin";
            this.panelAdmin.Size = new System.Drawing.Size(882, 739);
            this.panelAdmin.TabIndex = 0;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(709, 0);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(173, 28);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Выйти";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // panelSearchHIstory
            // 
            this.panelSearchHIstory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchHIstory.Controls.Add(this.buttonReport);
            this.panelSearchHIstory.Controls.Add(this.buttonCurrency);
            this.panelSearchHIstory.Controls.Add(this.buttonSupply);
            this.panelSearchHIstory.Controls.Add(this.buttonhistory);
            this.panelSearchHIstory.Controls.Add(this.textBoxSearch);
            this.panelSearchHIstory.Location = new System.Drawing.Point(0, 36);
            this.panelSearchHIstory.Margin = new System.Windows.Forms.Padding(2);
            this.panelSearchHIstory.Name = "panelSearchHIstory";
            this.panelSearchHIstory.Size = new System.Drawing.Size(882, 74);
            this.panelSearchHIstory.TabIndex = 6;
            // 
            // buttonReport
            // 
            this.buttonReport.AutoSize = true;
            this.buttonReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReport.Location = new System.Drawing.Point(214, 19);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(83, 35);
            this.buttonReport.TabIndex = 6;
            this.buttonReport.Text = "Отчет";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonCurrency
            // 
            this.buttonCurrency.AutoSize = true;
            this.buttonCurrency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCurrency.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCurrency.FlatAppearance.BorderSize = 0;
            this.buttonCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCurrency.Location = new System.Drawing.Point(316, 19);
            this.buttonCurrency.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCurrency.Name = "buttonCurrency";
            this.buttonCurrency.Size = new System.Drawing.Size(159, 35);
            this.buttonCurrency.TabIndex = 5;
            this.buttonCurrency.Text = "Выбор валюты";
            this.buttonCurrency.UseVisualStyleBackColor = false;
            this.buttonCurrency.Click += new System.EventHandler(this.buttonCurrency_Click);
            // 
            // buttonSupply
            // 
            this.buttonSupply.AutoSize = true;
            this.buttonSupply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSupply.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSupply.FlatAppearance.BorderSize = 0;
            this.buttonSupply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSupply.Location = new System.Drawing.Point(500, 19);
            this.buttonSupply.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSupply.Name = "buttonSupply";
            this.buttonSupply.Size = new System.Drawing.Size(109, 35);
            this.buttonSupply.TabIndex = 1;
            this.buttonSupply.Text = "Поставка";
            this.buttonSupply.UseVisualStyleBackColor = false;
            this.buttonSupply.Click += new System.EventHandler(this.buttonSupply_Click);
            // 
            // buttonhistory
            // 
            this.buttonhistory.AutoSize = true;
            this.buttonhistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonhistory.BackColor = System.Drawing.SystemColors.Control;
            this.buttonhistory.FlatAppearance.BorderSize = 0;
            this.buttonhistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonhistory.Location = new System.Drawing.Point(638, 19);
            this.buttonhistory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonhistory.Name = "buttonhistory";
            this.buttonhistory.Size = new System.Drawing.Size(190, 35);
            this.buttonhistory.TabIndex = 4;
            this.buttonhistory.Text = "История отгрузок";
            this.buttonhistory.UseVisualStyleBackColor = false;
            this.buttonhistory.Click += new System.EventHandler(this.ButtonHistory_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(19, 22);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 30);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.Text = "Поиск:";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
            // 
            // panelAdminData
            // 
            this.panelAdminData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAdminData.Controls.Add(this.dataGridViewMainForm);
            this.panelAdminData.Location = new System.Drawing.Point(0, 110);
            this.panelAdminData.Margin = new System.Windows.Forms.Padding(2);
            this.panelAdminData.Name = "panelAdminData";
            this.panelAdminData.Size = new System.Drawing.Size(882, 629);
            this.panelAdminData.TabIndex = 5;
            // 
            // dataGridViewMainForm
            // 
            this.dataGridViewMainForm.AllowUserToResizeRows = false;
            this.dataGridViewMainForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMainForm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMainForm.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridViewMainForm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMainForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainForm.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMainForm.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewMainForm.Name = "dataGridViewMainForm";
            this.dataGridViewMainForm.RowHeadersVisible = false;
            this.dataGridViewMainForm.RowHeadersWidth = 82;
            this.dataGridViewMainForm.RowTemplate.Height = 35;
            this.dataGridViewMainForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMainForm.Size = new System.Drawing.Size(882, 629);
            this.dataGridViewMainForm.TabIndex = 1;
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStripMainForm.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxAdd,
            this.toolStripComboBox2,
            this.toolStripComboBox3,
            this.toolStripTextBoxAdmin});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStripMainForm.Size = new System.Drawing.Size(882, 30);
            this.menuStripMainForm.TabIndex = 1;
            // 
            // toolStripComboBoxAdd
            // 
            this.toolStripComboBoxAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem,
            this.CategoryToolStripMenuItem});
            this.toolStripComboBoxAdd.Name = "toolStripComboBoxAdd";
            this.toolStripComboBoxAdd.Size = new System.Drawing.Size(113, 28);
            this.toolStripComboBoxAdd.Text = "Добавить";
            // 
            // ProductToolStripMenuItem
            // 
            this.ProductToolStripMenuItem.Name = "ProductToolStripMenuItem";
            this.ProductToolStripMenuItem.Size = new System.Drawing.Size(193, 28);
            this.ProductToolStripMenuItem.Text = "Товар";
            this.ProductToolStripMenuItem.Click += new System.EventHandler(this.ProductToolStripMenuItem_Click);
            // 
            // CategoryToolStripMenuItem
            // 
            this.CategoryToolStripMenuItem.Name = "CategoryToolStripMenuItem";
            this.CategoryToolStripMenuItem.Size = new System.Drawing.Size(193, 28);
            this.CategoryToolStripMenuItem.Text = "Категорию";
            this.CategoryToolStripMenuItem.Click += new System.EventHandler(this.CategoryToolStripMenuItem_Click);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem1,
            this.CategoryToolStripMenuItem1});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(163, 28);
            this.toolStripComboBox2.Text = "Редактировать";
            // 
            // ProductToolStripMenuItem1
            // 
            this.ProductToolStripMenuItem1.Name = "ProductToolStripMenuItem1";
            this.ProductToolStripMenuItem1.Size = new System.Drawing.Size(193, 28);
            this.ProductToolStripMenuItem1.Text = "Товар";
            this.ProductToolStripMenuItem1.Click += new System.EventHandler(this.ProductToolStripMenuItem1_Click);
            // 
            // CategoryToolStripMenuItem1
            // 
            this.CategoryToolStripMenuItem1.Name = "CategoryToolStripMenuItem1";
            this.CategoryToolStripMenuItem1.Size = new System.Drawing.Size(193, 28);
            this.CategoryToolStripMenuItem1.Text = "Категорию";
            this.CategoryToolStripMenuItem1.Click += new System.EventHandler(this.CategoryToolStripMenuItem1_Click);
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem2,
            this.CategoryToolStripMenuItem2});
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(100, 28);
            this.toolStripComboBox3.Text = "Удалить";
            // 
            // ProductToolStripMenuItem2
            // 
            this.ProductToolStripMenuItem2.Name = "ProductToolStripMenuItem2";
            this.ProductToolStripMenuItem2.Size = new System.Drawing.Size(193, 28);
            this.ProductToolStripMenuItem2.Text = "Товар";
            this.ProductToolStripMenuItem2.Click += new System.EventHandler(this.ProductToolStripMenuItem2_Click);
            // 
            // CategoryToolStripMenuItem2
            // 
            this.CategoryToolStripMenuItem2.Name = "CategoryToolStripMenuItem2";
            this.CategoryToolStripMenuItem2.Size = new System.Drawing.Size(193, 28);
            this.CategoryToolStripMenuItem2.Text = "Категорию";
            this.CategoryToolStripMenuItem2.Click += new System.EventHandler(this.CategoryToolStripMenuItem2_Click);
            // 
            // toolStripTextBoxAdmin
            // 
            this.toolStripTextBoxAdmin.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBoxAdmin.Name = "toolStripTextBoxAdmin";
            this.toolStripTextBoxAdmin.ReadOnly = true;
            this.toolStripTextBoxAdmin.Size = new System.Drawing.Size(335, 28);
            this.toolStripTextBoxAdmin.Text = "Администратор";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 739);
            this.Controls.Add(this.panelAdmin);
            this.MainMenuStrip = this.menuStripMainForm;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(900, 400);
            this.Name = "AdminForm";
            this.Text = "Главная форма";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.panelAdmin.ResumeLayout(false);
            this.panelAdmin.PerformLayout();
            this.panelSearchHIstory.ResumeLayout(false);
            this.panelSearchHIstory.PerformLayout();
            this.panelAdminData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).EndInit();
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAdmin;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBoxAdd;
        private System.Windows.Forms.ToolStripMenuItem ProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem ProductToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CategoryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBox3;
        private System.Windows.Forms.ToolStripMenuItem ProductToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem CategoryToolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxAdmin;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonhistory;
        private System.Windows.Forms.Panel panelAdminData;
        private System.Windows.Forms.DataGridView dataGridViewMainForm;
        private System.Windows.Forms.Panel panelSearchHIstory;
        private System.Windows.Forms.Button buttonSupply;
        private System.Windows.Forms.Button buttonCurrency;
        private System.Windows.Forms.Button buttonReport;
    }
}