namespace AutomechanicsProject.Formes
{
    partial class WarehouseHeatmapFilterForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.comboBoxSortBy = new System.Windows.Forms.ComboBox();
            this.labelSortBy = new System.Windows.Forms.Label();
            this.labelmain = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.laberule = new System.Windows.Forms.Label();
            this.labelbluet = new System.Windows.Forms.Label();
            this.labelblue = new System.Windows.Forms.Label();
            this.labelredt = new System.Windows.Forms.Label();
            this.labelred = new System.Windows.Forms.Label();
            this.labeloranget = new System.Windows.Forms.Label();
            this.labelorange = new System.Windows.Forms.Label();
            this.labelyellowt = new System.Windows.Forms.Label();
            this.labelyellow = new System.Windows.Forms.Label();
            this.labelgreent = new System.Windows.Forms.Label();
            this.labelgreen = new System.Windows.Forms.Label();
            this.labellegend = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.dataGridViewWarehouse = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehouse)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Menu;
            this.pnlTop.Controls.Add(this.buttonReset);
            this.pnlTop.Controls.Add(this.buttonApply);
            this.pnlTop.Controls.Add(this.comboBoxSortBy);
            this.pnlTop.Controls.Add(this.labelSortBy);
            this.pnlTop.Controls.Add(this.labelmain);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1974, 130);
            this.pnlTop.TabIndex = 0;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(774, 72);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(200, 48);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Сбросить";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click_1);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(550, 72);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(200, 48);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "Применить";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click_1);
            // 
            // comboBoxSortBy
            // 
            this.comboBoxSortBy.FormattingEnabled = true;
            this.comboBoxSortBy.Location = new System.Drawing.Point(24, 81);
            this.comboBoxSortBy.Name = "comboBoxSortBy";
            this.comboBoxSortBy.Size = new System.Drawing.Size(459, 33);
            this.comboBoxSortBy.TabIndex = 5;
            this.comboBoxSortBy.Enter += new System.EventHandler(this.comboBoxSortBy_Enter);
            this.comboBoxSortBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxSortBy_KeyPress);
            this.comboBoxSortBy.Leave += new System.EventHandler(this.comboBoxSortBy_Leave);
            // 
            // labelSortBy
            // 
            this.labelSortBy.AutoSize = true;
            this.labelSortBy.BackColor = System.Drawing.SystemColors.Control;
            this.labelSortBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSortBy.Location = new System.Drawing.Point(19, 80);
            this.labelSortBy.Name = "labelSortBy";
            this.labelSortBy.Size = new System.Drawing.Size(205, 29);
            this.labelSortBy.TabIndex = 4;
            this.labelSortBy.Text = "Сортировать по:";
            this.labelSortBy.Visible = false;
            // 
            // labelmain
            // 
            this.labelmain.AutoSize = true;
            this.labelmain.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelmain.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelmain.Location = new System.Drawing.Point(0, 0);
            this.labelmain.Name = "labelmain";
            this.labelmain.Size = new System.Drawing.Size(762, 55);
            this.labelmain.TabIndex = 3;
            this.labelmain.Text = "Тепловая карта склада (фильтр)";
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlRight.Controls.Add(this.laberule);
            this.pnlRight.Controls.Add(this.labelbluet);
            this.pnlRight.Controls.Add(this.labelblue);
            this.pnlRight.Controls.Add(this.labelredt);
            this.pnlRight.Controls.Add(this.labelred);
            this.pnlRight.Controls.Add(this.labeloranget);
            this.pnlRight.Controls.Add(this.labelorange);
            this.pnlRight.Controls.Add(this.labelyellowt);
            this.pnlRight.Controls.Add(this.labelyellow);
            this.pnlRight.Controls.Add(this.labelgreent);
            this.pnlRight.Controls.Add(this.labelgreen);
            this.pnlRight.Controls.Add(this.labellegend);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(1374, 130);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(600, 1399);
            this.pnlRight.TabIndex = 2;
            // 
            // laberule
            // 
            this.laberule.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laberule.Location = new System.Drawing.Point(32, 490);
            this.laberule.Name = "laberule";
            this.laberule.Size = new System.Drawing.Size(359, 74);
            this.laberule.TabIndex = 11;
            this.laberule.Text = "   сначала учитывается срок, потом количество";
            // 
            // labelbluet
            // 
            this.labelbluet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelbluet.Location = new System.Drawing.Point(131, 401);
            this.labelbluet.Name = "labelbluet";
            this.labelbluet.Size = new System.Drawing.Size(257, 72);
            this.labelbluet.TabIndex = 10;
            this.labelbluet.Text = "Нет срока годности";
            // 
            // labelblue
            // 
            this.labelblue.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelblue.Location = new System.Drawing.Point(33, 407);
            this.labelblue.Name = "labelblue";
            this.labelblue.Size = new System.Drawing.Size(50, 50);
            this.labelblue.TabIndex = 9;
            // 
            // labelredt
            // 
            this.labelredt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelredt.Location = new System.Drawing.Point(131, 310);
            this.labelredt.Name = "labelredt";
            this.labelredt.Size = new System.Drawing.Size(257, 76);
            this.labelredt.TabIndex = 8;
            this.labelredt.Text = "Количество менее 10 штук";
            // 
            // labelred
            // 
            this.labelred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.labelred.Location = new System.Drawing.Point(33, 316);
            this.labelred.Name = "labelred";
            this.labelred.Size = new System.Drawing.Size(50, 50);
            this.labelred.TabIndex = 7;
            // 
            // labeloranget
            // 
            this.labeloranget.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labeloranget.Location = new System.Drawing.Point(125, 225);
            this.labeloranget.Name = "labeloranget";
            this.labeloranget.Size = new System.Drawing.Size(275, 77);
            this.labeloranget.TabIndex = 6;
            this.labeloranget.Text = "Срок годности менее 7 дней";
            // 
            // labelorange
            // 
            this.labelorange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.labelorange.Location = new System.Drawing.Point(33, 231);
            this.labelorange.Name = "labelorange";
            this.labelorange.Size = new System.Drawing.Size(50, 50);
            this.labelorange.TabIndex = 5;
            // 
            // labelyellowt
            // 
            this.labelyellowt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelyellowt.Location = new System.Drawing.Point(125, 146);
            this.labelyellowt.Name = "labelyellowt";
            this.labelyellowt.Size = new System.Drawing.Size(215, 70);
            this.labelyellowt.TabIndex = 4;
            this.labelyellowt.Text = "Срок годности от 7 до 30 дней";
            // 
            // labelyellow
            // 
            this.labelyellow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelyellow.Location = new System.Drawing.Point(33, 152);
            this.labelyellow.Name = "labelyellow";
            this.labelyellow.Size = new System.Drawing.Size(50, 50);
            this.labelyellow.TabIndex = 3;
            // 
            // labelgreent
            // 
            this.labelgreent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelgreent.Location = new System.Drawing.Point(118, 65);
            this.labelgreent.Name = "labelgreent";
            this.labelgreent.Size = new System.Drawing.Size(222, 73);
            this.labelgreent.TabIndex = 2;
            this.labelgreent.Text = "Срок годности больше 30 дней";
            // 
            // labelgreen
            // 
            this.labelgreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelgreen.Location = new System.Drawing.Point(33, 65);
            this.labelgreen.Name = "labelgreen";
            this.labelgreen.Size = new System.Drawing.Size(50, 50);
            this.labelgreen.TabIndex = 1;
            // 
            // labellegend
            // 
            this.labellegend.AutoSize = true;
            this.labellegend.Dock = System.Windows.Forms.DockStyle.Top;
            this.labellegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labellegend.Location = new System.Drawing.Point(0, 0);
            this.labellegend.Name = "labellegend";
            this.labellegend.Size = new System.Drawing.Size(340, 37);
            this.labellegend.TabIndex = 0;
            this.labellegend.Text = "Условные обозначения";
            // 
            // pnlCenter
            // 
            this.pnlCenter.AutoScroll = true;
            this.pnlCenter.Controls.Add(this.dataGridViewWarehouse);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 130);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(1374, 1399);
            this.pnlCenter.TabIndex = 3;
            // 
            // dataGridViewWarehouse
            // 
            this.dataGridViewWarehouse.AllowUserToAddRows = false;
            this.dataGridViewWarehouse.AllowUserToDeleteRows = false;
            this.dataGridViewWarehouse.AllowUserToResizeColumns = false;
            this.dataGridViewWarehouse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewWarehouse.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewWarehouse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewWarehouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWarehouse.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewWarehouse.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewWarehouse.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewWarehouse.MultiSelect = false;
            this.dataGridViewWarehouse.Name = "dataGridViewWarehouse";
            this.dataGridViewWarehouse.ReadOnly = true;
            this.dataGridViewWarehouse.RowHeadersVisible = false;
            this.dataGridViewWarehouse.RowHeadersWidth = 82;
            this.dataGridViewWarehouse.RowTemplate.Height = 33;
            this.dataGridViewWarehouse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewWarehouse.Size = new System.Drawing.Size(1374, 1399);
            this.dataGridViewWarehouse.TabIndex = 0;
            // 
            // WarehouseHeatmapFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1974, 1529);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlTop);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "WarehouseHeatmapFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тепловая карта склада (фильтр)";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehouse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Label labelmain;
        private System.Windows.Forms.Label labelSortBy;
        private System.Windows.Forms.ComboBox comboBoxSortBy;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelredt;
        private System.Windows.Forms.Label labelred;
        private System.Windows.Forms.Label labeloranget;
        private System.Windows.Forms.Label labelorange;
        private System.Windows.Forms.Label labelyellowt;
        private System.Windows.Forms.Label labelyellow;
        private System.Windows.Forms.Label labelgreent;
        private System.Windows.Forms.Label labelgreen;
        private System.Windows.Forms.Label labellegend;
        private System.Windows.Forms.Label laberule;
        private System.Windows.Forms.Label labelbluet;
        private System.Windows.Forms.Label labelblue;
        private System.Windows.Forms.DataGridView dataGridViewWarehouse;
    }
}