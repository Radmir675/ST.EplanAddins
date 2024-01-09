using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.MasterData;
using Eplan.EplSDK.WPF.EEvent;
using Eplan.EplSDK.WPF.Interfaces.DialogServices;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EplAddin.Article_AddImageContextDialog
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PartsManagementExtensionContent : IDialog
    {
        public string Caption => nameof(PartsManagementExtensionContent);
        public bool IsTabsheet => true;
        public ViewModel ViewModel { get; set; }
        private readonly MDPartsManagement _partsManagement = new MDPartsManagement();
        public PartsManagementExtensionContent()
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                InitializeComponent();
                ViewModel = new ViewModel();
                DataContext = ViewModel;
                try
                {
                    ViewModel.IsReadOnly = _partsManagement.SelectedPartsDatabase.IsReadOnly;
                }
                catch (AccessViolationException e)
                {
                    MessageBox.Show(e.Message);
                }
                // Events, called from Action of this Tab
                WPFDialogEventManager dialogEventManager = new WPFDialogEventManager();
                dialogEventManager.getOnWPFNotifyEvent("XPartsManagementDialog", "SelectItem").Notify += SelectItem;
                dialogEventManager.getOnWPFNotifyEvent("XPartsManagementDialog", "SaveItem").Notify += SaveItem;
                dialogEventManager.getOnWPFNotifyEvent("XPartsManagementDialog", "PreShowTab").Notify += PreShowTab;
                dialogEventManager.getOnWPFNotifyEvent("XPartsManagementDialog", "OpenDatabase").Notify += OpenDatabase;
                dialogEventManager.getOnWPFNotifyEvent("XPartsManagementDialog", "CreateDatabase").Notify += CreateDatabase;
                clip.Click += Cipboard;
                save.Click += Save;
                safetyPoint.Commit();
            }

        }

        public void Cipboard(object sender, RoutedEventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            bool isImage = Clipboard.ContainsImage();

            if (isImage)
            {
                //get data
                BitmapSource bs = Clipboard.GetImage();
                img.Source = bs;
            }
        }

        public void Save(object sender, RoutedEventArgs e)
        {
            string ImagePath = new ProjectManager().Paths.Images;
            bool isEmptyImg = img.Source == null;

            MDPartsDatabaseItem[] items = _partsManagement.GetSelectedPartDatabaseItems(false);

            if (items.Count() == 1 && !isEmptyImg)
            {
                try
                {
                    string filename = "";
                    string man = items.First().Properties.ARTICLE_MANUFACTURER;
                    string art = items.First().Properties.ARTICLE_PARTNR;

                    if (man != "")
                    {
                        filename += man + "\\";

                        bool exists = System.IO.Directory.Exists(ImagePath + "\\" + filename);
                        if (!exists)
                            System.IO.Directory.CreateDirectory(ImagePath + "\\" + filename);
                    }
                    art = art.Replace("\\", "_");
                    art = art.Replace("*", "_");
                    art = art.Replace("?", "_");
                    art = art.Replace("/", "_");
                    art = art.Replace("<", "_");
                    art = art.Replace(">", "_");
                    art = art.Replace("|", "_");
                    art = art.Replace(":", "_");
                    filename += art + ".png";

                    string filePath = ImagePath + "\\" + filename;
                    string filePathShort = "$(MD_IMG)" + "\\" + filename;


                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);



                    MDPartsDatabaseItem mdprt = items.First();

                    items.First().Properties.ARTICLE_PICTUREFILE = filePathShort;
                    _partsManagement.RefreshPartsManagementDialog();

                    EventParameterString eventParameterString2 = new EventParameterString();
                    eventParameterString2.String = "";
                    new Eplan.EplApi.ApplicationFramework.EventManager().Send("ActualiseMenuConditions", eventParameterString2);


                    EventParameterString eventParameterString3 = new EventParameterString();
                    eventParameterString2.String = "";
                    new Eplan.EplApi.ApplicationFramework.EventManager().Send("onModifiedChanged.Bool.Dialog", eventParameterString3);

                    EventParameterString eventParameterString4 = new EventParameterString();
                    eventParameterString2.String = "";
                    new Eplan.EplApi.ApplicationFramework.EventManager().Send("Ged.ClearFastImageCache", eventParameterString4);


                    bool res = _partsManagement.SetModified();
                    //_partsManagement.RefreshPartsManagementDialog();

                    /*
                     17:06:43	onSelChanged.SelectionSet.XPreview	
17:06:43	onSelChanged.SelectionSet.XMessageManagement	
17:06:44	Ged.ClearFastImageCache	
17:06:47	onModifiedChanged.Bool.Dialog
PartsManagement.SaveData
PartsManagement.PartsModified
                     * 
                     */

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                //SetImage(filePathShort);
            }



        }

        public void Clear()
        {
            img.Source = null;
        }


        public void SetImage(string filename)
        {
            MDPartsManagement _partsManagement2 = new MDPartsManagement();
            MDPartsDatabaseItem[] items = _partsManagement2.GetSelectedPartDatabaseItems(false);

            if (items.Count() == 1 && filename != "")
            {
                items.First().Properties.ARTICLE_PICTUREFILE = filename;
            }

            _partsManagement2.RefreshPartsManagementDialog();
        }


        private void CreateDatabase(string data)
        {

        }
        private void OpenDatabase(string data)
        {

        }
        private void PreShowTab(string data)
        {

        }
        private void SelectItem(string data)
        {
            Clear();
        }
        private void SaveItem(string data)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
