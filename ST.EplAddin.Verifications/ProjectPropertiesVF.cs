using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace ST.EplAddin.Verifications
{
    internal class ProjectPropertiesVF : Verification
    {
        private bool IsDone = false;
        private const int m_iMessageId = 36;
        private string PathToBaseProject;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ProjectPropertiesVF";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override string GetMessageText()
        {
            return "Свойства текущего проекта отличаются от базового %1!s!";
        }
        public override void OnStartInspection(bool bOnline)
        {
        }

        public override void OnEndInspection()
        {
            //может все проверку сюда запихнуть?
        }

        public override void Execute(StorableObject storableObject)
        {
            if (IsDone) return;
            var currentProject = Project;
            PathToBaseProject = @"O:\Шаблоны\Базовый проект\BaseProject.edb\ProjectInfo.xml";
            if (!File.Exists(PathToBaseProject))
            {
                var dialogResult = MessageBox.Show($"Базовый проект с шаблоном по пути {PathToBaseProject} не найден.\n Хотите указать путь к файлу?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (dialogResult != DialogResult.OK)
                {
                    IsDone = true;
                    return;
                }

                var resultDialog = ShowFileDialog();
                if (resultDialog == false)
                {
                    IsDone = true;
                    return;
                }
            }

            var baseProjectЗProperties = ReadFile(PathToBaseProject).ToList();
            var formatPropertiesOnly = baseProjectЗProperties.Where(x => x.Name.Contains("Формат"));
            // CheckProperties(projectToCompare);
            IsDone = true;
        }

        private IEnumerable<ParsedProperty> ReadFile(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    // получаем атрибут name
                    var name = xnode.Attributes.GetNamedItem("name");
                    var id = xnode.Attributes.GetNamedItem("id");
                    var index = xnode.Attributes.GetNamedItem("index");
                    var type = xnode.Attributes.GetNamedItem("type");
                    var value = xnode.InnerText;

                    yield return new ParsedProperty()
                    {
                        Id = id.Value,
                        Name = name.Value,
                        Index = index.Value,
                        Value = value,
                        Type = type.Value
                    };

                }
            }
        }

        private void CheckProperties(Project currentProject)
        {
            string strTmp = string.Empty;
            PropertyValue oPropValue;

            foreach (AnyPropertyId hPProp in Properties.AllProjectPropIDs)
            {
                // check if exists
                if (!currentProject.Properties[hPProp].IsEmpty)
                {
                    if (currentProject.Properties[hPProp].Definition.Type == PropertyDefinition.PropertyType.String)
                    {
                        //read string property
                        oPropValue = currentProject.Properties[hPProp];
                        strTmp = oPropValue.ToString();
                        var res = currentProject.Properties[hPProp].Definition.IsNamePart;
                    }
                }
            }
            IsDone = true;
        }

        private bool ShowFileDialog()
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "ProjectInfo(*.xml)|*.xml";
            var result = openFileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                PathToBaseProject = openFileDlg.FileName;
                return true;
            }
            return false;
        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification,
            ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Warning;
            iOrdinal = 20;
        }


        public override void DoHelp()
        {
        }
    }


    public class ParsedProperty
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Index { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
