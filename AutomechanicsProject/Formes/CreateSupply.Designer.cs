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
            this.labelSupply.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSupply.Location = new System.Drawing.Point(27, 19);
            this.labelSupply.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSupply.Name = "labelSupply";
            this.labelSupply.Size = new System.Drawing.Size(325, 29);
            this.labelSupply.TabIndex = 0;
            this.labelSupply.Text = "Формирование поставки";
            // 
            // panelSupply
            // 
            this.panelSupply.BackColor = System.Drawing.SystemColors.Window;
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
            this.panelSupply.Margin = new System.Windows.Forms.Padding(2);
            this.panelSupply.Name = "panelSupply";
            this.panelSupply.Size = new System.Drawing.Size(1100, 650);
            this.panelSupply.TabIndex = 1;
            // 
            // labelTotalValue
            // 
            this.labelTotalValue.AutoSize = true;
            this.labelTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalValue.Location = new System.Drawing.Point(93, 510);
            this.labelTotalValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalValue.Name = "labelTotalValue";
            this.labelTotalValue.Size = new System.Drawing.Size(0, 25);
            this.labelTotalValue.TabIndex = 21;
            // 
            // labelTotalCaption
            // 
            this.labelTotalCaption.AutoSize = true;
            this.labelTotalCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalCaption.Location = new System.Drawing.Point(27, 510);
            this.labelTotalCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalCaption.Name = "labelTotalCaption";
            this.labelTotalCaption.Size = new System.Drawing.Size(74, 25);
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
            this.dataGridViewSupply.Location = new System.Drawing.Point(371, 19);
            this.dataGridViewSupply.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewSupply.Name = "dataGridViewSupply";
            this.dataGridViewSupply.ReadOnly = true;
            this.dataGridViewSupply.RowHeadersVisible = false;
            this.dataGridViewSupply.RowHeadersWidth = 82;
            this.dataGridViewSupply.RowTemplate.Height = 35;
            this.dataGridViewSupply.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSupply.Size = new System.Drawing.Size(718, 604);
            this.dataGridViewSupply.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Артикул";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Название";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Кол-во";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Цена";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Сумма";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Поставщик";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Срок годности";
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
            this.buttonConfirmSupply.Location = new System.Drawing.Point(187, 560);
            this.buttonConfirmSupply.Margin = new System.Windows.Forms.Padding(2);
            this.buttonConfirmSupply.Name = "buttonConfirmSupply";
            this.buttonConfirmSupply.Size = new System.Drawing.Size(161, 63);
            this.buttonConfirmSupply.TabIndex = 18;
            this.buttonConfirmSupply.Text = "Подтвердить поставку";
            this.buttonConfirmSupply.UseVisualStyleBackColor = false;
            this.buttonConfirmSupply.Click += new System.EventHandler(this.ButtonConfirmSupply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.White;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(27, 560);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(133, 63);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAddToList
            // 
            this.buttonAddToList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAddToList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddToList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddToList.ForeColor = System.Drawing.Color.Black;
            this.buttonAddToList.Location = new System.Drawing.Point(32, 439);
            this.buttonAddToList.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddToList.Name = "buttonAddToList";
            this.buttonAddToList.Size = new System.Drawing.Size(167, 59);
            this.buttonAddToList.TabIndex = 16;
            this.buttonAddToList.Text = "Добавить в список";
            this.buttonAddToList.UseVisualStyleBackColor = false;
            this.buttonAddToList.Click += new System.EventHandler(this.ButtonAddToList_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonImport.ForeColor = System.Drawing.Color.Black;
            this.buttonImport.Location = new System.Drawing.Point(203, 439);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(132, 59);
            this.buttonImport.TabIndex = 15;
            this.buttonImport.Text = "Импорт файла";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.DropDownHeight = 150;
            this.comboBoxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.IntegralHeight = false;
            this.comboBoxProduct.Location = new System.Drawing.Point(27, 90);
            this.comboBoxProduct.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxProduct.MaxDropDownItems = 10;
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(308, 30);
            this.comboBoxProduct.TabIndex = 3;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxQuantity.Location = new System.Drawing.Point(27, 152);
            this.textBoxQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(308, 28);
            this.textBoxQuantity.TabIndex = 5;
            this.textBoxQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboBoxSupplier
            // 
            this.comboBoxSupplier.BackColor = System.Drawing.SystemColors.Menu;
            this.comboBoxSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSupplier.FormattingEnabled = true;
            this.comboBoxSupplier.Location = new System.Drawing.Point(27, 272);
            this.comboBoxSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSupplier.Name = "comboBoxSupplier";
            this.comboBoxSupplier.Size = new System.Drawing.Size(308, 30);
            this.comboBoxSupplier.TabIndex = 9;
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.Items.AddRange(new object[] {
            "RUB",
            "USD",
            "EUR",
            "CNY"});
            this.comboBoxCurrency.Location = new System.Drawing.Point(32, 336);
            this.comboBoxCurrency.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(303, 30);
            this.comboBoxCurrency.TabIndex = 11;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPrice.Location = new System.Drawing.Point(36, 401);
            this.textBoxPrice.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(299, 28);
            this.textBoxPrice.TabIndex = 13;
            this.textBoxPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dateTimePickerExpiry
            // 
            this.dateTimePickerExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerExpiry.Location = new System.Drawing.Point(27, 214);
            this.dateTimePickerExpiry.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerExpiry.Name = "dateTimePickerExpiry";
            this.dateTimePickerExpiry.Size = new System.Drawing.Size(308, 28);
            this.dateTimePickerExpiry.TabIndex = 7;
            // 
            // labelProduct
            // 
            this.labelProduct.AutoSize = true;
            this.labelProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProduct.Location = new System.Drawing.Point(24, 65);
            this.labelProduct.Name = "labelProduct";
            this.labelProduct.Size = new System.Drawing.Size(244, 20);
            this.labelProduct.TabIndex = 2;
            this.labelProduct.Text = "Выберите товар из списка: ";
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQuantity.Location = new System.Drawing.Point(24, 128);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(191, 20);
            this.labelQuantity.TabIndex = 4;
            this.labelQuantity.Text = "Введите количество:";
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSupplier.Location = new System.Drawing.Point(24, 250);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(205, 20);
            this.labelSupplier.TabIndex = 8;
            this.labelSupplier.Text = "Выберите поставщика:";
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrency.Location = new System.Drawing.Point(28, 314);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(234, 20);
            this.labelCurrency.TabIndex = 10;
            this.labelCurrency.Text = "Выберите валюту закупки:";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrice.Location = new System.Drawing.Point(32, 379);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(131, 20);
            this.labelPrice.TabIndex = 12;
            this.labelPrice.Text = "Введите цену:";
            // 
            // labelExpiry
            // 
            this.labelExpiry.AutoSize = true;
            this.labelExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelExpiry.Location = new System.Drawing.Point(24, 190);
            this.labelExpiry.Name = "labelExpiry";
            this.labelExpiry.Size = new System.Drawing.Size(139, 20);
            this.labelExpiry.TabIndex = 6;
            this.labelExpiry.Text = "Срок годности:";
            // 
            // CreateSupply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.panelSupply);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "CreateSupply";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Формирование поставки";
            this.Load += new System.EventHandler(this.CreateSupply_Load);
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
    }
}