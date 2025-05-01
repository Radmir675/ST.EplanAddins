using System;
using System.Drawing;

namespace ST.EplAddin.PlcEdit
{
    public class PlcDataModelView : ViewModelBase, ICloneable
    {
        private string _devicePointDescription;
        private string _plcAdress;
        private string _datatype;
        private string _symbolicAdress;
        private string _functionText;
        private string _dt;
        private string _identicalDt;
        private string _devicePointDesignation;
        private string _functionDefinition;
        private string _symbolicAdressDefined;
        private string _functionType;
        private int _functionTypeId;
        private string _terminalId;
        private string _deviceNameShort;
        private string _devicePinNumber;
        private Image _statusImage;

        public string DevicePointDescription
        {
            get => _devicePointDescription;
            set
            {
                if (value == _devicePointDescription) return;
                _devicePointDescription = value;
                OnPropertyChanged();
            }
        } //DI3

        public string PLCAdress
        {
            get => _plcAdress;
            set
            {
                if (value == _plcAdress) return;
                _plcAdress = value;
                OnPropertyChanged();
            }
        } //I131.7

        public string Datatype
        {
            get => _datatype;
            set
            {
                if (value == _datatype) return;
                _datatype = value;
                OnPropertyChanged();
            }
        } //BOOL

        public string SymbolicAdress
        {
            get => _symbolicAdress;
            set
            {
                if (value == _symbolicAdress) return;
                _symbolicAdress = value;
                OnPropertyChanged();
            }
        } //DO_xLinAct2CP

        public string FunctionText
        {
            get => _functionText;
            set
            {
                if (value == _functionText) return;
                _functionText = value;
                OnPropertyChanged();
            }
        } //Линейный актуатор 2 в позицию калибровки

        public string DT
        {
            get => _dt;
            set
            {
                if (value == _dt) return;
                _dt = value;
                OnPropertyChanged();
            }
        } //+S2-2A5:4 имя полное

        public string IdenticalDT
        {
            get => _identicalDt;
            set
            {
                if (value == _identicalDt) return;
                _identicalDt = value;
                OnPropertyChanged();
            }
        } // 2A5

        public string DevicePointDesignation
        {
            get => _devicePointDesignation;
            set
            {
                if (value == _devicePointDesignation) return;
                _devicePointDesignation = value;
                OnPropertyChanged();
            }
        } //-X10:34

        public string FunctionDefinition
        {
            get => _functionDefinition;
            set
            {
                if (value == _functionDefinition) return;
                _functionDefinition = value;
                OnPropertyChanged();
            }
        } //Вывод устройства ПЛК, Дискретный вход

        public string SymbolicAdressDefined
        {
            get => _symbolicAdressDefined;
            set
            {
                if (value == _symbolicAdressDefined) return;
                _symbolicAdressDefined = value;
                OnPropertyChanged();
            }
        } //символический адрес определен если он не пустой значит вывод куда-то присвоен

        public string FunctionType
        {
            get => _functionType;
            set
            {
                if (value == _functionType) return;
                _functionType = value;
                OnPropertyChanged();
            }
        } //многополюсный или обзор
        public int FunctionTypeId
        {
            get => _functionTypeId;
            set
            {
                if (value == _functionTypeId) return;
                _functionTypeId = value;
                OnPropertyChanged();
            }
        } //многополюсный или обзор/1//3
        public string TerminalId
        {
            get => _terminalId;
            set
            {
                if (value == _terminalId) return;
                _terminalId = value;
                OnPropertyChanged();
            }
        } //id вывода ПЛК

        public string DeviceNameShort
        {
            get => _deviceNameShort;
            set
            {
                if (value == _deviceNameShort) return;
                _deviceNameShort = value;
                OnPropertyChanged();
            }
        } //A5

        public string DevicePinNumber
        {
            get => _devicePinNumber;
            set
            {
                if (value == _devicePinNumber) return;
                _devicePinNumber = value;
                OnPropertyChanged();
            }
        } //5 

        public Image StatusImage
        {
            get => _statusImage;
            set
            {
                if (Equals(value, _statusImage)) return;
                _statusImage = value;
                OnPropertyChanged();
            }
        } //image of type
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
