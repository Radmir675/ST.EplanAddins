namespace ST.EplAddin.Footnote.Forms
{
    partial class Form1
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
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView
            // 
            this.treeListView.CellEditUseWholeCell = false;
            this.treeListView.HideSelection = false;
            this.treeListView.Location = new System.Drawing.Point(114, 57);
            this.treeListView.Name = "treeListView";
            this.treeListView.ShowGroups = false;
            this.treeListView.Size = new System.Drawing.Size(420, 218);
            this.treeListView.TabIndex = 0;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeListView);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListView;
    }
}