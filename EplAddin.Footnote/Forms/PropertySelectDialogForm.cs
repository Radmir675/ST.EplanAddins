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
            var properties = Placement3D.FunctionDefinition.Properties.ExistingValues;



            List<string> s = new List<string>();
            foreach (var property in Eplan.EplApi.DataModel.Properties.AllPlacement3DPropIDs)
            {
                try
                {
                    PropertyDefinition propertyDefinition = new PropertyDefinition(property);
                    bool IsIndexed = propertyDefinition.IsIndexed;
                    var fff = property.Definition.Name;
                    var ss = property.Definition.Id;
                    if (IsIndexed /*&& !Placement3D.Properties[property].IsEmpty*/)
                    {

                        var value = Placement3D.Properties[property];
                        var name = property.Definition.Name;
                        s.Add(value);

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
