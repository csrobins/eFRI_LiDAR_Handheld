using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using eLiDAR.Helpers;
using eLiDAR.Models;
using Xamarin.Forms;
using eLiDAR.Utilities;
using System.Windows.Input;
using eLiDAR.API;
using System.Threading.Tasks;
using eLiDAR.Styles;
using eLiDAR.Domain.Global;


namespace eLiDAR.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged   {

        public INavigation _navigation;
        private Utils util;
       
        public ICommand SynchCommand { get; private set; }
      
        public AboutViewModel(INavigation navigation)
        {
            _navigation = navigation;
            util = new Utils();
            FetchSettings();
        }
        public void FetchSettings()
        {
            NotifyPropertyChanged("ErrorList");        
        }
        public string ErrorList
        {
            get => util.ErrorList ;
            set
            {
                util.ErrorList   = value;
                NotifyPropertyChanged("ErrorList");
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
