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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePlcForm));
            this.Ok_button = new System.Windows.Forms.Button();
            this.Apply_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.flowLayoutPanelDown = new System.Windows.Forms.FlowLayoutPanel();
            this.plcDataModelViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.sourceFile_button = new System.Windows.Forms.Button();
            this.export_button = new System.Windows.Forms.Button();
            this.import_button = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SymbolicAdressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PLCAdressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteOverviewFunctionTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lower_button = new System.Windows.Forms.Button();
            this.upper_button = new System.Windows.Forms.Button();
            this.exchange_button = new System.Windows.Forms.Button();
            this.dowm_button = new System.Windows.Forms.Button();
            this.up_button = new System.Windows.Forms.Button();
            this.properties_button = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.reviewPLC_button = new System.Windows.Forms.Button();
            this.FastInput = new System.Windows.Forms.CheckBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.StatusImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.PLCAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevicePointDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolicAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevicePointDesignation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionDefinition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datatype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdenticalDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolicAdressDefined = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerminalId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceNameShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevicePinNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plcDataModelViewBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.plcDataModelViewBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.plcDataModelViewBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.RewriteFunctionsTextInImport = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanelDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource3)).BeginInit();
            this.SuspendLayout();
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
            this.flowLayoutPanelDown.Location = new System.Drawing.Point(1024, 3);
            this.flowLayoutPanelDown.Name = "flowLayoutPanelDown";
            this.flowLayoutPanelDown.Size = new System.Drawing.Size(243, 29);
            this.flowLayoutPanelDown.TabIndex = 5;
            this.flowLayoutPanelDown.WrapContents = false;
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
            this.tableLayoutPanel.Size = new System.Drawing.Size(1270, 35);
            this.tableLayoutPanel.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.sourceFile_button);
            this.flowLayoutPanel1.Controls.Add(this.export_button);
            this.flowLayoutPanel1.Controls.Add(this.import_button);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(291, 29);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // sourceFile_button
            // 
            this.sourceFile_button.Location = new System.Drawing.Point(178, 3);
            this.sourceFile_button.Name = "sourceFile_button";
            this.sourceFile_button.Size = new System.Drawing.Size(110, 23);
            this.sourceFile_button.TabIndex = 5;
            this.sourceFile_button.Text = "Load  source file";
            this.sourceFile_button.UseVisualStyleBackColor = true;
            this.sourceFile_button.Click += new System.EventHandler(this.sourceFile_button_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(5, 5);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(5);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(122, 19);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.AutoSize = false;
            this.toolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SymbolicAdressToolStripMenuItem,
            this.PLCAdressToolStripMenuItem,
            this.deleteOverviewFunctionTextToolStripMenuItem,
            this.RewriteFunctionsTextInImport});
            this.toolStripMenuItem.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.png_transparent_gear_encapsulated_postscript_computer_icons_engrenagem;
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem.Size = new System.Drawing.Size(122, 29);
            this.toolStripMenuItem.Text = "Settings";
            // 
            // SymbolicAdressToolStripMenuItem
            // 
            this.SymbolicAdressToolStripMenuItem.Name = "SymbolicAdressToolStripMenuItem";
            this.SymbolicAdressToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.SymbolicAdressToolStripMenuItem.Text = "Rewrite overview symbolic adress";
            this.SymbolicAdressToolStripMenuItem.Click += new System.EventHandler(this.rewriteOwerviewSymbolicAdressToolStripMenuItem_Click);
            // 
            // PLCAdressToolStripMenuItem
            // 
            this.PLCAdressToolStripMenuItem.Name = "PLCAdressToolStripMenuItem";
            this.PLCAdressToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.PLCAdressToolStripMenuItem.Text = "Rewrite overview PLC adress";
            this.PLCAdressToolStripMenuItem.Click += new System.EventHandler(this.rewriteOverviewAdressToolStripMenuItem_Click);
            // 
            // deleteOverviewFunctionTextToolStripMenuItem
            // 
            this.deleteOverviewFunctionTextToolStripMenuItem.Name = "deleteOverviewFunctionTextToolStripMenuItem";
            this.deleteOverviewFunctionTextToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.deleteOverviewFunctionTextToolStripMenuItem.Text = "Delete overview function text";
            this.deleteOverviewFunctionTextToolStripMenuItem.Click += new System.EventHandler(this.deleteOverviewFunctionTextToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.36404F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.63595F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1270, 37);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lower_button);
            this.flowLayoutPanel2.Controls.Add(this.upper_button);
            this.flowLayoutPanel2.Controls.Add(this.exchange_button);
            this.flowLayoutPanel2.Controls.Add(this.dowm_button);
            this.flowLayoutPanel2.Controls.Add(this.up_button);
            this.flowLayoutPanel2.Controls.Add(this.properties_button);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(909, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(358, 31);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // lower_button
            // 
            this.lower_button.BackColor = System.Drawing.Color.Transparent;
            this.lower_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lower_button.Image = global::ST.EplAddin.PlcEdit.Properties.Resources.Down1;
            this.lower_button.Location = new System.Drawing.Point(332, 3);
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
            this.upper_button.Location = new System.Drawing.Point(303, 3);
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
            this.exchange_button.Location = new System.Drawing.Point(274, 3);
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
            this.dowm_button.Location = new System.Drawing.Point(245, 3);
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
            this.up_button.Location = new System.Drawing.Point(216, 3);
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
            this.properties_button.Location = new System.Drawing.Point(187, 3);
            this.properties_button.Name = "properties_button";
            this.properties_button.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.properties_button.Size = new System.Drawing.Size(23, 23);
            this.properties_button.TabIndex = 1;
            this.properties_button.TabStop = false;
            this.properties_button.UseVisualStyleBackColor = false;
            this.properties_button.Click += new System.EventHandler(this.properties_button_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.menuStrip1);
            this.flowLayoutPanel3.Controls.Add(this.reviewPLC_button);
            this.flowLayoutPanel3.Controls.Add(this.FastInput);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(900, 31);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // reviewPLC_button
            // 
            this.reviewPLC_button.Location = new System.Drawing.Point(135, 3);
            this.reviewPLC_button.Name = "reviewPLC_button";
            this.reviewPLC_button.Size = new System.Drawing.Size(75, 23);
            this.reviewPLC_button.TabIndex = 4;
            this.reviewPLC_button.Text = "PLC review";
            this.reviewPLC_button.UseVisualStyleBackColor = true;
            this.reviewPLC_button.Click += new System.EventHandler(this.reviewPLC_button_Click);
            // 
            // FastInput
            // 
            this.FastInput.AutoSize = true;
            this.FastInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.FastInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FastInput.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FastInput.Location = new System.Drawing.Point(216, 2);
            this.FastInput.Margin = new System.Windows.Forms.Padding(3, 2, 0, 0);
            this.FastInput.Name = "FastInput";
            this.FastInput.Size = new System.Drawing.Size(74, 27);
            this.FastInput.TabIndex = 6;
            this.FastInput.Text = "Fast input";
            this.FastInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FastInput.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StatusImage,
            this.PLCAdress,
            this.DevicePointDescription,
            this.SymbolicAdress,
            this.FunctionText,
            this.DevicePointDesignation,
            this.FunctionDefinition,
            this.FunctionType,
            this.Datatype,
            this.DT,
            this.IdenticalDT,
            this.SymbolicAdressDefined,
            this.TerminalId,
            this.DeviceNameShort,
            this.DevicePinNumber});
            this.dataGridView.DataSource = this.plcDataModelViewBindingSource1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Gray;
            this.dataGridView.Location = new System.Drawing.Point(0, 37);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersWidth = 17;
            this.dataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowTemplate.Height = 17;
            this.dataGridView.Size = new System.Drawing.Size(1270, 693);
            this.dataGridView.TabIndex = 8;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            this.dataGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseUp);
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
            this.PLCAdress.HeaderText = "PLCAdress";
            this.PLCAdress.Name = "PLCAdress";
            // 
            // DevicePointDescription
            // 
            this.DevicePointDescription.DataPropertyName = "DevicePointDescription";
            this.DevicePointDescription.HeaderText = "DevicePointDescription";
            this.DevicePointDescription.Name = "DevicePointDescription";
            this.DevicePointDescription.ReadOnly = true;
            // 
            // SymbolicAdress
            // 
            this.SymbolicAdress.DataPropertyName = "SymbolicAdress";
            this.SymbolicAdress.HeaderText = "SymbolicAdress";
            this.SymbolicAdress.Name = "SymbolicAdress";
            // 
            // FunctionText
            // 
            this.FunctionText.DataPropertyName = "FunctionText";
            this.FunctionText.HeaderText = "FunctionText";
            this.FunctionText.Name = "FunctionText";
            // 
            // DevicePointDesignation
            // 
            this.DevicePointDesignation.DataPropertyName = "DevicePointDesignation";
            this.DevicePointDesignation.HeaderText = "DevicePointDesignation";
            this.DevicePointDesignation.Name = "DevicePointDesignation";
            this.DevicePointDesignation.ReadOnly = true;
            // 
            // FunctionDefinition
            // 
            this.FunctionDefinition.DataPropertyName = "FunctionDefinition";
            this.FunctionDefinition.HeaderText = "FunctionDefinition";
            this.FunctionDefinition.Name = "FunctionDefinition";
            this.FunctionDefinition.ReadOnly = true;
            // 
            // FunctionType
            // 
            this.FunctionType.DataPropertyName = "FunctionType";
            this.FunctionType.HeaderText = "FunctionType";
            this.FunctionType.Name = "FunctionType";
            this.FunctionType.ReadOnly = true;
            // 
            // Datatype
            // 
            this.Datatype.DataPropertyName = "Datatype";
            this.Datatype.HeaderText = "Datatype";
            this.Datatype.Name = "Datatype";
            this.Datatype.ReadOnly = true;
            this.Datatype.Visible = false;
            // 
            // DT
            // 
            this.DT.DataPropertyName = "DT";
            this.DT.HeaderText = "DT";
            this.DT.Name = "DT";
            this.DT.ReadOnly = true;
            this.DT.Visible = false;
            // 
            // IdenticalDT
            // 
            this.IdenticalDT.DataPropertyName = "IdenticalDT";
            this.IdenticalDT.HeaderText = "IdenticalDT";
            this.IdenticalDT.Name = "IdenticalDT";
            this.IdenticalDT.ReadOnly = true;
            this.IdenticalDT.Visible = false;
            // 
            // SymbolicAdressDefined
            // 
            this.SymbolicAdressDefined.DataPropertyName = "SymbolicAdressDefined";
            this.SymbolicAdressDefined.HeaderText = "SymbolicAdressDefined";
            this.SymbolicAdressDefined.Name = "SymbolicAdressDefined";
            this.SymbolicAdressDefined.ReadOnly = true;
            this.SymbolicAdressDefined.Visible = false;
            // 
            // TerminalId
            // 
            this.TerminalId.DataPropertyName = "TerminalId";
            this.TerminalId.HeaderText = "TerminalId";
            this.TerminalId.Name = "TerminalId";
            this.TerminalId.ReadOnly = true;
            this.TerminalId.Visible = false;
            // 
            // DeviceNameShort
            // 
            this.DeviceNameShort.DataPropertyName = "DeviceNameShort";
            this.DeviceNameShort.HeaderText = "DeviceNameShort";
            this.DeviceNameShort.Name = "DeviceNameShort";
            this.DeviceNameShort.ReadOnly = true;
            this.DeviceNameShort.Visible = false;
            // 
            // DevicePinNumber
            // 
            this.DevicePinNumber.DataPropertyName = "DevicePinNumber";
            this.DevicePinNumber.HeaderText = "DevicePinNumber";
            this.DevicePinNumber.Name = "DevicePinNumber";
            this.DevicePinNumber.ReadOnly = true;
            this.DevicePinNumber.Visible = false;
            // 
            // plcDataModelViewBindingSource1
            // 
            this.plcDataModelViewBindingSource1.DataSource = typeof(ST.EplAddin.PlcEdit.PlcDataModelView);
            // 
            // plcDataModelViewBindingSource2
            // 
            this.plcDataModelViewBindingSource2.DataSource = typeof(ST.EplAddin.PlcEdit.PlcDataModelView);
            // 
            // plcDataModelViewBindingSource3
            // 
            this.plcDataModelViewBindingSource3.DataSource = typeof(ST.EplAddin.PlcEdit.PlcDataModelView);
            // 
            // RewriteFunctionsTextInImport
            // 
            this.RewriteFunctionsTextInImport.Name = "RewriteFunctionsTextInImport";
            this.RewriteFunctionsTextInImport.Size = new System.Drawing.Size(250, 22);
            this.RewriteFunctionsTextInImport.Text = "Rewrite function texts in import";
            this.RewriteFunctionsTextInImport.Click += new System.EventHandler(this.dontRewriteFunctionTextsToolStripMenuItem_Click);
            // 
            // ManagePlcForm
            // 
            this.AcceptButton = this.Ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(1270, 765);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(528, 278);
            this.Name = "ManagePlcForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ManagePlcForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManagePlcForm_FormClosed);
            this.Load += new System.EventHandler(this.ManagePlcForm_Load);
            this.Shown += new System.EventHandler(this.ManagePlcForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.ManagePlcForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManagePlcForm_KeyDown);
            this.flowLayoutPanelDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plcDataModelViewBindingSource3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Ok_button;
        private System.Windows.Forms.Button Apply_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDown;
        private System.Windows.Forms.BindingSource plcDataModelViewBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button export_button;
        private System.Windows.Forms.Button import_button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SymbolicAdressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PLCAdressToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button lower_button;
        private System.Windows.Forms.Button properties_button;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.Button dowm_button;
        private System.Windows.Forms.Button exchange_button;
        private System.Windows.Forms.Button upper_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button reviewPLC_button;
        private System.Windows.Forms.CheckBox FastInput;
        private System.Windows.Forms.Button sourceFile_button;
        private System.Windows.Forms.ToolStripMenuItem deleteOverviewFunctionTextToolStripMenuItem;
        private System.Windows.Forms.BindingSource plcDataModelViewBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource plcDataModelViewBindingSource2;
        private System.Windows.Forms.BindingSource plcDataModelViewBindingSource3;
        private System.Windows.Forms.DataGridViewImageColumn StatusImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLCAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevicePointDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolicAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionText;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevicePointDesignation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionDefinition;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Datatype;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdenticalDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolicAdressDefined;
        private System.Windows.Forms.DataGridViewTextBoxColumn TerminalId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceNameShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevicePinNumber;
        private System.Windows.Forms.ToolStripMenuItem RewriteFunctionsTextInImport;
    }
}