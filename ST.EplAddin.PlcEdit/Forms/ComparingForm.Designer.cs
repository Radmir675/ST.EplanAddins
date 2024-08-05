namespace ST.EplAddin.PlcEdit.Forms
{
    partial class ComparingForm
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
            this.sourceDataGridView = new System.Windows.Forms.DataGridView();
            this.targetDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ok_button = new System.Windows.Forms.Button();
            this.Upload_doc_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.isCheckedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.symbolicAdressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bitNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pLCAdressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceNameShortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csvFileDataModelViewsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetDataGridView)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.csvFileDataModelViewsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // sourceDataGridView
            // 
            this.sourceDataGridView.AllowUserToAddRows = false;
            this.sourceDataGridView.AllowUserToDeleteRows = false;
            this.sourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceDataGridView.Location = new System.Drawing.Point(3, 3);
            this.sourceDataGridView.Name = "sourceDataGridView";
            this.sourceDataGridView.ReadOnly = true;
            this.sourceDataGridView.Size = new System.Drawing.Size(628, 607);
            this.sourceDataGridView.TabIndex = 0;
            // 
            // targetDataGridView
            // 
            this.targetDataGridView.AllowUserToAddRows = false;
            this.targetDataGridView.AllowUserToDeleteRows = false;
            this.targetDataGridView.AutoGenerateColumns = false;
            this.targetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCheckedDataGridViewCheckBoxColumn,
            this.symbolicAdressDataGridViewTextBoxColumn,
            this.bitNumberDataGridViewTextBoxColumn,
            this.unitDataGridViewTextBoxColumn,
            this.functionTextDataGridViewTextBoxColumn,
            this.pLCAdressDataGridViewTextBoxColumn,
            this.deviceNameShortDataGridViewTextBoxColumn});
            this.targetDataGridView.DataSource = this.csvFileDataModelViewsBindingSource;
            this.targetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetDataGridView.Location = new System.Drawing.Point(637, 3);
            this.targetDataGridView.Name = "targetDataGridView";
            this.targetDataGridView.Size = new System.Drawing.Size(643, 607);
            this.targetDataGridView.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.targetDataGridView, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.sourceDataGridView, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1283, 613);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.comboBox1);
            this.flowLayoutPanel1.Controls.Add(this.ok_button);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(627, 26);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(130, 3);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 2;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // Upload_doc_button
            // 
            this.Upload_doc_button.Location = new System.Drawing.Point(3, 3);
            this.Upload_doc_button.Name = "Upload_doc_button";
            this.Upload_doc_button.Size = new System.Drawing.Size(194, 23);
            this.Upload_doc_button.TabIndex = 0;
            this.Upload_doc_button.Text = "Upload document to compare";
            this.Upload_doc_button.UseVisualStyleBackColor = true;
            this.Upload_doc_button.Click += new System.EventHandler(this.Upload_doc_button_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.41543F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.58457F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 581);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1283, 32);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.Upload_doc_button);
            this.flowLayoutPanel2.Controls.Add(this.checkBox);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(636, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(643, 26);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox.Location = new System.Drawing.Point(203, 3);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(67, 23);
            this.checkBox.TabIndex = 4;
            this.checkBox.Text = "SelectAll";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // isCheckedDataGridViewCheckBoxColumn
            // 
            this.isCheckedDataGridViewCheckBoxColumn.DataPropertyName = "IsChecked";
            this.isCheckedDataGridViewCheckBoxColumn.HeaderText = "IsChecked";
            this.isCheckedDataGridViewCheckBoxColumn.Name = "isCheckedDataGridViewCheckBoxColumn";
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
            // csvFileDataModelViewsBindingSource
            // 
            this.csvFileDataModelViewsBindingSource.DataSource = typeof(ST.EplAddin.PlcEdit.View.CsvFileDataModelViews);
            // 
            // ComparingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 613);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ComparingForm";
            this.Text = "ComparingForm";
            this.Load += new System.EventHandler(this.ComparingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetDataGridView)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.csvFileDataModelViewsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sourceDataGridView;
        private System.Windows.Forms.DataGridView targetDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.BindingSource csvFileDataModelViewsBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolicAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bitNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pLCAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceNameShortDataGridViewTextBoxColumn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button Upload_doc_button;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox;
    }
}