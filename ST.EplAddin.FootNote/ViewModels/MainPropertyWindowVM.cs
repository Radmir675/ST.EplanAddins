namespace ST.EplAddin.FootNote.ViewModels
{
    internal class MainPropertyWindowVM
    {
        public string Title { get; set; } = "Условный объект";
        public double TextHeight { get; set; }
        public double CircleRadius { get; set; }
        public double LineThickness { get; set; }
        public string TextColor { get; set; }
        public string LinesColor { get; set; }
        public bool RememberAll { get; set; }
        public string Text { get; set; }
        public string StartShape { get; set; }
        public string TextAlignment { get; set; }

        #region Commands



        #endregion

    }
}
