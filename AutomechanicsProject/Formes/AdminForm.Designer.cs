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
            this.panelSearchHIstory = new System.Windows.Forms.Panel();
            this.buttonhistory = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelAdminData = new System.Windows.Forms.Panel();
            this.dataGridViewMainForm = new System.Windows.Forms.DataGridView();
            this.buttonExit = new System.Windows.Forms.Button();
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
            this.Load += new System.EventHandler(this.AdminForm_Load);
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
            this.panelAdmin.Controls.Add(this.panelSearchHIstory);
            this.panelAdmin.Controls.Add(this.panelAdminData);
            this.panelAdmin.Controls.Add(this.buttonExit);
            this.panelAdmin.Controls.Add(this.menuStripMainForm);
            this.panelAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdmin.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelAdmin.Location = new System.Drawing.Point(0, 0);
            this.panelAdmin.Name = "panelAdmin";
            this.panelAdmin.Size = new System.Drawing.Size(1322, 1154);
            this.panelAdmin.TabIndex = 0;
            // 
            // panelSearchHIstory
            // 
            this.panelSearchHIstory.Controls.Add(this.buttonhistory);
            this.panelSearchHIstory.Controls.Add(this.textBoxSearch);
            this.panelSearchHIstory.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchHIstory.Location = new System.Drawing.Point(0, 57);
            this.panelSearchHIstory.Name = "panelSearchHIstory";
            this.panelSearchHIstory.Size = new System.Drawing.Size(1322, 115);
            this.panelSearchHIstory.TabIndex = 6;
            // 
            // buttonhistory
            // 
            this.buttonhistory.BackColor = System.Drawing.SystemColors.Control;
            this.buttonhistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonhistory.Location = new System.Drawing.Point(999, 0);
            this.buttonhistory.Name = "buttonhistory";
            this.buttonhistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonhistory.Size = new System.Drawing.Size(323, 115);
            this.buttonhistory.TabIndex = 4;
            this.buttonhistory.Text = "История отгрузок";
            this.buttonhistory.UseVisualStyleBackColor = false;
            this.buttonhistory.Click += new System.EventHandler(this.ButtonHistory_Click);
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
            // panelAdminData
            // 
            this.panelAdminData.Controls.Add(this.dataGridViewMainForm);
            this.panelAdminData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAdminData.Location = new System.Drawing.Point(0, 174);
            this.panelAdminData.Name = "panelAdminData";
            this.panelAdminData.Size = new System.Drawing.Size(1322, 980);
            this.panelAdminData.TabIndex = 5;
            // 
            // dataGridViewMainForm
            // 
            this.dataGridViewMainForm.AllowUserToResizeRows = false;
            this.dataGridViewMainForm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMainForm.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridViewMainForm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMainForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMainForm.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMainForm.Name = "dataGridViewMainForm";
            this.dataGridViewMainForm.RowHeadersVisible = false;
            this.dataGridViewMainForm.RowHeadersWidth = 82;
            this.dataGridViewMainForm.RowTemplate.Height = 35;
            this.dataGridViewMainForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMainForm.Size = new System.Drawing.Size(1322, 980);
            this.dataGridViewMainForm.TabIndex = 1;
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
            this.menuStripMainForm.Size = new System.Drawing.Size(1322, 57);
            this.menuStripMainForm.TabIndex = 1;
            // 
            // toolStripComboBoxAdd
            // 
            this.toolStripComboBoxAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem,
            this.CategoryToolStripMenuItem});
            this.toolStripComboBoxAdd.Name = "toolStripComboBoxAdd";
            this.toolStripComboBoxAdd.Size = new System.Drawing.Size(158, 49);
            this.toolStripComboBoxAdd.Text = "Добавить";
            // 
            // ProductToolStripMenuItem
            // 
            this.ProductToolStripMenuItem.Name = "ProductToolStripMenuItem";
            this.ProductToolStripMenuItem.Size = new System.Drawing.Size(289, 50);
            this.ProductToolStripMenuItem.Text = "Товар";
            // 
            // CategoryToolStripMenuItem
            // 
            this.CategoryToolStripMenuItem.Name = "CategoryToolStripMenuItem";
            this.CategoryToolStripMenuItem.Size = new System.Drawing.Size(289, 50);
            this.CategoryToolStripMenuItem.Text = "Категорию";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem1,
            this.CategoryToolStripMenuItem1});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(221, 49);
            this.toolStripComboBox2.Text = "Редактировать";
            // 
            // ProductToolStripMenuItem1
            // 
            this.ProductToolStripMenuItem1.Name = "ProductToolStripMenuItem1";
            this.ProductToolStripMenuItem1.Size = new System.Drawing.Size(289, 50);
            this.ProductToolStripMenuItem1.Text = "Товар";
            // 
            // CategoryToolStripMenuItem1
            // 
            this.CategoryToolStripMenuItem1.Name = "CategoryToolStripMenuItem1";
            this.CategoryToolStripMenuItem1.Size = new System.Drawing.Size(289, 50);
            this.CategoryToolStripMenuItem1.Text = "Категорию";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem2,
            this.CategoryToolStripMenuItem2});
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(141, 49);
            this.toolStripComboBox3.Text = "Удалить";
            // 
            // ProductToolStripMenuItem2
            // 
            this.ProductToolStripMenuItem2.Name = "ProductToolStripMenuItem2";
            this.ProductToolStripMenuItem2.Size = new System.Drawing.Size(289, 50);
            this.ProductToolStripMenuItem2.Text = "Товар";
            // 
            // CategoryToolStripMenuItem2
            // 
            this.CategoryToolStripMenuItem2.Name = "CategoryToolStripMenuItem2";
            this.CategoryToolStripMenuItem2.Size = new System.Drawing.Size(289, 50);
            this.CategoryToolStripMenuItem2.Text = "Категорию";
            // 
            // toolStripTextBoxAdmin
            // 
            this.toolStripTextBoxAdmin.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxAdmin.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBoxAdmin.Name = "toolStripTextBoxAdmin";
            this.toolStripTextBoxAdmin.ReadOnly = true;
            this.toolStripTextBoxAdmin.Size = new System.Drawing.Size(500, 49);
            this.toolStripTextBoxAdmin.Text = "Администратор";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 1154);
            this.Controls.Add(this.panelAdmin);
            this.MainMenuStrip = this.menuStripMainForm;
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
    }
}