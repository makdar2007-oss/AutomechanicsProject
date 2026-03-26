namespace AutomechanicsProject.Formes
{
    partial class RedactProduct
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
            this.panelRedact = new System.Windows.Forms.Panel();
            this.buttonRedact = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.textBoxCategory = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxArt = new System.Windows.Forms.TextBox();
            this.labelRedact = new System.Windows.Forms.Label();
            this.panelRedact.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRedact
            // 
            this.panelRedact.BackColor = System.Drawing.SystemColors.Window;
            this.panelRedact.Controls.Add(this.buttonRedact);
            this.panelRedact.Controls.Add(this.buttonCancel);
            this.panelRedact.Controls.Add(this.textBoxPrice);
            this.panelRedact.Controls.Add(this.textBoxUnit);
            this.panelRedact.Controls.Add(this.textBoxCategory);
            this.panelRedact.Controls.Add(this.textBoxName);
            this.panelRedact.Controls.Add(this.textBoxArt);
            this.panelRedact.Controls.Add(this.labelRedact);
            this.panelRedact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRedact.Font = new System.Drawing.Font("Jost", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelRedact.Location = new System.Drawing.Point(0, 0);
            this.panelRedact.Name = "panelRedact";
            this.panelRedact.Size = new System.Drawing.Size(935, 804);
            this.panelRedact.TabIndex = 0;
            // 
            // buttonRedact
            // 
            this.buttonRedact.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRedact.Location = new System.Drawing.Point(595, 574);
            this.buttonRedact.Name = "buttonRedact";
            this.buttonRedact.Size = new System.Drawing.Size(254, 83);
            this.buttonRedact.TabIndex = 7;
            this.buttonRedact.Text = "Редактировать";
            this.buttonRedact.UseVisualStyleBackColor = true;
            this.buttonRedact.Click += new System.EventHandler(this.ButtonRedact_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Menu;
            this.buttonCancel.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(35, 574);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(254, 83);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrice.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPrice.Location = new System.Drawing.Point(144, 487);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(599, 54);
            this.textBoxPrice.TabIndex = 5;
            this.textBoxPrice.Text = "Цена";
            this.textBoxPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUnit.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUnit.Location = new System.Drawing.Point(144, 409);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(599, 54);
            this.textBoxUnit.TabIndex = 4;
            this.textBoxUnit.Text = "Единица измерения";
            this.textBoxUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCategory.Location = new System.Drawing.Point(144, 335);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.ReadOnly = true;
            this.textBoxCategory.Size = new System.Drawing.Size(599, 44);
            this.textBoxCategory.TabIndex = 3;
            this.textBoxCategory.Text = "Категория";
            this.textBoxCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(144, 259);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(599, 54);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.Text = "Название";
            this.textBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxArt
            // 
            this.textBoxArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxArt.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxArt.Location = new System.Drawing.Point(144, 167);
            this.textBoxArt.Name = "textBoxArt";
            this.textBoxArt.Size = new System.Drawing.Size(599, 54);
            this.textBoxArt.TabIndex = 1;
            this.textBoxArt.Text = "Артикул";
            this.textBoxArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRedact
            // 
            this.labelRedact.AutoSize = true;
            this.labelRedact.Font = new System.Drawing.Font("Jost", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRedact.Location = new System.Drawing.Point(169, 51);
            this.labelRedact.Name = "labelRedact";
            this.labelRedact.Size = new System.Drawing.Size(507, 77);
            this.labelRedact.TabIndex = 0;
            this.labelRedact.Text = "Редактировать товар";
            // 
            // RedactProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 804);
            this.Controls.Add(this.panelRedact);
            this.Name = "RedactProduct";
            this.Text = "RedactProduct";
            this.panelRedact.ResumeLayout(false);
            this.panelRedact.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRedact;
        private System.Windows.Forms.Button buttonRedact;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.TextBox textBoxUnit;
        private System.Windows.Forms.TextBox textBoxCategory;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxArt;
        private System.Windows.Forms.Label labelRedact;
    }
}