namespace ST.EplAddin.PlcEdit
{
    partial class ManagePlcForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePlcForm));
            this.flowLayoutPanelUp = new System.Windows.Forms.FlowLayoutPanel();
            this.exchange_button = new System.Windows.Forms.Button();
            this.dowm_button = new System.Windows.Forms.Button();
            this.up_button = new System.Windows.Forms.Button();
            this.Ok_button = new System.Windows.Forms.Button();
            this.Apply_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.flowLayoutPanelDown = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.devicePointDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pLCAdressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datatypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.symbolicAdressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.devicePointDesignationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionDefinitionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.symbolicAdressDefinedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plcDataModelViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanelUp.SuspendLayout();
            this.flowLayoutPanelDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelUp
            // 
            this.flowLayoutPanelUp.AutoSize = true;
            this.flowLayoutPanelUp.Controls.Add(this.exchange_button);
            this.flowLayoutPanelUp.Controls.Add(this.dowm_button);
            this.flowLayoutPanelUp.Controls.Add(this.up_button);
            this.flowLayoutPanelUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanelUp.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelUp.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelUp.Name = "flowLayoutPanelUp";
            this.flowLayoutPanelUp.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flowLayoutPanelUp.Size = new System.Drawing.Size(991, 31);
            this.flowLayoutPanelUp.TabIndex = 3;
            this.flowLayoutPanelUp.WrapContents = false;
            // 
            // exchange_button
            // 
            this.exchange_button.BackColor = System.Drawing.Color.Transparent;
            this.exchange_button.BackgroundImage = global::ST.EplAddin.PlcEdit.Properties.Resources.reverse;
            this.exchange_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exchange_button.Location = new System.Drawing.Point(955, 3);
            this.exchange_button.Name = "exchange_button";
            this.exchange_button.Size = new System.Drawing.Size(25, 25);
            this.exchange_button.TabIndex = 2;
            this.exchange_button.TabStop = false;
            this.exchange_button.UseVisualStyleBackColor = false;
            this.exchange_button.Click += new System.EventHandler(this.exchange_button_Click);
            // 
            // dowm_button
            // 
            this.dowm_button.BackColor = System.Drawing.Color.Transparent;
            this.dowm_button.BackgroundImage = global::ST.EplAddin.PlcEdit.Properties.Resources.down;
            this.dowm_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dowm_button.Location = new System.Drawing.Point(924, 3);
            this.dowm_button.Name = "dowm_button";
            this.dowm_button.Size = new System.Drawing.Size(25, 25);
            this.dowm_button.TabIndex = 0;
            this.dowm_button.TabStop = false;
            this.dowm_button.UseVisualStyleBackColor = false;
            this.dowm_button.Click += new System.EventHandler(this.dowm_button_Click);
            // 
            // up_button
            // 
            this.up_button.BackColor = System.Drawing.Color.Transparent;
            this.up_button.BackgroundImage = global::ST.EplAddin.PlcEdit.Properties.Resources.up;
            this.up_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.up_button.ForeColor = System.Drawing.Color.Black;
            this.up_button.Location = new System.Drawing.Point(893, 3);
            this.up_button.Name = "up_button";
            this.up_button.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.up_button.Size = new System.Drawing.Size(25, 25);
            this.up_button.TabIndex = 1;
            this.up_button.TabStop = false;
            this.up_button.UseVisualStyleBackColor = false;
            this.up_button.Click += new System.EventHandler(this.up_button_Click);
            // 
            // Ok_button
            // 
            this.Ok_button.Location = new System.Drawing.Point(751, 3);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(75, 23);
            this.Ok_button.TabIndex = 4;
            this.Ok_button.Text = "Ok";
            this.Ok_button.UseVisualStyleBackColor = true;
            this.Ok_button.Click += new System.EventHandler(this.Ok_button_Click);
            // 
            // Apply_button
            // 
            this.Apply_button.Location = new System.Drawing.Point(913, 3);
            this.Apply_button.Name = "Apply_button";
            this.Apply_button.Size = new System.Drawing.Size(75, 23);
            this.Apply_button.TabIndex = 4;
            this.Apply_button.Text = "Apply";
            this.Apply_button.UseVisualStyleBackColor = true;
            this.Apply_button.Click += new System.EventHandler(this.Apply_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(832, 3);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 4;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // flowLayoutPanelDown
            // 
            this.flowLayoutPanelDown.AutoSize = true;
            this.flowLayoutPanelDown.Controls.Add(this.Apply_button);
            this.flowLayoutPanelDown.Controls.Add(this.Cancel_button);
            this.flowLayoutPanelDown.Controls.Add(this.Ok_button);
            this.flowLayoutPanelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelDown.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelDown.Location = new System.Drawing.Point(0, 562);
            this.flowLayoutPanelDown.Name = "flowLayoutPanelDown";
            this.flowLayoutPanelDown.Size = new System.Drawing.Size(991, 29);
            this.flowLayoutPanelDown.TabIndex = 5;
            this.flowLayoutPanelDown.WrapContents = false;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.devicePointDescriptionDataGridViewTextBoxColumn,
            this.pLCAdressDataGridViewTextBoxColumn,
            this.datatypeDataGridViewTextBoxColumn,
            this.symbolicAdressDataGridViewTextBoxColumn,
            this.functionTextDataGridViewTextBoxColumn,
            this.dTDataGridViewTextBoxColumn,
            this.devicePointDesignationDataGridViewTextBoxColumn,
            this.functionDefinitionDataGridViewTextBoxColumn,
            this.symbolicAdressDefinedDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.plcDataModelViewBindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 31);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.RowHeadersWidth = 30;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowTemplate.Height = 20;
            this.dataGridView.Size = new System.Drawing.Size(991, 531);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // devicePointDescriptionDataGridViewTextBoxColumn
            // 
            this.devicePointDescriptionDataGridViewTextBoxColumn.DataPropertyName = "DevicePointDescription";
            this.devicePointDescriptionDataGridViewTextBoxColumn.HeaderText = "DevicePointDescription";
            this.devicePointDescriptionDataGridViewTextBoxColumn.Name = "devicePointDescriptionDataGridViewTextBoxColumn";
            this.devicePointDescriptionDataGridViewTextBoxColumn.Width = 143;
            // 
            // pLCAdressDataGridViewTextBoxColumn
            // 
            this.pLCAdressDataGridViewTextBoxColumn.DataPropertyName = "PLCAdress";
            this.pLCAdressDataGridViewTextBoxColumn.HeaderText = "PLCAdress";
            this.pLCAdressDataGridViewTextBoxColumn.Name = "pLCAdressDataGridViewTextBoxColumn";
            this.pLCAdressDataGridViewTextBoxColumn.Width = 84;
            // 
            // datatypeDataGridViewTextBoxColumn
            // 
            this.datatypeDataGridViewTextBoxColumn.DataPropertyName = "Datatype";
            this.datatypeDataGridViewTextBoxColumn.HeaderText = "Datatype";
            this.datatypeDataGridViewTextBoxColumn.Name = "datatypeDataGridViewTextBoxColumn";
            this.datatypeDataGridViewTextBoxColumn.Width = 75;
            // 
            // symbolicAdressDataGridViewTextBoxColumn
            // 
            this.symbolicAdressDataGridViewTextBoxColumn.DataPropertyName = "SymbolicAdress";
            this.symbolicAdressDataGridViewTextBoxColumn.HeaderText = "SymbolicAdress";
            this.symbolicAdressDataGridViewTextBoxColumn.Name = "symbolicAdressDataGridViewTextBoxColumn";
            this.symbolicAdressDataGridViewTextBoxColumn.Width = 106;
            // 
            // functionTextDataGridViewTextBoxColumn
            // 
            this.functionTextDataGridViewTextBoxColumn.DataPropertyName = "FunctionText";
            this.functionTextDataGridViewTextBoxColumn.HeaderText = "FunctionText";
            this.functionTextDataGridViewTextBoxColumn.Name = "functionTextDataGridViewTextBoxColumn";
            this.functionTextDataGridViewTextBoxColumn.Width = 94;
            // 
            // dTDataGridViewTextBoxColumn
            // 
            this.dTDataGridViewTextBoxColumn.DataPropertyName = "DT";
            this.dTDataGridViewTextBoxColumn.HeaderText = "DT";
            this.dTDataGridViewTextBoxColumn.Name = "dTDataGridViewTextBoxColumn";
            this.dTDataGridViewTextBoxColumn.Width = 47;
            // 
            // devicePointDesignationDataGridViewTextBoxColumn
            // 
            this.devicePointDesignationDataGridViewTextBoxColumn.DataPropertyName = "DevicePointDesignation";
            this.devicePointDesignationDataGridViewTextBoxColumn.HeaderText = "DevicePointDesignation";
            this.devicePointDesignationDataGridViewTextBoxColumn.Name = "devicePointDesignationDataGridViewTextBoxColumn";
            this.devicePointDesignationDataGridViewTextBoxColumn.Width = 146;
            // 
            // functionDefinitionDataGridViewTextBoxColumn
            // 
            this.functionDefinitionDataGridViewTextBoxColumn.DataPropertyName = "FunctionDefinition";
            this.functionDefinitionDataGridViewTextBoxColumn.HeaderText = "FunctionDefinition";
            this.functionDefinitionDataGridViewTextBoxColumn.Name = "functionDefinitionDataGridViewTextBoxColumn";
            this.functionDefinitionDataGridViewTextBoxColumn.Width = 117;
            // 
            // symbolicAdressDefinedDataGridViewTextBoxColumn
            // 
            this.symbolicAdressDefinedDataGridViewTextBoxColumn.DataPropertyName = "SymbolicAdressDefined";
            this.symbolicAdressDefinedDataGridViewTextBoxColumn.HeaderText = "SymbolicAdressDefined";
            this.symbolicAdressDefinedDataGridViewTextBoxColumn.Name = "symbolicAdressDefinedDataGridViewTextBoxColumn";
            this.symbolicAdressDefinedDataGridViewTextBoxColumn.Width = 143;
            // 
            // plcDataModelViewBindingSource
            // 
            this.plcDataModelViewBindingSource.DataSource = typeof(ST.EplAddin.PlcEdit.PlcDataModelView);
            // 
            // ManagePlcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(991, 591);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.flowLayoutPanelDown);
            this.Controls.Add(this.flowLayoutPanelUp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagePlcForm";
            this.Text = "ManagePlcForm";
            this.Load += new System.EventHandler(this.ManagePlcForm_Load);
            this.flowLayoutPanelUp.ResumeLayout(false);
            this.flowLayoutPanelDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dowm_button;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.Button exchange_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelUp;
        private System.Windows.Forms.Button Ok_button;
        private System.Windows.Forms.Button Apply_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDown;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn devicePointDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pLCAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datatypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolicAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn devicePointDesignationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionDefinitionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolicAdressDefinedDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource plcDataModelViewBindingSource;
    }
}