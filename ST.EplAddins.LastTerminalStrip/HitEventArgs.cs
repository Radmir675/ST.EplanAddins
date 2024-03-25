using Eplan.EplApi.DataModel;
using System;

namespace ST.EplAddin.LastTerminalStrip
{
    public class HitEventArgs : EventArgs
    {
        public Project currentProject { get; set; }
        public StorableObject parametr { get; set; }
        public HitEventArgs(Project currentProject, StorableObject parametr)
        {
            this.currentProject = currentProject;
            this.parametr = parametr;
        }

    }
}