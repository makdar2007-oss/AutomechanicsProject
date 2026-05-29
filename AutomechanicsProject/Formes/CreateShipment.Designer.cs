using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System.Windows.Forms;

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
            this.textBoxINN = new System.Windows.Forms.TextBox();
            this.btncheck = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxcustomer = new System.Windows.Forms.ComboBox();
            this.comboBoxtown = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.labelShipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelShipment.Location = new System.Drawing.Point(49, 30);
            this.labelShipment.Name = "labelShipment";
            this.labelShipment.Size = new System.Drawing.Size(445, 42);
            this.labelShipment.TabIndex = 0;
            this.labelShipment.Text = "Формирование отгрузки";
            // 
            // panelShipment
            // 
            this.panelShipment.BackColor = System.Drawing.SystemColors.Window;
            this.panelShipment.Controls.Add(this.textBoxINN);
            this.panelShipment.Controls.Add(this.btncheck);
            this.panelShipment.Controls.Add(this.label6);
            this.panelShipment.Controls.Add(this.comboBoxcustomer);
            this.panelShipment.Controls.Add(this.comboBoxtown);
            this.panelShipment.Controls.Add(this.label5);
            this.panelShipment.Controls.Add(this.comboBox1);
            this.panelShipment.Controls.Add(this.label4);
            this.panelShipment.Controls.Add(this.label3);
            this.panelShipment.Controls.Add(this.label2);
            this.panelShipment.Controls.Add(this.label1);
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
            this.panelShipment.Name = "panelShipment";
            this.panelShipment.Size = new System.Drawing.Size(1763, 1146);
            this.panelShipment.TabIndex = 1;
            // 
            // textBoxINN
            // 
            this.textBoxINN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxINN.Location = new System.Drawing.Point(21, 275);
            this.textBoxINN.Name = "textBoxINN";
            this.textBoxINN.Size = new System.Drawing.Size(319, 41);
            this.textBoxINN.TabIndex = 23;
            // 
            // btncheck
            // 
            this.btncheck.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btncheck.Location = new System.Drawing.Point(356, 268);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(173, 58);
            this.btncheck.TabIndex = 22;
            this.btncheck.Text = "Проверить";
            this.btncheck.UseVisualStyleBackColor = false;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label6.Location = new System.Drawing.Point(46, 160);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 36);
            this.label6.TabIndex = 21;
            this.label6.Text = "Тип заказчика";
            this.label6.Visible = false;
            // 
            // comboBoxcustomer
            // 
            this.comboBoxcustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxcustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxcustomer.FormattingEnabled = true;
            this.comboBoxcustomer.Location = new System.Drawing.Point(34, 202);
            this.comboBoxcustomer.Name = "comboBoxcustomer";
            this.comboBoxcustomer.Size = new System.Drawing.Size(460, 41);
            this.comboBoxcustomer.TabIndex = 20;
            // 
            // comboBoxtown
            // 
            this.comboBoxtown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxtown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.comboBoxtown.FormattingEnabled = true;
            this.comboBoxtown.Location = new System.Drawing.Point(34, 684);
            this.comboBoxtown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxtown.Name = "comboBoxtown";
            this.comboBoxtown.Size = new System.Drawing.Size(460, 41);
            this.comboBoxtown.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label5.Location = new System.Drawing.Point(40, 643);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(404, 36);
            this.label5.TabIndex = 18;
            this.label5.Text = "Выберите город доставки:";
            this.label5.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(34, 114);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(460, 41);
            this.comboBox1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label4.Location = new System.Drawing.Point(46, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(205, 36);
            this.label4.TabIndex = 15;
            this.label4.Text = "Тип отгрузки";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(40, 534);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(343, 36);
            this.label3.TabIndex = 14;
            this.label3.Text = "Выберите получателя:";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(40, 433);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(426, 36);
            this.label2.TabIndex = 13;
            this.label2.Text = "Введите количество товара:";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(59, 337);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 36);
            this.label1.TabIndex = 12;
            this.label1.Text = "Выберите товар:";
            this.label1.Visible = false;
            // 
            // labelTotalValue
            // 
            this.labelTotalValue.AutoSize = true;
            this.labelTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalValue.Location = new System.Drawing.Point(181, 843);
            this.labelTotalValue.Name = "labelTotalValue";
            this.labelTotalValue.Size = new System.Drawing.Size(0, 37);
            this.labelTotalValue.TabIndex = 9;
            // 
            // labelTotalCaption
            // 
            this.labelTotalCaption.AutoSize = true;
            this.labelTotalCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalCaption.Location = new System.Drawing.Point(27, 843);
            this.labelTotalCaption.Name = "labelTotalCaption";
            this.labelTotalCaption.Size = new System.Drawing.Size(110, 37);
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
            this.dataGridViewShipment.Location = new System.Drawing.Point(550, 30);
            this.dataGridViewShipment.Name = "dataGridViewShipment";
            this.dataGridViewShipment.ReadOnly = true;
            this.dataGridViewShipment.RowHeadersVisible = false;
            this.dataGridViewShipment.RowHeadersWidth = 82;
            this.dataGridViewShipment.RowTemplate.Height = 35;
            this.dataGridViewShipment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewShipment.Size = new System.Drawing.Size(1169, 1066);
            this.dataGridViewShipment.TabIndex = 7;
            this.dataGridViewShipment.DoubleClick += new System.EventHandler(this.DataGridViewShipment_DoubleClick);
            // 
            // buttonShipment
            // 
            this.buttonShipment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonShipment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShipment.ForeColor = System.Drawing.Color.White;
            this.buttonShipment.Location = new System.Drawing.Point(264, 1035);
            this.buttonShipment.Name = "buttonShipment";
            this.buttonShipment.Size = new System.Drawing.Size(242, 70);
            this.buttonShipment.TabIndex = 6;
            this.buttonShipment.Text = global::AutomechanicsProject.Properties.Resources.Shipment_ButtonShipment_Text;
            this.buttonShipment.UseVisualStyleBackColor = false;
            this.buttonShipment.Click += new System.EventHandler(this.ButtonShipment_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.White;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(21, 1035);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(200, 70);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = global::AutomechanicsProject.Properties.Resources.Shipment_ButtonCancel_Text;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(115, 906);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(285, 106);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = global::AutomechanicsProject.Properties.Resources.Shipment_ButtonAdd_Text;
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.Location = new System.Drawing.Point(34, 389);
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(460, 41);
            this.comboBoxProduct.TabIndex = 3;
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUnit.Location = new System.Drawing.Point(34, 472);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(460, 41);
            this.textBoxUnit.TabIndex = 1;
            // 
            // comboBoxRecipient1
            // 
            this.comboBoxRecipient1.BackColor = System.Drawing.SystemColors.Menu;
            this.comboBoxRecipient1.DisplayMember = "Text";
            this.comboBoxRecipient1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecipient1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRecipient1.FormattingEnabled = true;
            this.comboBoxRecipient1.Location = new System.Drawing.Point(34, 573);
            this.comboBoxRecipient1.Name = "comboBoxRecipient1";
            this.comboBoxRecipient1.Size = new System.Drawing.Size(460, 41);
            this.comboBoxRecipient1.TabIndex = 2;
            this.comboBoxRecipient1.ValueMember = "Id";
            // 
            // labelExpiry
            // 
            this.labelExpiry.AutoSize = true;
            this.labelExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelExpiry.Location = new System.Drawing.Point(40, 730);
            this.labelExpiry.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExpiry.Name = "labelExpiry";
            this.labelExpiry.Size = new System.Drawing.Size(238, 36);
            this.labelExpiry.TabIndex = 10;
            this.labelExpiry.Text = "Срок годности:";
            this.labelExpiry.Visible = false;
            // 
            // comboBoxExpiry
            // 
            this.comboBoxExpiry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExpiry.Enabled = false;
            this.comboBoxExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.comboBoxExpiry.FormattingEnabled = true;
            this.comboBoxExpiry.Location = new System.Drawing.Point(34, 771);
            this.comboBoxExpiry.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxExpiry.Name = "comboBoxExpiry";
            this.comboBoxExpiry.Size = new System.Drawing.Size(460, 41);
            this.comboBoxExpiry.TabIndex = 11;
            // 
            // CreateShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1763, 1146);
            this.Controls.Add(this.panelShipment);
            this.MinimumSize = new System.Drawing.Size(1196, 687);
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
        private Label label1;
        private Label label3;
        private Label label2;
        private ComboBox comboBox1;
        private Label label4;
        private Label label5;
        private ComboBox comboBoxcustomer;
        private ComboBox comboBoxtown;
        private TextBox textBoxINN;
        private Button btncheck;
        private Label label6;
    }
}