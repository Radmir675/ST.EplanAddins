namespace ST.EplAddin.Footnote
{
    public partial class Footnote_InsertInteraction
    {
        private enum State
        {
            Init,
            Selection,
            SourcePoint,
            TargetPoint,
            Finished
        }
    }
}
