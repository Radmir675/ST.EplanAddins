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
            this.sourceDataGridView = new System.Windows.Forms.DataGridView();
            this.targetDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Upload_doc_button = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.sourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetDataGridView)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.sourceDataGridView.Size = new System.Drawing.Size(525, 607);
            this.sourceDataGridView.TabIndex = 0;
            // 
            // targetDataGridView
            // 
            this.targetDataGridView.AllowUserToAddRows = false;
            this.targetDataGridView.AllowUserToDeleteRows = false;
            this.targetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetDataGridView.Location = new System.Drawing.Point(534, 3);
            this.targetDataGridView.Name = "targetDataGridView";
            this.targetDataGridView.ReadOnly = true;
            this.targetDataGridView.Size = new System.Drawing.Size(538, 607);
            this.targetDataGridView.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.48837F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.51163F));
            this.tableLayoutPanel.Controls.Add(this.targetDataGridView, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.sourceDataGridView, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1075, 613);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Upload_doc_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 581);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1075, 32);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // Upload_doc_button
            // 
            this.Upload_doc_button.Location = new System.Drawing.Point(540, 3);
            this.Upload_doc_button.Name = "Upload_doc_button";
            this.Upload_doc_button.Size = new System.Drawing.Size(137, 23);
            this.Upload_doc_button.TabIndex = 0;
            this.Upload_doc_button.Text = "Upload doc to compare";
            this.Upload_doc_button.UseVisualStyleBackColor = true;
            this.Upload_doc_button.Click += new System.EventHandler(this.Upload_doc_button_Click);
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
            // ComparingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 613);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ComparingForm";
            this.Text = "ComparingForm";
            this.Load += new System.EventHandler(this.ComparingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetDataGridView)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sourceDataGridView;
        private System.Windows.Forms.DataGridView targetDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Upload_doc_button;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}