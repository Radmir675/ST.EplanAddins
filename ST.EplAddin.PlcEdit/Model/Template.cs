﻿namespace ST.EplAddin.PlcEdit.Model
{
    public class Template
    {
        public Template(int indexFirstRow, int indexLastRow, string fileName)
        {
            IndexFirstRow = indexFirstRow;
            IndexLastRow = indexLastRow;
            FileName = fileName;
        }

        public int IndexFirstRow { get; set; }
        public int IndexLastRow { get; set; }
        public string FileName { get; set; }
    }
}
