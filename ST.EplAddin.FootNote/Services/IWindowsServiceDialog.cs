using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;

namespace ST.EplAddin.FootNote.Services
{
    public interface IWindowsServiceDialog
    {
        bool ShowMainPropertiesWindow();
        bool ShowFullPropertiesWindow(Placement3D placement3D, out PropertyEplan result);

    }
}
