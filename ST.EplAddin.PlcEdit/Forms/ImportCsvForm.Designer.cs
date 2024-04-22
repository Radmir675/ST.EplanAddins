namespace ST.EplAddin.PlcEdit.Forms
{
    partial class ImportCsvForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportCsvForm));
            this.importDataGridView = new System.Windows.Forms.DataGridView();
            this.label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelDown = new System.Windows.Forms.FlowLayoutPanel();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.Ok_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.importDataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanelDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // importDataGridView
            // 
            this.importDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.importDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importDataGridView.Location = new System.Drawing.Point(3, 28);
            this.importDataGridView.Name = "importDataGridView";
            this.importDataGridView.Size = new System.Drawing.Size(803, 584);
            this.importDataGridView.TabIndex = 0;
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 6);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(137, 13);
            this.label.TabIndex = 1;
            this.label.Text = "Please select data to import";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanelDown, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.importDataGridView, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 650);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanelDown
            // 
            this.flowLayoutPanelDown.AutoSize = true;
            this.flowLayoutPanelDown.Controls.Add(this.Cancel_button);
            this.flowLayoutPanelDown.Controls.Add(this.Ok_button);
            this.flowLayoutPanelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelDown.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelDown.Location = new System.Drawing.Point(3, 618);
            this.flowLayoutPanelDown.Name = "flowLayoutPanelDown";
            this.flowLayoutPanelDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanelDown.Size = new System.Drawing.Size(803, 29);
            this.flowLayoutPanelDown.TabIndex = 6;
            this.flowLayoutPanelDown.WrapContents = false;
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.Cancel_button.Location = new System.Drawing.Point(725, 3);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 4;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // Ok_button
            // 
            this.Ok_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Ok_button.Location = new System.Drawing.Point(644, 3);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(75, 23);
            this.Ok_button.TabIndex = 1;
            this.Ok_button.Text = "Ok";
            this.Ok_button.UseVisualStyleBackColor = true;
            this.Ok_button.Click += new System.EventHandler(this.Ok_button_Click);
            // 
            // ImportCsvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportCsvForm";
            this.Text = "ImportCsvForm";
            ((System.ComponentModel.ISupportInitialize)(this.importDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanelDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView importDataGridView;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDown;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button Ok_button;
    }
}