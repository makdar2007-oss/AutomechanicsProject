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
            this.labelTotalValue = new System.Windows.Forms.Label();
            this.labelTotalCaption = new System.Windows.Forms.Label();
            this.dataGridViewShipment = new System.Windows.Forms.DataGridView();
            this.buttonShipment = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.comboBoxRecipient1 = new System.Windows.Forms.ComboBox();
            this.labelExpiry = new System.Windows.Forms.Label();
            this.comboBoxExpiry = new System.Windows.Forms.ComboBox();
            this.panelShipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShipment)).BeginInit();
            this.SuspendLayout();
            // 
            // labelShipment
            // 
            this.labelShipment.AutoSize = true;
            this.labelShipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelShipment.Location = new System.Drawing.Point(27, 19);
            this.labelShipment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelShipment.Name = "labelShipment";
            this.labelShipment.Size = new System.Drawing.Size(321, 29);
            this.labelShipment.TabIndex = 0;
            this.labelShipment.Text = "Формирование отгрузки";
            // 
            // panelShipment
            // 
            this.panelShipment.BackColor = System.Drawing.SystemColors.Window;
            this.panelShipment.Controls.Add(this.labelTotalValue);
            this.panelShipment.Controls.Add(this.labelTotalCaption);
            this.panelShipment.Controls.Add(this.dataGridViewShipment);
            this.panelShipment.Controls.Add(this.buttonShipment);
            this.panelShipment.Controls.Add(this.buttonCancel);
            this.panelShipment.Controls.Add(this.buttonAdd);
            this.panelShipment.Controls.Add(this.comboBoxProduct);
            this.panelShipment.Controls.Add(this.textBoxUnit);
            this.panelShipment.Controls.Add(this.comboBoxRecipient1);
            this.panelShipment.Controls.Add(this.labelShipment);
            this.panelShipment.Controls.Add(this.labelExpiry);
            this.panelShipment.Controls.Add(this.comboBoxExpiry);
            this.panelShipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShipment.Location = new System.Drawing.Point(0, 0);
            this.panelShipment.Margin = new System.Windows.Forms.Padding(2);
            this.panelShipment.Name = "panelShipment";
            this.panelShipment.Size = new System.Drawing.Size(896, 576);
            this.panelShipment.TabIndex = 1;
            // 
            // labelTotalValue
            // 
            this.labelTotalValue.AutoSize = true;
            this.labelTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalValue.Location = new System.Drawing.Point(93, 291);
            this.labelTotalValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalValue.Name = "labelTotalValue";
            this.labelTotalValue.Size = new System.Drawing.Size(0, 25);
            this.labelTotalValue.TabIndex = 9;
            // 
            // labelTotalCaption
            // 
            this.labelTotalCaption.AutoSize = true;
            this.labelTotalCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalCaption.Location = new System.Drawing.Point(27, 291);
            this.labelTotalCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalCaption.Name = "labelTotalCaption";
            this.labelTotalCaption.Size = new System.Drawing.Size(74, 25);
            this.labelTotalCaption.TabIndex = 8;
            this.labelTotalCaption.Text = "Итого:";
            // 
            // dataGridViewShipment
            // 
            this.dataGridViewShipment.AllowUserToAddRows = false;
            this.dataGridViewShipment.AllowUserToDeleteRows = false;
            this.dataGridViewShipment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewShipment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewShipment.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewShipment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewShipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShipment.Location = new System.Drawing.Point(367, 19);
            this.dataGridViewShipment.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewShipment.Name = "dataGridViewShipment";
            this.dataGridViewShipment.ReadOnly = true;
            this.dataGridViewShipment.RowHeadersVisible = false;
            this.dataGridViewShipment.RowHeadersWidth = 82;
            this.dataGridViewShipment.RowTemplate.Height = 35;
            this.dataGridViewShipment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewShipment.Size = new System.Drawing.Size(500, 525);
            this.dataGridViewShipment.TabIndex = 7;
            this.dataGridViewShipment.DoubleClick += new System.EventHandler(this.DataGridViewShipment_DoubleClick);
            // 
            // buttonShipment
            // 
            this.buttonShipment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonShipment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShipment.ForeColor = System.Drawing.Color.White;
            this.buttonShipment.Location = new System.Drawing.Point(187, 448);
            this.buttonShipment.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShipment.Name = "buttonShipment";
            this.buttonShipment.Size = new System.Drawing.Size(161, 45);
            this.buttonShipment.TabIndex = 6;
            this.buttonShipment.Text = "Подтвердить отгрузку";
            this.buttonShipment.UseVisualStyleBackColor = false;
            this.buttonShipment.Click += new System.EventHandler(this.ButtonShipment_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.White;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(27, 448);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(133, 45);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(85, 350);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(190, 68);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Добавить в список";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.Location = new System.Drawing.Point(27, 70);
            this.comboBoxProduct.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(308, 30);
            this.comboBoxProduct.TabIndex = 3;
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUnit.Location = new System.Drawing.Point(27, 128);
            this.textBoxUnit.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(308, 28);
            this.textBoxUnit.TabIndex = 1;
            // 
            // comboBoxRecipient1
            // 
            this.comboBoxRecipient1.BackColor = System.Drawing.SystemColors.Menu;
            this.comboBoxRecipient1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecipient1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRecipient1.FormattingEnabled = true;
            this.comboBoxRecipient1.Location = new System.Drawing.Point(27, 186);
            this.comboBoxRecipient1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxRecipient1.Name = "comboBoxRecipient1";
            this.comboBoxRecipient1.Size = new System.Drawing.Size(308, 30);
            this.comboBoxRecipient1.TabIndex = 2;
            // 
            // labelExpiry
            // 
            this.labelExpiry.AutoSize = true;
            this.labelExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelExpiry.Location = new System.Drawing.Point(27, 230);
            this.labelExpiry.Name = "labelExpiry";
            this.labelExpiry.Size = new System.Drawing.Size(149, 24);
            this.labelExpiry.TabIndex = 10;
            this.labelExpiry.Text = "Срок годности:";
            // 
            // comboBoxExpiry
            // 
            this.comboBoxExpiry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExpiry.Enabled = false;
            this.comboBoxExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.comboBoxExpiry.FormattingEnabled = true;
            this.comboBoxExpiry.Location = new System.Drawing.Point(27, 258);
            this.comboBoxExpiry.Name = "comboBoxExpiry";
            this.comboBoxExpiry.Size = new System.Drawing.Size(308, 30);
            this.comboBoxExpiry.TabIndex = 11;
            // 
            // CreateShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 576);
            this.Controls.Add(this.panelShipment);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(806, 465);
            this.Name = "CreateShipment";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Формирование отгрузки";
            this.Load += new System.EventHandler(this.CreateShipment_Load);
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
        private System.Windows.Forms.TextBox textBoxUnit;
        private System.Windows.Forms.DataGridView dataGridViewShipment;
        private System.Windows.Forms.Label labelTotalValue;
        private System.Windows.Forms.Label labelTotalCaption;
        private System.Windows.Forms.ComboBox comboBoxRecipient1;
        private System.Windows.Forms.ComboBox comboBoxExpiry;
        private System.Windows.Forms.Label labelExpiry;
    }
}