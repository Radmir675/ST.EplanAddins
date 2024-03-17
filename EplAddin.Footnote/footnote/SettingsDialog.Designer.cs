namespace ST.EplAddin.Footnote
{
    partial class SettingsDialog
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
            this.apply_Button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // apply_Button
            // 
            this.apply_Button.Location = new System.Drawing.Point(206, 252);
            this.apply_Button.Name = "apply_Button";
            this.apply_Button.Size = new System.Drawing.Size(75, 23);
            this.apply_Button.TabIndex = 0;
            this.apply_Button.Text = "Apply";
            this.apply_Button.UseVisualStyleBackColor = true;
            this.apply_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // close_button
            // 
            this.close_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close_button.Location = new System.Drawing.Point(287, 252);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 1;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(350, 234);
            this.propertyGrid1.TabIndex = 12;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.apply_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.close_button;
            this.ClientSize = new System.Drawing.Size(374, 287);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.apply_Button);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsDialog";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button apply_Button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}