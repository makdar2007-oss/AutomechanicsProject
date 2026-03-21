namespace AutomechanicsProject.Formes
{
    partial class CreateShipment
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
            this.labelShipment = new System.Windows.Forms.Label();
            this.panelShipment = new System.Windows.Forms.Panel();
            this.dataGridViewShipment = new System.Windows.Forms.DataGridView();
            this.Articul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonShipment = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.panelShipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShipment)).BeginInit();
            this.SuspendLayout();
            // 
            // labelShipment
            // 
            this.labelShipment.AutoSize = true;
            this.labelShipment.BackColor = System.Drawing.SystemColors.Window;
            this.labelShipment.Font = new System.Drawing.Font("Jost", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelShipment.Location = new System.Drawing.Point(39, 53);
            this.labelShipment.Name = "labelShipment";
            this.labelShipment.Size = new System.Drawing.Size(461, 54);
            this.labelShipment.TabIndex = 0;
            this.labelShipment.Text = "Формирование отгрузки";
            // 
            // panelShipment
            // 
            this.panelShipment.BackColor = System.Drawing.SystemColors.Window;
            this.panelShipment.Controls.Add(this.dataGridViewShipment);
            this.panelShipment.Controls.Add(this.buttonShipment);
            this.panelShipment.Controls.Add(this.buttonCancel);
            this.panelShipment.Controls.Add(this.buttonAdd);
            this.panelShipment.Controls.Add(this.comboBoxProduct);
            this.panelShipment.Controls.Add(this.textBoxUser);
            this.panelShipment.Controls.Add(this.textBoxUnit);
            this.panelShipment.Controls.Add(this.labelShipment);
            this.panelShipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelShipment.Location = new System.Drawing.Point(0, 0);
            this.panelShipment.Name = "panelShipment";
            this.panelShipment.Size = new System.Drawing.Size(1339, 900);
            this.panelShipment.TabIndex = 1;
            // 
            // dataGridViewShipment
            // 
            this.dataGridViewShipment.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridViewShipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShipment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Articul,
            this.Name,
            this.User});
            this.dataGridViewShipment.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridViewShipment.Location = new System.Drawing.Point(565, 0);
            this.dataGridViewShipment.Name = "dataGridViewShipment";
            this.dataGridViewShipment.RowHeadersWidth = 82;
            this.dataGridViewShipment.RowTemplate.Height = 33;
            this.dataGridViewShipment.Size = new System.Drawing.Size(774, 900);
            this.dataGridViewShipment.TabIndex = 7;
            // 
            // Articul
            // 
            this.Articul.HeaderText = "Артикул";
            this.Articul.MinimumWidth = 10;
            this.Articul.Name = "Articul";
            this.Articul.Width = 150;
            // 
            // Name
            // 
            this.Name.HeaderText = "Название";
            this.Name.MinimumWidth = 10;
            this.Name.Name = "Name";
            this.Name.Width = 300;
            // 
            // User
            // 
            this.User.HeaderText = "Кому";
            this.User.MinimumWidth = 10;
            this.User.Name = "User";
            this.User.Width = 250;
            // 
            // buttonShipment
            // 
            this.buttonShipment.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonShipment.Font = new System.Drawing.Font("Jost", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShipment.Location = new System.Drawing.Point(315, 659);
            this.buttonShipment.Name = "buttonShipment";
            this.buttonShipment.Size = new System.Drawing.Size(190, 88);
            this.buttonShipment.TabIndex = 6;
            this.buttonShipment.Text = "Подтвердить отгрузку";
            this.buttonShipment.UseVisualStyleBackColor = false;
            this.buttonShipment.Click += new System.EventHandler(this.ButtonShipment_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Jost", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(28, 659);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(190, 88);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonAdd.Font = new System.Drawing.Font("Jost", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(132, 401);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(258, 94);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Добавить в список";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.BackColor = System.Drawing.SystemColors.MenuBar;
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.Location = new System.Drawing.Point(46, 126);
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(459, 45);
            this.comboBoxProduct.TabIndex = 3;
            // 
            // textBoxUser
            // 
            this.textBoxUser.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBoxUser.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUser.Location = new System.Drawing.Point(46, 286);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(459, 54);
            this.textBoxUser.TabIndex = 2;
            this.textBoxUser.Text = "Введите кому";
            this.textBoxUser.Enter += new System.EventHandler(this.textBoxUser_Enter);
            this.textBoxUser.Leave += new System.EventHandler(this.textBoxUser_Leave);
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBoxUnit.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUnit.Location = new System.Drawing.Point(46, 204);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(459, 54);
            this.textBoxUnit.TabIndex = 1;
            this.textBoxUnit.Text = "Введите количество товара";
            this.textBoxUnit.Enter += new System.EventHandler(this.textBoxUnit_Enter);
            this.textBoxUnit.Leave += new System.EventHandler(this.textBoxUnit_Leave);
            // 
            // CreateShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 900);
            this.Controls.Add(this.panelShipment);
            this.Text = " Отгрузка";
            this.panelShipment.ResumeLayout(false);
            this.panelShipment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShipment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelShipment;
        private System.Windows.Forms.Panel panelShipment;
        private System.Windows.Forms.Button buttonShipment;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ComboBox comboBoxProduct;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxUnit;
        private System.Windows.Forms.DataGridView dataGridViewShipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articul;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
    }
}