using Eplan.EplSDK.WPF.Interfaces;
using Eplan.EplSDK.WPF.Interfaces.DialogServices;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ST.EplAddin.PlacementNavigator
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Navigator : UserControl, IDialog, IDialogBar, IDialogComponentAccess, ICallingContext, IDialogState, IDialogAction, IDialogClose, IElementStateAccess
    {
        public string Caption { get; }
        public bool IsTabsheet { get; }
        public int UniqueBarID { get; }
        public IDialogComponent Component { get; set; }
        public object Context { get; set; }
        public IDialogStateManager DialogStateManager { get; set; }
        public IElementStateCollection ElementStateCollection { get; }

        public Navigator()
        {
            // Init
            ElementStateCollection = new Eplan.EplSDK.WPF.Controls.Persistency.ElementStateCollection();
            ElementStateCollection.Load(nameof(Navigator));

            InitializeComponent();
            Caption = "Навигатор символов";
            IsTabsheet = false;

            // Use Class name for uniqueid
            // ReSharper disable once PossibleNullReferenceException
            var className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            UniqueBarID = className.GetHashCode();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void init()
        {
        }

        public bool isValid()
        {
            return true;
        }

        public void reload()
        {
            this.InvalidateVisual();
        }

        public void save()
        {
        }

        public void close()
        {
        }

    }
}
