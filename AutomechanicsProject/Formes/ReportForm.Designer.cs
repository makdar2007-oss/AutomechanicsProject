namespace AutomechanicsProject.Formes
{
    partial class ReportForm
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelTable = new System.Windows.Forms.Panel();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.panelDateFilter = new System.Windows.Forms.TableLayoutPanel();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.panelSummary = new System.Windows.Forms.TableLayoutPanel();
            this.labelTotalAmountValue = new System.Windows.Forms.Label();
            this.labelProfitCaption = new System.Windows.Forms.Label();
            this.labelProfitValue = new System.Windows.Forms.Label();
            this.labelTotalAmountCaption = new System.Windows.Forms.Label();
            this.panelExport = new System.Windows.Forms.Panel();
            this.buttonExport = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelDateFilter.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelTitle, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelTable, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panelBottom, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(984, 611);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTitle.Location = new System.Drawing.Point(10, 10);
            this.panelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(964, 60);
            this.panelTitle.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(964, 60);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Отчет";
            // 
            // panelTable
            // 
            this.panelTable.Controls.Add(this.dataGridViewReport);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(10, 70);
            this.panelTable.Margin = new System.Windows.Forms.Padding(0);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(964, 451);
            this.panelTable.TabIndex = 1;
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReport.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewReport.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewReport.MultiSelect = false;
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.RowHeadersVisible = false;
            this.dataGridViewReport.RowHeadersWidth = 82;
            this.dataGridViewReport.RowTemplate.Height = 33;
            this.dataGridViewReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReport.Size = new System.Drawing.Size(964, 451);
            this.dataGridViewReport.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.ColumnCount = 3;
            this.panelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.97694F));
            this.panelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.25157F));
            this.panelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panelBottom.Controls.Add(this.panelDateFilter, 0, 0);
            this.panelBottom.Controls.Add(this.panelSummary, 1, 0);
            this.panelBottom.Controls.Add(this.panelExport, 2, 0);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(10, 521);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(5);
            this.panelBottom.RowCount = 1;
            this.panelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelBottom.Size = new System.Drawing.Size(964, 80);
            this.panelBottom.TabIndex = 2;
            // 
            // panelDateFilter
            // 
            this.panelDateFilter.ColumnCount = 5;
            this.panelDateFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDateFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDateFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDateFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDateFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelDateFilter.Controls.Add(this.labelPeriod, 0, 0);
            this.panelDateFilter.Controls.Add(this.labelFrom, 1, 0);
            this.panelDateFilter.Controls.Add(this.dateTimePickerFrom, 2, 0);
            this.panelDateFilter.Controls.Add(this.labelTo, 3, 0);
            this.panelDateFilter.Controls.Add(this.dateTimePickerTo, 4, 0);
            this.panelDateFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDateFilter.Location = new System.Drawing.Point(5, 5);
            this.panelDateFilter.Margin = new System.Windows.Forms.Padding(0);
            this.panelDateFilter.Name = "panelDateFilter";
            this.panelDateFilter.RowCount = 1;
            this.panelDateFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelDateFilter.Size = new System.Drawing.Size(410, 70);
            this.panelDateFilter.TabIndex = 0;
            // 
            // labelPeriod
            // 
            this.labelPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelPeriod.Location = new System.Drawing.Point(3, 15);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(101, 40);
            this.labelPeriod.TabIndex = 0;
            this.labelPeriod.Text = "Выберите\n период:";
            // 
            // labelFrom
            // 
            this.labelFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrom.AutoSize = true;
            this.labelFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelFrom.Location = new System.Drawing.Point(110, 26);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(16, 18);
            this.labelFrom.TabIndex = 1;
            this.labelFrom.Text = "с";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(132, 23);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(114, 24);
            this.dateTimePickerFrom.TabIndex = 2;
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.dateTimePickerFrom_ValueChanged);
            // 
            // labelTo
            // 
            this.labelTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelTo.Location = new System.Drawing.Point(252, 26);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(25, 18);
            this.labelTo.TabIndex = 3;
            this.labelTo.Text = "по";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(283, 23);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(124, 24);
            this.dateTimePickerTo.TabIndex = 4;
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.dateTimePickerTo_ValueChanged);
            // 
            // panelSummary
            // 
            this.panelSummary.ColumnCount = 2;
            this.panelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSummary.Controls.Add(this.labelTotalAmountValue, 1, 0);
            this.panelSummary.Controls.Add(this.labelProfitCaption, 0, 1);
            this.panelSummary.Controls.Add(this.labelProfitValue, 1, 1);
            this.panelSummary.Controls.Add(this.labelTotalAmountCaption, 0, 0);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummary.Location = new System.Drawing.Point(415, 5);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.RowCount = 2;
            this.panelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSummary.Size = new System.Drawing.Size(384, 70);
            this.panelSummary.TabIndex = 1;
            // 
            // labelTotalAmountValue
            // 
            this.labelTotalAmountValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTotalAmountValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelTotalAmountValue.ForeColor = System.Drawing.Color.Black;
            this.labelTotalAmountValue.Location = new System.Drawing.Point(195, 7);
            this.labelTotalAmountValue.Name = "labelTotalAmountValue";
            this.labelTotalAmountValue.Size = new System.Drawing.Size(108, 20);
            this.labelTotalAmountValue.TabIndex = 1;
            this.labelTotalAmountValue.Text = "0.00 руб.";
            this.labelTotalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProfitCaption
            // 
            this.labelProfitCaption.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelProfitCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelProfitCaption.Location = new System.Drawing.Point(3, 42);
            this.labelProfitCaption.Name = "labelProfitCaption";
            this.labelProfitCaption.Size = new System.Drawing.Size(186, 20);
            this.labelProfitCaption.TabIndex = 2;
            this.labelProfitCaption.Text = "Прибыль(RUB):";
            this.labelProfitCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProfitValue
            // 
            this.labelProfitValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProfitValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelProfitValue.ForeColor = System.Drawing.Color.Black;
            this.labelProfitValue.Location = new System.Drawing.Point(195, 42);
            this.labelProfitValue.Name = "labelProfitValue";
            this.labelProfitValue.Size = new System.Drawing.Size(108, 20);
            this.labelProfitValue.TabIndex = 3;
            this.labelProfitValue.Text = "0.00 руб.";
            this.labelProfitValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTotalAmountCaption
            // 
            this.labelTotalAmountCaption.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTotalAmountCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelTotalAmountCaption.Location = new System.Drawing.Point(17, 7);
            this.labelTotalAmountCaption.Name = "labelTotalAmountCaption";
            this.labelTotalAmountCaption.Size = new System.Drawing.Size(172, 20);
            this.labelTotalAmountCaption.TabIndex = 0;
            this.labelTotalAmountCaption.Text = "Сумма(RUB):                                                ";
            this.labelTotalAmountCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExport
            // 
            this.panelExport.Controls.Add(this.buttonExport);
            this.panelExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExport.Location = new System.Drawing.Point(799, 5);
            this.panelExport.Margin = new System.Windows.Forms.Padding(0);
            this.panelExport.Name = "panelExport";
            this.panelExport.Size = new System.Drawing.Size(160, 70);
            this.panelExport.TabIndex = 2;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonExport.BackColor = System.Drawing.Color.White;
            this.buttonExport.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.buttonExport.ForeColor = System.Drawing.Color.Black;
            this.buttonExport.Location = new System.Drawing.Point(18, 1);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(126, 67);
            this.buttonExport.TabIndex = 0;
            this.buttonExport.Text = "Экспорт\n файла";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Отчет";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelDateFilter.ResumeLayout(false);
            this.panelDateFilter.PerformLayout();
            this.panelSummary.ResumeLayout(false);
            this.panelExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }
#endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.TableLayoutPanel panelBottom;
        private System.Windows.Forms.TableLayoutPanel panelDateFilter;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.TableLayoutPanel panelSummary;
        private System.Windows.Forms.Label labelTotalAmountCaption;
        private System.Windows.Forms.Label labelTotalAmountValue;
        private System.Windows.Forms.Label labelProfitCaption;
        private System.Windows.Forms.Label labelProfitValue;
        private System.Windows.Forms.Panel panelExport;
        private System.Windows.Forms.Button buttonExport;
    }
}