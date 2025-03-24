using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.DataModel.Graphics;
using NLog;
using ST.EplAddin.FootNote.ProperyBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using static Eplan.EplApi.DataModel.Placement;

namespace ST.EplAddin.FootNote.FootNote
{
    /// <summary>
    /// Класс объекта выноски
    /// </summary>
    [DataContract]
    [RefreshProperties(RefreshProperties.All)]
    public partial class FootnoteItem
    {
        Logger logger;
        public Block block = null;
        public ViewPlacement viewPlacement = null; //текущий обзор модели с которым группируемся
        public Placement3D sourceItem3D = null; //объект пространства листа
        public List<Placement> subItems = null;

        private Page currentPage = null;
        private Line itemline = null;
        private Line noteline = null;
        private Text label = null;
        private Text jsontext = null;
        private Text propid = null;
        private Arc startpoint = null;


        #region Properties
        [Description("ID выноски")]
        [DisplayName("Тип выноски текста")]
        public PropertiesList PROPERTYID { get; set; } = STSettings.instance.PROPERTYID;

        [Browsable(false)]
        private PointD _startPosition;
        [Browsable(false)]
        public PointD StartPosition
        {
            get
            {
                PointD? result = null;
                try
                {
                    result = itemline?.StartPoint;
                }
                catch { }
                finally
                {
                    result ??= _startPosition;
                }
                return result.Value;
            }
            set
            {
                if (itemline != null)
                {
                    itemline.StartPoint = value;
                }
                _startPosition = value;
            }
        }

        [Browsable(false)]
        private PointD _finishPosition;

        [Browsable(false)]
        public PointD FinishPosition
        {
            get
            {
                PointD? result = null;
                try
                {
                    result = noteline?.StartPoint;
                }
                catch { }
                finally
                {
                    result ??= _finishPosition;
                }
                return result.Value;
            }
            set
            {
                if (noteline != null)
                {
                    noteline.StartPoint = value;
                }
                _finishPosition = value;
            }
        }
        [Browsable(false)]
        public bool IsUserTextUpdated { get; set; } = false;

        [Browsable(false)] public double PageScale { get; set; } = 1;

        [Browsable(false)]
        [Description("Текст выноски")]
        [Category("Text"), ReadOnly(true), DefaultValue("")]
        public string Text { get; set; } = "label";

        [DataMember]
        [Description("Высота текста выноски")]
        [Category("Text"), DefaultValue(2.5)]
        [DisplayName("Высота текста")]
        public double TEXTHEIGHT { get; set; } = STSettings.instance.TEXTHEIGHT;

        [DataMember(Name = "Толщина линии")]
        [Description("Толщина линий выноски")]
        [Category("Line"), DefaultValue(0.25)]
        [DisplayName("Толщина линий")]
        public double LINEWIDTH
        {
            get { return Math.Round(mLINEWIDTH, 2); }
            set { mLINEWIDTH = Math.Round(value, 2); }
        }
        private double mLINEWIDTH = STSettings.instance.LINEWIDTH;

        [DataMember(Name = "Направление выноски")] //этот текст попадает в Json
        [Description("Инвертировать направление полки")]
        [Category("Line"), DefaultValue(2.5)]
        [DisplayName("Инвертировать направление")]
        public bool INVERTDIRECTION { get; set; } = false;

        [DataMember]
        [Description("Кружок")]
        [Category("Line"), DefaultValue(2.5)]
        [DisplayName("Указатель-круг")]
        public bool STARTPOINT { get; set; } = STSettings.instance.STARTPOINT;

        [ReadOnly(false)]
        [DataMember]
        [Description("Радиус кружка")]
        // [TypeConverter(typeof(FigureConverter))]
        [Category("Line"), DefaultValue(0.25)]
        [DisplayName("Радиус кружка")]
        // [Editor(typeof(Editor), typeof(UITypeEditor))]
        public double STARTPOINTRADIUS { get; set; } = STSettings.instance.STARTPOINTRADIUS;

        [DataMember]
        [Description("Введенный пользователем текст")]
        [Category("Text"), DefaultValue(0.25)]
        [DisplayName("Введенный пользователем текст")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string USERTEXT { get; set; }

        //[DataMember]
        [Description("Индекс размещаемого свойства")]
        [Category("Text"), DefaultValue(PropertiesList.P20450)]
        #endregion

        public FootnoteItem()
        {
            PropertiesDialogForm.ApplyEventClick += ResetLabelText;
            GetLoggerConfig();


        }
        public FootnoteItem(PointD startPoint, PointD endPoint) : this()
        {
            StartPosition = startPoint;
            FinishPosition = endPoint;
        }


        private void GetLoggerConfig()
        {

            var config = new NLog.Config.LoggingConfiguration();
            // Куда выводим: Файл и Консоль
            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var path = Path.Combine(fileName, "file.txt");
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = path, Layout = @"${date:format=HH\\:mm\\:ss}  message=${message} ${callsite}" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            // Правила сопоставления регистраторов          
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            // Установка конфигурации         
            NLog.LogManager.Configuration = config;
            logger = LogManager.GetLogger("logfile");
            logger.Debug("");
        }

        private void ResetLabelText(object sender, EventArgs e)
        {
            logger.Debug("");
            IsUserTextUpdated = true;
        }

        /// <summary>
        /// Сериазизует поля помеченные [DataMember] в строку и записывает содержимое в скрытый элемент jsontext
        /// </summary>
        /// <returns></returns>
        public String Serialize()
        {
            logger.Debug("");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FootNote.FootnoteItem));
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
            logger.Debug("");
            try
            {
                var json = jsontext.Contents.GetString(0);
                var jsonnote = new FootNote.FootnoteItem();
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var ser = new DataContractJsonSerializer(jsonnote.GetType());

                jsonnote = ser.ReadObject(ms) as FootNote.FootnoteItem;

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
            logger.Debug("Block");
            if (FootnoteVerification.IsFootnoteBlock(block))
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
        public void GetBlockInfoToDrag(Block block)
        {
            logger.Debug("");
            this.block = block;
            currentPage = this.block.Page;
            viewPlacement = this.block.Group as ViewPlacement;

            GetSourceObject();
            GetSubItems(block.SubPlacements);
            Deserialize(); //получение сохраненных свойств
        }

        public void UpdateBlockItems(Block block)
        {
            logger.Debug("");
            if (FootnoteVerification.IsFootnoteBlock(block))
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
            logger.Debug("");
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
            logger.Debug("");
            currentPage = page;
            UpdateBlock();
        }

        /// <summary>
        /// Установить начальную точку
        /// </summary>
        /// <param name="point"></param>
        public void SetItemPoint(PointD point)
        {
            logger.Debug("");
            StartPosition = point;
        }

        /// <summary>
        /// Установить конечную точку
        /// </summary>
        /// <param name="point"></param>
        public void SetNotePoint(PointD point)
        {
            logger.Debug("");
            FinishPosition = point;
        }


        /// <summary>
        /// Создание вложенных в блок элементов 
        /// </summary>
        public void CreateSubItems()
        {
            logger.Debug("");
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                Pen penline = SetNoteLineProperties();
                GraphicalLayer layer = currentPage.Project.LayerTable["EPLAN107"];

                if (label == null)
                {
                    label = new Text();
                    label.Create(currentPage, Text, TEXTHEIGHT);
                    SetTextWithProperties();
                    label.Justification = TextBase.JustificationType.SpecialCenter;
                    label.Layer = layer;
                }

                if (jsontext == null)
                {
                    jsontext = new Text();
                    jsontext.Create(currentPage, "", 0.0);
                    SetJsonContextLocation();
                }

                if (propid == null)
                {
                    propid = new Text();
                    propid.Create(currentPage, PROPERTYID.ToString(), TEXTHEIGHT / 2);
                    SetPropId(label.Location);
                    propid.IsSetAsVisible = Visibility.Invisible;
                }

                if (noteline == null)
                {
                    noteline = new Line();
                    noteline.Create(currentPage, FinishPosition, new PointD(FinishPosition.X + GetTextWidthBound(), FinishPosition.Y));
                    noteline.Layer = layer;
                    noteline.Pen = penline;
                }
                if (itemline == null)
                {
                    itemline = new Line();
                    itemline.Create(currentPage, StartPosition, FinishPosition);
                    itemline.Layer = layer;
                    itemline.Pen = penline;
                }

                if (startpoint == null)
                {
                    startpoint = new Arc();
                    startpoint.Create(currentPage);
                    startpoint.Pen = penline;//эта строка несовсем понятна
                    Pen penpoint = SetPenPointProperties();
                    SetStartPointer(penpoint);
                }
                SetPointsPositionInvertDirectionCase(GetTextWidthBound());
                SetPropertiesToLines();
                subItems = new List<Placement> { label, itemline, noteline, startpoint, jsontext, propid };

                safetyPoint.Commit();
            }
        }

        /// <summary>
        /// Обновление вложенных в блок элементов
        /// </summary>
        public void UpdateSubItems(string oldText = null)
        {
            logger.Debug("");
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                Pen penpoint = SetPenPointProperties();
                SetStartPointer(penpoint);
                SetJsonContextLocation();
                SetTextWithProperties();
                double textwidth = GetTextWidthBound();
                itemline.EndPoint = FinishPosition;
                noteline.StartPoint = FinishPosition;
                SetPointsPositionInvertDirectionCase(textwidth);
                SetPropertiesToLines();
                safetyPoint.Commit();
            }
        }

        private void SetJsonContextLocation()
        {
            if (jsontext != null)
                jsontext.Location = FinishPosition;
        }

        private double GetTextWidthBound()
        {
            PageScale = currentPage?.Properties.PAGE_SCALE;
            return (label.GetBoundingBox()[1].X - label.GetBoundingBox()[0].X + TEXTHEIGHT) * PageScale;
        }



        private void SetPropertiesToLines()
        {
            //update lines
            Pen penline = SetNoteLineProperties();
            noteline.Pen = penline;//это полка текста
            itemline.Pen = penline;//это линия выноски
        }

        private void SetPointsPositionInvertDirectionCase(double textwidth)
        {
            PointD endpoint = FinishPosition;
            PointD labelpoint = FinishPosition;

            if (FinishPosition.X > StartPosition.X ^ INVERTDIRECTION)
            {
                endpoint.X += textwidth;
                if (noteline != null)
                {
                    noteline.EndPoint = endpoint;
                }
                labelpoint.X += textwidth / 2;
                label.Location = new PointD(labelpoint.X, labelpoint.Y + 3);
            }
            else
            {
                endpoint.X -= textwidth;
                if (noteline != null)
                {
                    noteline.EndPoint = endpoint;
                }

                labelpoint.X -= textwidth / 2;
                label.Location = new PointD(labelpoint.X, labelpoint.Y + 3);
            }
            SetPropId(labelpoint);
        }

        private void SetPropId(PointD labelpoint)
        {
            if (propid != null)
            {
                MultiLangString mlsid = new MultiLangString();
                mlsid.SetAsString(PROPERTYID.ToString());
                propid.Contents = mlsid;

                PointD idpoint = new PointD(labelpoint.X, labelpoint.Y - TEXTHEIGHT);
                propid.Location = idpoint;
            }
        }

        private void SetTextWithProperties()
        {
            Text = GetSourceObjectProperty();
            MultiLangString mls = new MultiLangString();
            mls.SetAsString(Text);
            label.Contents = mls;
            label.Height = TEXTHEIGHT;
            label.TextColorId = (Text == "-1") ? (short)1 : (short)-16002; // если свойство не прочиталось красим в красный
        }

        private void SetStartPointer(Pen penpoint)
        {
            if (STARTPOINT)
            {
                startpoint.SetCircle(StartPosition, STARTPOINTRADIUS);
                startpoint.IsSurfaceFilled = true;
                startpoint.Pen = penpoint;
                itemline.StartArrow = false;
            }
            else
            {
                itemline.StartArrow = true;
                startpoint.SetCircle(StartPosition, 0);
            }
        }

        private Pen SetNoteLineProperties()
        {
            Pen penline = new Pen();
            penline.ColorId = 0;
            penline.StyleId = 0;
            penline.StyleFactor = -16002;
            penline.Width = LINEWIDTH;
            penline.LineEndType = 0;
            return penline;
        }

        private Pen SetPenPointProperties()
        {
            Pen penpoint = new Pen();
            penpoint.ColorId = 0;
            penpoint.StyleId = 0;
            penpoint.StyleFactor = -16002;
            penpoint.Width = 0.0;
            penpoint.LineEndType = 0;
            return penpoint;
        }

        public void CreateBlock()
        {
            logger.Debug("");
            CreateBlock(subItems.ToArray());
        }

        /// <summary>
        /// При создании блока переданные элементы удаляются со страницы и объеденяются в блок
        /// </summary>
        /// <param name="items">элементы для создания блока</param>
        public void CreateBlock(Placement[] items)
        {
            logger.Debug("Placement");
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
            logger.Debug("");
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
                CreateBlock(); //объединить в блок
                GetSubItems(block.SubPlacements); //получили существующие экземпляры после объединения в блок
                //GetSourceObject(); //!!!!он может понадобится в обновлении отчета
                SetSourceObject(sourceItem3D); //устанавливаем ссылку на исходный объект, (ссылку на который получили ранее!)//TODO:это можно уже пропустить
                safetyPoint.Commit();
            }
        }



        public void GetSubItems(Placement[] array)
        {
            logger.Debug("");
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
            logger.Debug("");
            String referenceID = block.Name.Split('#').Last().Replace('_', '/');
            string objIDDB = block.DatabaseIdentifier + "/" + referenceID;

            StorableObject obj = null;
            StorableObject.TryParseIdentifier(objIDDB, ref obj);

            if (obj == null) return;

            Placement3D placement3D = obj as Placement3D;
            if (placement3D == null)
            {
                ViewPart viewPart = obj as ViewPart;
                sourceItem3D = viewPart.Source;
            }
            else
            {
                sourceItem3D = placement3D;
            }
        }

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        public string GetSourceObjectProperty()
        {
            logger.Debug("");
            return GetSourceObjectProperty(sourceItem3D);
        }


        /// <summary>
        /// Присвоить исходный объект
        /// </summary>
        /// <param name="vpart">элемент обзора модели</param>
        public void SetSourceObject(ViewPart vpart)
        {
            logger.Debug("");
            if (vpart != null)
                SetSourceObject(vpart.Source);
        }
        /// <summary>
        /// Поиск 3D объекта
        /// </summary>
        /// <param name="placement3d">3D модель</param>
        public void SetSourceObject(Placement3D placement3d)
        {
            logger.Debug("Placement3D");
            if (placement3d != null)
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    string objectId = placement3d.Properties.PROPUSER_DBOBJECTID; //get object id
                    int idxOfSlash = objectId.IndexOf("/", 1, objectId.Length - 1, StringComparison.InvariantCultureIgnoreCase);    //get index of first separator
                    string objectIdWithoutProjectId = objectId.Substring(idxOfSlash + 1, (objectId.Length - idxOfSlash - 1));   //cut off value before first separator together with this separator
                    String referenceID = objectIdWithoutProjectId;
                    sourceItem3D = placement3d;
                    if (sourceItem3D != null)
                        Text = GetSourceObjectProperty(sourceItem3D);
                    if (block != null)
                        block.Name = FootnoteVerification.FOOTNOTE_KEY + referenceID;//здесь ловим ошибку
                    safetyPoint.Commit();
                }
            else MessageBox.Show("Не найдена ссылка на исходный объект", "FootNote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Группировка блока с исходным объектом
        /// </summary>
        public void GroupWithViewPlacement()
        {
            logger.Debug("");
            GroupWithViewPlacement(viewPlacement);
        }

        public void GroupWithViewPlacement(ViewPlacement viewPlacement)
        {
            logger.Debug("ViewPlacement");
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

