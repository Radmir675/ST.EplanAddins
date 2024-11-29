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
    }
}
