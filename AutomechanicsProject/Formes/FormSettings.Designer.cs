using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Formes
{
    partial class FormSettings
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
            this.labelSettingsTitle = new System.Windows.Forms.TextBox();
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelSettingsTitle
            // 
            this.labelSettingsTitle.BackColor = System.Drawing.SystemColors.Window;
            this.labelSettingsTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSettingsTitle.Location = new System.Drawing.Point(275, 78);
            this.labelSettingsTitle.Margin = new System.Windows.Forms.Padding(2);
            this.labelSettingsTitle.MaximumSize = new System.Drawing.Size(440, 100);
            this.labelSettingsTitle.Name = "labelSettingsTitle";
            this.labelSettingsTitle.ReadOnly = true;
            this.labelSettingsTitle.Size = new System.Drawing.Size(440, 68);
            this.labelSettingsTitle.TabIndex = 0;
            this.labelSettingsTitle.TabStop = false;
            this.labelSettingsTitle.Text = "Настройки";
            this.labelSettingsTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.DropDownHeight = 200;
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.IntegralHeight = false;
            this.comboBoxCurrency.Location = new System.Drawing.Point(275, 230);
            this.comboBoxCurrency.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(477, 45);
            this.comboBoxCurrency.TabIndex = 1;
            // 
            // buttonChoose
            // 
            this.buttonChoose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonChoose.Location = new System.Drawing.Point(684, 559);
            this.buttonChoose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(191, 55);
            this.buttonChoose.TabIndex = 2;
            this.buttonChoose.Text = global::AutomechanicsProject.Properties.Resources.Currency_ButtonChoose_Text;
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(181, 545);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(191, 55);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = global::AutomechanicsProject.Properties.Resources.Currency_ButtonCancel_Text;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(275, 334);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(483, 45);
            this.comboBoxLanguage.TabIndex = 4;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1118, 815);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonChoose);
            this.Controls.Add(this.comboBoxCurrency);
            this.Controls.Add(this.labelSettingsTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormSettings";
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion 

        private System.Windows.Forms.TextBox labelSettingsTitle;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}