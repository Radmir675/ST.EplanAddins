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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePlcForm));
            this.flowLayoutPanelUp = new System.Windows.Forms.FlowLayoutPanel();
            this.lower_button = new System.Windows.Forms.Button();
            this.upper_button = new System.Windows.Forms.Button();
            this.exchange_button = new System.Windows.Forms.Button();
            this.dowm_button = new System.Windows.Forms.Button();
            this.up_button = new System.Windows.Forms.Button();
            this.properties_button = new System.Windows.Forms.Button();
            this.FastInput = new System.Windows.Forms.CheckBox();
            this.Ok_button = new System.Windows.Forms.Button();
            this.Apply_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.flowLayoutPanelDown = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.plcDataModelViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.loadTemplate_button = new System.Windows.Forms.Button();
            this.dropDownList = new System.Windows.Forms.ComboBox();
            this.export_button = new System.Windows.Forms.Button();
            this.import_button = new System.Windows.Forms.Button();
            this.StatusImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.PLCAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datatype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolicAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevicePointDesignation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionDefinition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolicAdressDefined = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanelUp.SuspendLayout();
            this.flowLayoutPanelDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelUp
            // 
            this.flowLayoutPanelUp.AutoSize = true;
            this.flowLayoutPanelUp.Controls.Add(this.lower_button);
            this.flowLayoutPanelUp.Controls.Add(this.upper_button);
            this.flowLayoutPanelUp.Controls.Add(this.exchange_button);
            this.flowLayoutPanelUp.Controls.Add(this.dowm_button);
            this.flowLayoutPanelUp.Controls.Add(this.up_button);
            this.flowLayoutPanelUp.Controls.Add(this.properties_button);
            this.flowLayoutPanelUp.Controls.Add(this.FastInput);
            this.flowLayoutPanelUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanelUp.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelUp.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelUp.Name = "flowLayoutPanelUp";
            this.flowLayoutPanelUp.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flowLayoutPanelUp.Size = new System.Drawing.Size(1107, 29);
            this.flowLayoutPanelUp.TabIndex = 3;
            this.flowLayoutPanelUp.WrapContents = false;
            // 
            // lower_button
            // 
            this.lower_button.BackColor = System.Drawing.Color.Transparent;
            this.lower_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lower_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.Down1;
            this.lower_button.Location = new System.Drawing.Point(1073, 3);
            this.lower_button.Name = "lower_button";
            this.lower_button.Size = new System.Drawing.Size(23, 23);
            this.lower_button.TabIndex = 2;
            this.lower_button.TabStop = false;
            this.lower_button.UseVisualStyleBackColor = false;
            this.lower_button.Click += new System.EventHandler(this.lower_button_Click);
            // 
            // upper_button
            // 
            this.upper_button.BackColor = System.Drawing.Color.Transparent;
            this.upper_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.upper_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.Upper;
            this.upper_button.Location = new System.Drawing.Point(1044, 3);
            this.upper_button.Name = "upper_button";
            this.upper_button.Size = new System.Drawing.Size(23, 23);
            this.upper_button.TabIndex = 2;
            this.upper_button.TabStop = false;
            this.upper_button.UseVisualStyleBackColor = false;
            this.upper_button.Click += new System.EventHandler(this.upper_button_Click);
            // 
            // exchange_button
            // 
            this.exchange_button.BackColor = System.Drawing.Color.Transparent;
            this.exchange_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.exchange_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.downup;
            this.exchange_button.Location = new System.Drawing.Point(1015, 3);
            this.exchange_button.Name = "exchange_button";
            this.exchange_button.Size = new System.Drawing.Size(23, 23);
            this.exchange_button.TabIndex = 2;
            this.exchange_button.TabStop = false;
            this.exchange_button.UseVisualStyleBackColor = false;
            this.exchange_button.Click += new System.EventHandler(this.exchange_button_Click);
            // 
            // dowm_button
            // 
            this.dowm_button.BackColor = System.Drawing.Color.Transparent;
            this.dowm_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dowm_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.arrowDown1;
            this.dowm_button.Location = new System.Drawing.Point(986, 3);
            this.dowm_button.Name = "dowm_button";
            this.dowm_button.Size = new System.Drawing.Size(23, 23);
            this.dowm_button.TabIndex = 0;
            this.dowm_button.TabStop = false;
            this.dowm_button.UseVisualStyleBackColor = false;
            this.dowm_button.Click += new System.EventHandler(this.dowm_button_Click);
            // 
            // up_button
            // 
            this.up_button.BackColor = System.Drawing.Color.Transparent;
            this.up_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.up_button.ForeColor = System.Drawing.Color.Transparent;
            this.up_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.arrowUp;
            this.up_button.Location = new System.Drawing.Point(957, 3);
            this.up_button.Name = "up_button";
            this.up_button.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.up_button.Size = new System.Drawing.Size(23, 23);
            this.up_button.TabIndex = 1;
            this.up_button.TabStop = false;
            this.up_button.UseVisualStyleBackColor = false;
            this.up_button.Click += new System.EventHandler(this.up_button_Click);
            // 
            // properties_button
            // 
            this.properties_button.BackColor = System.Drawing.Color.Transparent;
            this.properties_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.properties_button.ForeColor = System.Drawing.Color.Transparent;
            this.properties_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.image_psd;
            this.properties_button.Location = new System.Drawing.Point(928, 3);
            this.properties_button.Name = "properties_button";
            this.properties_button.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.properties_button.Size = new System.Drawing.Size(23, 23);
            this.properties_button.TabIndex = 1;
            this.properties_button.TabStop = false;
            this.properties_button.UseVisualStyleBackColor = false;
            this.properties_button.Click += new System.EventHandler(this.properties_button_Click);
            // 
            // FastInput
            // 
            this.FastInput.AutoSize = true;
            this.FastInput.Dock = System.Windows.Forms.DockStyle.Right;
            this.FastInput.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FastInput.Location = new System.Drawing.Point(842, 3);
            this.FastInput.Name = "FastInput";
            this.FastInput.Size = new System.Drawing.Size(80, 23);
            this.FastInput.TabIndex = 3;
            this.FastInput.Text = "Fast input";
            this.FastInput.UseVisualStyleBackColor = true;
            // 
            // Ok_button
            // 
            this.Ok_button.Location = new System.Drawing.Point(3, 3);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(75, 23);
            this.Ok_button.TabIndex = 1;
            this.Ok_button.Text = "Ok";
            this.Ok_button.UseVisualStyleBackColor = true;
            this.Ok_button.Click += new System.EventHandler(this.Ok_button_Click);
            // 
            // Apply_button
            // 
            this.Apply_button.Location = new System.Drawing.Point(165, 3);
            this.Apply_button.Name = "Apply_button";
            this.Apply_button.Size = new System.Drawing.Size(75, 23);
            this.Apply_button.TabIndex = 4;
            this.Apply_button.Text = "Apply";
            this.Apply_button.UseVisualStyleBackColor = true;
            this.Apply_button.Click += new System.EventHandler(this.Apply_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(84, 3);
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
            this.flowLayoutPanelDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanelDown.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelDown.Location = new System.Drawing.Point(861, 3);
            this.flowLayoutPanelDown.Name = "flowLayoutPanelDown";
            this.flowLayoutPanelDown.Size = new System.Drawing.Size(243, 29);
            this.flowLayoutPanelDown.TabIndex = 5;
            this.flowLayoutPanelDown.WrapContents = false;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StatusImage,
            this.PLCAdress,
            this.DeviceDescription,
            this.Datatype,
            this.SymbolicAdress,
            this.FunctionText,
            this.DT,
            this.DevicePointDesignation,
            this.FunctionDefinition,
            this.SymbolicAdressDefined,
            this.FunctionType});
            this.dataGridView.DataSource = this.plcDataModelViewBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 29);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.RowHeadersWidth = 17;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowTemplate.Height = 17;
            this.dataGridView.Size = new System.Drawing.Size(1107, 701);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            this.dataGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseUp);
            // 
            // plcDataModelViewBindingSource
            // 
            this.plcDataModelViewBindingSource.DataSource = typeof(ST.EplAddin.PlcEdit.PlcDataModelView);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanelDown, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 730);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1107, 35);
            this.tableLayoutPanel.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.loadTemplate_button);
            this.flowLayoutPanel1.Controls.Add(this.dropDownList);
            this.flowLayoutPanel1.Controls.Add(this.export_button);
            this.flowLayoutPanel1.Controls.Add(this.import_button);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(396, 29);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // loadTemplate_button
            // 
            this.loadTemplate_button.Location = new System.Drawing.Point(305, 3);
            this.loadTemplate_button.Name = "loadTemplate_button";
            this.loadTemplate_button.Size = new System.Drawing.Size(88, 23);
            this.loadTemplate_button.TabIndex = 5;
            this.loadTemplate_button.Text = "Load Template";
            this.loadTemplate_button.UseVisualStyleBackColor = true;
            this.loadTemplate_button.Click += new System.EventHandler(this.loadTemplate_button_Click);
            // 
            // dropDownList
            // 
            this.dropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList.FormattingEnabled = true;
            this.dropDownList.Location = new System.Drawing.Point(178, 3);
            this.dropDownList.Name = "dropDownList";
            this.dropDownList.Size = new System.Drawing.Size(121, 21);
            this.dropDownList.TabIndex = 6;
            this.dropDownList.SelectedIndexChanged += new System.EventHandler(this.dropDownList_SelectedIndexChanged);
            // 
            // export_button
            // 
            this.export_button.Location = new System.Drawing.Point(84, 3);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(88, 23);
            this.export_button.TabIndex = 5;
            this.export_button.Text = "Export";
            this.export_button.UseVisualStyleBackColor = true;
            this.export_button.Click += new System.EventHandler(this.export_button_Click);
            // 
            // import_button
            // 
            this.import_button.Location = new System.Drawing.Point(3, 3);
            this.import_button.Name = "import_button";
            this.import_button.Size = new System.Drawing.Size(75, 23);
            this.import_button.TabIndex = 5;
            this.import_button.Text = "Import";
            this.import_button.UseVisualStyleBackColor = true;
            this.import_button.Click += new System.EventHandler(this.import_button_Click);
            // 
            // StatusImage
            // 
            this.StatusImage.DataPropertyName = "StatusImage";
            this.StatusImage.HeaderText = "Status";
            this.StatusImage.Name = "StatusImage";
            this.StatusImage.ReadOnly = true;
            // 
            // PLCAdress
            // 
            this.PLCAdress.DataPropertyName = "PLCAdress";
            this.PLCAdress.FillWeight = 86.17369F;
            this.PLCAdress.HeaderText = "PLCAdress";
            this.PLCAdress.Name = "PLCAdress";
            // 
            // DeviceDescription
            // 
            this.DeviceDescription.DataPropertyName = "DevicePointDescription";
            this.DeviceDescription.HeaderText = "DevicePointDescription";
            this.DeviceDescription.Name = "DeviceDescription";
            this.DeviceDescription.ReadOnly = true;
            // 
            // Datatype
            // 
            this.Datatype.DataPropertyName = "Datatype";
            this.Datatype.HeaderText = "Datatype";
            this.Datatype.Name = "Datatype";
            this.Datatype.ReadOnly = true;
            this.Datatype.Visible = false;
            // 
            // SymbolicAdress
            // 
            this.SymbolicAdress.DataPropertyName = "SymbolicAdress";
            this.SymbolicAdress.FillWeight = 114.2454F;
            this.SymbolicAdress.HeaderText = "SymbolicAdress";
            this.SymbolicAdress.Name = "SymbolicAdress";
            // 
            // FunctionText
            // 
            this.FunctionText.DataPropertyName = "FunctionText";
            this.FunctionText.FillWeight = 78.17257F;
            this.FunctionText.HeaderText = "FunctionText";
            this.FunctionText.MinimumWidth = 140;
            this.FunctionText.Name = "FunctionText";
            this.FunctionText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DT
            // 
            this.DT.DataPropertyName = "DT";
            this.DT.HeaderText = "DT";
            this.DT.Name = "DT";
            this.DT.ReadOnly = true;
            this.DT.Visible = false;
            // 
            // DevicePointDesignation
            // 
            this.DevicePointDesignation.DataPropertyName = "DevicePointDesignation";
            this.DevicePointDesignation.FillWeight = 114.2454F;
            this.DevicePointDesignation.HeaderText = "DevicePointDesignation";
            this.DevicePointDesignation.Name = "DevicePointDesignation";
            this.DevicePointDesignation.ReadOnly = true;
            // 
            // FunctionDefinition
            // 
            this.FunctionDefinition.DataPropertyName = "FunctionDefinition";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionDefinition.DefaultCellStyle = dataGridViewCellStyle3;
            this.FunctionDefinition.FillWeight = 114.2454F;
            this.FunctionDefinition.HeaderText = "FunctionDefinition";
            this.FunctionDefinition.MinimumWidth = 150;
            this.FunctionDefinition.Name = "FunctionDefinition";
            this.FunctionDefinition.ReadOnly = true;
            // 
            // SymbolicAdressDefined
            // 
            this.SymbolicAdressDefined.DataPropertyName = "SymbolicAdressDefined";
            this.SymbolicAdressDefined.HeaderText = "SymbolicAdressDefined";
            this.SymbolicAdressDefined.Name = "SymbolicAdressDefined";
            this.SymbolicAdressDefined.ReadOnly = true;
            this.SymbolicAdressDefined.Visible = false;
            // 
            // FunctionType
            // 
            this.FunctionType.DataPropertyName = "FunctionType";
            this.FunctionType.FillWeight = 78.67199F;
            this.FunctionType.HeaderText = "FunctionType";
            this.FunctionType.Name = "FunctionType";
            this.FunctionType.ReadOnly = true;
            // 
            // ManagePlcForm
            // 
            this.AcceptButton = this.Ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(1107, 765);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.flowLayoutPanelUp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(528, 278);
            this.Name = "ManagePlcForm";
            this.Text = "ManagePlcForm";
            this.Load += new System.EventHandler(this.ManagePlcForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManagePlcForm_KeyDown);
            this.flowLayoutPanelUp.ResumeLayout(false);
            this.flowLayoutPanelUp.PerformLayout();
            this.flowLayoutPanelDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.BindingSource plcDataModelViewBindingSource;
        private System.Windows.Forms.Button properties_button;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button export_button;
        private System.Windows.Forms.Button import_button;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevicePointDescription;
        private System.Windows.Forms.Button loadTemplate_button;
        private System.Windows.Forms.ComboBox dropDownList;
        private System.Windows.Forms.Button upper_button;
        private System.Windows.Forms.Button lower_button;
        private System.Windows.Forms.CheckBox FastInput;
        private System.Windows.Forms.DataGridViewImageColumn StatusImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLCAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Datatype;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolicAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionText;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevicePointDesignation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionDefinition;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolicAdressDefined;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionType;
    }
}