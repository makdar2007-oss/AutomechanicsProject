using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Formes
{
    partial class AddCategory
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
            this.labelAddCategory = new System.Windows.Forms.Label();
            this.textBoxAddCategory = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxScrapMetal = new System.Windows.Forms.GroupBox();
            this.radioButtonScrapYes = new System.Windows.Forms.RadioButton();
            this.radioButtonScrapNo = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBoxScrapMetal.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAddCategory
            // 
            this.labelAddCategory.AutoSize = true;
            this.labelAddCategory.BackColor = System.Drawing.SystemColors.Window;
            this.labelAddCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddCategory.Location = new System.Drawing.Point(220, 100);
            this.labelAddCategory.Name = "labelAddCategory";
            this.labelAddCategory.Size = new System.Drawing.Size(490, 55);
            this.labelAddCategory.TabIndex = 0;
            this.labelAddCategory.Text = "Добавить категорию";
            // 
            // textBoxAddCategory
            // 
            this.textBoxAddCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAddCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAddCategory.Location = new System.Drawing.Point(171, 261);
            this.textBoxAddCategory.Name = "textBoxAddCategory";
            this.textBoxAddCategory.Size = new System.Drawing.Size(573, 49);
            this.textBoxAddCategory.TabIndex = 1;
            this.textBoxAddCategory.Text = "Введите название";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Menu;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(81, 613);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(250, 80);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = global::AutomechanicsProject.Properties.Resources.AddCategory_ButtonCancelText;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(580, 613);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(250, 80);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = global::AutomechanicsProject.Properties.Resources.AddCategory_ButtonAddText;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.groupBoxScrapMetal);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 829);
            this.panel1.TabIndex = 4;
            // 
            // groupBoxScrapMetal
            // 
            this.groupBoxScrapMetal.Controls.Add(this.radioButtonScrapNo);
            this.groupBoxScrapMetal.Controls.Add(this.radioButtonScrapYes);
            this.groupBoxScrapMetal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxScrapMetal.Location = new System.Drawing.Point(171, 355);
            this.groupBoxScrapMetal.Name = "groupBoxScrapMetal";
            this.groupBoxScrapMetal.Size = new System.Drawing.Size(573, 204);
            this.groupBoxScrapMetal.TabIndex = 4;
            this.groupBoxScrapMetal.TabStop = false;
            this.groupBoxScrapMetal.Text = "Металлолом";
            // 
            // radioButtonScrapYes
            // 
            this.radioButtonScrapYes.AutoSize = true;
            this.radioButtonScrapYes.Location = new System.Drawing.Point(45, 101);
            this.radioButtonScrapYes.Name = "radioButtonScrapYes";
            this.radioButtonScrapYes.Size = new System.Drawing.Size(99, 46);
            this.radioButtonScrapYes.TabIndex = 0;
            this.radioButtonScrapYes.Text = "Да";
            this.radioButtonScrapYes.UseVisualStyleBackColor = true;
            // 
            // radioButtonScrapNo
            // 
            this.radioButtonScrapNo.AutoSize = true;
            this.radioButtonScrapNo.Checked = true;
            this.radioButtonScrapNo.Location = new System.Drawing.Point(329, 101);
            this.radioButtonScrapNo.Name = "radioButtonScrapNo";
            this.radioButtonScrapNo.Size = new System.Drawing.Size(114, 46);
            this.radioButtonScrapNo.TabIndex = 1;
            this.radioButtonScrapNo.TabStop = true;
            this.radioButtonScrapNo.Text = "Нет";
            this.radioButtonScrapNo.UseVisualStyleBackColor = true;
            // 
            // AddCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 829);
            this.Controls.Add(this.textBoxAddCategory);
            this.Controls.Add(this.labelAddCategory);
            this.Controls.Add(this.panel1);
            this.Name = "AddCategory";
            this.Text = "Добавить категорию";
            this.panel1.ResumeLayout(false);
            this.groupBoxScrapMetal.ResumeLayout(false);
            this.groupBoxScrapMetal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAddCategory;
        private System.Windows.Forms.TextBox textBoxAddCategory;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxScrapMetal;
        private System.Windows.Forms.RadioButton radioButtonScrapYes;
        private System.Windows.Forms.RadioButton radioButtonScrapNo;
    }
}