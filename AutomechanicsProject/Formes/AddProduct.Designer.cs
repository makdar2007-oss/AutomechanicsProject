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
            this.labelAddProduct = new System.Windows.Forms.Label();
            this.textBoxArt = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.panelAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAddProduct
            // 
            this.labelAddProduct.AutoSize = true;
            this.labelAddProduct.BackColor = System.Drawing.SystemColors.Window;
            this.labelAddProduct.Font = new System.Drawing.Font("Jost", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddProduct.Location = new System.Drawing.Point(242, 46);
            this.labelAddProduct.Name = "labelAddProduct";
            this.labelAddProduct.Size = new System.Drawing.Size(352, 69);
            this.labelAddProduct.TabIndex = 0;
            this.labelAddProduct.Text = "Добавить товар";
            // 
            // textBoxArt
            // 
            this.textBoxArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxArt.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxArt.Location = new System.Drawing.Point(177, 153);
            this.textBoxArt.Name = "textBoxArt";
            this.textBoxArt.Size = new System.Drawing.Size(476, 54);
            this.textBoxArt.TabIndex = 1;
            this.textBoxArt.Text = "Введите артикул";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(177, 230);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(476, 54);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.Text = "Введите название";
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUnit.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUnit.Location = new System.Drawing.Point(177, 391);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(476, 54);
            this.textBoxUnit.TabIndex = 4;
            this.textBoxUnit.Text = "Введите единицы измерения";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrice.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPrice.Location = new System.Drawing.Point(177, 463);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(476, 54);
            this.textBoxPrice.TabIndex = 5;
            this.textBoxPrice.Text = "Введите цену";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonCancel.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(31, 561);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(257, 62);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Jost", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(535, 561);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(257, 62);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // panelAdd
            // 
            this.panelAdd.BackColor = System.Drawing.SystemColors.Window;
            this.panelAdd.Controls.Add(this.comboBoxCategory);
            this.panelAdd.Controls.Add(this.labelAddProduct);
            this.panelAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdd.Location = new System.Drawing.Point(0, 0);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(866, 691);
            this.panelAdd.TabIndex = 8;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(177, 314);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(476, 45);
            this.comboBoxCategory.TabIndex = 1;
            this.comboBoxCategory.Text = "Выберите категорию";
            // 
            // AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 691);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.textBoxUnit);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxArt);
            this.Controls.Add(this.panelAdd);
            this.Name = "AddProduct";
            this.Text = "Добавить товар";
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAddProduct;
        private System.Windows.Forms.TextBox textBoxArt;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxUnit;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.ComboBox comboBoxCategory;
    }
}