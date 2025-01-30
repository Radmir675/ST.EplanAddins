using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace EplAddin.ClipboardHelper
{
    class ClipboardAction : IEplAction
    {
        int lastHash = -1;

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string function = "";
            oActionCallingContext.GetParameter("function", ref function);
            function = function.ToLower();


            if (function == "copy")
            {
                lastHash = -1;
                Clipboard.Clear();
            }

            if (function == "paste")
            {
                bool clipboardChanged = false;
                IDataObject iData = Clipboard.GetDataObject();

                bool isImage = Clipboard.ContainsImage();

                if (isImage)
                {
                    //check hash
                    int hash = iData != null ? iData.GetData(DataFormats.Bitmap).GetHashCode() : 0;
                    if (hash != lastHash)
                    {
                        clipboardChanged = true;
                        lastHash = hash;
                    }

                    //get current project path
                    SelectionSet Set = new SelectionSet();
                    Set.LockProjectByDefault = false;
                    Set.LockSelectionByDefault = false;
                    Project CurrentProject = Set.GetCurrentProject(true);
                    string ImageDirectory = CurrentProject.ImageDirectory;

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

                    if (clipboardChanged)
                    {
                        Decider eDecision = new Decider();
                        EnumDecisionReturn eAnswer = eDecision.Decide(EnumDecisionType.eYesNoDecision, "Вставить изображение?", "Вставка", EnumDecisionReturn.eOK, EnumDecisionReturn.eOK, "YES", false, EnumDecisionIcon.eQUESTION);
                        if (eAnswer == EnumDecisionReturn.eYES)
                        {
                            //action
                            new CommandLineInterpreter(true).Execute("XGedStartInteractionAction2D /Name:XGedIaInsertImage /FileName:\"" + FileName + "\"");
                            return true;
                        }
                    }
                }

                bool isText = Clipboard.ContainsText();
                if (isText)
                {
                    //check hash
                    int hash = iData != null ? iData.GetData(DataFormats.Text).GetHashCode() : 0;
                    if (hash != lastHash)
                    {
                        clipboardChanged = true;
                        lastHash = hash;
                    }

                    string text = Clipboard.GetText();
                    if (Uri.IsWellFormedUriString(text, UriKind.Absolute) && text.Contains(".pdf"))
                    {
                        if (clipboardChanged)
                        {
                            Decider eDecision = new Decider();
                            EnumDecisionReturn eAnswer = eDecision.Decide(EnumDecisionType.eYesNoDecision, "Вставить ссылку?", "Вставка", EnumDecisionReturn.eOK, EnumDecisionReturn.eOK, "YES", false, EnumDecisionIcon.eQUESTION);
                            if (eAnswer == EnumDecisionReturn.eYES)
                            {
                                new CommandLineInterpreter(true).Execute("XGedStartInteractionAction2D /Name:XGedIaInsertHyperlink /FileName:\"" + text + "\"");
                                return true;
                            }
                        }
                    }
                }
            }

            ActionManager oMng = new ActionManager();
            Action baseAction = oMng.FindBaseAction(this, true);
            bool result = baseAction.Execute(oActionCallingContext);
            //Clipboard.Clear();
            return result;
        }

        public void GetActionProperties(ref ActionProperties actionProperties) { }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "GfDlgMgrActionIGfWind";
            Ordinal = 98;
            return true;
        }
    }
}
