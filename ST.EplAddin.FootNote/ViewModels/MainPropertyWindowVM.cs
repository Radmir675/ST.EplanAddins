using ST.EplAddin.FootNote.Models;

namespace ST.EplAddin.FootNote.ViewModels
{
    internal class MainPropertyWindowVM
    {
        public string Title { get; set; } = "Формат выноски";
        public double TextHeight { get; set; } = 2.5;
        public double CircleRadius { get; set; } = 0.4;
        public double LineThickness { get; set; } = 0.18;
        public string TextColor { get; set; }
        public string LinesColor { get; set; }
        public bool RememberAll { get; set; }
        public string Text { get; set; }
        public Shape StartShape { get; set; } = Shape.Arrow;
        public Alignment TextAlignment { get; set; } = Alignment.Left;

        #region Commands

        private RelayCommand _nextFootNote;
        public RelayCommand NextFootNote
        {
            get
            {
                return _nextFootNote ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _previousFootNote;
        public RelayCommand PreviousFootNote
        {
            get
            {
                return _previousFootNote ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _copyStyle;
        public RelayCommand CopyStyle
        {
            get
            {
                return _copyStyle ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _reset;
        public RelayCommand Reset
        {
            get
            {
                return _reset ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _delete;
        public RelayCommand Delete
        {
            get
            {
                return _delete ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _addEplanProperties;
        public RelayCommand AddEplanProperties
        {
            get
            {
                return _addEplanProperties ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _addSpecialSign;
        public RelayCommand AddSpecialSign
        {
            get
            {
                return _addSpecialSign ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand _apply;


        public RelayCommand Apply
        {
            get
            {
                return _addSpecialSign ??= new RelayCommand(obj =>
                {

                });
            }
        }
        #endregion

    }
}
