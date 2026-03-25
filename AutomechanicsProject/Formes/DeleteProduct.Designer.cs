using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    partial class DeleteProduct
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxArt = new System.Windows.Forms.TextBox();
            this.labelDeleteProduct = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.textBoxArt);
            this.panel1.Controls.Add(this.labelDeleteProduct);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 685);
            this.panel1.TabIndex = 0;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.Location = new System.Drawing.Point(516, 406);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(251, 67);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Menu;
            this.buttonCancel.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(40, 406);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(251, 67);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // textBoxArt
            // 
            this.textBoxArt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxArt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBoxArt.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxArt.Location = new System.Drawing.Point(201, 271);
            this.textBoxArt.Name = "textBoxArt";
            this.textBoxArt.ReadOnly = true;
            this.textBoxArt.Size = new System.Drawing.Size(350, 54);
            this.textBoxArt.TabIndex = 1;
            this.textBoxArt.Text = "Введите артикул товара";
            this.textBoxArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDeleteProduct
            // 
            this.labelDeleteProduct.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDeleteProduct.AutoSize = true;
            this.labelDeleteProduct.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDeleteProduct.Location = new System.Drawing.Point(257, 52);
            this.labelDeleteProduct.MaximumSize = new System.Drawing.Size(360, 0);
            this.labelDeleteProduct.Name = "labelDeleteProduct";
            this.labelDeleteProduct.Size = new System.Drawing.Size(218, 46);
            this.labelDeleteProduct.TabIndex = 0;
            this.labelDeleteProduct.Text = "Удалить товар";
            this.labelDeleteProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeleteProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 685);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Удалить товар";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxArt;
        private System.Windows.Forms.Label labelDeleteProduct;
    }
}