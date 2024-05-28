using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.Footnote.ProperyBrowser
{
    public partial class PropertySelectDialogForm : Form
    {
        public Placement3D Placement3D { get; }
        public PropertySelectDialogForm(Placement3D placement3D)
        {
            InitializeComponent();
            Placement3D = placement3D;
            GetProperties();
        }
        public void GetProperties()
        {
            var properties = Placement3D.Properties.GetType().GetProperties();
            List<string> s = new List<string>();
            foreach (var property in Eplan.EplApi.DataModel.Properties.AllPropertyPlacementPropIDs)
            {
                try
                {
                    if (!Placement3D.Properties[property].IsEmpty)
                    {
                        if (Placement3D.Properties[property].Definition.Type == PropertyDefinition.PropertyType.String)
                        {
                            var value = Placement3D.Properties[property];
                            var name = property.Definition.Name;
                            s.Add(value);
                        }
                    }

                }
                catch (System.Exception)
                {


                }
            }
            s.Add("");
        }

    }
}
