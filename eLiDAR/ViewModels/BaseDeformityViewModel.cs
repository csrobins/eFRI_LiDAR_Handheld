using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using eLiDAR.Validator;
using FluentValidation.Results;

namespace eLiDAR.ViewModels
{
    public class BaseDeformityViewModel : INotifyPropertyChanged {

        public DEFORMITY _deformity;
        public INavigation _navigation;
        public IValidator _deformityValidator;
        public IDeformityRepository _deformityRepository;
        public string _fk;

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
                DeformityValidator _fullvalidator = new DeformityValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_deformity);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);

            }
        }

        public string DEFORMITYID
        
       {
                get => _deformity.DEFORMITYID;
                set
            {
                    _deformity.DEFORMITYID = value;
                    NotifyPropertyChanged("DEFORMITYID");
                }
            }

        public string TREEID
        {
            get => _deformity.TREEID ;
            set
            {
                _deformity.TREEID = value;
                NotifyPropertyChanged("TREEID");
            }
        }

        public int TYPE
        {
            get => _deformity.DEFORMITYTYPECODE;
            set
            {
                if (!_deformity.DEFORMITYTYPECODE.Equals(value)) { IsChanged = true; }
                _deformity.DEFORMITYTYPECODE = value;
                NotifyPropertyChanged("TYPE");
               
            }
        }

        public int CAUSE
        {
            get => _deformity.CAUSE;
            set
            {
                if (!_deformity.CAUSE.Equals(value)) { IsChanged = true; }
                _deformity.CAUSE = value;
                NotifyPropertyChanged("CAUSE");
              
            }
        }

        public double HT_FROM
        {
            get => _deformity.HEIGHTFROM;
            set
            {
                if (!_deformity.HEIGHTFROM.Equals(value)) { IsChanged = true; }
                _deformity.HEIGHTFROM = value;
                NotifyPropertyChanged("HT_FROM");
          
            }
        }

        public double HT_TO
        {
            get => _deformity.HEIGHTTO;
            set
            {
                if (!_deformity.HEIGHTTO.Equals(value)) { IsChanged = true; }
                _deformity.HEIGHTTO = value;
                NotifyPropertyChanged("HT_TO");
               
            }
        }

        public string QUAD
        {
            get => _deformity.QUADRANTCODE;
            set
            {
                if (_deformity.QUADRANTCODE != value) { IsChanged = true; }
                _deformity.QUADRANTCODE = value;
                NotifyPropertyChanged("QUAD");
               
            }
        }

        public int EXTENT
        {
            get => _deformity.EXTENT;
            set
            {
                if (!_deformity.EXTENT.Equals(value)) { IsChanged = true; }
                _deformity.EXTENT = value;
                NotifyPropertyChanged("EXTENT");
                
            }
        }

        public int LEAN
        {
            get => _deformity.DEGREELEANARCH;
            set
            {
                if (!_deformity.DEGREELEANARCH.Equals(value)) { IsChanged = true; }
                _deformity.DEGREELEANARCH = value;
                NotifyPropertyChanged("LEAN");
              
            }
        }

        public int AZIMUTH
        {
            get => _deformity.AZIMUTH;
            set
            {
                if (!_deformity.AZIMUTH.Equals(value)) { IsChanged = true; }
                _deformity.AZIMUTH = value;
                NotifyPropertyChanged("AZIMUTH");
            
            }
        }

        public double LENGTH
        {
            get => _deformity.DEFORMITYLENGTH;
            set
            {
                if (!_deformity.DEFORMITYLENGTH.Equals(value)) { IsChanged = true; }
                _deformity.DEFORMITYLENGTH = value;
                NotifyPropertyChanged("LENGTH");
               
            }
        }

        public double WIDTH
        {
            get => _deformity.DEFORMITYWIDTH;
            set
            {
                if (!_deformity.DEFORMITYWIDTH.Equals(value)) { IsChanged = true; }
                _deformity.DEFORMITYWIDTH = value;
                NotifyPropertyChanged("WIDTH");
                
            }
        }

        public int PCT_SCUFF
        {
            get => _deformity.SCUFF;
            set
            {
                if (!_deformity.SCUFF.Equals(value)) { IsChanged = true; }
                _deformity.SCUFF = value;
                NotifyPropertyChanged("PCT_SCRUFF");
               
            }
        }
        public int PCT_SCRAPE
        {
            get => _deformity.SCRAPE;
            set
            {
                if (!_deformity.SCRAPE.Equals(value)) { IsChanged = true; }
                _deformity.SCRAPE = value;
                NotifyPropertyChanged("PCT_SCRAPE");
 
            }
        }

        public int PCT_GOUGE
        {
            get => _deformity.GOUGE;
            set
            {
                if (!_deformity.GOUGE.Equals(value)) { IsChanged = true; }
                _deformity.GOUGE = value;
                NotifyPropertyChanged("PCT_GOUGE");
            
            }
        }

        public string OLD_FEEDING_CAVITY
        {
            get => _deformity.OLD_FEEDING_CAVITY;
            set
            {
                if (_deformity.OLD_FEEDING_CAVITY != value) { IsChanged = true; }
                _deformity.OLD_FEEDING_CAVITY = value;
                NotifyPropertyChanged("OLD_FEEDING_CAVITY");
                
            }
        }

        public string NEW_FEEDING_CAVITY
        {
            get => _deformity.NEW_FEEDING_CAVITY;
            set
            {
                if (_deformity.NEW_FEEDING_CAVITY != value) { IsChanged = true; }
                _deformity.NEW_FEEDING_CAVITY = value;
                NotifyPropertyChanged("NEW_FEEDING_CAVITY");
                
            }
        }

        public string OLD_NEST_CAVITY
        {
            get => _deformity.OLD_NEST_CAVITY;
            set
            {
                if (_deformity.OLD_NEST_CAVITY != value) { IsChanged = true; }
                _deformity.OLD_NEST_CAVITY = value;
                NotifyPropertyChanged("OLD_NEST_CAVITY");
                
            }
        }

        public string NEW_NEST_CAVITY
        {
            get => _deformity.NEW_NEST_CAVITY;
            set
            {
                if (_deformity.NEW_NEST_CAVITY != value) { IsChanged = true; }
                _deformity.NEW_NEST_CAVITY = value;
                NotifyPropertyChanged("NEW_NEST_CAVITY");
                
            }
        }

        public string STICK_NEST
        {
            get => _deformity.STICK_NEST;
            set
            {
                if (_deformity.STICK_NEST != value) { IsChanged = true; }
                _deformity.STICK_NEST = value;
                NotifyPropertyChanged("STICK_NEST");
           
            }
        }
        public int ERRORCOUNT
        {
            get => _deformity.ERRORCOUNT;
            set
            {
                _deformity.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
              
            }
        }
        public string ERRORMSG
        {
            get => _deformity.ERRORMSG;
            set
            {
                _deformity.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
              
            }
        }


        List<DEFORMITY> _deformityList;
        public List<DEFORMITY> DeformityList
        {
            get => _deformityList;
            set
            {
                _deformityList = value;
                NotifyPropertyChanged("DeformityList");
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
