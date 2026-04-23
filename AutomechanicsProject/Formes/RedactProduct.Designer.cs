using AutomechanicsProject.Properties;
using System.Drawing;

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
            this.comboBoxUnit = new System.Windows.Forms.ComboBox();
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
            this.panelRedact.Controls.Add(this.comboBoxUnit);
            this.panelRedact.Controls.Add(this.textBoxCategory);
            this.panelRedact.Controls.Add(this.textBoxName);
            this.panelRedact.Controls.Add(this.textBoxArt);
            this.panelRedact.Controls.Add(this.labelRedact);
            this.panelRedact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRedact.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelRedact.Location = new System.Drawing.Point(0, 0);
            this.panelRedact.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelRedact.Name = "panelRedact";
            this.panelRedact.Size = new System.Drawing.Size(623, 515);
            this.panelRedact.TabIndex = 0;
            // 
            // buttonRedact
            // 
            this.buttonRedact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRedact.Location = new System.Drawing.Point(372, 367);
            this.buttonRedact.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRedact.Name = "buttonRedact";
            this.buttonRedact.Size = new System.Drawing.Size(194, 53);
            this.buttonRedact.TabIndex = 7;
            this.buttonRedact.Text = Resources.RedactProduct_ButtonRedact_Text;
            this.buttonRedact.UseVisualStyleBackColor = true;
            this.buttonRedact.Click += new System.EventHandler(this.ButtonRedact_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Menu;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(23, 367);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(169, 53);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = Resources.RedactProduct_ButtonCancel_Text;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPrice.ForeColor = System.Drawing.Color.Black;
            this.textBoxPrice.Location = new System.Drawing.Point(96, 312);
            this.textBoxPrice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(400, 30);
            this.textBoxPrice.TabIndex = 5;
            this.textBoxPrice.Text = Resources.RedactProduct_TextBoxPrice_Watermark;
            this.textBoxPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxUnit
            // 
            this.comboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxUnit.ForeColor = System.Drawing.Color.Black;
            this.comboBoxUnit.FormattingEnabled = true;
            this.comboBoxUnit.Location = new System.Drawing.Point(96, 262);
            this.comboBoxUnit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxUnit.Name = "comboBoxUnit";
            this.comboBoxUnit.Size = new System.Drawing.Size(401, 33);
            this.comboBoxUnit.TabIndex = 4;
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCategory.ForeColor = System.Drawing.Color.Black;
            this.textBoxCategory.Location = new System.Drawing.Point(96, 214);
            this.textBoxCategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.ReadOnly = true;
            this.textBoxCategory.Size = new System.Drawing.Size(400, 30);
            this.textBoxCategory.TabIndex = 3;
            this.textBoxCategory.Text = Resources.RedactProduct_TextBoxCategory_Watermark;
            this.textBoxCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.ForeColor = System.Drawing.Color.Black;
            this.textBoxName.Location = new System.Drawing.Point(96, 166);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(400, 30);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.Text = Resources.RedactProduct_TextBoxName_Watermark;
            this.textBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxArt
            // 
            this.textBoxArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxArt.ForeColor = System.Drawing.Color.Black;
            this.textBoxArt.Location = new System.Drawing.Point(96, 107);
            this.textBoxArt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxArt.Name = "textBoxArt";
            this.textBoxArt.Size = new System.Drawing.Size(400, 30);
            this.textBoxArt.TabIndex = 1;
            this.textBoxArt.Text = Resources.RedactProduct_TextBoxArt_Watermark;
            this.textBoxArt.ReadOnly = true;
            this.textBoxArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRedact
            // 
            this.labelRedact.AutoSize = true;
            this.labelRedact.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRedact.Location = new System.Drawing.Point(113, 33);
            this.labelRedact.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedact.Name = "labelRedact";
            this.labelRedact.Size = new System.Drawing.Size(359, 39);
            this.labelRedact.TabIndex = 0;
            this.labelRedact.Text = Resources.RedactProduct_LabelTitle_Text;
            // 
            // RedactProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 515);
            this.Controls.Add(this.panelRedact);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Text = Resources.RedactProduct_Form_Title;
            this.Name = "RedactProduct";
            this.panelRedact.ResumeLayout(false);
            this.panelRedact.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRedact;
        private System.Windows.Forms.Button buttonRedact;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.ComboBox comboBoxUnit;
        private System.Windows.Forms.TextBox textBoxCategory;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxArt;
        private System.Windows.Forms.Label labelRedact;
    }
}