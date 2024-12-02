using Microsoft.Extensions.DependencyInjection;
using ST.EplAddin.FootNote.ViewModels;
using ST.EplAddin.FootNote.Views;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ST.EplAddin.FootNote
{
    public class App
    {
        public App()
        {
            LoadAssemblies();
            Services.GetRequiredService<PropertiesWindow>().ShowDialog(); //чтобы стартануть надо сделать вот так
        }
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

        private static IServiceProvider? _services;
        public static IServiceProvider Services => _services ??= InitializeServices().BuildServiceProvider();
        public static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<PropertiesWindowVM>();

            services.AddTransient(S =>
            {
                var model = Services.GetRequiredService<PropertiesWindowVM>();
                var window = new PropertiesWindow { DataContext = model };
                return window;
            });
            return services;
        }
    }
}
