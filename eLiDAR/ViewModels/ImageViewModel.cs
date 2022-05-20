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
using eLiDAR.Services;
using System.Linq;

namespace eLiDAR.ViewModels
{
    public class ImageViewModel : INotifyPropertyChanged   {

        public INavigation _navigation;

      
        public ImageViewModel(INavigation navigation)
        {
            _navigation = navigation;
           
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


        private string _imagepick = "Drainage";
        public string IMAGEPICK
        {
            get => _imagepick;
            set
            {
                _imagepick = value;
                if (value == "Drainage")
                {
                    IMAGESOURCE = "eLiDAR.Assets.Images.drainage.jpg";
                }
                else if (value == "Humus Form")
                {
                    IMAGESOURCE = "eLiDAR.Assets.Images.humusform.jpg";
                }
                else if (value == "Moisture Regime")
                {
                    IMAGESOURCE = "eLiDAR.Assets.Images.moistureregime.jpg";
                }
                else if (value == "Stratification")
                {
                    IMAGESOURCE = "eLiDAR.Assets.Images.stratification.jpg";
                }
                NotifyPropertyChanged("IMAGEPICK");
            }
        }
        private string _imagesource = "eLiDAR.Assets.Images.drainage.jpg";
        public string IMAGESOURCE
        {
            get => _imagesource;
            set
            {
                _imagesource = value;
                NotifyPropertyChanged("IMAGESOURCE");
                NotifyPropertyChanged("GetImage");

            }
        }
        
        public Xamarin.Forms.ImageSource GetImage
        {
            get => EmbeddedSourceror.SourceFor(IMAGESOURCE);
        }

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = ""){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
