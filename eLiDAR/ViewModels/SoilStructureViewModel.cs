using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Services;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class SoilStructureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged2;
        public INavigation _navigation;
        public SOIL _thissoil;
        public List<PickerItemsString> ListGrade { get; set; }
        public List<PickerItemsString> ListKind { get; set; }
        public List<PickerItemsString> ListClass { get; set; }

        public ICommand ClearCommand { get; private set; }

        private string _master;
        private string _suffix1;
        private string _suffix2;

        public SoilStructureViewModel(INavigation navigation, SOIL _soil)
        {
            _navigation = navigation;
            _thissoil = _soil;
            ListGrade = PickerService.GradeItems ().ToList();
            ListKind = PickerService.KindItems().ToList();
            ListClass = PickerService.ClassItems().ToList();

            ClearCommand = new Command(() => ClearItems());
            SetCalc();
    }
        void ClearItems()
        {
            _thissoil.STRUCTURE = "";
            _ = _navigation.PopAsync();
       
        }
        private PickerItemsString _selectedMaster = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaster
        {
            get
            {
                _selectedMaster = PickerService.GetItem(ListGrade, MASTER);
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
                _selectedSuffix1 = PickerService.GetItem(ListKind, SUFFIX1);
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
                _selectedSuffix2 = PickerService.GetItem(ListClass, SUFFIX2);
                return _selectedSuffix2;
            }
            set
            {
                SetProperty(ref _selectedSuffix2, value);
                SUFFIX2 = _selectedSuffix2.ID;
                Calc();
            }
        }
        public string SoilTitle
        {
            get => "Soil Structure" ;
            set
            {
            }
        }
        void Calc()
        {
            STRUCTURE = MASTER + SUFFIX1 + SUFFIX2;  
        }
        void SetCalc()
        {
            if (STRUCTURE != null)
            {
                int len = STRUCTURE.Length;
                if (len >= 1) { MASTER = STRUCTURE.Substring(0, 1); }
                if (len >= 2) { SUFFIX1 = STRUCTURE.Substring(1, 2); }
                if (len >= 4) { SUFFIX2 = STRUCTURE.Substring(3); }
            }
        }
        public string STRUCTURE
        {
            get
            {
                return _thissoil.STRUCTURE;
            }

            set
            {
                _thissoil.STRUCTURE = value;
                NotifyPropertyChanged("STRUCTURE");
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
