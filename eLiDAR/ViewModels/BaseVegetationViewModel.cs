using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using System.Linq;

namespace eLiDAR.ViewModels
{
    public class BaseVegetationViewModel : INotifyPropertyChanged {

        public VEGETATION _vegetation;
        public INavigation _navigation;
        public IValidator _soilValidator;
        public IVegetationRepository _vegetationRepository;
        public string _fk;
        public List<PickerItemsString> ListVeg  = PickerService.VegItems().ToList();
        private string _getscientific;

        public event PropertyChangedEventHandler PropertyChanged2;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName]string propertyName =null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string VEGETATIONID
        {
            get => _vegetation.VEGETATIONID;
            set
            {
                _vegetation.VEGETATIONID = value;
                NotifyPropertyChanged("VEGETATIONID");
            }
        }
        public string PLOTID
        {
            get => _vegetation.PLOTID;
            set
            {
                _vegetation.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }
        public string SPECIES
        {
            get => _vegetation.VSNSPECIESCODE;
            set
            {
                _vegetation.VSNSPECIESCODE = value;
               NotifyPropertyChanged("SPECIES");
                SetScientific();
            }
        }
        
        void SetScientific()
        {
           GetScientific = PickerService.GetItem(ListVeg, SPECIES).NAME;
        }
        public string GetScientific
        {
            get
            {
                return _getscientific;
            }
            set {
                _getscientific = value;
                NotifyPropertyChanged("GetScientific");
            }
            
        }
        public int QUAD1
        {
            get => _vegetation.QUAD1;
            set
            {
                _vegetation.QUAD1 = value;
                NotifyPropertyChanged("QUAD1");
            }
        }
        public int QUAD2
        {
            get => _vegetation.QUAD2;
            set
            {
                _vegetation.QUAD2 = value;
                NotifyPropertyChanged("QUAD2");
            }
        }
        public int QUAD3
        {
            get => _vegetation.QUAD3;
            set
            {
                _vegetation.QUAD3 = value;
                NotifyPropertyChanged("QUAD3");
            }
        }
        public int QUAD4
        {
            get => _vegetation.QUAD4;
            set
            {
                _vegetation.QUAD4 = value;
                NotifyPropertyChanged("QUAD4");
            }
        }
        public int ELCLAYER3
        {
            get => _vegetation.ELCLAYER3;
            set
            {
                _vegetation.ELCLAYER3 = value;
                NotifyPropertyChanged("ELCLAYER3");
            }
        }
        public int ELCLAYER4
        {
            get => _vegetation.ELCLAYER4;
            set
            {
                _vegetation.ELCLAYER4 = value;
                NotifyPropertyChanged("ELCLAYER4");
            }
        }
        public int ELCLAYER5
        {
            get => _vegetation.ELCLAYER5;
            set
            {
                _vegetation.ELCLAYER5 = value;
                NotifyPropertyChanged("ELCLAYER5");
            }
        }
        public int ELCLAYER6
        {
            get => _vegetation.ELCLAYER6;
            set
            {
                _vegetation.ELCLAYER6 = value;
                NotifyPropertyChanged("ELCLAYER6");
            }
        }
        public int ELCLAYER7
        {
            get => _vegetation.ELCLAYER7;
            set
            {
                _vegetation.ELCLAYER7 = value;
                NotifyPropertyChanged("ELCLAYER7");
            }
        }

        List<VEGETATION> _vegetationList;
        public List<VEGETATION> VegetationList
        {
            get => _vegetationList;
            set
            {
                _vegetationList = value;
                NotifyPropertyChanged("VegetationList");
            }
        }

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = ""){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
