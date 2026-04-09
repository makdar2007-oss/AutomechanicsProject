namespace AutomechanicsProject.Formes
{
    partial class ShipmentHistoryForm
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
            this.panelHistory = new System.Windows.Forms.Panel();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.textBoxHistory = new System.Windows.Forms.TextBox();
            this.panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.tableLayoutPanelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHistory
            // 
            this.panelHistory.BackColor = System.Drawing.SystemColors.Window;
            this.panelHistory.Controls.Add(this.dataGridViewHistory);
            this.panelHistory.Controls.Add(this.tableLayoutPanelTop);
            this.panelHistory.Controls.Add(this.textBoxHistory);
            this.panelHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelHistory.Location = new System.Drawing.Point(0, 0);
            this.panelHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Size = new System.Drawing.Size(836, 547);
            this.panelHistory.TabIndex = 0;
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.AllowUserToDeleteRows = false;
            this.dataGridViewHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHistory.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewHistory.Location = new System.Drawing.Point(0, 100);
            this.dataGridViewHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewHistory.MultiSelect = false;
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.ReadOnly = true;
            this.dataGridViewHistory.RowHeadersVisible = false;
            this.dataGridViewHistory.RowHeadersWidth = 82;
            this.dataGridViewHistory.RowTemplate.Height = 33;
            this.dataGridViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistory.Size = new System.Drawing.Size(836, 447);
            this.dataGridViewHistory.TabIndex = 1;
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.AutoSize = true;
            this.tableLayoutPanelTop.ColumnCount = 7;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Controls.Add(this.labelPeriod, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.dateTimePickerFrom, 1, 0);
            this.tableLayoutPanelTop.Controls.Add(this.labelTo, 2, 0);
            this.tableLayoutPanelTop.Controls.Add(this.dateTimePickerTo, 3, 0);
            this.tableLayoutPanelTop.Controls.Add(this.buttonApplyFilter, 4, 0);
            this.tableLayoutPanelTop.Controls.Add(this.buttonExport, 5, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(0, 38);
            this.tableLayoutPanelTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.Padding = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(836, 62);
            this.tableLayoutPanelTop.TabIndex = 2;
            // 
            // labelPeriod
            // 
            this.labelPeriod.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPeriod.Location = new System.Drawing.Point(44, 21);
            this.labelPeriod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(18, 20);
            this.labelPeriod.TabIndex = 0;
            this.labelPeriod.Text = "с";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(66, 18);
            this.dateTimePickerFrom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(111, 26);
            this.dateTimePickerFrom.TabIndex = 1;
            // 
            // labelTo
            // 
            this.labelTo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTo.Location = new System.Drawing.Point(188, 21);
            this.labelTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(29, 20);
            this.labelTo.TabIndex = 2;
            this.labelTo.Text = "по";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(221, 18);
            this.dateTimePickerTo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(121, 26);
            this.dateTimePickerTo.TabIndex = 3;
            // 
            // buttonApplyFilter
            // 
            this.buttonApplyFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonApplyFilter.BackColor = System.Drawing.Color.White;
            this.buttonApplyFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonApplyFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonApplyFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.buttonApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApplyFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonApplyFilter.ForeColor = System.Drawing.Color.Black;
            this.buttonApplyFilter.Location = new System.Drawing.Point(346, 7);
            this.buttonApplyFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(135, 48);
            this.buttonApplyFilter.TabIndex = 4;
            this.buttonApplyFilter.Text = "Применить";
            this.buttonApplyFilter.UseVisualStyleBackColor = false;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonExport.BackColor = System.Drawing.Color.White;
            this.buttonExport.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExport.ForeColor = System.Drawing.Color.Black;
            this.buttonExport.Location = new System.Drawing.Point(489, 5);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(170, 52);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "Экспорт файла";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // textBoxHistory
            // 
            this.textBoxHistory.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxHistory.Location = new System.Drawing.Point(0, 0);
            this.textBoxHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxHistory.Name = "textBoxHistory";
            this.textBoxHistory.ReadOnly = true;
            this.textBoxHistory.Size = new System.Drawing.Size(836, 38);
            this.textBoxHistory.TabIndex = 0;
            this.textBoxHistory.Text = "История отгрузок";
            this.textBoxHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ShipmentHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 547);
            this.Controls.Add(this.panelHistory);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(539, 401);
            this.Name = "ShipmentHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "История отгрузок";
            this.Load += new System.EventHandler(this.ShipmentHistoryForm_Load);
            this.panelHistory.ResumeLayout(false);
            this.panelHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion 

        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.TextBox textBoxHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button buttonApplyFilter;
        private System.Windows.Forms.Button buttonExport;
    }
}