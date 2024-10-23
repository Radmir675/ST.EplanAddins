using System;
using Block = Eplan.EplApi.DataModel.Block;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;

namespace ST.EplAddin.Footnote.ProperyBrowser
{
    static class FootnoteVerification
    {
        public static String FOOTNOTE_KEY = "FOOTNOTE_OBJID#";

        public static bool IsFootnoteBlock(StorableObject storableObject)
        {
            if (storableObject != null)
            {
                if (storableObject is Block block)
                {
                    return IsFootnoteBlock(block);
                }
            }
            return false;
        }
        public static bool IsFootnoteBlock(Block block)
        {
            var result = (block?.Name.Contains(FOOTNOTE_KEY)).Value;
            return result;
        }
    }
}
