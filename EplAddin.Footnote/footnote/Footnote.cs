using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.DataModel.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
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
        public Placement3D sourceItem = null; //объект пространства листа
        public List<Placement> subItems = null;

        private Page currentPage = null;
        private Line itemline = null;
        private Line noteline = null;
        private Text label = null;
        private Text jsontext = null;
        private Text propid = null;
        private Arc startpoint = null;

        [Browsable(false)]
        public PointD itemPosition
        {
            get => itemline.StartPoint;
            set => itemline.StartPoint = value;
        }
        [Browsable(false)]
        public PointD notePosition
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
        [Description("Индекс размещаемого свойства")]
        [CategoryAttribute("Text"), DefaultValueAttribute(PropertiesList.P20450)]
        public PropertiesList PROPERTYID { get; set; } = STSettings.instance.PROPERTYID;

        /* public FootnoteItem()
         {
             //Task.Run((System.Action) (() => this.OpenDataPortalSettings()));
         }*/

        /// <summary>
        /// Сериазизует поля помеченные [DataMember] в строку и записываетсодержимое в скрытый элемент jsontext
        /// </summary>
        /// <returns></returns>
        public String Serialize()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FootnoteItem));
            var stream1 = new MemoryStream();
            serializer.WriteObject(stream1, this);

            byte[] json = stream1.ToArray();
            stream1.Close();
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

                if (jsonnote.PROPERTYID != 0)
                    this.PROPERTYID = jsonnote.PROPERTYID;

                ms.Close();
            }
            catch (Exception e)
            {
                String msg = $"Json desirialize error";
                Eplan.EplApi.Base.BaseException exc = new Eplan.EplApi.Base.BaseException(msg, Eplan.EplApi.Base.MessageLevel.Message);
                exc.FixMessage();
            }
        }

        /// <summary>
        /// Создаение элемента выноски из блока
        /// </summary>
        /// <param name="blo"></param>
        public void Create(Block blo)
        {

            if (blo.Name.Contains(FOOTNOTE_KEY))
            {
                block = blo;
                currentPage = block.Page;
                viewPlacement = block.Group as ViewPlacement;

                GetSourceObject(); // извлечение из имени идентификатор исходного объекта
                UpdateBlock(); //обновление или создание элементов
                //SetSourceObject(sourceItem); //Указать исходный объект

                Deserialize(); //получение сохраненных свойств
                UpdateSubItems(); //обновление элементов
                //setReferencedObject(sourceItem);

                GroupWithViewPlacement();
                //checkSubPlacements();


                //updateSubItems();

                /*Text = label.Contents.GetAsString();
                updateSubItems();*/

                /*
                String msg = $"Block RECREATE_{referenceID} position X:{itemPosition.X} Y:{itemPosition.Y}";
                Eplan.EplApi.Base.BaseException exc = new Eplan.EplApi.Base.BaseException(msg, Eplan.EplApi.Base.MessageLevel.Message);
                exc.FixMessage();*/
            }
        }

        public void UpdateBlockItems(Block blo)
        {

            if (blo.Name.Contains(FOOTNOTE_KEY))
            {
                block = blo;
                currentPage = block.Page;
                viewPlacement = block.Group as ViewPlacement;
                GetSubItems(block.SubPlacements);
                GetSourceObject(); // извлечение из имени идентификатор исходного объекта
                                   //UpdateBlock(); //обновление или создание элементов
                                   //SetSourceObject(sourceItem); //Указать исходный объект

                Deserialize(); //получение сохраненных свойств
                UpdateSubItems(); //обновление элементов
                //setReferencedObject(sourceItem);

                //GroupWithViewPlacement();
                //checkSubPlacements();


                //updateSubItems();

                /*Text = label.Contents.GetAsString();
                updateSubItems();*/

                /*
                String msg = $"Block RECREATE_{referenceID} position X:{itemPosition.X} Y:{itemPosition.Y}";
                Eplan.EplApi.Base.BaseException exc = new Eplan.EplApi.Base.BaseException(msg, Eplan.EplApi.Base.MessageLevel.Message);
                exc.FixMessage();*/
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
            //    CreateBlock();
            //     GetSubItems();
            SetSourceObject(vpart);
        }

        /// <summary>
        /// Создание элемента выноски пустого на странице
        /// </summary>
        /// <param name="page"></param>
        public void Create(Page page)
        {
            currentPage = page;
            //createSubItems();
            UpdateBlock();
            UpdateSubItems();
            //getSubPlacements();
        }

        /// <summary>
        /// Установить начальную точку
        /// </summary>
        /// <param name="point"></param>
        public void SetItemPoint(PointD point)
        {
            itemPosition = point;
            UpdateSubItems();
        }

        /// <summary>
        /// Установить конечную точку
        /// </summary>
        /// <param name="point"></param>
        public void SetNotePoint(PointD point)
        {
            notePosition = point;
            UpdateSubItems();
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
                    label.Justification = TextBase.JustificationType.BottomCenter;
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


                /*
                string name = block.Name;*/
                //PointD p = itemPosition;
                /*Placement[] groupb = block.BreakUp();*/
                /*
                block = new Block();
                block.Create(currentPage, group);
                block.Name = name;*/

                //getSubPlacements();
                /*
                Placement[] groupnew = group.Concat(new Placement[] { 2 }).ToArray();
                block.Create(block.SubPlacements,)
                block.SubPlacements.Contains(label) block.SubPlacements.
                    block.*/

                // }

                //getSubPlacements();

                safetyPoint.Commit();
            }

        }
        public void CreateBlock()
        {
            CreateBlock(subItems.ToArray());
        }
        public void CreateBlock(Placement[] items)
        {
            ///
            /*
             * При создании блока переданные элементы удаляются со старницы и объеденяются в блок 
            */
            //Placement[] group = { label, itemline, noteline, startpoint, jsontext, propid };

            //subItems.AddRange(new List<Placement>{ label, itemline, noteline, startpoint, jsontext, propid });

            ///TODO: Убрать создание блока в отдельную функцию
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

        /*
        public void lockBlock()
        {
            Placement[] group = { label, itemline, noteline, startpoint, jsontext };
            block = new Block();
            block.Create(currentPage, group);
            block.Name = FOOTNOTE_KEY;

            getSubPlacements();
            //setReferencedObject()
        }*/

        /// <summary>
        /// Обновление блока
        /// При изменении вложенных компанентов необходимо пересобрать блок для корректировки BoundingBox
        /// </summary>
        public void UpdateBlock()
        {

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                //Если блок создан ломаем его и пересобираем
                if (block != null)
                {

                    //получить обзор модели на которм лежит блок
                    viewPlacement = block.Group as ViewPlacement;
                    Placement[] items = block.BreakUp();
                    GetSubItems(items); //получили существующие экземпляры после извлечения из блока
                    block = null;

                }

                CreateSubItems(); //создание недостающих элементов
                //updateSubItems(); //обновление текстов и расположения
                CreateBlock(); //объединенить в блок
                GetSubItems(block.SubPlacements); //получили существующие экземпляры после объединения в блок
                if (sourceItem != null) //Подумать
                    SetSourceObject(sourceItem); //устанавливаем ссылку на исходный объект, (ссылку на который получили ранее!)
                safetyPoint.Commit();
            }

            //getSubPlacements();
        }


        /// <summary>
        /// Обновление блока
        /// При изменении вложенных компанентов необходимо пересобрать блок для корректировки BoundingBox
        /// </summary>
        public void UpdateBlock_original()
        {

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                //Если блок создан ломаем его и пересобираем
                if (block != null)
                {
                    //получить обзор модели на которм лежит блок
                    viewPlacement = block.Group as ViewPlacement;
                    Placement[] items = block.BreakUp();
                    GetSubItems(items); //получили существующие экземпляры после извлечения из блока
                    block = null;
                }

                CreateSubItems(); //создание недостающих элементов
                //updateSubItems(); //обновление текстов и расположения
                CreateBlock(); //объединенить в блок
                GetSubItems(block.SubPlacements); //получили существующие экземпляры после объединения в блок
                if (sourceItem != null) //Подумать
                    SetSourceObject(sourceItem); //устанавливаем ссылку на исходный объект, (ссылку на который получили ранее!)
                safetyPoint.Commit();
            }

            //getSubPlacements();
        }

        /// <summary>
        /// Обновленеи Вложенных в блок элементов
        /// </summary>
        public void UpdateSubItems()
        {

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                //if (itemline.IsTransient) return;

                if (STARTPOINT)
                {
                    //update lines
                    Pen penpoint = new Pen();
                    penpoint.ColorId = 0;
                    penpoint.StyleId = 0;
                    penpoint.StyleFactor = -16002;
                    penpoint.Width = 0.0;
                    penpoint.LineEndType = 0;

                    startpoint.SetCircle(itemPosition, STARTPOINTRADIUS);
                    startpoint.IsSurfaceFilled = true;
                    startpoint.Pen = penpoint;
                }
                else
                {
                    Pen penpoint = new Pen();
                    penpoint.ColorId = 0;
                    penpoint.StyleId = 0;
                    penpoint.StyleFactor = -16002;
                    penpoint.Width = 0.0;
                    penpoint.LineEndType = 0;

                    startpoint.IsSurfaceFilled = true;
                    startpoint.Pen = penpoint;
                    startpoint.SetCircle(itemPosition, 0.0);
                }


                if (jsontext != null)
                    jsontext.Location = notePosition;


                //update Text
                Text = GetSourceObjectProperty();
                MultiLangString mls = new MultiLangString();
                mls.SetAsString(Text);
                label.Contents = mls;
                label.Height = TEXTHEIGHT;
                label.TextColorId = (Text == "-1") ? (short)1 : (short)-16002; // если свойство не прочиталось красим в красный

                //itemline.StartPoint = itemPosition;
                itemline.EndPoint = notePosition;

                double textwidth = label.GetBoundingBox()[1].X - label.GetBoundingBox()[0].X + TEXTHEIGHT;
                //textwidth = textwidth;/** (sourceItem.Group as ViewPlacement).Scale;*/

                noteline.StartPoint = notePosition;

                PointD endpoint = notePosition;
                PointD labelpoint = notePosition;

                if (notePosition.X > itemPosition.X ^ INVERTDIRECTION)
                {
                    endpoint.X += textwidth;
                    noteline.EndPoint = endpoint;

                    labelpoint.X += textwidth / 2;
                    label.Location = labelpoint;
                }
                else
                {
                    endpoint.X -= textwidth;
                    noteline.EndPoint = endpoint;

                    labelpoint.X -= textwidth / 2;
                    label.Location = labelpoint;
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
                //Properties.Function.FUNC_COMMENT

                label = array.ElementAtOrDefault(0) as Text;
                itemline = array.ElementAtOrDefault(1) as Line;
                noteline = array.ElementAtOrDefault(2) as Line;
                startpoint = array.ElementAtOrDefault(3) as Arc;
                jsontext = array.ElementAtOrDefault(4) as Text;
                propid = array.ElementAtOrDefault(5) as Text;
                subItems = new List<Placement> { label, itemline, noteline, startpoint, jsontext, propid };

                // Если нуль то создать и присвоить значения
                //block.BreakUp();
                /*if (
                    label == null ||
                    itemline == null ||
                    noteline == null ||
                    startpoint == null ||
                    jsontext == null ||
                    propid == null || true
                    )
                {

                    //Placement[] bp = block.BreakUp();
                    //пересоздать блок, создав элементы недостающие
                    createSubItems();
                    //getSubPlacements();
                    
                    updateBlock();
                    setReferencedObject(sourceItem);
                    GroupWithReferencedObject();
                    updateSubItems();
                }*/
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message, "FootNote", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //создаем недостающие
                //createSubItems();

                if (result == DialogResult.Abort)
                {

                }
            }

        }

        ///// <summary>
        ///// Разбор блока на вложенные элементы
        ///// </summary>
        //public void GetSubItems()
        //{
        //    try
        //    {
        //        //Properties.Function.FUNC_COMMENT

        //        label = block.SubPlacements.ElementAtOrDefault(0) as Text;
        //        itemline = block.SubPlacements.ElementAtOrDefault(1) as Line;
        //        noteline = block.SubPlacements.ElementAtOrDefault(2) as Line;
        //        startpoint = block.SubPlacements.ElementAtOrDefault(3) as Arc;
        //        jsontext = block.SubPlacements.ElementAtOrDefault(4) as Text;
        //        propid = block.SubPlacements.ElementAtOrDefault(5) as Text;
        //        subItems = new List<Placement> { label, itemline, noteline, startpoint, jsontext, propid };
        //        //block.BreakUp();
        //        /*if (
        //            label == null ||
        //            itemline == null ||
        //            noteline == null ||
        //            startpoint == null ||
        //            jsontext == null ||
        //            propid == null || true
        //            )
        //        {

        //            //Placement[] bp = block.BreakUp();
        //            //пересоздать блок, создав элементы недостающие
        //            createSubItems();
        //            //getSubPlacements();

        //            updateBlock();
        //            setReferencedObject(sourceItem);
        //            GroupWithReferencedObject();
        //            updateSubItems();
        //        }*/
        //    }
        //    catch (Exception e)
        //    {
        //        DialogResult result = MessageBox.Show(e.Message, "FootNote", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //        //создаем недостающие
        //        //createSubItems();

        //        if (result == DialogResult.Abort)
        //        {

        //        }
        //    }

        //}

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
                sourceItem = si_vp.Source;
            }
            else
            {
                sourceItem = si_p3d;
            }
            /*
            if (sourceItem != null)
                Text = getObjectProperty(sourceItem);*/
            //Text = sourceItem.Source.Properties[PROPERTYID].ToInt().ToString();
        }

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        public string GetSourceObjectProperty()
        {
            return GetSourceObjectProperty(sourceItem);
        }

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        /// <param name="so">исходный объект</param>
        /// <returns></returns>
        public string GetSourceObjectProperty(Placement3D so)
        {

            String result = "-1";

            if (so != null)
            {
                switch (PROPERTYID)
                {
                    case PropertiesList.P20450:
                        result = so.Properties[20450].ToInt().ToString();
                        break;
                    case PropertiesList.P20008:
                        result = so.Properties[20008].ToString();
                        break;
                    case PropertiesList.P20487:

                        Eplan.EplApi.DataModel.E3D.Function3D e3dc = so as Eplan.EplApi.DataModel.E3D.Function3D;
                        if (e3dc == null) { MessageBox.Show("Недействительный объект источника"); break; }

                        ArticleReference ar = e3dc.ArticleReferences.FirstOrDefault();
                        if (ar == null || ar.IsTransient)
                        {
                            //Попытка найти номер в связанных элементах
                            var arr = e3dc.CrossReferencedObjectsAll.Where(a => (a as Function3D).ArticleReferences.Count() > 0);
                            StorableObject arrItem = null;
                            if (arr != null)
                            {
                                arrItem = arr.FirstOrDefault();
                                ar = (arrItem as Function3D).ArticleReferences.FirstOrDefault();
                            }
                        }

                        if (ar == null || ar.IsTransient)
                        { MessageBox.Show("Недействительная ссылка изделия объекта источника"); break; }

                        if (ar.Properties.Exists(20487) == false) { MessageBox.Show("Несуществующее свойство ссылки изделия объекта источника"); break; }

                        if (ar.Properties[20487].IsEmpty)
                        {
                            //MessageBox.Show("Пустой Номер позиции");
                            result = "-1";
                            break;
                        }

                        result = ar.Properties[20487].ToInt().ToString();
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Присвоить исходный объект
        /// </summary>
        /// <param name="vpartID">ID объекта</param>
        //public void setReferencedObject(String vpartID)
        //{

        //    using (SafetyPoint safetyPoint = SafetyPoint.Create())
        //    {
        //        string objectId = vpartID; //get object id
        //        int idxOfSlash = objectId.IndexOf("/", 1, objectId.Length - 1, StringComparison.InvariantCultureIgnoreCase);    //get index of first separator
        //        string objectIdWithoutProjectId = objectId.Substring(idxOfSlash + 1, (objectId.Length - idxOfSlash - 1));   //cut off value before first separator together with this separator
        //        String referenceID = objectIdWithoutProjectId;

        //        StorableObject obj = null;
        //        StorableObject.TryParseIdentifier(vpartID, ref obj);

        //        sourceItem = obj as Placement3D;

        //        if (sourceItem != null)
        //            Text = getObjectProperty(sourceItem);
        //        else
        //        {
        //            MessageBox.Show("setReferencedObject Недействительная ссылка на объекта источника"); 
        //        }

        //        /*
        //        if (sourceItem.Source.Properties.Exists(PROPERTYID))
        //        {
        //            Text = (sourceItem.Source as Eplan.EplApi.DataModel.E3D.Component).ArticleReferences.First().Properties[PROPERTYID].ToString();
        //            //.Properties[PROPERTYID].ToString();
        //            //Text = sourceItem.Source.Properties[PROPERTYID].ToInt().ToString();
        //        }
        //        */
        //        block.Name = FOOTNOTE_KEY + referenceID;
        //        safetyPoint.Commit();
        //    }
        //}

        /// <summary>
        /// Присвоить исходный объект
        /// </summary>
        /// <param name="vpart">элемент обзора модели</param>
        public void SetSourceObject(ViewPart vpart)
        {
            if (vpart != null)
                SetSourceObject(vpart.Source);
        }

        public void SetSourceObject(Placement3D pl3d)
        {

            if (pl3d != null)
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    //string objectId = vpart.Properties.PROPUSER_DBOBJECTID; //get object id
                    string objectId = pl3d.Properties.PROPUSER_DBOBJECTID; //get object id
                    int idxOfSlash = objectId.IndexOf("/", 1, objectId.Length - 1, StringComparison.InvariantCultureIgnoreCase);    //get index of first separator
                    string objectIdWithoutProjectId = objectId.Substring(idxOfSlash + 1, (objectId.Length - idxOfSlash - 1));   //cut off value before first separator together with this separator
                    String referenceID = objectIdWithoutProjectId;
                    sourceItem = pl3d;
                    if (sourceItem != null)
                        Text = GetSourceObjectProperty(sourceItem);

                    //Text = vpart.Source.Properties[PROPERTYID].ToInt().ToString();

                    block.Name = FOOTNOTE_KEY + referenceID;
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

        public void GroupWithViewPlacement(ViewPlacement vp)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                if (vp != null)
                {
                    vp.InsertSubPlacement(block);
                    //sourceItem.Group.InsertSubPlacement(block);
                    safetyPoint.Commit();
                }
            }
        }

        ///// <summary>
        ///// Обновление текстов выносок,
        ///// используется при обновлении отчетов
        ///// </summary>
        ///// <param name="block"></param>
        //public static void updateBlockProperties(Block block)
        //{

        //    if (!block.IsTransient)
        //    {
        //        using (SafetyPoint safetyPoint = SafetyPoint.Create())
        //        {
        //            if (block.Name.Contains(FOOTNOTE_KEY))
        //            {
        //                String objID = block.Name.Split('#').Last().Replace('_', '/');
        //                string objIDDB = block.DatabaseIdentifier + "/" + objID;

        //                StorableObject obj = null;
        //                StorableObject.TryParseIdentifier(objIDDB, ref obj);

        //                Text textblock = block.SubPlacements.First(placement => placement is Text) as Text;

        //                string val = "empty";
        //                if (obj != null)
        //                {
        //                    Placement3D source = obj as Placement3D;

        //                    FootnoteItem note = new FootnoteItem();
        //                    note.Create(block);
        //                    //TODO ЗАменить на Update dcnhjtyysq

        //                    if (note != null)
        //                    {
        //                        val = note.GetSourceObjectProperty(source);
        //                        // Text = getObjectProperty(sourceItem);
        //                        //val = source.Source.Properties[note.PROPERTYID].ToInt().ToString();
        //                    }
        //                }
        //                else
        //                {
        //                    val = "Error";
        //                    textblock.TextColorId = 1;
        //                }

        //                MultiLangString labeltext = new MultiLangString();
        //                labeltext.SetAsString(val);
        //                textblock.Contents = labeltext;
        //            }
        //            safetyPoint.Commit();
        //        }
        //    }
        //}
    }
}

