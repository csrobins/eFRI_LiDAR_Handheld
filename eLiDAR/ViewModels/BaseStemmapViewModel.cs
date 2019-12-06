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
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
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
                _stemmap.AZIMUTH  = value;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public double DISTANCE
        {
            get => _stemmap.DISTANCE;
            set
            {
                _stemmap.DISTANCE = value;
                NotifyPropertyChanged("DISTANCE");
            }
        }

        public double CROWN_AXIS_LONG
        {
            get => _stemmap.CROWN_AXIS_LONG;
            set
            {
                _stemmap.CROWN_AXIS_LONG  = value;
                NotifyPropertyChanged("CROWN_AXIS_LONG");
            }
        }
        public double CROWN_AXIS_SHORT
        {
            get => _stemmap.CROWN_AXIS_SHORT;
            set
            {
                _stemmap.CROWN_AXIS_SHORT = value;
                NotifyPropertyChanged("CROWN_AXIS_SHORT");
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
