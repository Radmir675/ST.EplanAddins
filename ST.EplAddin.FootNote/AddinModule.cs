using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.FootNote
{

    /* TODO
    -добавить обработку присвоения формата
    -добавить иконку
    -добавить UNDO
    +добавить в настройки отображение курсора нормальный упрощенный
    -точка стрелочка опцией кружок в невыделяемый слой?
    -запретить изменять размер окна
    -локализация
    -обработать TAB при вставке
    -после создания блока начальную точку блока переместить к объекту
    */

    public class AddinModule : IEplAddIn
    {
        private Eplan.EplApi.Base.TraceListener m_oTrace;
        private ContextActionEventListener m_ContextActionEventListener;
        public bool OnInit()
        {
            m_oTrace = new Eplan.EplApi.Base.TraceListener();
            System.Diagnostics.Trace.Listeners.Add(m_oTrace);
            m_ContextActionEventListener = new ContextActionEventListener();
            App app = new();//загрузка всего необходимого для программы
            return true;
        }

        public bool OnInitGui()
        {
            Eplan.EplApi.Gui.Menu oMenu = new Eplan.EplApi.Gui.Menu();
            uint MenuIDPopupComments = oMenu.AddPopupMenuItem(
                "Сноски >", "Вставить сноску", "XGedStartInteractionAction /Name:XGedFootnote",
                "Выберите объект на пространстве листа", 37265, 0, false, false);
            oMenu.AddMenuItem("Параметры", "FootnoteSettings", "Настройки выносок", MenuIDPopupComments, 1, true, false);


            return true;
        }

        public bool OnExit()
        {
            System.Diagnostics.Trace.Listeners.Remove(m_oTrace);
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
