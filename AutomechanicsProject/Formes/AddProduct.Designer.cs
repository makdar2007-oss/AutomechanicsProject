namespace AutomechanicsProject.Formes
{
    partial class AddProduct
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
            this.panelAdd = new System.Windows.Forms.Panel();
            this.labelAddProduct = new System.Windows.Forms.Label();
            this.textBoxArt = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.comboBoxUnit = new System.Windows.Forms.ComboBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.groupBoxExpiry = new System.Windows.Forms.GroupBox();
            this.radioButtonNoExpiry = new System.Windows.Forms.RadioButton();
            this.radioButtonHasExpiry = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelAdd.SuspendLayout();
            this.groupBoxExpiry.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAdd
            // 
            this.panelAdd.BackColor = System.Drawing.SystemColors.Window;
            this.panelAdd.Controls.Add(this.labelAddProduct);
            this.panelAdd.Controls.Add(this.textBoxArt);
            this.panelAdd.Controls.Add(this.textBoxName);
            this.panelAdd.Controls.Add(this.comboBoxCategory);
            this.panelAdd.Controls.Add(this.comboBoxUnit);
            this.panelAdd.Controls.Add(this.textBoxPrice);
            this.panelAdd.Controls.Add(this.groupBoxExpiry);
            this.panelAdd.Controls.Add(this.buttonCancel);
            this.panelAdd.Controls.Add(this.buttonAdd);
            this.panelAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdd.Location = new System.Drawing.Point(0, 0);
            this.panelAdd.Margin = new System.Windows.Forms.Padding(2);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(577, 442);
            this.panelAdd.TabIndex = 0;
            // 
            // labelAddProduct
            // 
            this.labelAddProduct.AutoSize = true;
            this.labelAddProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddProduct.Location = new System.Drawing.Point(171, 19);
            this.labelAddProduct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAddProduct.Name = "labelAddProduct";
            this.labelAddProduct.Size = new System.Drawing.Size(250, 36);
            this.labelAddProduct.TabIndex = 0;
            this.labelAddProduct.Text = "Добавить товар";
            // 
            // textBoxArt
            // 
            this.textBoxArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxArt.Location = new System.Drawing.Point(118, 77);
            this.textBoxArt.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxArt.Name = "textBoxArt";
            this.textBoxArt.Size = new System.Drawing.Size(342, 30);
            this.textBoxArt.TabIndex = 1;
            this.textBoxArt.Text = "Введите артикул";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(118, 122);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(342, 30);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.Text = "Введите название";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(118, 166);
            this.comboBoxCategory.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(343, 33);
            this.comboBoxCategory.TabIndex = 3;
            // 
            // comboBoxUnit
            // 
            this.comboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxUnit.FormattingEnabled = true;
            this.comboBoxUnit.Location = new System.Drawing.Point(118, 211);
            this.comboBoxUnit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxUnit.Name = "comboBoxUnit";
            this.comboBoxUnit.Size = new System.Drawing.Size(343, 33);
            this.comboBoxUnit.TabIndex = 4;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPrice.Location = new System.Drawing.Point(118, 256);
            this.textBoxPrice.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(342, 30);
            this.textBoxPrice.TabIndex = 5;
            this.textBoxPrice.Text = "Введите цену";
            // 
            // groupBoxExpiry
            // 
            this.groupBoxExpiry.Controls.Add(this.radioButtonNoExpiry);
            this.groupBoxExpiry.Controls.Add(this.radioButtonHasExpiry);
            this.groupBoxExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBoxExpiry.Location = new System.Drawing.Point(118, 291);
            this.groupBoxExpiry.Name = "groupBoxExpiry";
            this.groupBoxExpiry.Size = new System.Drawing.Size(343, 80);
            this.groupBoxExpiry.TabIndex = 6;
            this.groupBoxExpiry.TabStop = false;
            this.groupBoxExpiry.Text = "Срок годности";
            // 
            // radioButtonNoExpiry
            // 
            this.radioButtonNoExpiry.AutoSize = true;
            this.radioButtonNoExpiry.Checked = true;
            this.radioButtonNoExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButtonNoExpiry.Location = new System.Drawing.Point(200, 35);
            this.radioButtonNoExpiry.Name = "radioButtonNoExpiry";
            this.radioButtonNoExpiry.Size = new System.Drawing.Size(116, 24);
            this.radioButtonNoExpiry.TabIndex = 1;
            this.radioButtonNoExpiry.TabStop = true;
            this.radioButtonNoExpiry.Text = "Нет срока";
            this.radioButtonNoExpiry.CheckedChanged += new System.EventHandler(this.RadioButtonExpiry_CheckedChanged);
            // 
            // radioButtonHasExpiry
            // 
            this.radioButtonHasExpiry.AutoSize = true;
            this.radioButtonHasExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButtonHasExpiry.Location = new System.Drawing.Point(20, 35);
            this.radioButtonHasExpiry.Name = "radioButtonHasExpiry";
            this.radioButtonHasExpiry.Size = new System.Drawing.Size(114, 24);
            this.radioButtonHasExpiry.TabIndex = 0;
            this.radioButtonHasExpiry.Text = "Есть срок";
            this.radioButtonHasExpiry.CheckedChanged += new System.EventHandler(this.RadioButtonExpiry_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(118, 391);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(167, 40);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(300, 391);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(167, 40);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 442);
            this.Controls.Add(this.panelAdd);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddProduct";
            this.Text = "Добавить товар";
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.groupBoxExpiry.ResumeLayout(false);
            this.groupBoxExpiry.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Label labelAddProduct;
        private System.Windows.Forms.TextBox textBoxArt;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.ComboBox comboBoxUnit;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBoxExpiry;
        private System.Windows.Forms.RadioButton radioButtonNoExpiry;
        private System.Windows.Forms.RadioButton radioButtonHasExpiry;
    }
}