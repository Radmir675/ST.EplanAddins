using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.DataModel.Graphics;
using ST.EplAddin.Footnote.ProperyBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Eplan.EplApi.DataModel.Placement;

//LEGEND NOTE
namespace ST.EplAddin.Footnote
{
    /// <summary>
    /// Класс объекта выноски
    /// </summary>
    [DataContract]
    [RefreshProperties(RefreshProperties.All)]
    public partial class FootnoteItem
    {
        public static String FOOTNOTE_KEY = "FOOTNOTE_OBJID#";

        public Block block = null;
        public ViewPlacement viewPlacement = null; //текущий обзор модели с которым группируемся
        public Placement3D sourceItem3D = null; //объект пространства листа
        public List<Placement> subItems = null;
        public PropertiesList PROPERTYID { get; set; } = STSettings.instance.PROPERTYID;

        private Page currentPage = null;
        private Line itemline = null;
        private Line noteline = null;
        private Text label = null;
        private Text jsontext = null;
        private Text propid = null;
        private Arc startpoint = null;

        #region Properties
        [Browsable(false)]
        public PointD startPosition
        {
            get => itemline.StartPoint;
            set => itemline.StartPoint = value;
        }
        [Browsable(false)]
        public PointD finishPosition
        {
            get => noteline.StartPoint;
            set => noteline.StartPoint = value;
        }

        [Browsable(false)]
        [Description("Текст выноски")]
        [CategoryAttribute("Text"), ReadOnlyAttribute(true), DefaultValueAttribute("")]
        public string Text { get; set; } = "label";

        [DataMember]
        [Description("Высота текста выноски")]
        [CategoryAttribute("Text"), DefaultValueAttribute(2.5)]
        public double TEXTHEIGHT { get; set; } = STSettings.instance.TEXTHEIGHT;

        [DataMember(Name = "Толщина линии")]
        [Description("Толщина линий выноски")]
        [CategoryAttribute("Line"), DefaultValueAttribute(0.25)]
        public double LINEWIDTH
        {
            get { return Math.Round(mLINEWIDTH, 2); }
            set { mLINEWIDTH = Math.Round(value, 2); }
        }
        private double mLINEWIDTH = STSettings.instance.LINEWIDTH;

        [DataMember(Name = "Направление выноски")] //этот текст попадает в Json
        [Description("Инвертировать направление полки")]
        [CategoryAttribute("Line"), DefaultValueAttribute(2.5)]
        public bool INVERTDIRECTION { get; set; } = false;

        [DataMember]
        [Description("Кружок")]
        [CategoryAttribute("Line"), DefaultValueAttribute(2.5)]
        public bool STARTPOINT { get; set; } = STSettings.instance.STARTPOINT;

        [DataMember]
        [Description("Радиус кружка")]
        [CategoryAttribute("Line"), DefaultValueAttribute(0.25)]
        public double STARTPOINTRADIUS { get; set; } = STSettings.instance.STARTPOINTRADIUS;

        [DataMember]
        [Description("Введенный пользователем текст")]
        [CategoryAttribute("Text"), DefaultValueAttribute(0.25)]
        public string USERTEXT { get; set; } = STSettings.instance.USERTEXT;

        //[DataMember]
        [Description("Индекс размещаемого свойства")]
        [Category("Text"), DefaultValue(PropertiesList.P20450)]
        #endregion

        public FootnoteItem()
        {
            PropertiesDialogForm.ApplyEventClick += ResetLabelText;
        }

        private void ResetLabelText(object sender, EventArgs e)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                if (label != null)
                {
                    MultiLangString mls = new MultiLangString();
                    mls.SetAsString("-1");
                    label.Contents = mls;
                }
            }
        }

        /// <summary>
        /// Сериазизует поля помеченные [DataMember] в строку и записываетсодержимое в скрытый элемент jsontext
        /// </summary>
        /// <returns></returns>
        public String Serialize()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FootnoteItem));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, this);

            byte[] json = stream.ToArray();
            stream.Close();
            string jsonstring = Encoding.UTF8.GetString(json, 0, json.Length);

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                MultiLangString labeltext = new MultiLangString();
                labeltext.AddString(0, jsonstring);
                jsontext.Contents = labeltext;
                safetyPoint.Commit();
            }

            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        /// <summary>
        /// Десериализует данные из текста скрытого элемента jsontext, и присваевает значени текущему экземпляру
        /// </summary>
        public void Deserialize()
        {
            try
            {
                var json = jsontext.Contents.GetString(0);
                var jsonnote = new FootnoteItem();
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var ser = new DataContractJsonSerializer(jsonnote.GetType());

                jsonnote = ser.ReadObject(ms) as FootnoteItem;

                this.TEXTHEIGHT = jsonnote.TEXTHEIGHT;
                this.LINEWIDTH = jsonnote.LINEWIDTH;
                this.STARTPOINT = jsonnote.STARTPOINT;
                this.STARTPOINTRADIUS = jsonnote.STARTPOINTRADIUS;
                this.INVERTDIRECTION = jsonnote.INVERTDIRECTION;
                this.USERTEXT = jsonnote.USERTEXT;

                if (jsonnote.PROPERTYID != 0)// тут надо подумать
                    this.PROPERTYID = jsonnote.PROPERTYID;

                ms.Close();
            }
            catch (Exception)
            {
                String msg = $"Json desirialize error";
                Eplan.EplApi.Base.BaseException exc = new Eplan.EplApi.Base.BaseException(msg, Eplan.EplApi.Base.MessageLevel.Message);
                exc.FixMessage();
            }
        }

        /// <summary>
        /// Создаение элемента выноски из блока
        /// </summary>
        /// <param name="block"></param>
        public void Create(Block block)
        {
            if (block.Name.Contains(FOOTNOTE_KEY))
            {
                this.block = block;
                currentPage = this.block.Page;
                viewPlacement = this.block.Group as ViewPlacement;

                GetSourceObject(); // извлечение из имени идентификатор исходного объекта
                UpdateBlock(); //обновление или создание элементов
                Deserialize(); //получение сохраненных свойств
                UpdateSubItems(); //обновление элементов
                GroupWithViewPlacement();
            }
        }

        public void UpdateBlockItems(Block block)
        {

            if (block.Name.Contains(FOOTNOTE_KEY))
            {
                this.block = block;
                currentPage = this.block.Page;
                viewPlacement = this.block.Group as ViewPlacement;
                GetSubItems(this.block.SubPlacements);
                GetSourceObject(); // извлечение из имени идентификатор исходного объекта
                Deserialize(); //получение сохраненных свойств
                UpdateSubItems(); //обновление элементов
            }
        }

        /// <summary>
        /// Создание элемента выноски из объекта 3D размещения
        /// </summary>
        /// <param name="vpart"></param>
        public void Create(ViewPart vpart)
        {
            currentPage = vpart.Page;
            viewPlacement = vpart.Group as ViewPlacement;
            CreateSubItems();
            SetSourceObject(vpart);
        }

        /// <summary>
        /// Создание элемента выноски пустого на странице
        /// </summary>
        /// <param name="page"></param>
        public void Create(Page page)
        {
            currentPage = page;
            UpdateBlock();
            UpdateSubItems();
        }

        /// <summary>
        /// Установить начальную точку
        /// </summary>
        /// <param name="point"></param>
        public void SetItemPoint(PointD point)
        {
            startPosition = point;
            UpdateSubItems();
        }

        /// <summary>
        /// Установить конечную точку
        /// </summary>
        /// <param name="point"></param>
        public void SetNotePoint(PointD point)
        {
            finishPosition = point;
            UpdateSubItems();
            //TODO: зачем везде пихать обновление вложенных элементов
        }


        /// <summary>
        /// Создание вложенных в блок элементов 
        /// </summary>
        public void CreateSubItems()
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                Pen penline = new Pen();
                penline.ColorId = 0;
                penline.StyleId = 0;
                penline.StyleFactor = -16002;
                penline.Width = LINEWIDTH;
                penline.LineEndType = 0;

                GraphicalLayer layer = currentPage.Project.LayerTable["EPLAN107"];

                if (label == null)
                {
                    label = new Text();
                    label.Create(currentPage, Text, TEXTHEIGHT);
                    label.Justification = TextBase.JustificationType.SpecialCenter;
                    label.Layer = layer;

                }

                if (jsontext == null)
                {
                    jsontext = new Text();
                    jsontext.Create(currentPage, "", 0.0);
                }

                if (propid == null)
                {
                    propid = new Text();
                    propid.Create(currentPage, PROPERTYID.ToString(), TEXTHEIGHT / 2);
                    propid.IsSetAsVisible = Visibility.Invisible;
                }

                if (itemline == null)
                {
                    itemline = new Line();
                    itemline.Create(currentPage);
                    itemline.Layer = layer;
                    itemline.Pen = penline;
                }

                if (noteline == null)
                {
                    noteline = new Line();
                    noteline.Create(currentPage);
                    noteline.Layer = layer;
                    noteline.Pen = penline;
                }

                if (startpoint == null)
                {
                    startpoint = new Arc();
                    startpoint.Create(currentPage);
                    startpoint.Pen = penline;
                }
                subItems = new List<Placement> { label, itemline, noteline, startpoint, jsontext, propid };

                safetyPoint.Commit();
            }
        }
        public void CreateBlock()
        {
            CreateBlock(subItems.ToArray());
        }

        /// <summary>
        /// При создании блока переданные элементы удаляются со страницы и объеденяются в блок
        /// </summary>
        /// <param name="items">элементы для создания блока</param>
        public void CreateBlock(Placement[] items)
        {
            if (block == null)
            {
                block = new Block();
                block.Create(currentPage, items);
            }
            else
            {
                MessageBox.Show("Блок уже создан", "FootNote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обновление блока
        /// При изменении вложенных компонентов необходимо пересобрать блок для корректировки BoundingBox
        /// </summary>
        public void UpdateBlock()
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                //Если блок создан ломаем его и пересобираем
                if (block != null)
                {
                    //получить обзор модели на котором лежит блок
                    viewPlacement = block.Group as ViewPlacement;
                    Placement[] items = block.BreakUp();
                    GetSubItems(items); //получили существующие экземпляры после извлечения из блока
                    block = null;
                }

                CreateSubItems(); //создание недостающих элементов
                //updateSubItems(); //обновление текстов и расположения
                CreateBlock(); //объединенить в блок
                GetSubItems(block.SubPlacements); //получили существующие экземпляры после объединения в блок
                //if (sourceItem3D != null) //Подумать
                GetSourceObject();
                SetSourceObject(sourceItem3D); //устанавливаем ссылку на исходный объект, (ссылку на который получили ранее!)//TODO:это можно уже пропустить
                safetyPoint.Commit();
            }
        }

        /// <summary>
        /// Обновление вложенных в блок элементов
        /// </summary>
        public void UpdateSubItems(string oldText = null)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                //update lines
                Pen penpoint = new Pen();
                penpoint.ColorId = 0;
                penpoint.StyleId = 0;
                penpoint.StyleFactor = -16002;
                penpoint.Width = 0.0;
                penpoint.LineEndType = 0;

                if (STARTPOINT)
                {
                    startpoint.SetCircle(startPosition, STARTPOINTRADIUS);
                }
                else
                {
                    startpoint.SetCircle(startPosition, 0.0);
                }

                startpoint.IsSurfaceFilled = true;
                startpoint.Pen = penpoint;

                if (jsontext != null)
                    jsontext.Location = finishPosition;

                //update Text
                Text = GetSourceObjectProperty();
                MultiLangString mls = new MultiLangString();
                mls.SetAsString(Text);
                label.Contents = mls;
                label.Height = TEXTHEIGHT;
                label.TextColorId = (Text == "-1") ? (short)1 : (short)-16002; // если свойство не прочиталось красим в красный

                //itemline.StartPoint = itemPosition;
                itemline.EndPoint = finishPosition;

                double textwidth = label.GetBoundingBox()[1].X - label.GetBoundingBox()[0].X + TEXTHEIGHT;
                //textwidth = textwidth;/** (sourceItem.Group as ViewPlacement).Scale;*/

                noteline.StartPoint = finishPosition;

                PointD endpoint = finishPosition;
                PointD labelpoint = finishPosition;

                if (finishPosition.X > startPosition.X ^ INVERTDIRECTION)
                {
                    endpoint.X += textwidth;
                    noteline.EndPoint = endpoint;

                    labelpoint.X += textwidth / 2;
                    label.Location = new PointD(labelpoint.X, labelpoint.Y + 3);
                }
                else
                {
                    endpoint.X -= textwidth;
                    noteline.EndPoint = endpoint;

                    labelpoint.X -= textwidth / 2;
                    label.Location = new PointD(labelpoint.X, labelpoint.Y + 3);
                }

                if (propid != null)
                {

                    MultiLangString mlsid = new MultiLangString();
                    mlsid.SetAsString(PROPERTYID.ToString());
                    propid.Contents = mlsid;

                    PointD idpoint = new PointD(labelpoint.X, labelpoint.Y - TEXTHEIGHT);
                    propid.Location = idpoint;
                }
                //update lines
                Pen penline = new Pen();
                penline.ColorId = 0;
                penline.StyleId = 0;
                penline.StyleFactor = -16002;
                penline.Width = LINEWIDTH;
                penline.LineEndType = 0;

                noteline.Pen = penline;
                itemline.Pen = penline;

                safetyPoint.Commit();
            }
        }


        public void GetSubItems(Placement[] array)
        {
            try
            {
                label = array.ElementAtOrDefault(0) as Text;
                itemline = array.ElementAtOrDefault(1) as Line;
                noteline = array.ElementAtOrDefault(2) as Line;
                startpoint = array.ElementAtOrDefault(3) as Arc;
                jsontext = array.ElementAtOrDefault(4) as Text;
                propid = array.ElementAtOrDefault(5) as Text;
                subItems = new List<Placement> { label, itemline, noteline, startpoint, jsontext, propid };
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message, "FootNote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.Abort)
                {
                }
            }
        }

        /// <summary>
        /// Получить исходный объект
        /// </summary>
        public void GetSourceObject()
        {
            String referenceID = block.Name.Split('#').Last().Replace('_', '/');
            string objIDDB = block.DatabaseIdentifier + "/" + referenceID;
            string objIDDB1 = currentPage.DatabaseIdentifier + "/" + referenceID;
            StorableObject obj = null;
            StorableObject.TryParseIdentifier(objIDDB, ref obj);

            if (obj == null) return;

            Placement3D si_p3d = obj as Placement3D;
            if (si_p3d == null)
            {
                ViewPart si_vp = obj as ViewPart;
                sourceItem3D = si_vp.Source;
            }
            else
            {
                sourceItem3D = si_p3d;
            }
        }

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        public string GetSourceObjectProperty()
        {
            return GetSourceObjectProperty(sourceItem3D);
        }

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        /// <param name="placement3D">исходный объект</param>
        /// <returns></returns>
        public string GetSourceObjectProperty(Placement3D placement3D)
        {
            String result = "-1"; //default result

            if (placement3D != null)
            {
                switch (PROPERTYID)
                {
                    case PropertiesList.AllAvailableProperties:
                        PropertySelectDialogForm propertySelectDialogForm = new PropertySelectDialogForm(placement3D);
                        propertySelectDialogForm.ShowDialog();
                        break;

                    case PropertiesList.User_defined:
                        if (label != null && label.Contents.GetStringToDisplay(ISOCode.Language.L_ru_RU) != "-1")
                        {
                            result = label.Contents.GetStringToDisplay(ISOCode.Language.L_ru_RU);
                            break;
                        }
                        else
                        {
                            Footnote_CustomTextForm form = new Footnote_CustomTextForm();
                            form.ShowDialog();
                            if (form.DialogResult == DialogResult.OK)
                            {
                                USERTEXT = form.GetUserText();
                                var propertiesId = GetPropID(USERTEXT);
                                var validPropertiesText = GetValidPropertiesText(placement3D, propertiesId).ToList();
                                if (!validPropertiesText.Any())
                                {
                                    result = USERTEXT;
                                }
                                for (var i = 0; i < validPropertiesText.Count; i++)
                                {
                                    result = USERTEXT.Replace($"{{{propertiesId[i].ToString()}}}", validPropertiesText[i]);
                                }
                            }
                            form.Close();
                        }
                        break;

                    case PropertiesList.P20450:
                        result = placement3D.Properties[20450].ToInt().ToString();
                        break;

                    case PropertiesList.P20008:
                        result = placement3D.Properties[20008].ToString();
                        break;

                    case PropertiesList.P20487:
                        Function3D function3D = placement3D as Function3D;
                        if (function3D == null) { MessageBox.Show("Недействительный объект источника"); break; }

                        ArticleReference articleReference = function3D.ArticleReferences.FirstOrDefault();
                        if (articleReference == null || articleReference.IsTransient)
                        {
                            //Попытка найти номер в связанных элементах
                            var arr = function3D.CrossReferencedObjectsAll.Where(a => (a as Function3D).ArticleReferences.Count() > 0);
                            StorableObject arrItem = null;
                            if (arr != null)
                            {
                                arrItem = arr.FirstOrDefault();
                                articleReference = (arrItem as Function3D).ArticleReferences.FirstOrDefault();
                            }
                        }

                        if (articleReference == null || articleReference.IsTransient)
                        { MessageBox.Show("Недействительная ссылка изделия объекта источника"); break; }

                        if (articleReference.Properties.Exists(20487) == false) { MessageBox.Show("Несуществующее свойство ссылки изделия объекта источника"); break; }

                        if (articleReference.Properties[20487].IsEmpty)
                        {
                            //MessageBox.Show("Пустой Номер позиции");
                            result = "-1";
                            break;
                        }

                        result = articleReference.Properties[20487].ToInt().ToString();
                        break;
                }
            }
            return result;
        }

        private IEnumerable<string> GetValidPropertiesText(Placement3D placement3D, List<int> propertiesId)
        {
            foreach (var property in propertiesId)
            {
                using (PropertyDefinition propertyDefinition = new PropertyDefinition(property))
                {
                    bool IsIndexed = propertyDefinition.IsIndexed;
                    if (IsIndexed == false)
                    {
                        var propertyText = placement3D.Properties[property].ToString(ISOCode.Language.L_ru_RU);
                        yield return propertyText;
                    }
                }
            }
        }

        private List<int> GetPropID(string inputText)
        {
            string pattern = @"(?<=\{).*?(?=\})";
            Regex regex = new Regex(pattern);
            MatchCollection collection = regex.Matches(inputText);

            var result = collection.Cast<Match>().Select(s => new { Success = int.TryParse(s.Value, out var value), value })
                    .Where(pair => pair.Success)
                    .Select(pair => pair.value).ToList();
            return result;
        }
        /// <summary>
        /// Присвоить исходный объект
        /// </summary>
        /// <param name="vpart">элемент обзора модели</param>
        public void SetSourceObject(ViewPart vpart)
        {
            if (vpart != null)
                SetSourceObject(vpart.Source);
        }
        /// <summary>
        /// Поиск 3D объекта
        /// </summary>
        /// <param name="placement3d">3D модель</param>
        public void SetSourceObject(Placement3D placement3d)
        {
            if (placement3d != null)
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    string objectId = placement3d.Properties.PROPUSER_DBOBJECTID; //get object id
                    int idxOfSlash = objectId.IndexOf("/", 1, objectId.Length - 1, StringComparison.InvariantCultureIgnoreCase);    //get index of first separator
                    string objectIdWithoutProjectId = objectId.Substring(idxOfSlash + 1, (objectId.Length - idxOfSlash - 1));   //cut off value before first separator together with this separator
                    String referenceID = objectIdWithoutProjectId;
                    sourceItem3D = placement3d;
                    if (sourceItem3D != null)
                        Text = GetSourceObjectProperty(sourceItem3D);//получение текста из свойства но надо ли оно тут?????
                    if (block != null)
                        block.Name = FOOTNOTE_KEY + referenceID;//здесь ловим ошибку
                    safetyPoint.Commit();
                }
            else MessageBox.Show("Не найдена ссылка на исходный объект", "FootNote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Группировка блока с исходным объектом
        /// </summary>
        public void GroupWithViewPlacement()
        {
            GroupWithViewPlacement(viewPlacement);
        }

        public void GroupWithViewPlacement(ViewPlacement viewPlacement)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                if (viewPlacement != null)
                {
                    viewPlacement.InsertSubPlacement(block);
                    safetyPoint.Commit();
                }
            }
        }
    }
}

