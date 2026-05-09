using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Formes
{
    partial class StorekeeperForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истине, если управляемый ресурс должен быть удален; иначе ложь.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelStorekeeper = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.buttonCurrency = new System.Windows.Forms.Button();
            this.buttonSupply = new System.Windows.Forms.Button();
            this.buttonShipment = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelData = new System.Windows.Forms.Panel();
            this.dataGridViewStore = new System.Windows.Forms.DataGridView();
            this.menuStripStorekeeper = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBoxStorekeeper = new System.Windows.Forms.ToolStripTextBox();
            this.panelStorekeeper.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStore)).BeginInit();
            this.menuStripStorekeeper.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStorekeeper
            // 
            this.panelStorekeeper.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelStorekeeper.Controls.Add(this.buttonExit);
            this.panelStorekeeper.Controls.Add(this.panelSearch);
            this.panelStorekeeper.Controls.Add(this.panelData);
            this.panelStorekeeper.Controls.Add(this.menuStripStorekeeper);
            this.panelStorekeeper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStorekeeper.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelStorekeeper.Location = new System.Drawing.Point(0, 0);
            this.panelStorekeeper.Name = "panelStorekeeper";
            this.panelStorekeeper.Size = new System.Drawing.Size(1323, 1155);
            this.panelStorekeeper.TabIndex = 0;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(1092, 0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(232, 66);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = global::AutomechanicsProject.Properties.Resources.Storekeeper_ButtonExitText;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.Controls.Add(this.buttonCurrency);
            this.panelSearch.Controls.Add(this.buttonSupply);
            this.panelSearch.Controls.Add(this.buttonShipment);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Location = new System.Drawing.Point(0, 56);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1323, 116);
            this.panelSearch.TabIndex = 6;
            // 
            // buttonCurrency
            // 
            this.buttonCurrency.AutoSize = true;
            this.buttonCurrency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCurrency.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCurrency.FlatAppearance.BorderSize = 0;
            this.buttonCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCurrency.Location = new System.Drawing.Point(362, 38);
            this.buttonCurrency.MinimumSize = new System.Drawing.Size(180, 55);
            this.buttonCurrency.Name = "buttonCurrency";
            this.buttonCurrency.Size = new System.Drawing.Size(229, 55);
            this.buttonCurrency.TabIndex = 5;
            this.buttonCurrency.Text = global::AutomechanicsProject.Properties.Resources.Storekeeper_ButtonCurrencyText;
            this.buttonCurrency.UseVisualStyleBackColor = false;
            this.buttonCurrency.Click += new System.EventHandler(this.ButtonCurrency_Click);
            // 
            // buttonSupply
            // 
            this.buttonSupply.AutoSize = true;
            this.buttonSupply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSupply.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSupply.FlatAppearance.BorderSize = 0;
            this.buttonSupply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSupply.Location = new System.Drawing.Point(640, 38);
            this.buttonSupply.MinimumSize = new System.Drawing.Size(150, 55);
            this.buttonSupply.Name = "buttonSupply";
            this.buttonSupply.Size = new System.Drawing.Size(164, 55);
            this.buttonSupply.TabIndex = 6;
            this.buttonSupply.Text = global::AutomechanicsProject.Properties.Resources.Storekeeper_ButtonSupplyText;
            this.buttonSupply.UseVisualStyleBackColor = false;
            this.buttonSupply.Click += new System.EventHandler(this.buttonSupply_Click_1);
            // 
            // buttonShipment
            // 
            this.buttonShipment.AutoSize = true;
            this.buttonShipment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonShipment.BackColor = System.Drawing.SystemColors.Control;
            this.buttonShipment.FlatAppearance.BorderSize = 0;
            this.buttonShipment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShipment.Location = new System.Drawing.Point(850, 38);
            this.buttonShipment.MinimumSize = new System.Drawing.Size(225, 55);
            this.buttonShipment.Name = "buttonShipment";
            this.buttonShipment.Size = new System.Drawing.Size(306, 55);
            this.buttonShipment.TabIndex = 4;
            this.buttonShipment.Text = global::AutomechanicsProject.Properties.Resources.Storekeeper_ButtonShipmentText;
            this.buttonShipment.UseVisualStyleBackColor = false;
            this.buttonShipment.Click += new System.EventHandler(this.ButtonShipment_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(28, 41);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(298, 44);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.Text = "Поиск:";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
            // 
            // panelData
            // 
            this.panelData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelData.Controls.Add(this.dataGridViewStore);
            this.panelData.Location = new System.Drawing.Point(0, 172);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(1323, 981);
            this.panelData.TabIndex = 5;
            // 
            // dataGridViewStore
            // 
            this.dataGridViewStore.AllowUserToAddRows = false;
            this.dataGridViewStore.AllowUserToDeleteRows = false;
            this.dataGridViewStore.AllowUserToResizeRows = false;
            this.dataGridViewStore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStore.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStore.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridViewStore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewStore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStore.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewStore.MultiSelect = false;
            this.dataGridViewStore.Name = "dataGridViewStore";
            this.dataGridViewStore.ReadOnly = true;
            this.dataGridViewStore.RowHeadersVisible = false;
            this.dataGridViewStore.RowHeadersWidth = 82;
            this.dataGridViewStore.RowTemplate.Height = 35;
            this.dataGridViewStore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStore.Size = new System.Drawing.Size(1323, 981);
            this.dataGridViewStore.TabIndex = 1;
            this.dataGridViewStore.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridViewStore_DataBindingComplete);
            // 
            // menuStripStorekeeper
            // 
            this.menuStripStorekeeper.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStripStorekeeper.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStripStorekeeper.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripStorekeeper.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripStorekeeper.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxStorekeeper});
            this.menuStripStorekeeper.Location = new System.Drawing.Point(0, 0);
            this.menuStripStorekeeper.Name = "menuStripStorekeeper";
            this.menuStripStorekeeper.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStripStorekeeper.Size = new System.Drawing.Size(1323, 48);
            this.menuStripStorekeeper.TabIndex = 1;
            // 
            // toolStripTextBoxStorekeeper
            // 
            this.toolStripTextBoxStorekeeper.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxStorekeeper.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBoxStorekeeper.Name = "toolStripTextBoxStorekeeper";
            this.toolStripTextBoxStorekeeper.ReadOnly = true;
            this.toolStripTextBoxStorekeeper.Size = new System.Drawing.Size(500, 44);
            this.toolStripTextBoxStorekeeper.Text = "Кладовщик";
            // 
            // StorekeeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 1155);
            this.Controls.Add(this.panelStorekeeper);
            this.MainMenuStrip = this.menuStripStorekeeper;
            this.MinimumSize = new System.Drawing.Size(1337, 585);
            this.Name = "StorekeeperForm";
            this.Text = "Кладовщик - Управление складом";
            this.panelStorekeeper.ResumeLayout(false);
            this.panelStorekeeper.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStore)).EndInit();
            this.menuStripStorekeeper.ResumeLayout(false);
            this.menuStripStorekeeper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStorekeeper;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button buttonShipment;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.DataGridView dataGridViewStore;
        private System.Windows.Forms.MenuStrip menuStripStorekeeper;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxStorekeeper;
        private System.Windows.Forms.Button buttonCurrency;   
        private System.Windows.Forms.Button buttonSupply;
    }
}