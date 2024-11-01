namespace ST.EplAddin.FootNote
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
