using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Formes
{
    partial class EditCategory
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
            this.panelEditCategory = new System.Windows.Forms.Panel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxNewName = new System.Windows.Forms.TextBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBoxScrapMetal = new System.Windows.Forms.GroupBox();
            this.radioButtonScrapYes = new System.Windows.Forms.RadioButton();
            this.radioButtonScrapNo = new System.Windows.Forms.RadioButton();
            this.panelEditCategory.SuspendLayout();
            this.groupBoxScrapMetal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEditCategory
            // 
            this.panelEditCategory.BackColor = System.Drawing.SystemColors.Window;
            this.panelEditCategory.Controls.Add(this.groupBoxScrapMetal);
            this.panelEditCategory.Controls.Add(this.buttonEdit);
            this.panelEditCategory.Controls.Add(this.buttonCancel);
            this.panelEditCategory.Controls.Add(this.textBoxNewName);
            this.panelEditCategory.Controls.Add(this.comboBoxCategory);
            this.panelEditCategory.Controls.Add(this.labelTitle);
            this.panelEditCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditCategory.Location = new System.Drawing.Point(0, 0);
            this.panelEditCategory.Name = "panelEditCategory";
            this.panelEditCategory.Size = new System.Drawing.Size(1037, 857);
            this.panelEditCategory.TabIndex = 0;
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.SystemColors.Control;
            this.buttonEdit.Enabled = false;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEdit.Location = new System.Drawing.Point(525, 654);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(290, 70);
            this.buttonEdit.TabIndex = 4;
            this.buttonEdit.Text = global::AutomechanicsProject.Properties.Resources.EditCategory_ButtonEdit_Text;
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(102, 654);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(250, 70);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = global::AutomechanicsProject.Properties.Resources.EditCategory_ButtonCancel_Text;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // textBoxNewName
            // 
            this.textBoxNewName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxNewName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNewName.Enabled = false;
            this.textBoxNewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNewName.ForeColor = System.Drawing.Color.Gray;
            this.textBoxNewName.Location = new System.Drawing.Point(150, 280);
            this.textBoxNewName.Name = "textBoxNewName";
            this.textBoxNewName.Size = new System.Drawing.Size(650, 50);
            this.textBoxNewName.TabIndex = 2;
            this.textBoxNewName.Text = "Введите название";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.BackColor = System.Drawing.SystemColors.MenuBar;
            this.comboBoxCategory.DisplayMember = "Text";
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(150, 159);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(650, 50);
            this.comboBoxCategory.TabIndex = 1;
            this.comboBoxCategory.ValueMember = "Id";
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCategory_SelectedIndexChanged);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.SystemColors.Window;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(117, 42);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(708, 63);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Редактировать категорию";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxScrapMetal
            // 
            this.groupBoxScrapMetal.Controls.Add(this.radioButtonScrapNo);
            this.groupBoxScrapMetal.Controls.Add(this.radioButtonScrapYes);
            this.groupBoxScrapMetal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxScrapMetal.Location = new System.Drawing.Point(161, 403);
            this.groupBoxScrapMetal.Name = "groupBoxScrapMetal";
            this.groupBoxScrapMetal.Size = new System.Drawing.Size(639, 187);
            this.groupBoxScrapMetal.TabIndex = 5;
            this.groupBoxScrapMetal.TabStop = false;
            this.groupBoxScrapMetal.Text = "Металлолом";
            // 
            // radioButtonScrapYes
            // 
            this.radioButtonScrapYes.AutoSize = true;
            this.radioButtonScrapYes.Location = new System.Drawing.Point(67, 82);
            this.radioButtonScrapYes.Name = "radioButtonScrapYes";
            this.radioButtonScrapYes.Size = new System.Drawing.Size(89, 41);
            this.radioButtonScrapYes.TabIndex = 0;
            this.radioButtonScrapYes.Text = "Да";
            this.radioButtonScrapYes.UseVisualStyleBackColor = true;
            // 
            // radioButtonScrapNo
            // 
            this.radioButtonScrapNo.AutoSize = true;
            this.radioButtonScrapNo.Checked = true;
            this.radioButtonScrapNo.Location = new System.Drawing.Point(338, 82);
            this.radioButtonScrapNo.Name = "radioButtonScrapNo";
            this.radioButtonScrapNo.Size = new System.Drawing.Size(102, 41);
            this.radioButtonScrapNo.TabIndex = 1;
            this.radioButtonScrapNo.TabStop = true;
            this.radioButtonScrapNo.Text = "Нет";
            this.radioButtonScrapNo.UseVisualStyleBackColor = true;
            // 
            // EditCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 857);
            this.Controls.Add(this.panelEditCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактировать категорию";
            this.Load += new System.EventHandler(this.EditCategory_Load);
            this.panelEditCategory.ResumeLayout(false);
            this.panelEditCategory.PerformLayout();
            this.groupBoxScrapMetal.ResumeLayout(false);
            this.groupBoxScrapMetal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEditCategory;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.TextBox textBoxNewName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.GroupBox groupBoxScrapMetal;
        private System.Windows.Forms.RadioButton radioButtonScrapNo;
        private System.Windows.Forms.RadioButton radioButtonScrapYes;
    }
}