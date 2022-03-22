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
    public class BasePhotoViewModel : INotifyPropertyChanged {

        public PHOTO _photo;
        public INavigation _navigation;
        public IValidator _photoValidator;
        public IPhotoRepository _photoRepository;
        public string _selectedplotid;
        public string _selectedphotoid;
        private bool _IsChanged = false; 

        public bool IsChanged
        {
            get => _IsChanged;
            set
            {
                _IsChanged = value;
            }
        }
        public string PLOTID
        {
            get => _photo.PLOTID;
            set
            {
                _photo.PLOTID = value ;
                NotifyPropertyChanged("PLOTID");
            }
        }
        public string PHOTOID
        {
            get => _photo.PHOTOID;
            set
            {
                _photo.PHOTOID = value;
                IsChanged = true;
                NotifyPropertyChanged("PHOTOID");
            }
        }
        public string PHOTOTYPE
        {
            get => _photo.PHOTOTYPE;
            set
            {
                _photo.PHOTOTYPE = value;
                IsChanged = true;
                NotifyPropertyChanged("PHOTOTYPE");
            }
        }
        public string DESCRIPTION
        {
            get => _photo.DESCRIPTION;
            set
            {
                _photo.DESCRIPTION = value;
                IsChanged = true;
                NotifyPropertyChanged("DESCRIPTION");
            }
        }
        public int PHOTONUMBER
        {
            get => _photo.PHOTONUMBER;
            set
            {
                _photo.PHOTONUMBER = value;
                IsChanged = true;
                NotifyPropertyChanged("PHOTONUMBER");
            }
        }
        public string FRAMENUMBER
        {
            get => _photo.FRAMENUMBER;
            set
            {
                _photo.FRAMENUMBER = value;
                IsChanged = true;
                NotifyPropertyChanged("FRAMENUMBER");
            }
        }
        public int AZIMUTH
        {
            get => _photo.AZIMUTH;
            set
            {
                _photo.AZIMUTH = value;
                IsChanged = true;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public Single DISTANCE
        {
            get => _photo.DISTANCE;
            set
            {
                _photo.DISTANCE = value;
                IsChanged = true;
                NotifyPropertyChanged("DISTANCE");
            }
        }
        public int ERRORCOUNT
        {
            get => _photo.ERRORCOUNT;
            set
            {
                _photo.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
                IsChanged = true;
            }
        }
        public string ERRORMSG
        {
            get => _photo.ERRORMSG;
            set
            {
                _photo.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
                IsChanged = true;
            }
        }

        List<PHOTO> _photoList;
        public List<PHOTO> PhotoList
        {
            get => _photoList;
            set
            {
                _photoList = value;
                NotifyPropertyChanged("PhotoList");
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
