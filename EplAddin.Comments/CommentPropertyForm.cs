using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.Graphics;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.Comments
{
    public partial class CommentPropertyForm : Form
    {
        public static ISOCode.Language GuiLanguage
        {
            get
            {
                return new Languages().GuiLanguage.GetNumber();
            }
        }

        Comment cc;

        public CommentPropertyForm()
        {
            InitializeComponent();

            CancelButton = RejectButton_btn;
            AcceptButton = AcceptButton_btn;
            tabPage1.UseVisualStyleBackColor = false;

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabPage tabPage = tabControl.TabPages[e.Index];
            Brush foreBrush = tabControl.Focused && tabControl.SelectedIndex == e.Index ? Brushes.Black : Brushes.Black;

            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(tabPage.Text, tabPage.Font, foreBrush, e.Bounds, format);
            }
        }


        public void SetComment(ref Comment c)
        {
            cc = c;
            UserNameTextBox.Text = c.Author;
            answers_textBox.Text = c.Answer;
            CommentText.Text = c.Contents.GetStringToDisplay(GuiLanguage);
            creationdate_maskedTextBox.Text = c.CreationDate.ToString("dd.MM.yyyy HH:mm:ss");
            editdate_maskedTextBox2.Text = c.ModificationDate.ToString("dd.MM.yyyy HH:mm:ss");

            StatusComboBox.SelectedIndex = (int)c.ReviewState;
        }
        private void RejectButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {

                //update comment text
                MultiLangString mls = new MultiLangString();
                mls.SetAsString(CommentText.Text);
                cc.Contents = mls;

                //update status icon
                cc.ReviewState = (Comment.Enums.ReviewStateType)StatusComboBox.SelectedIndex;

                //update Sub

                string iconText = "";
                short iconColor = -16002;
                double iconHeight = 2.5;

                switch (cc.ReviewState)
                {//⚠
                    case Comment.Enums.ReviewStateType.ReviewNone: iconText = " "; iconColor = -16002; iconHeight = 2.5; break;
                    case Comment.Enums.ReviewStateType.Accepted: iconText = "Ok"; iconColor = 277; iconHeight = 2.5; break;
                    case Comment.Enums.ReviewStateType.Rejected: iconText = "✖"; iconColor = 1; iconHeight = 2.5; break;
                    case Comment.Enums.ReviewStateType.Cancelled: iconText = "🛇"; iconColor = 1; iconHeight = 3.5; break;
                    case Comment.Enums.ReviewStateType.Completed: iconText = "✔"; iconColor = 277; iconHeight = 2.5; break;
                }

                MultiLangString mls_i = new MultiLangString();
                mls_i.SetAsString(iconText);

                var ico = cc.Group.SubPlacements
                    .Where(p => p is Text)
                    .Where(p => !(p is Comment))
                    .Cast<Text>()
                    .Where(t => t.Justification == TextBase.JustificationType.MiddleCenter)
                    .FirstOrDefault();

                if (ico != null)
                {

                    ico.Contents = mls_i;
                    ico.TextColorId = iconColor;
                    ico.Height = iconHeight;
                }
                else
                {
                    Text t = Eplan.EplApi.DataModel.Graphics.Text.Create(cc.Group);

                    t.Contents = mls_i;
                    t.TextColorId = iconColor;
                    t.Height = iconHeight;
                    t.HasBox = false;
                    t.Location = new Eplan.EplApi.Base.PointD(cc.Location.X + 3, cc.Location.Y + 3);
                    t.Justification = Eplan.EplApi.DataModel.Graphics.TextBase.JustificationType.MiddleCenter;
                    t.Layer = cc.Project.LayerTable["EPLAN519"];

                }

                var itex = cc.Group.SubPlacements.Where(p => p is Text && !(p is Comment)).ToList().Cast<Text>().Where(t => t.HasBox == true).FirstOrDefault();

                if (itex != null)
                {
                    itex.Contents = mls;
                }
                safetyPoint.Commit();
            }
            ///update answers recreate
            Close();
        }
    }
}
