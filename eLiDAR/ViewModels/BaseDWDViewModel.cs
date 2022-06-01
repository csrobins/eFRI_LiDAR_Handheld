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
    public class BaseDWDViewModel : INotifyPropertyChanged {

        public DWD _dwd;
        public INavigation _navigation;
        public IValidator _dwdValidator;
        public IDWDRepository _dwdRepository;
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
            else { IsChanged = true; }
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
                DWDValidator _fullvalidator = new DWDValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_dwd);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
            }
        }

        public string DWDID
        {
            get => _dwd.DWDID;
            set
            {
                _dwd.DWDID = value;
                NotifyPropertyChanged("DWDID");
            }
        }

        public string PLOTID
        {
            get => _dwd.PLOTID;
            set
            {
                _dwd.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int LINE
        {
            get => _dwd.LINENUMBER;
            set
            {
                if (!_dwd.LINENUMBER.Equals(value)) { IsChanged = true; }
                _dwd.LINENUMBER = value;
                NotifyPropertyChanged("LINE");
               
            }
        }

        public int DWDNUM
        {
            get => _dwd.DWDNUM;
            set
            {
                if (!_dwd.DWDNUM.Equals(value)) { IsChanged = true; }
                _dwd.DWDNUM = value;
                NotifyPropertyChanged("DWDNUM");
               
            }
        }

        public int SPECIES
        {
            get => _dwd.SPECIESCODE;
            set
            {
                if (!_dwd.SPECIESCODE.Equals(value)) { IsChanged = true; }
                _dwd.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
                
            }
        }
        public double DIAM
        {
            get => _dwd.DIAMETER;
            set
            {
                if (!_dwd.DIAMETER.Equals(value)) { IsChanged = true; }
                _dwd.DIAMETER = value;
                NotifyPropertyChanged("DIAM");
                
            }
        }
        public int DECOMP_CLASS
        {
            get => _dwd.DECOMPOSITIONCLASS;
            set
            {
                if (!_dwd.DECOMPOSITIONCLASS.Equals(value)) { IsChanged = true; }
                _dwd.DECOMPOSITIONCLASS = value;
                NotifyPropertyChanged("DECOMP_CLASS");
              
            }
        }

        public string ORIGIN
        {
            get => _dwd.DOWNWOODYDEBRISORIGINCODE;
            set
            {
                if (_dwd.DOWNWOODYDEBRISORIGINCODE !=value) { IsChanged = true; }
                _dwd.DOWNWOODYDEBRISORIGINCODE = value;
                NotifyPropertyChanged("ORIGIN");
              
            }
        }

        public int TILT_ANGLE
        {
            get => _dwd.TILTANGLE;
            set
            {
                if (!_dwd.TILTANGLE.Equals(value)) { IsChanged = true; }
                _dwd.TILTANGLE = value;
                NotifyPropertyChanged("TILT_ANGLE");
               
            }
        }

        public double LENGTH
        {
            get => _dwd.DOWNWOODYDEBRISLENGTH;
            set
            {
                if (Math.Abs(LENGTH - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_dwd.DOWNWOODYDEBRISLENGTH.Equals(value)) { IsChanged = true; }
                    _dwd.DOWNWOODYDEBRISLENGTH = value;
                    NotifyPropertyChanged("LENGTH");
                    
                }
            }
        }

        public double SMALL_DIAM
        {
            get => _dwd.SMALLDIAMETER;
            set
            {
                if (!_dwd.SMALLDIAMETER.Equals(value)) { IsChanged = true; }
                _dwd.SMALLDIAMETER = value;
                NotifyPropertyChanged("SMALL_DIAM");
              
            }
        }

        public double LARGE_DIAM
        {
            get => _dwd.LARGEDIAMETER;
            set
            {
                if (!_dwd.LARGEDIAMETER.Equals(value)) { IsChanged = true; }
                _dwd.LARGEDIAMETER = value;
                NotifyPropertyChanged("LARGE_DIAM");
               
            }
        }

        public string GT_50_MOSS
        {
            get => _dwd.MOSS;
            set
            {
                if (_dwd.MOSS != value) { IsChanged = true; }
                _dwd.MOSS = value;
                NotifyPropertyChanged("GT_50_MOSS");
               
            }
        }

        public string BURNED
        {
            get => _dwd.BURNED;
            set
            {
                if (_dwd.BURNED !=value) { IsChanged = true; }
                _dwd.BURNED = value;
                NotifyPropertyChanged("BURNED");
               
            }
        }

        public string HOLLOW
        {
            get => _dwd.HOLLOW;
            set
            {
                if (_dwd.HOLLOW != value) { IsChanged = true; }
                _dwd.HOLLOW = value;
                NotifyPropertyChanged("HOLLOW");
                
            }
        }
        public string IS_ACCUM
        {
            get => _dwd.IS_ACCUM;
            set
            {
                if (_dwd.IS_ACCUM != value) { IsChanged = true; }
                _dwd.IS_ACCUM = value;
                NotifyPropertyChanged("IS_ACCUM");
               
            }
        }
        public double ACCUM_LENGTH
        {
            get => _dwd.ACCUMULATIONLENGTH;
            set
            {
                if (Math.Abs(ACCUM_LENGTH - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_dwd.ACCUMULATIONLENGTH.Equals(value)) { IsChanged = true; }
                    _dwd.ACCUMULATIONLENGTH = value;
                    NotifyPropertyChanged("ACCUM_LENGTH");
                   
                }
            }
        }
        public double ACCUM_DEPTH
        {
            get => _dwd.ACCUMULATIONDEPTH;
            set
            {
                if (Math.Abs(ACCUM_DEPTH - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_dwd.ACCUMULATIONDEPTH.Equals(value)) { IsChanged = true; }
                    _dwd.ACCUMULATIONDEPTH = value;
                    NotifyPropertyChanged("ACCUM_DEPTH");
                   
                }
            }
        }
        public int PERCENT_CONIFER
        {
            get => _dwd.PERCENTCONIFER;
            set
            {
                if (!_dwd.PERCENTCONIFER.Equals(value)) { IsChanged = true; }
                _dwd.PERCENTCONIFER = value;
                NotifyPropertyChanged("PERCENT_CONIFER");
                
            }
        }
        public int PERCENT_DECID
        {
            get => _dwd.PERCENTHARDWOOD;
            set
            {
                if (!_dwd.PERCENTHARDWOOD.Equals(value)) { IsChanged = true; }
                _dwd.PERCENTHARDWOOD = value;
                NotifyPropertyChanged("PERCENT_DECID");
                
            }
        }
        public int ERRORCOUNT
        {
            get => _dwd.ERRORCOUNT;
            set
            {
                _dwd.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
           
            }
        }
        public string ERRORMSG
        {
            get => _dwd.ERRORMSG;
            set
            {
                _dwd.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
               
            }
        }

        List<DWD> _dwdList;
        public List<DWD> DWDList
        {
            get => _dwdList;
            set
            {
                _dwdList = value;
                NotifyPropertyChanged("DWDList");
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
