using Microsoft.Extensions.DependencyInjection;
using ST.EplAddin.FootNote.Services;
using ST.EplAddin.FootNote.Services.Implementations;
using ST.EplAddin.FootNote.ViewModels;
using ST.EplAddin.FootNote.Views;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ST.EplAddin.FootNote
{
    public class App
    {

        public App()
        {
            LoadAssemblies();
            StartWindow();
        }

        #region DELETE
        private void LoadAppDictionary()
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri(("/ST.EplAddin.FootNote;component/PropertiesWindowDictionary.xaml"), UriKind.RelativeOrAbsolute)
            };

        }
        void StartWindow()
        {
            Services.GetRequiredService<MainPropertyWindow>().ShowDialog(); //чтобы стартануть надо сделать вот так
        }

        #endregion

        #region CheckAssemblies
        private void LoadAssemblies()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(assemblyFolder!, "*.dll");
            var toLoad = referencedPaths.Where(reference => CheckDll(reference, loadedPaths)).ToList();
            try
            {
                toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
            }
            catch (Exception e)
            {
                //listener есть
            }
        }

        private bool CheckDll(string reference, string[] loadedPaths)
        {
            var name = Path.GetFileName(reference);

            if (name.Contains("Eplan")) return false;
            if (name.Contains("EplAddin")) return false;
            if (loadedPaths.Contains(reference, StringComparer.InvariantCultureIgnoreCase)) return false;

            return true;
        }



        #endregion

        #region Services

        private static IServiceProvider? _services;
        public static IServiceProvider Services => _services ??= InitializeServices().BuildServiceProvider();
        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainPropertyWindowVM>();
            services.AddTransient<IWindowsServiceDialog, WindowsDialog>();

            services.AddTransient(S =>
            {
                var model = Services.GetRequiredService<MainPropertyWindowVM>();
                var window = new MainPropertyWindow() { DataContext = model };
                return window;
            });

            return services;

            #endregion
        }
    }
}
