using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.Comments
{
    public class AddinModule : IEplAddIn
    {
        public bool OnExit()
        {
            return true;
        }

        public bool OnInit()
        {
            //   MessageBox.Show("AddinModule TestAction loaded");
            return true;
        }

        public bool OnInitGui()
        {
            Eplan.EplApi.Gui.Menu oMenu = new Eplan.EplApi.Gui.Menu();
            uint MenuIDPopupComments = oMenu.AddPopupMenuItem(
                "Комментарии", "Вставить комментарий", "CommentInsertDialog", "CommentInsertDialog", 35381, 0, false, false);

            uint MenuIDPopupComments2 = oMenu.AddPopupMenuItem(
               "Комментарии", "Вставить комментарий", "CommentInsertDialog", "CommentInsertDialog", 35381, 0, false, false);

            uint MenuIDPopupCommentsShow = oMenu.AddPopupMenuItem(
                "Показать", "Показать комментарий", "EMPTY", "", MenuIDPopupComments, 1, false, false);

            oMenu.AddMenuItem("Показать комментарии \"ReviewNone\"", "CommentManager /show ReviewNone", "", MenuIDPopupCommentsShow, 1, false, false);
            oMenu.AddMenuItem("Показать комментарии \"Accepted\"", "CommentManager /show Accepted", "", MenuIDPopupCommentsShow, 1, false, false);
            oMenu.AddMenuItem("Показать комментарии \"Rejected\"", "CommentManager /show Rejected", "", MenuIDPopupCommentsShow, 1, false, false);
            oMenu.AddMenuItem("Показать комментарии \"Cancelled\"", "CommentManager /show Cancelled", "", MenuIDPopupCommentsShow, 1, false, false);
            oMenu.AddMenuItem("Показать комментарии \"Completed\"", "CommentManager /show Completed", "", MenuIDPopupCommentsShow, 1, false, false);

            oMenu.AddMenuItem("TEST \"Completed\"", "MyPartSelectionGuiIGfWindonAssignData", "", MenuIDPopupCommentsShow, 1, false, false);


            uint MenuIDPopupCommentsHide = oMenu.AddPopupMenuItem(
                "Скрыть", "Скрыть комментарий", "EMPTY", "", MenuIDPopupComments, 1, false, false);

            oMenu.AddMenuItem("Скрыть комментарии \"ReviewNone\"", "CommentManager /hide ReviewNone", "", MenuIDPopupCommentsHide, 1, false, false);
            oMenu.AddMenuItem("Скрыть комментарии \"Accepted\"", "CommentManager /hide Accepted", "", MenuIDPopupCommentsHide, 1, false, false);
            oMenu.AddMenuItem("Скрыть комментарии \"Rejected\"", "CommentManager /hide Rejected", "", MenuIDPopupCommentsHide, 1, false, false);
            oMenu.AddMenuItem("Скрыть комментарии \"Cancelled\"", "CommentManager /hide Cancelled", "", MenuIDPopupCommentsHide, 1, false, false);
            oMenu.AddMenuItem("Скрыть комментарии \"Completed\"", "CommentManager /hide Completed", "", MenuIDPopupCommentsHide, 1, false, false);

            uint MenuIDPopupCommentsRemove = oMenu.AddPopupMenuItem(
                "Удалить", "Удалить комментарий", "EMPTY", "", MenuIDPopupComments, 1, false, false);

            oMenu.AddMenuItem("Удалить комментарии \"ReviewNone\"", "CommentManager /remove ReviewNone", "", MenuIDPopupCommentsRemove, 1, false, false);
            oMenu.AddMenuItem("Удалить комментарии \"Accepted\"", "CommentManager /remove Accepted", "", MenuIDPopupCommentsRemove, 1, false, false);
            oMenu.AddMenuItem("Удалить комментарии \"Rejected\"", "CommentManager /remove Rejected", "", MenuIDPopupCommentsRemove, 1, false, false);
            oMenu.AddMenuItem("Удалить комментарии \"Cancelled\"", "CommentManager /remove Cancelled", "", MenuIDPopupCommentsRemove, 1, false, false);
            oMenu.AddMenuItem("Удалить комментарии \"Completed\"", "CommentManager /remove Completed", "", MenuIDPopupCommentsRemove, 1, false, false);

            uint MenuIDPopupCommentsExportImport = oMenu.AddPopupMenuItem(
                "Экспорт/Импорт", "Показать комментарий", "EMPTY", "", MenuIDPopupComments, 1, false, false);

            oMenu.AddMenuItem("Экспортировать комментарии", "CommentStateExport", "", MenuIDPopupCommentsExportImport, 1, false, false);
            oMenu.AddMenuItem("Импортировать комментарии", "CommentStateImport", "", MenuIDPopupCommentsExportImport, 1, false, false);

            oMenu.AddMenuItem("Конфигурация системы", "RunSystemConfigurationDialog", "", MenuIDPopupCommentsExportImport, 1, false, false);
            //oMenu.AddMenuItem("Присвоить длины кабелям", "CableLengthAssigner", "", MenuIDPopupCommentsExportImport, 1, false, false);
            //oMenu.AddMenuItem("Присвоить длины кабелям arg", "CableLengthAssigner arg", "", MenuIDPopupCommentsExportImport, 1, false, false);
            oMenu.AddMenuItem("Присвоить Длины", "CableLengthAssigner", "", MenuIDPopupCommentsExportImport, 1, false, false);
            oMenu.AddMenuItem("Присвоить Длины в Доп.Поле 1", "CableLengthAssigner /copyTo 1 ", "", MenuIDPopupCommentsExportImport, 1, false, false);
            oMenu.AddMenuItem("Присвоить Длины в Доп.Поле 2", "CableLengthAssigner /copyTo 2 ", "", MenuIDPopupCommentsExportImport, 1, false, false);
            oMenu.AddMenuItem("Присвоить Длины из Доп.Поле 1", "CableLengthAssigner /copyFrom 1 ", "", MenuIDPopupCommentsExportImport, 1, false, false);
            oMenu.AddMenuItem("Присвоить Длины из Доп.Поле 2", "CableLengthAssigner /copyFrom 2 ", "", MenuIDPopupCommentsExportImport, 1, false, false);
            //"C:\Program Files\EPLAN\Platform\2.6.3\Bin\Eplan.exe" / Variant:"Electric P8" / SystemConfiguration:"API"



            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
