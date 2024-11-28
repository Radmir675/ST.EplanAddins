using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ST.EplAddin.FootNote
{
    internal class App
    {
        public void Initialize()
        {
            LoadAssemblies();
        }
        private void LoadAssemblies()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(assemblyFolder!, "*.dll");
            var toLoad = referencedPaths.Where(r =>
            {
                if (!r.Contains("Eplan") && !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                return false;


            }).ToList();

            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
        }
    }
}
