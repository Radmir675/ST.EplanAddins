using ST.EplAddin.FootNote.Models;
using System;
using System.Windows.Media;


namespace ST.EplAddin.FootNote.ViewModels
{
    internal class MainPropertyWindowVM : ViewModelBase
    {
        public string Title { get; set; } = "Формат выноски";
        public double TextHeight { get; set; } = 2.5;
        private double? _circleRadius;
        public double? CircleRadius
        {
            get => _circleRadius;
            set
            {
                if (Nullable.Equals(value, _circleRadius)) return;
                _circleRadius = value;
                OnPropertyChanged();
            }
        }

        public double LineThickness { get; set; } = 0.18;
        public Color TextColor { get; set; } = Color.FromRgb(0, 0, 127);
        public Color LinesColor { get; set; } = Color.FromRgb(0, 0, 127);
        public bool RememberAll { get; set; }
        public string Text { get; set; }
        private Shape _startShape = Shape.Arrow;
        public Shape StartShape
        {
            get => _startShape;
            set
            {
                if (value == _startShape) return;
                _startShape = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsInputCircleRadiusEnabled));
            }
        }
        public Alignment TextAlignment { get; set; } = Alignment.Left;
        public bool IsInputCircleRadiusEnabled
        {
            get
            {
                if (StartShape != Shape.Circle)
                {
                    CircleRadius = null;
                    return false;
                }
                CircleRadius = 0.4;
                return true;
            }
        }

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
