using Eplan.EplApi.DataModel.E3D;
using Microsoft.Extensions.DependencyInjection;
using ST.EplAddin.FootNote.Forms;
using ST.EplAddin.FootNote.ViewModels;
using ST.EplAddin.FootNote.Views;
using System;

namespace ST.EplAddin.FootNote.Services.Implementations
{
    public class WindowsDialog : IWindowsServiceDialog
    {
        private readonly IServiceProvider _services;
        public WindowsDialog(IServiceProvider services)
        {
            _services = services;
        }
        public bool ShowMainPropertiesWindow()
        {
            var window = _services.GetRequiredService<MainPropertyWindow>();
            bool? result = window.ShowDialog();
            return result.GetValueOrDefault(false);
        }

        public bool ShowFullPropertiesWindow(Placement3D placement3D, out PropertyEplan selectedProperty)
        {
            var window = _services.GetRequiredService<FullPropertiesWindow>();
            var viewModel = new FullPropertiesWindowVM(placement3D);
            window.DataContext = viewModel;
            bool? result = window.ShowDialog();
            selectedProperty = viewModel.CurrentSelectedProperty;
            return result.GetValueOrDefault(false);
        }


    }
}
