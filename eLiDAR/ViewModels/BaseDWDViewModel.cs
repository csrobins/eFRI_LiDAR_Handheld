using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using Xamarin.Forms;

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
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
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
            get => _dwd.LINE;
            set
            {
                _dwd.LINE = value;
                NotifyPropertyChanged("LINE");
            }
        }

        public int DWDNUM
        {
            get => _dwd.DWDNUM;
            set
            {
                _dwd.DWDNUM = value;
                NotifyPropertyChanged("DWDNUM");
            }
        }

        public int SPECIES
        {
            get => _dwd.SPECIES;
            set
            {
                _dwd.SPECIES = value;
                NotifyPropertyChanged("SPECIES");
            }
        }
        public double DIAM
        {
            get => _dwd.DIAM;
            set
            {
                _dwd.DIAM = value;
                NotifyPropertyChanged("DIAM");
            }
        }
        public int DECOMP_CLASS
        {
            get => _dwd.DECOMP_CLASS;
            set
            {
                _dwd.DECOMP_CLASS = value;
                NotifyPropertyChanged("DECOMP_CLASS");
            }
        }

        public string ORIGIN
        {
            get => _dwd.ORIGIN;
            set
            {
                _dwd.ORIGIN = value;
                NotifyPropertyChanged("ORIGIN");
            }
        }

        public int TILT_ANGLE
        {
            get => _dwd.TILT_ANGLE;
            set
            {
                _dwd.TILT_ANGLE = value;
                NotifyPropertyChanged("TILT_ANGLE");
            }
        }

        public double LENGTH
        {
            get => _dwd.LENGTH;
            set
            {
                _dwd.LENGTH = value;
                NotifyPropertyChanged("LENGTH");
            }
        }

        public double SMALL_DIAM
        {
            get => _dwd.SMALL_DIAM;
            set
            {
                _dwd.SMALL_DIAM = value;
                NotifyPropertyChanged("SMALL_DIAM");
            }
        }

        public double LARGE_DIAM
        {
            get => _dwd.LARGE_DIAM;
            set
            {
                _dwd.LARGE_DIAM = value;
                NotifyPropertyChanged("LARGE_DIAM");
            }
        }

        public string GT_50_MOSS
        {
            get => _dwd.GT_50_MOSS;
            set
            {
                _dwd.GT_50_MOSS = value;
                NotifyPropertyChanged("GT_50_MOSS");
            }
        }

        public string BURNED
        {
            get => _dwd.BURNED;
            set
            {
                _dwd.BURNED = value;
                NotifyPropertyChanged("BURNED");
            }
        }

        public string HOLLOW
        {
            get => _dwd.HOLLOW;
            set
            {
                _dwd.HOLLOW = value;
                NotifyPropertyChanged("HOLLOW");
            }
        }
        public string IS_ACCUM
        {
            get => _dwd.IS_ACCUM;
            set
            {
                _dwd.IS_ACCUM = value;
                NotifyPropertyChanged("IS_ACCUM");
            }
        }
        public double ACCUM_LENGTH
        {
            get => _dwd.ACCUM_LENGTH;
            set
            {
                _dwd.ACCUM_LENGTH = value;
                NotifyPropertyChanged("ACCUM_LENGTH");
            }
        }
        public double ACCUM_DEPTH
        {
            get => _dwd.ACCUM_DEPTH;
            set
            {
                _dwd.ACCUM_DEPTH = value;
                NotifyPropertyChanged("ACCUM_DEPTH");
            }
        }
        public int PERCENT_CONIFER
        {
            get => _dwd.PERCENT_CONIFER;
            set
            {
                _dwd.PERCENT_CONIFER = value;
                NotifyPropertyChanged("PERCENT_CONIFER");
            }
        }
        public int PERCENT_DECID
        {
            get => _dwd.PERCENT_DECID;
            set
            {
                _dwd.PERCENT_DECID = value;
                NotifyPropertyChanged("PERCENT_DECID");
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
