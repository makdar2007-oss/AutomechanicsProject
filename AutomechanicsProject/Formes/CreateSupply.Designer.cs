using AutomechanicsProject.Properties;
using System;
namespace AutomechanicsProject.Formes
{
    partial class CreateSupply
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
            this.labelSupply = new System.Windows.Forms.Label();
            this.panelSupply = new System.Windows.Forms.Panel();
            this.textBoxinn = new System.Windows.Forms.TextBox();
            this.buttoncheck = new System.Windows.Forms.Button();
            this.comboBoxsyppliertipe = new System.Windows.Forms.ComboBox();
            this.labelTotalValue = new System.Windows.Forms.Label();
            this.labelTotalCaption = new System.Windows.Forms.Label();
            this.dataGridViewSupply = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonConfirmSupply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddToList = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.comboBoxSupplier = new System.Windows.Forms.ComboBox();
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.dateTimePickerExpiry = new System.Windows.Forms.DateTimePicker();
            this.labelProduct = new System.Windows.Forms.Label();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelExpiry = new System.Windows.Forms.Label();
            this.panelSupply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSupply)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSupply
            // 
            this.labelSupply.AutoSize = true;
            this.labelSupply.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSupply.Location = new System.Drawing.Point(158, 9);
            this.labelSupply.Name = "labelSupply";
            this.labelSupply.Size = new System.Drawing.Size(183, 42);
            this.labelSupply.TabIndex = 0;
            this.labelSupply.Text = "Поставка";
            // 
            // panelSupply
            // 
            this.panelSupply.BackColor = System.Drawing.SystemColors.Window;
            this.panelSupply.Controls.Add(this.textBoxinn);
            this.panelSupply.Controls.Add(this.buttoncheck);
            this.panelSupply.Controls.Add(this.comboBoxsyppliertipe);
            this.panelSupply.Controls.Add(this.labelTotalValue);
            this.panelSupply.Controls.Add(this.labelTotalCaption);
            this.panelSupply.Controls.Add(this.dataGridViewSupply);
            this.panelSupply.Controls.Add(this.buttonConfirmSupply);
            this.panelSupply.Controls.Add(this.buttonCancel);
            this.panelSupply.Controls.Add(this.buttonAddToList);
            this.panelSupply.Controls.Add(this.buttonImport);
            this.panelSupply.Controls.Add(this.comboBoxProduct);
            this.panelSupply.Controls.Add(this.textBoxQuantity);
            this.panelSupply.Controls.Add(this.comboBoxSupplier);
            this.panelSupply.Controls.Add(this.comboBoxCurrency);
            this.panelSupply.Controls.Add(this.textBoxPrice);
            this.panelSupply.Controls.Add(this.dateTimePickerExpiry);
            this.panelSupply.Controls.Add(this.labelProduct);
            this.panelSupply.Controls.Add(this.labelQuantity);
            this.panelSupply.Controls.Add(this.labelSupplier);
            this.panelSupply.Controls.Add(this.labelCurrency);
            this.panelSupply.Controls.Add(this.labelPrice);
            this.panelSupply.Controls.Add(this.labelExpiry);
            this.panelSupply.Controls.Add(this.labelSupply);
            this.panelSupply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSupply.Location = new System.Drawing.Point(0, 0);
            this.panelSupply.Name = "panelSupply";
            this.panelSupply.Size = new System.Drawing.Size(1823, 1149);
            this.panelSupply.TabIndex = 1;
            // 
            // textBoxinn
            // 
            this.textBoxinn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxinn.Location = new System.Drawing.Point(12, 127);
            this.textBoxinn.Name = "textBoxinn";
            this.textBoxinn.Size = new System.Drawing.Size(346, 41);
            this.textBoxinn.TabIndex = 24;
            // 
            // buttoncheck
            // 
            this.buttoncheck.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.buttoncheck.Location = new System.Drawing.Point(377, 126);
            this.buttoncheck.Name = "buttoncheck";
            this.buttoncheck.Size = new System.Drawing.Size(173, 47);
            this.buttoncheck.TabIndex = 23;
            this.buttoncheck.Text = "Проверить";
            this.buttoncheck.UseVisualStyleBackColor = false;
            this.buttoncheck.Click += new System.EventHandler(this.buttoncheck_Click);
            // 
            // comboBoxsyppliertipe
            // 
            this.comboBoxsyppliertipe.DropDownHeight = 150;
            this.comboBoxsyppliertipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxsyppliertipe.FormattingEnabled = true;
            this.comboBoxsyppliertipe.IntegralHeight = false;
            this.comboBoxsyppliertipe.Location = new System.Drawing.Point(42, 54);
            this.comboBoxsyppliertipe.MaxDropDownItems = 10;
            this.comboBoxsyppliertipe.Name = "comboBoxsyppliertipe";
            this.comboBoxsyppliertipe.Size = new System.Drawing.Size(460, 41);
            this.comboBoxsyppliertipe.TabIndex = 22;
            // 
            // labelTotalValue
            // 
            this.labelTotalValue.AutoSize = true;
            this.labelTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalValue.Location = new System.Drawing.Point(194, 814);
            this.labelTotalValue.Name = "labelTotalValue";
            this.labelTotalValue.Size = new System.Drawing.Size(0, 37);
            this.labelTotalValue.TabIndex = 21;
            // 
            // labelTotalCaption
            // 
            this.labelTotalCaption.AutoSize = true;
            this.labelTotalCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalCaption.Location = new System.Drawing.Point(49, 814);
            this.labelTotalCaption.Name = "labelTotalCaption";
            this.labelTotalCaption.Size = new System.Drawing.Size(110, 37);
            this.labelTotalCaption.TabIndex = 20;
            this.labelTotalCaption.Text = "Итого:";
            // 
            // dataGridViewSupply
            // 
            this.dataGridViewSupply.AllowUserToAddRows = false;
            this.dataGridViewSupply.AllowUserToDeleteRows = false;
            this.dataGridViewSupply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSupply.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSupply.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewSupply.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewSupply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSupply.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.dataGridViewSupply.Location = new System.Drawing.Point(556, 30);
            this.dataGridViewSupply.Name = "dataGridViewSupply";
            this.dataGridViewSupply.ReadOnly = true;
            this.dataGridViewSupply.RowHeadersVisible = false;
            this.dataGridViewSupply.RowHeadersWidth = 82;
            this.dataGridViewSupply.RowTemplate.Height = 35;
            this.dataGridViewSupply.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSupply.Size = new System.Drawing.Size(1250, 1077);
            this.dataGridViewSupply.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnArticle;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnName;
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnQuantity;
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnPrice;
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnTotal;
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnSupplier;
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = global::AutomechanicsProject.Properties.Resources.Supply_DataGridView_ColumnExpiry;
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // buttonConfirmSupply
            // 
            this.buttonConfirmSupply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonConfirmSupply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConfirmSupply.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConfirmSupply.ForeColor = System.Drawing.Color.Black;
            this.buttonConfirmSupply.Location = new System.Drawing.Point(270, 1011);
            this.buttonConfirmSupply.Name = "buttonConfirmSupply";
            this.buttonConfirmSupply.Size = new System.Drawing.Size(242, 98);
            this.buttonConfirmSupply.TabIndex = 18;
            this.buttonConfirmSupply.Text = global::AutomechanicsProject.Properties.Resources.Supply_ButtonConfirmSupply_Text;
            this.buttonConfirmSupply.UseVisualStyleBackColor = false;
            this.buttonConfirmSupply.Click += new System.EventHandler(this.ButtonConfirmSupply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.White;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(35, 1011);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(200, 98);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = global::AutomechanicsProject.Properties.Resources.Supply_ButtonCancel_Text;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAddToList
            // 
            this.buttonAddToList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAddToList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddToList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddToList.ForeColor = System.Drawing.Color.Black;
            this.buttonAddToList.Location = new System.Drawing.Point(145, 887);
            this.buttonAddToList.Name = "buttonAddToList";
            this.buttonAddToList.Size = new System.Drawing.Size(250, 92);
            this.buttonAddToList.TabIndex = 16;
            this.buttonAddToList.Text = global::AutomechanicsProject.Properties.Resources.Supply_ButtonAddToList_Text;
            this.buttonAddToList.UseVisualStyleBackColor = false;
            this.buttonAddToList.Click += new System.EventHandler(this.ButtonAddToList_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonImport.ForeColor = System.Drawing.Color.Black;
            this.buttonImport.Location = new System.Drawing.Point(58, 720);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(454, 62);
            this.buttonImport.TabIndex = 15;
            this.buttonImport.Text = global::AutomechanicsProject.Properties.Resources.Supply_ButtonImport_Text;
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.DropDownHeight = 150;
            this.comboBoxProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.IntegralHeight = false;
            this.comboBoxProduct.Location = new System.Drawing.Point(56, 220);
            this.comboBoxProduct.MaxDropDownItems = 10;
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(460, 41);
            this.comboBoxProduct.TabIndex = 3;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxQuantity.Location = new System.Drawing.Point(50, 298);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(460, 41);
            this.textBoxQuantity.TabIndex = 5;
            // 
            // comboBoxSupplier
            // 
            this.comboBoxSupplier.BackColor = System.Drawing.SystemColors.Menu;
            this.comboBoxSupplier.DisplayMember = "Text";
            this.comboBoxSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSupplier.FormattingEnabled = true;
            this.comboBoxSupplier.Location = new System.Drawing.Point(50, 475);
            this.comboBoxSupplier.Name = "comboBoxSupplier";
            this.comboBoxSupplier.Size = new System.Drawing.Size(460, 41);
            this.comboBoxSupplier.TabIndex = 9;
            this.comboBoxSupplier.ValueMember = "Id";
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.DisplayMember = "DisplayText";
            this.comboBoxCurrency.DropDownHeight = 200;
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.IntegralHeight = false;
            this.comboBoxCurrency.Location = new System.Drawing.Point(50, 565);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(454, 41);
            this.comboBoxCurrency.TabIndex = 11;
            this.comboBoxCurrency.ValueMember = "Code";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPrice.Location = new System.Drawing.Point(58, 660);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(454, 41);
            this.textBoxPrice.TabIndex = 13;
            // 
            // dateTimePickerExpiry
            // 
            this.dateTimePickerExpiry.Checked = false;
            this.dateTimePickerExpiry.Enabled = false;
            this.dateTimePickerExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerExpiry.Location = new System.Drawing.Point(56, 388);
            this.dateTimePickerExpiry.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerExpiry.Name = "dateTimePickerExpiry";
            this.dateTimePickerExpiry.Size = new System.Drawing.Size(460, 41);
            this.dateTimePickerExpiry.TabIndex = 7;
            // 
            // labelProduct
            // 
            this.labelProduct.AutoSize = true;
            this.labelProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProduct.Location = new System.Drawing.Point(59, 177);
            this.labelProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProduct.Name = "labelProduct";
            this.labelProduct.Size = new System.Drawing.Size(354, 31);
            this.labelProduct.TabIndex = 2;
            this.labelProduct.Text = "Выберите товар из списка:";
            this.labelProduct.Visible = false;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQuantity.Location = new System.Drawing.Point(52, 264);
            this.labelQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(277, 31);
            this.labelQuantity.TabIndex = 4;
            this.labelQuantity.Text = "Введите количество:";
            this.labelQuantity.Visible = false;
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSupplier.Location = new System.Drawing.Point(59, 432);
            this.labelSupplier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(304, 31);
            this.labelSupplier.TabIndex = 8;
            this.labelSupplier.Text = "Выберите поставщика:";
            this.labelSupplier.Visible = false;
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrency.Location = new System.Drawing.Point(52, 519);
            this.labelCurrency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(225, 31);
            this.labelCurrency.TabIndex = 10;
            this.labelCurrency.Text = "Валюта закупки:";
            this.labelCurrency.Visible = false;
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrice.Location = new System.Drawing.Point(59, 609);
            this.labelPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(194, 31);
            this.labelPrice.TabIndex = 12;
            this.labelPrice.Text = "Введите цену:";
            this.labelPrice.Visible = false;
            // 
            // labelExpiry
            // 
            this.labelExpiry.AutoSize = true;
            this.labelExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelExpiry.Location = new System.Drawing.Point(72, 342);
            this.labelExpiry.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExpiry.Name = "labelExpiry";
            this.labelExpiry.Size = new System.Drawing.Size(205, 31);
            this.labelExpiry.TabIndex = 6;
            this.labelExpiry.Text = "Срок годности:";
            this.labelExpiry.Visible = false;
            // 
            // CreateSupply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1823, 1149);
            this.Controls.Add(this.panelSupply);
            this.MinimumSize = new System.Drawing.Size(1337, 819);
            this.Name = "CreateSupply";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Формирование поставки";
            this.panelSupply.ResumeLayout(false);
            this.panelSupply.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSupply)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSupply;
        private System.Windows.Forms.Panel panelSupply;
        private System.Windows.Forms.Label labelTotalValue;
        private System.Windows.Forms.Label labelTotalCaption;
        private System.Windows.Forms.DataGridView dataGridViewSupply;
        private System.Windows.Forms.Button buttonConfirmSupply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddToList;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ComboBox comboBoxProduct;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.ComboBox comboBoxSupplier;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.DateTimePicker dateTimePickerExpiry;
        private System.Windows.Forms.Label labelProduct;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.Label labelCurrency;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelExpiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.TextBox textBoxinn;
        private System.Windows.Forms.Button buttoncheck;
        private System.Windows.Forms.ComboBox comboBoxsyppliertipe;
    }
}