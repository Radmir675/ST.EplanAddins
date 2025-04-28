using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;

namespace ST.EplAddin.Verifications
{
    internal class ProjectPropertiesVF : Verification
    {
        private bool IsDone = false;
        private const int m_iMessageId = 36;
        private string PathToBaseProject;
        private Dictionary<PropertyKey, Property> _errors;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            //strName = "ProjectPropertiesVF";
            //iOrdinal = 30;
            //VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            //VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override string GetMessageText()
        {
            return "Свойства проекта категории \"Формат\" текущего проекта отличаются от базового %1!s!";
        }
        public override void OnStartInspection(bool bOnline)
        {
        }

        public override void OnEndInspection()
        {
            IsDone = false;
            ShowErrors();
        }

        private void ShowErrors()
        {
            foreach (var item in _errors)
            {
                DoErrorMessage(null, item.Value.Name + $"[{item.Value.Index}]" + " " + $"<{item.Value.Id}>");
            }
        }

        public override void Execute(StorableObject storableObject)
        {
            if (IsDone) return;
            var currentProject = Project;
            PathToBaseProject = @"O:\Шаблоны\Базовый проект\BaseProject.edb\ProjectInfo.xml";
            if (!File.Exists(PathToBaseProject))
            {
                var dialogResult = MessageBox.Show(
                    $"Базовый проект с шаблоном по пути {PathToBaseProject} не найден.\n Хотите указать путь к файлу?",
                    "Error",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
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

            _errors = new Dictionary<PropertyKey, Property>();

            var baseProjectЗProperties = ReadFile(PathToBaseProject);
            var baseProjectFormatPropertiesOnly = FindFormatProperties(baseProjectЗProperties);
            var currentProjectProperties = GetProjectValues(currentProject.Properties);
            var currentProjectFormatPropertiesOnly = FindFormatProperties(currentProjectProperties);
            try
            {
                Compare(baseProjectFormatPropertiesOnly, currentProjectFormatPropertiesOnly);
                IsDone = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                IsDone = true;
            }
            FindAbsenceProperties(baseProjectFormatPropertiesOnly, currentProjectFormatPropertiesOnly);
        }

        private void FindAbsenceProperties(Dictionary<PropertyKey, Property> baseProjectFormatPropertiesOnly, Dictionary<PropertyKey, Property> currentProjectProperties)
        {
            var result = baseProjectFormatPropertiesOnly.Except(currentProjectProperties, new PropertyComparer());
            foreach (var item in result)
            {
                if (!_errors.ContainsKey(item.Key))
                {
                    _errors.Add(item.Key, item.Value);
                }
            }
        }

        private Dictionary<PropertyKey, Property> FindFormatProperties(Dictionary<PropertyKey, Property> baseProjectЗProperties)
        {
            var result = new Dictionary<PropertyKey, Property>();
            foreach (var item in baseProjectЗProperties)
            {
                if (item.Value.Name.Contains("Свойство блока"))
                {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }

        private void Compare(Dictionary<PropertyKey, Property> baseProjectFormatPropertiesOnly,
            Dictionary<PropertyKey, Property> currentProjectFormatPropertiesOnly)
        {
            foreach (var item in currentProjectFormatPropertiesOnly)
            {
                if (baseProjectFormatPropertiesOnly.TryGetValue(item.Key, out Property property))
                {
                    if (property.Value == item.Value.Value)
                    {
                        continue;
                    }
                }
                _errors.Add(item.Key, item.Value);
            }
        }

        private Dictionary<PropertyKey, Property> ReadFile(string path)
        {
            Dictionary<PropertyKey, Property> result = new();
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

                    var propertyValue = new Property()
                    {
                        Id = int.Parse(id.Value),
                        Name = name.Value,
                        Index = int.Parse(index.Value),
                        Value = value,
                        Type = type.Value
                    };
                    var propertyKey = new PropertyKey()
                    {
                        Id = int.Parse(id.Value),
                        Index = int.Parse(index.Value)
                    };
                    result.Add(propertyKey, propertyValue);
                }
            }
            return result;
        }


        private Dictionary<PropertyKey, Property> GetProjectValues(ProjectPropertyList projectPropertyList)
        {
            var existingValues = projectPropertyList.ExistingValues;
            var dictionary = new Dictionary<PropertyKey, Property>(existingValues.Length);

            foreach (var value in existingValues)
            {
                var propertyValue = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                var id = value.Id.AsInt;

                if (value.Indexes.Length > 0)
                {
                    foreach (var ind in value.Indexes)
                    {
                        var idxVal = value[ind];

                        if (!idxVal.IsEmpty)
                        {
                            var propertyValue1 = idxVal.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                            if (!string.IsNullOrEmpty(propertyValue1))
                            {
                                var definitionName = idxVal.Definition.Name;

                                var index = int.Parse((ind + 1).ToString());
                                dictionary.Add(new PropertyKey(id, index), new Property()
                                {
                                    Name = definitionName,
                                    Id = id,
                                    Value = propertyValue1,
                                    Index = index
                                });
                            }
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(propertyValue))
                {
                    var definitionName = value.Definition.Name;
                    var index = 0;
                    dictionary.Add(new PropertyKey(id, index), new Property()
                    {
                        Name = definitionName,
                        Id = id,
                        Value = propertyValue,
                    });
                }
            }

            return dictionary;
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
            //strCreator = "Scantronic";
            //eRegion = IMessage.Region.Externals;
            //iMessageId = m_iMessageId;
            //eClassification = IMessage.Classification.Warning;
            //iOrdinal = 20;
        }

        public override void DoHelp() { }
    }

    public struct PropertyKey
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public PropertyKey(int id, int index)
        {
            Id = id;
            Index = index;
        }
    }

    public class Property
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class PropertyComparer : IEqualityComparer<KeyValuePair<PropertyKey, Property>>
    {
        public bool Equals(KeyValuePair<PropertyKey, Property> x, KeyValuePair<PropertyKey, Property> y)
        {
            return x.Key.Id == y.Key.Id && x.Key.Index == y.Key.Index;
        }

        public int GetHashCode(KeyValuePair<PropertyKey, Property> obj)
        {
            unchecked
            {
                return 0; /*obj.Key.GetHashCode();*/
            }
        }
    }
}
