namespace AutomechanicsProject.Formes
{
    partial class ChoosingCurrency
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
            this.textBoxCurrency = new System.Windows.Forms.TextBox();
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();

            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            this.SuspendLayout();
            // 
            // textBoxCurrency
            // 
            this.textBoxCurrency.BackColor = System.Drawing.SystemColors.ControlLight; 
            this.textBoxCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCurrency.Location = new System.Drawing.Point(199, 43);
            this.textBoxCurrency.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCurrency.MaximumSize = new System.Drawing.Size(240, 30);
            this.textBoxCurrency.Name = "textBoxCurrency";
            this.textBoxCurrency.ReadOnly = true;
            this.textBoxCurrency.Size = new System.Drawing.Size(240, 30);
            this.textBoxCurrency.TabIndex = 0;
            this.textBoxCurrency.Text = "Выбор валюты";
            this.textBoxCurrency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxCurrency.TabStop = false;
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCurrency.DropDownHeight = 200;
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.Location = new System.Drawing.Point(83, 135);
            this.comboBoxCurrency.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(477, 33);
            this.comboBoxCurrency.TabIndex = 1;
            // 
            // buttonChoose
            // 
            this.buttonChoose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonChoose.Location = new System.Drawing.Point(370, 306);
            this.buttonChoose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(191, 55);
            this.buttonChoose.TabIndex = 2;
            this.buttonChoose.Text = "Выбрать";
            this.buttonChoose.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(39, 306);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(191, 55);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // ChoosingCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(629, 462);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonChoose);
            this.Controls.Add(this.comboBoxCurrency);
            this.Controls.Add(this.textBoxCurrency);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ChoosingCurrency";
            this.Text = "Выбор валюты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion 

        private System.Windows.Forms.TextBox textBoxCurrency;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonCancel;
    }
}