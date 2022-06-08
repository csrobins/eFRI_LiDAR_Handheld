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
    public class BaseStemMapViewModel : INotifyPropertyChanged {

        public STEMMAP _stemmap;
        public INavigation _navigation;
        public IValidator _treeValidator;
        public IStemMapRepository _stemMapRepository;
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
                StemMapValidator _fullvalidator = new StemMapValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_stemmap);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);

                _IsChanged = value;
            }
        }
        public string STEMMAPID
        {
            get => _stemmap.STEMMAPID;
            set
            {
                _stemmap.STEMMAPID = value;
                NotifyPropertyChanged("STEMMAPID");
            }
        }

        public string TREEID
        {
            get => _stemmap.TREEID;
            set
            {
                _stemmap.TREEID = value;
                NotifyPropertyChanged("TREEID");
            }
        }

        public int AZIMUTH
        {
            get => _stemmap.AZIMUTH;
            set
            {
                if (!_stemmap.AZIMUTH.Equals(value)) { _stemmap.AZIMUTH = value; IsChanged = true; }

                NotifyPropertyChanged("AZIMUTH");
               
            }
        }

        public double DISTANCE
        {
            get => _stemmap.DISTANCE;
            set
            {
                if (Math.Abs(DISTANCE - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_stemmap.DISTANCE.Equals(value)) { _stemmap.DISTANCE = value; IsChanged = true; }

                    NotifyPropertyChanged("DISTANCE");
                   
                }
            }
        }

        public double CROWN_AXIS_LONG
        {
            get => _stemmap.CROWNWIDTH1;
            set
            {
                if (Math.Abs(CROWN_AXIS_LONG - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_stemmap.CROWNWIDTH1.Equals(value)) { _stemmap.CROWNWIDTH1 = value; IsChanged = true; }

                    NotifyPropertyChanged("CROWN_AXIS_LONG");
                   
                }
            }
        }
        public double CROWN_AXIS_SHORT
        {
            get => _stemmap.CROWNWIDTH2;
            set
            {
                if (Math.Abs(CROWN_AXIS_SHORT - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_stemmap.CROWNWIDTH2.Equals(value)) { _stemmap.CROWNWIDTH2 = value; IsChanged = true; }

                    NotifyPropertyChanged("CROWN_AXIS_SHORT");
                    
                }
            }
        }
        public int OFFSET_AZIMUTH
        {
            get => _stemmap.CROWNOFFSETAZIMUTH ;
            set
            {
                if (!_stemmap.CROWNOFFSETAZIMUTH.Equals(value)) { _stemmap.CROWNOFFSETAZIMUTH = value; IsChanged = true; }

                NotifyPropertyChanged("OFFSET_AZIMUTH");
               
            }
        }

        public double OFFSET_DISTANCE
        {
            get => _stemmap.CROWNOFFSETDISTANCE;
            set
            {
                if (Math.Abs(OFFSET_DISTANCE - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_stemmap.CROWNOFFSETDISTANCE.Equals(value)) { _stemmap.CROWNOFFSETDISTANCE = value; IsChanged = true; }

                    NotifyPropertyChanged("OFFSET_DISTANCE");
                    
                }
            }
        }
        public int ERRORCOUNT
        {
            get => _stemmap.ERRORCOUNT;
            set
            {
                _stemmap.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
               
            }
        }
        public string ERRORMSG
        {
            get => _stemmap.ERRORMSG;
            set
            {
                _stemmap.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
            
            }
        }

        List<STEMMAP> _stemmapList;
        public List<STEMMAP> StemMapList
        {
            get => _stemmapList;
            set
            {
                _stemmapList = value;
                NotifyPropertyChanged("StemMapList");
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
