using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using IDataObject = System.Windows.IDataObject;

namespace ST.EplAddin.Article_AddImageContextDialog
{
    class Class1
    {

        public void Cipboard()
        {
            IDataObject iData = Clipboard.GetDataObject();
            bool isImage = Clipboard.ContainsImage();

            if (isImage)
            {

                //get data
                BitmapSource bs = Clipboard.GetImage();

                //save png
                /*using (var fileStream = new FileStream(FileName, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bs));
                    encoder.Save(fileStream);
                }*/

            }


        }
        public void Save()
        {
            IDataObject iData = Clipboard.GetDataObject();
            bool isImage = Clipboard.ContainsImage();

            if (isImage)
            {

                //get current project path
                SelectionSet Set = new SelectionSet();
                Set.LockProjectByDefault = false;
                Set.LockSelectionByDefault = false;
                Project CurrentProject = Set.GetCurrentProject(true);

                string path = Environment.ExpandEnvironmentVariables("$(IMG)");
                string ImageDirectory = CurrentProject.ImageDirectory;
                return;
                //set file name random&
                string FileNameRnd = Path.GetRandomFileName();
                string FileName = ImageDirectory + "\\" + FileNameRnd + ".png";

                //get data
                BitmapSource bs = Clipboard.GetImage();

                //save png
                using (var fileStream = new FileStream(FileName, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bs));
                    encoder.Save(fileStream);
                }

            }


        }

    }
}
