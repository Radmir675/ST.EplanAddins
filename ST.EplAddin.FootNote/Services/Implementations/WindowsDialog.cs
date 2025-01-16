using Microsoft.Extensions.DependencyInjection;
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
        public bool ShowPropertiesWindow()
        {
            var window = _services.GetRequiredService<MainPropertyWindow>();
            bool? result = window.ShowDialog();
            return result.GetValueOrDefault(false);
        }

    }
}
