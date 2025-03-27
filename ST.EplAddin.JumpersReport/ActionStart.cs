using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.JumpersReport
{
    internal class ActionStart : IEplAction

    {
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "as";
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {

            return true;


            //int count = oActionCallingContext.GetParameterCount();
            //string[] contextParams = oActionCallingContext.GetParameters();
            //string[] contextStrings = oActionCallingContext.GetStrings();

            //string paramValue = "";
            //oActionCallingContext.GetParameter("_cmdline", ref paramValue);

            ///*
            //if (paramValue == "XLbLabelingStart")
            //{
            //    SelectionSet Set = new SelectionSet();
            //    Project CurrentProject = Set.GetCurrentProject(true);

            //    DMObjectsFinder dmof = new DMObjectsFinder(CurrentProject);
            //    Block[] blocks = dmof.GetStorableObjects(null).OfType<Block>().Where(b => b.Name.Contains("FOOTNOTE_OBJID#")).ToArray();

            //    foreach (Block block in blocks)
            //    {
            //        String name = block.Name;
            //        if (name.Contains("FOOTNOTE_OBJID#"))
            //            FootnoteItem.updateBlock(block);
            //    }
            //}*/



            //ActionManager oMng = new ActionManager();
            //Action oBaseAction = oMng.FindAction("JumpersReport", true);

            //string props = oBaseAction.ActionProperties.ToString();
            //bool res = oBaseAction.Execute(oActionCallingContext);

            //string[] contextParams2 = oActionCallingContext.GetParameters();
            //string[] contextStrings2 = oActionCallingContext.GetStrings();



            //return res;

        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
