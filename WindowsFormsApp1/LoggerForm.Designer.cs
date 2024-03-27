namespace WindowsFormsApp1
{
    partial class LoggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.roll_up_button = new System.Windows.Forms.Button();
            this.hide_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.gradientPanel1 = new WindowsFormsApp1.GradientPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.roll_up_button);
            this.panel1.Controls.Add(this.hide_button);
            this.panel1.Controls.Add(this.close_button);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Text = this.Text;
            // 
            // roll_up_button
            // 
            this.roll_up_button.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.roll_up_button, "roll_up_button");
            this.roll_up_button.FlatAppearance.BorderSize = 0;
            this.roll_up_button.ForeColor = System.Drawing.Color.Black;
            this.roll_up_button.Name = "roll_up_button";
            this.roll_up_button.UseVisualStyleBackColor = false;
            this.roll_up_button.Click += new System.EventHandler(this.button4_Click);
            // 
            // hide_button
            // 
            this.hide_button.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.hide_button, "hide_button");
            this.hide_button.FlatAppearance.BorderSize = 0;
            this.hide_button.ForeColor = System.Drawing.Color.Black;
            this.hide_button.Name = "hide_button";
            this.hide_button.UseVisualStyleBackColor = false;
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.close_button, "close_button");
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.ForeColor = System.Drawing.Color.Black;
            this.close_button.Name = "close_button";
            this.close_button.UseVisualStyleBackColor = false;
            this.close_button.Click += new System.EventHandler(this.button1_Click);
            this.close_button.MouseLeave += new System.EventHandler(this.close_button_MouseLeave);
            this.close_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.close_button_MouseMove);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.Empty;
            this.gradientPanel1.ColorTop = System.Drawing.Color.Empty;
            resources.ApplyResources(this.gradientPanel1, "gradientPanel1");
            this.gradientPanel1.Name = "gradientPanel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources._1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // LoggerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoggerForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button roll_up_button;
        private System.Windows.Forms.Button hide_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private GradientPanel gradientPanel1;
    }
}