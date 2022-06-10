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
                PhotoValidator _fullvalidator = new PhotoValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_photo);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
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
                NotifyPropertyChanged("PHOTOID");
            }
        }
        public string PHOTOTYPE
        {
            get => _photo.PHOTOTYPE;
            set
            {
                if (_photo.PHOTOTYPE != value) { IsChanged = true; }
                _photo.PHOTOTYPE = value;
                NotifyPropertyChanged("PHOTOTYPE");
            }
        }
        public string DESCRIPTION
        {
            get => _photo.DESCRIPTION;
            set
            {
                if (_photo.DESCRIPTION != value) { _photo.DESCRIPTION = value; IsChanged = true; }
 

                NotifyPropertyChanged("DESCRIPTION");
            }
        }
        public int PHOTONUMBER
        {
            get => _photo.PHOTONUMBER;
            set
            {
                if (!_photo.PHOTONUMBER.Equals(value)) { _photo.PHOTONUMBER = value; IsChanged = true; }

    
                NotifyPropertyChanged("PHOTONUMBER");
            }
        }
        public string FRAMENUMBER
        {
            get => _photo.FRAMENUMBER;
            set
            {
                if (_photo.FRAMENUMBER != value) { _photo.FRAMENUMBER = value; IsChanged = true; }

    
                NotifyPropertyChanged("FRAMENUMBER");
            }
        }
        public int AZIMUTH
        {
            get => _photo.AZIMUTH;
            set
            {
                if (!_photo.AZIMUTH.Equals(value)) { _photo.AZIMUTH = value; IsChanged = true; }

                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public Single DISTANCE
        {
            get => _photo.DISTANCE;
            set
            {
                if (!_photo.DISTANCE.Equals(value)) { _photo.DISTANCE = value; IsChanged = true; }

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
           
            }
        }
        public string ERRORMSG
        {
            get => _photo.ERRORMSG;
            set
            {
                _photo.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
              
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
