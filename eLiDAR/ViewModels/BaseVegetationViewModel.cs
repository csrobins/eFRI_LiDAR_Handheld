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
using eLiDAR.Utilities;
using eLiDAR.Validator;
using FluentValidation.Results;

namespace eLiDAR.ViewModels
{
    public class BaseVegetationViewModel : INotifyPropertyChanged {

        public VEGETATION _vegetation;
        public INavigation _navigation;
     
        public IVegetationRepository _vegetationRepository;
        public string _fk;
        public List<PickerItemsString> ListVeg  = PickerService.VegItems().ToList();
        private string _getscientific;
        private Utils util = new Utils();

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
            else
            {
                IsChanged = true;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private bool _IsChanged = false;
        public bool IsChanged
        {
            get => _IsChanged;
            set
            {
                _IsChanged = value;
                VegetationValidator _fullvalidator = new VegetationValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_vegetation);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
            }
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
                if (_vegetation.VSNSPECIESCODE != value) { IsChanged = true; }
                _vegetation.VSNSPECIESCODE = value;

              NotifyPropertyChanged("SPECIES");
              SetScientific();
              
            }
        }
        void UpdateSums()
        {
            if (util.AllowVegCalc)
            {
                ELCLAYER3 = (QUAD1 + QUAD2 + QUAD3 + QUAD4) / 4;
                ELCLAYER4 = (QUAD1_ELC4 + QUAD2_ELC4 + QUAD3_ELC4 + QUAD4_ELC4) / 4;
                ELCLAYER5 = (QUAD1_ELC5 + QUAD2_ELC5 + QUAD3_ELC5 + QUAD4_ELC5) / 4;
                ELCLAYER6 = (QUAD1_ELC6 + QUAD2_ELC6 + QUAD3_ELC6 + QUAD4_ELC6) / 4;
                ELCLAYER7 = (QUAD1_ELC7 + QUAD2_ELC7 + QUAD3_ELC7 + QUAD4_ELC7) / 4;
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
        public double QUAD1
        {
            get => _vegetation.QUAD1;
            set
            {
                if (!_vegetation.QUAD1.Equals(value)) { _vegetation.QUAD1 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD1");
                
                UpdateSums(); 
            }
        }
        public double QUAD2
        {
            get => _vegetation.QUAD2;
            set
            {
                if (!_vegetation.QUAD2.Equals(value)) { _vegetation.QUAD2 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD2");
                UpdateSums();
            }
        }
        public double QUAD3
        {
            get => _vegetation.QUAD3;
            set
            {
                if (!_vegetation.QUAD3.Equals(value)) { _vegetation.QUAD3 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD3");
                UpdateSums();
            }
        }
        public double QUAD4
        {
            get => _vegetation.QUAD4;
            set
            {
                if (!_vegetation.QUAD4.Equals(value)) { _vegetation.QUAD4 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD4");
                UpdateSums();
            }
        }
        public double ELCLAYER3
        {
            get => _vegetation.ELCLAYER3;
            set
            {
                if (!_vegetation.ELCLAYER3.Equals(value)) { _vegetation.ELCLAYER3 = value; IsChanged = true; }

                NotifyPropertyChanged("ELCLAYER3");
               
            }
        }
        public double ELCLAYER4
        {
            get => _vegetation.ELCLAYER4;
            set
            {
                if (!_vegetation.ELCLAYER4.Equals(value)) { _vegetation.ELCLAYER4 = value; IsChanged = true; }

                NotifyPropertyChanged("ELCLAYER4");
                
            }
        }
        public double ELCLAYER5
        {
            get => _vegetation.ELCLAYER5;
            set
            {
                if (!_vegetation.ELCLAYER5.Equals(value)) { _vegetation.ELCLAYER5 = value; IsChanged = true; }

                NotifyPropertyChanged("ELCLAYER5");
              
            }
        }
        public double ELCLAYER6
        {
            get => _vegetation.ELCLAYER6;
            set
            {
                if (!_vegetation.ELCLAYER6.Equals(value)) { _vegetation.ELCLAYER6 = value; IsChanged = true; }

                NotifyPropertyChanged("ELCLAYER6");
               
            }
        }
        public double ELCLAYER7
        {
            get => _vegetation.ELCLAYER7;
            set
            {
                if (!_vegetation.ELCLAYER7.Equals(value)) { _vegetation.ELCLAYER7 = value; IsChanged = true; }

                NotifyPropertyChanged("ELCLAYER7");
               
            }
        }
        public int SPECIMENNUMBER
        {
            get => _vegetation.SPECIMENNUMBER;
            set
            {
                if (!_vegetation.SPECIMENNUMBER.Equals(value)) { _vegetation.SPECIMENNUMBER = value; IsChanged = true; }

                NotifyPropertyChanged("SPECIMENNUMBER");
               
            }
        }
        public double QUAD1_ELC4
        {
            get => _vegetation.QUAD1_ELC4;
            set
            {
                if (!_vegetation.QUAD1_ELC4.Equals(value)) { _vegetation.QUAD1_ELC4 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD1_ELC4");
             
                UpdateSums();
            }
        }
        public double QUAD2_ELC4
        {
            get => _vegetation.QUAD2_ELC4;
            set
            {
                if (!_vegetation.QUAD2_ELC4.Equals(value)) { _vegetation.QUAD2_ELC4 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD2_ELC4");
                UpdateSums();
            }
        }

        public double QUAD3_ELC4
        {
            get => _vegetation.QUAD3_ELC4;
            set
            {
                if (!_vegetation.QUAD3_ELC4.Equals(value)) { _vegetation.QUAD3_ELC4 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD3_ELC4");

                UpdateSums();
            }
        }
        public double QUAD4_ELC4
        {
            get => _vegetation.QUAD4_ELC4;
            set
            {
                if (!_vegetation.QUAD4_ELC4.Equals(value)) { _vegetation.QUAD4_ELC4 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD4_ELC4");
                UpdateSums();
            }
        }
        public double QUAD1_ELC5
        {
            get => _vegetation.QUAD1_ELC5;
            set
            {
                if (!_vegetation.QUAD1_ELC5.Equals(value)) { _vegetation.QUAD1_ELC5 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD1_ELC5");
                UpdateSums();
            }
        }
        public double QUAD2_ELC5
        {
            get => _vegetation.QUAD2_ELC5;
            set
            {
                if (!_vegetation.QUAD2_ELC5.Equals(value)) { _vegetation.QUAD2_ELC5 = value; IsChanged = true; }
 
                NotifyPropertyChanged("QUAD2_ELC5");
                UpdateSums();
            }
        }
        public double QUAD3_ELC5
        {
            get => _vegetation.QUAD3_ELC5;
            set
            {
                if (!_vegetation.QUAD3_ELC5.Equals(value)) { _vegetation.QUAD3_ELC5 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD3_ELC5");
                UpdateSums();
            }
        }
        public double QUAD4_ELC5
        {
            get => _vegetation.QUAD4_ELC5;
            set
            {
                if (!_vegetation.QUAD4_ELC5.Equals(value)) { _vegetation.QUAD4_ELC5 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD4_ELC5");
                UpdateSums();
            }
        }
        public double QUAD1_ELC6
        {
            get => _vegetation.QUAD1_ELC6;
            set
            {
                if (!_vegetation.QUAD1_ELC6.Equals(value)) { _vegetation.QUAD1_ELC6 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD1_ELC6");
                UpdateSums();
            }
        }
        public double QUAD2_ELC6
        {
            get => _vegetation.QUAD2_ELC6;
            set
            {
                if (!_vegetation.QUAD2_ELC6.Equals(value)) { _vegetation.QUAD2_ELC6 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD2_ELC6");
                UpdateSums();
            }
        }
        public double QUAD3_ELC6
        {
            get => _vegetation.QUAD3_ELC6;
            set
            {
                if (!_vegetation.QUAD3_ELC6.Equals(value)) { _vegetation.QUAD3_ELC6 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD3_ELC6");
                UpdateSums();
            }
        }
        public double QUAD4_ELC6
        {
            get => _vegetation.QUAD4_ELC6;
            set
            {
                if (!_vegetation.QUAD4_ELC6.Equals(value)) { _vegetation.QUAD4_ELC6 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD4_ELC6");
                UpdateSums();
            }
        }
        public double QUAD1_ELC7
        {
            get => _vegetation.QUAD1_ELC7;
            set
            {
                if (!_vegetation.QUAD1_ELC7.Equals(value)) { _vegetation.QUAD1_ELC7 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD1_ELC7");
                UpdateSums();
            }
        }
        public double QUAD2_ELC7
        {
            get => _vegetation.QUAD2_ELC7;
            set
            {
                if (!_vegetation.QUAD2_ELC7.Equals(value)) { _vegetation.QUAD2_ELC7 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD2_ELC7");
                UpdateSums();
            }
        }
        public double QUAD3_ELC7
        {
            get => _vegetation.QUAD3_ELC7;
            set
            {
                if (!_vegetation.QUAD3_ELC7.Equals(value)) { _vegetation.QUAD3_ELC7 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD3_ELC7");
                UpdateSums();
            }
        }
        public double QUAD4_ELC7
        {
            get => _vegetation.QUAD4_ELC7;
            set
            {
                if (!_vegetation.QUAD4_ELC7.Equals(value)) { _vegetation.QUAD4_ELC7 = value; IsChanged = true; }

                NotifyPropertyChanged("QUAD4_ELC7");
                UpdateSums();
            }
        }
        public int ERRORCOUNT
        {
            get => _vegetation.ERRORCOUNT;
            set
            {
                _vegetation.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
               
            }
        }
        public string ERRORMSG
        {
            get => _vegetation.ERRORMSG;
            set
            {
                _vegetation.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
            
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
