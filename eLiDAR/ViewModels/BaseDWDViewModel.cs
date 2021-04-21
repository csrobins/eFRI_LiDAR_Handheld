using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
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
            get => _dwd.LINENUMBER;
            set
            {
                _dwd.LINENUMBER = value;
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
            get => _dwd.SPECIESCODE;
            set
            {
                _dwd.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
            }
        }
        public double DIAM
        {
            get => _dwd.DIAMETER;
            set
            {
                _dwd.DIAMETER = value;
                NotifyPropertyChanged("DIAM");
            }
        }
        public int DECOMP_CLASS
        {
            get => _dwd.DECOMPOSITIONCLASS;
            set
            {
                _dwd.DECOMPOSITIONCLASS = value;
                NotifyPropertyChanged("DECOMP_CLASS");
            }
        }

        public string ORIGIN
        {
            get => _dwd.DOWNWOODYDEBRISORIGINCODE;
            set
            {
                _dwd.DOWNWOODYDEBRISORIGINCODE = value;
                NotifyPropertyChanged("ORIGIN");
            }
        }

        public int TILT_ANGLE
        {
            get => _dwd.TILTANGLE;
            set
            {
                _dwd.TILTANGLE = value;
                NotifyPropertyChanged("TILT_ANGLE");
            }
        }

        public double LENGTH
        {
            get => _dwd.DOWNWOODYDEBRISLENGTH;
            set
            {
                _dwd.DOWNWOODYDEBRISLENGTH = value;
                NotifyPropertyChanged("LENGTH");
            }
        }

        public double SMALL_DIAM
        {
            get => _dwd.SMALLDIAMETER;
            set
            {
                _dwd.SMALLDIAMETER = value;
                NotifyPropertyChanged("SMALL_DIAM");
            }
        }

        public double LARGE_DIAM
        {
            get => _dwd.LARGEDIAMETER;
            set
            {
                _dwd.LARGEDIAMETER = value;
                NotifyPropertyChanged("LARGE_DIAM");
            }
        }

        public string GT_50_MOSS
        {
            get => _dwd.MOSS;
            set
            {
                _dwd.MOSS = value;
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
            get => _dwd.ACCUMULATIONLENGTH;
            set
            {
                _dwd.ACCUMULATIONLENGTH = value;
                NotifyPropertyChanged("ACCUM_LENGTH");
            }
        }
        public double ACCUM_DEPTH
        {
            get => _dwd.ACCUMULATIONDEPTH;
            set
            {
                _dwd.ACCUMULATIONDEPTH = value;
                NotifyPropertyChanged("ACCUM_DEPTH");
            }
        }
        public int PERCENT_CONIFER
        {
            get => _dwd.PERCENTCONIFER;
            set
            {
                _dwd.PERCENTCONIFER = value;
                NotifyPropertyChanged("PERCENT_CONIFER");
            }
        }
        public int PERCENT_DECID
        {
            get => _dwd.PERCENTHARDWOOD;
            set
            {
                _dwd.PERCENTHARDWOOD = value;
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
