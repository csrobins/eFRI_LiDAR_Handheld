using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class SoilHorizonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged2;
        public INavigation _navigation;
        public SOIL _thissoil;
        public List<PickerItemsString> ListHorizon { get; set; }
        public List<PickerItemsString> ListHorizonSuffix { get; set; }
        public ICommand ClearCommand { get; private set; }

        private string _master;
        private string _suffix1;
        private string _suffix2;
        private string _suffix3;
        private string _suffix4;


        public SoilHorizonViewModel(INavigation navigation, SOIL _soil)
        {
            _navigation = navigation;
            _thissoil = _soil;
            ListHorizon = PickerService.HorizonItems().ToList();
            ListHorizonSuffix = PickerService.HorizonSuffixItems().ToList();
            ClearCommand = new Command(() => ClearItems());
            SetCalc();
    }
        void ClearItems()
        {
            _thissoil.HORIZON = "";
            _ = _navigation.PopAsync();
       
        }
        private PickerItemsString _selectedMaster = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaster
        {
            get
            {
                _selectedMaster = PickerService.GetItem(ListHorizon, MASTER);
                return _selectedMaster;
            }
            set
            {
                SetProperty(ref _selectedMaster, value);
             
                MASTER = _selectedMaster.ID;
                Calc();
            }
        }
        private PickerItemsString _selectedSuffix1 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedSuffix1
        {
            get
            {
                _selectedSuffix1 = PickerService.GetItem(ListHorizonSuffix, SUFFIX1);
                return _selectedSuffix1;
            }
            set
            {
                SetProperty(ref _selectedSuffix1, value);
              
                SUFFIX1 = _selectedSuffix1.ID;
                Calc();
            }
        }
        private PickerItemsString _selectedSuffix2 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedSuffix2
        {
            get
            {
                _selectedSuffix2 = PickerService.GetItem(ListHorizonSuffix, SUFFIX2);
                return _selectedSuffix2;
            }
            set
            {
                SetProperty(ref _selectedSuffix2, value);
               
                SUFFIX2 = _selectedSuffix2.ID;
                Calc();
            }
        }
        private PickerItemsString _selectedSuffix3 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedSuffix3
        {
            get
            {
                _selectedSuffix3 = PickerService.GetItem(ListHorizonSuffix, SUFFIX3);
                return _selectedSuffix3;
            }
            set
            {
                SetProperty(ref _selectedSuffix3, value);
             
                SUFFIX3 = _selectedSuffix3.ID;
                Calc();
            }
        }
        private PickerItemsString _selectedSuffix4 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedSuffix4
        {
            get
            {
                _selectedSuffix4 = PickerService.GetItem(ListHorizonSuffix, SUFFIX4);
                return _selectedSuffix4;
            }
            set
            {
                SetProperty(ref _selectedSuffix4, value);

                SUFFIX4 = _selectedSuffix4.ID;
                Calc();
            }
        }
        public string SoilTitle
        {
            get => "Soil Horizon" ;
            set
            {
            }
        }
        void Calc()
        {
            HORIZON = MASTER + SUFFIX1 + SUFFIX2 + SUFFIX3 + SUFFIX4;  
        }
        void SetCalc()
        {
            if (HORIZON != null)
            {
                // Need to parse the horizon into parts
                string[] strary = new string[16] {"IIC", "LM", "IIBC", "IIB", "BC", "A", "B", "C", "F","H","L","R","W","Of","Om","Oh"};
                int len;
                int charlen;
                int horizlen;
                foreach(string str in strary) 
                {
                    len = HORIZON.IndexOf(str, 0);
                    if (len == 0) 
                    {
                        charlen = str.Length;
                        horizlen = HORIZON.Length;
                        if (horizlen >= charlen) { MASTER = HORIZON.Substring(0, charlen); }
                        if (horizlen >= charlen + 1) { SUFFIX1 = HORIZON.Substring(charlen, 1); }
                        if (horizlen >= charlen + 2) { SUFFIX2 = HORIZON.Substring(charlen+1, 1); }
                        if (horizlen >= charlen + 3) { SUFFIX3 = HORIZON.Substring(charlen+2, 1); }
                        if (horizlen >= charlen + 4) { SUFFIX4 = HORIZON.Substring(charlen+3, 1); }
                        break;
                    }
                }
            }
        }
        public string HORIZON
        {
            get
            {
                return _thissoil.HORIZON;
            }

            set
            {
                _thissoil.HORIZON = value;
                NotifyPropertyChanged("HORIZON");
            }
        }
        public string MASTER
        {
            get => _master;
            set
            {
                _master = value;

                NotifyPropertyChanged("MASTER");
            }
        }

        public string SUFFIX1
        {
            get => _suffix1;
            set
            {
                _suffix1 = value;

                NotifyPropertyChanged("SUFFIX1");
            }
        }
        public string SUFFIX2
        {
            get => _suffix2;
            set
            {
                _suffix2 = value;

                NotifyPropertyChanged("SUFFIX2");
            }
        }
        public string SUFFIX3
        {
            get => _suffix3;
            set
            {
                _suffix3 = value;

                NotifyPropertyChanged("SUFFIX3");
            }
        }
        public string SUFFIX4
        {
            get => _suffix4;
            set
            {
                _suffix4 = value;
     
                NotifyPropertyChanged("SUFFIX4");
            }
        }

        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
