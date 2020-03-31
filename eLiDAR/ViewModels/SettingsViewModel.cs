using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using Xamarin.Forms;
using eLiDAR.Utilities;

namespace eLiDAR.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged {

        public INavigation _navigation;
        private Utils util;
        public SettingsViewModel(INavigation navigation)
        {
            _navigation = navigation;
            util = new Utils();
        }

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

        public bool IsBoreal
        {
            get => util.BorealSpecies;
            set
            {
                util.BorealSpecies = value;
                NotifyPropertyChanged("IsBoreal");
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
