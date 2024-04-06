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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePlcForm));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.exchange_button = new System.Windows.Forms.Button();
            this.dowm_button = new System.Windows.Forms.Button();
            this.up_button = new System.Windows.Forms.Button();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.Controls.Add(this.exchange_button);
            this.flowLayoutPanel.Controls.Add(this.dowm_button);
            this.flowLayoutPanel.Controls.Add(this.up_button);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flowLayoutPanel.Size = new System.Drawing.Size(642, 31);
            this.flowLayoutPanel.TabIndex = 3;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // exchange_button
            // 
            this.exchange_button.BackColor = System.Drawing.Color.Transparent;
            this.exchange_button.BackgroundImage = global::ST.EplAddin.PlcEdit.Properties.Resources.reverse;
            this.exchange_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exchange_button.Location = new System.Drawing.Point(606, 3);
            this.exchange_button.Name = "exchange_button";
            this.exchange_button.Size = new System.Drawing.Size(25, 25);
            this.exchange_button.TabIndex = 2;
            this.exchange_button.TabStop = false;
            this.exchange_button.UseVisualStyleBackColor = false;
            // 
            // dowm_button
            // 
            this.dowm_button.BackColor = System.Drawing.Color.Transparent;
            this.dowm_button.BackgroundImage = global::ST.EplAddin.PlcEdit.Properties.Resources.down;
            this.dowm_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dowm_button.Location = new System.Drawing.Point(575, 3);
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
            this.up_button.Location = new System.Drawing.Point(544, 3);
            this.up_button.Name = "up_button";
            this.up_button.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.up_button.Size = new System.Drawing.Size(25, 25);
            this.up_button.TabIndex = 1;
            this.up_button.TabStop = false;
            this.up_button.UseVisualStyleBackColor = false;
            this.up_button.Click += new System.EventHandler(this.up_button_Click);
            // 
            // ManagePlcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 407);
            this.Controls.Add(this.flowLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagePlcForm";
            this.Text = "ManagePlcForm";
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dowm_button;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.Button exchange_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}