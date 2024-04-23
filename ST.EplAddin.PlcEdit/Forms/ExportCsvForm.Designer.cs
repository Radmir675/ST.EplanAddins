namespace ST.EplAddin.PlcEdit.Forms
{
    partial class ExportCsvForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportCsvForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.symbolicAdressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bitNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pLCAdressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceNameShortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csvFileDataModelViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelDown = new System.Windows.Forms.FlowLayoutPanel();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.export_button = new System.Windows.Forms.Button();
            this.Load_button = new System.Windows.Forms.Button();
            this.replaceSelectedRows_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csvFileDataModelViewBindingSource)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanelDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.symbolicAdressDataGridViewTextBoxColumn,
            this.bitNumberDataGridViewTextBoxColumn,
            this.unitDataGridViewTextBoxColumn,
            this.functionTextDataGridViewTextBoxColumn,
            this.pLCAdressDataGridViewTextBoxColumn,
            this.deviceNameShortDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.csvFileDataModelViewBindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(921, 584);
            this.dataGridView.TabIndex = 0;
            // 
            // symbolicAdressDataGridViewTextBoxColumn
            // 
            this.symbolicAdressDataGridViewTextBoxColumn.DataPropertyName = "SymbolicAdress";
            this.symbolicAdressDataGridViewTextBoxColumn.HeaderText = "SymbolicAdress";
            this.symbolicAdressDataGridViewTextBoxColumn.Name = "symbolicAdressDataGridViewTextBoxColumn";
            this.symbolicAdressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bitNumberDataGridViewTextBoxColumn
            // 
            this.bitNumberDataGridViewTextBoxColumn.DataPropertyName = "BitNumber";
            this.bitNumberDataGridViewTextBoxColumn.HeaderText = "BitNumber";
            this.bitNumberDataGridViewTextBoxColumn.Name = "bitNumberDataGridViewTextBoxColumn";
            this.bitNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
            this.unitDataGridViewTextBoxColumn.HeaderText = "Unit";
            this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            this.unitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // functionTextDataGridViewTextBoxColumn
            // 
            this.functionTextDataGridViewTextBoxColumn.DataPropertyName = "FunctionText";
            this.functionTextDataGridViewTextBoxColumn.HeaderText = "FunctionText";
            this.functionTextDataGridViewTextBoxColumn.Name = "functionTextDataGridViewTextBoxColumn";
            this.functionTextDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pLCAdressDataGridViewTextBoxColumn
            // 
            this.pLCAdressDataGridViewTextBoxColumn.DataPropertyName = "PLCAdress";
            this.pLCAdressDataGridViewTextBoxColumn.HeaderText = "PLCAdress";
            this.pLCAdressDataGridViewTextBoxColumn.Name = "pLCAdressDataGridViewTextBoxColumn";
            this.pLCAdressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deviceNameShortDataGridViewTextBoxColumn
            // 
            this.deviceNameShortDataGridViewTextBoxColumn.DataPropertyName = "DeviceNameShort";
            this.deviceNameShortDataGridViewTextBoxColumn.HeaderText = "DeviceNameShort";
            this.deviceNameShortDataGridViewTextBoxColumn.Name = "deviceNameShortDataGridViewTextBoxColumn";
            this.deviceNameShortDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // csvFileDataModelViewBindingSource
            // 
            this.csvFileDataModelViewBindingSource.DataSource = typeof(ST.EplAddin.PlcEdit.CsvFileDataModelView);
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 6);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(144, 13);
            this.label.TabIndex = 1;
            this.label.Text = "Please select data to replace";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanelDown, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.label, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(927, 650);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // flowLayoutPanelDown
            // 
            this.flowLayoutPanelDown.AutoSize = true;
            this.flowLayoutPanelDown.Controls.Add(this.Cancel_button);
            this.flowLayoutPanelDown.Controls.Add(this.export_button);
            this.flowLayoutPanelDown.Controls.Add(this.Load_button);
            this.flowLayoutPanelDown.Controls.Add(this.replaceSelectedRows_button);
            this.flowLayoutPanelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelDown.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelDown.Location = new System.Drawing.Point(3, 618);
            this.flowLayoutPanelDown.Name = "flowLayoutPanelDown";
            this.flowLayoutPanelDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanelDown.Size = new System.Drawing.Size(921, 29);
            this.flowLayoutPanelDown.TabIndex = 6;
            this.flowLayoutPanelDown.WrapContents = false;
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.Cancel_button.Location = new System.Drawing.Point(843, 3);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 4;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // export_button
            // 
            this.export_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.export_button.Location = new System.Drawing.Point(762, 3);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(75, 23);
            this.export_button.TabIndex = 1;
            this.export_button.Text = "Export";
            this.export_button.UseVisualStyleBackColor = true;
            this.export_button.Click += new System.EventHandler(this.Export_button_Click);
            // 
            // Load_button
            // 
            this.Load_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Load_button.Location = new System.Drawing.Point(661, 3);
            this.Load_button.Name = "Load_button";
            this.Load_button.Size = new System.Drawing.Size(95, 23);
            this.Load_button.TabIndex = 1;
            this.Load_button.Text = "Load template";
            this.Load_button.UseVisualStyleBackColor = true;
            this.Load_button.Click += new System.EventHandler(this.Load_button_Click);
            // 
            // replaceSelectedRows_button
            // 
            this.replaceSelectedRows_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.replaceSelectedRows_button.Location = new System.Drawing.Point(525, 3);
            this.replaceSelectedRows_button.Name = "replaceSelectedRows_button";
            this.replaceSelectedRows_button.Size = new System.Drawing.Size(130, 23);
            this.replaceSelectedRows_button.TabIndex = 1;
            this.replaceSelectedRows_button.Text = "Replace SelectedRows";
            this.replaceSelectedRows_button.UseVisualStyleBackColor = true;
            this.replaceSelectedRows_button.Click += new System.EventHandler(this.ReplaceSelectedRows_button_Click);
            // 
            // ExportCsvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 650);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportCsvForm";
            this.Text = "ExportCsvForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csvFileDataModelViewBindingSource)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanelDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDown;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button export_button;
        private System.Windows.Forms.Button Load_button;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolicAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bitNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pLCAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceNameShortDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource csvFileDataModelViewBindingSource;
        private System.Windows.Forms.Button replaceSelectedRows_button;
    }
}