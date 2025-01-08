namespace ST.EplAddin.FootNote.FootNote
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
