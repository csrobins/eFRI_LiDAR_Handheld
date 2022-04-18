using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Validator;
using FluentValidation;
using FluentValidation.Results;

using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class AddPhotoViewModel: BasePhotoViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public AddPhotoViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _photo = new PHOTO();
            _photo.PLOTID  = selectedID;
            _photo.PHOTOTYPE = "Stand Information";
            _photoRepository = new PhotoRepository();
            _selectedplotid = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            CheckTable();
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
            IsChanged = false;
        }

        void CheckTable() 
        {
            // run this to prepopulate table with photos the first time through
            if (_photoRepository.IsPhotoTableEmpty(_selectedplotid))
            {
                PHOTO _newphoto = new PHOTO();
                _newphoto.PHOTOTYPE = "Stand Information";
                _newphoto.PHOTONUMBER =  1;
                _newphoto.AZIMUTH = 0;
                _newphoto.DISTANCE = 0;
                _newphoto.PLOTID = _selectedplotid;
                _newphoto.Created = System.DateTime.UtcNow;
                _newphoto.LastModified = _newphoto.Created;
                _newphoto.IsDeleted = "N";
                _photoRepository.InsertPhoto(_newphoto);
                PHOTO _newphoto2 = new PHOTO();
                _newphoto2.PHOTOTYPE = "Stand Information";
                _newphoto2.PHOTONUMBER = 18;
                _newphoto2.AZIMUTH = 0;
                _newphoto2.DISTANCE = 0;
                _newphoto2.Created = System.DateTime.UtcNow;
                _newphoto2.LastModified = _newphoto2.Created;
                _newphoto2.IsDeleted = "N";
                _newphoto2.PLOTID = _selectedplotid;
                _photoRepository.InsertPhoto(_newphoto2);
                // Insert records for the Stand Infor Photos
                for (int i = 0; i < 8; i++)
                {
                    PHOTO _newphoto3 = new PHOTO();
                    _newphoto3.PHOTOTYPE = "Stand Information";
                    _newphoto3.PHOTONUMBER = i+2;
                    _newphoto3.AZIMUTH  = 45 * i;
                    _newphoto3.DISTANCE =Constants.DefaultPhoto1Distance ;
                    _newphoto3.PLOTID = _selectedplotid;
                    _newphoto3.Created = System.DateTime.UtcNow;
                    _newphoto3.LastModified = _newphoto3.Created;
                    _newphoto3.IsDeleted = "N";
                    _photoRepository.InsertPhoto(_newphoto3);
                 }
                for (int i = 0; i < 8; i++)
                {
                    PHOTO _newphoto4 = new PHOTO();
                    _newphoto4.PHOTOTYPE = "Stand Information";
                    _newphoto4.PHOTONUMBER = i + 10;
                    _newphoto4.AZIMUTH = 45 *  i;
                    _newphoto4.DISTANCE = Constants.DefaultPhoto2Distance;
                    _newphoto4.PLOTID = _selectedplotid;
                    _newphoto4.Created = System.DateTime.UtcNow;
                    _newphoto4.LastModified = _newphoto4.Created;
                    _newphoto4.IsDeleted = "N";
                    _photoRepository.InsertPhoto(_newphoto4);
                }

            }
        }
        void FetchDetails(string fk){
            _photo = _photoRepository.GetPhotoData(fk);
        }
        private Task Update() {
            try
            {
                        _photo.Created = System.DateTime.UtcNow;
                        _photo.LastModified = _photo.Created;
                        _photo.IsDeleted = "N"; 
                        _photoRepository.InsertPhoto(_photo);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                var myerror = e.Message;
                return Task.CompletedTask;// error
                                          //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Photo Details", "Delete Vegetation Details", "OK", "Cancel");
            if (isUserAccept) {
                _photoRepository.DeletePhoto (_photo );
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Photo info for plot " + _photoRepository.GetPlotTitle(_selectedplotid);
            set
            {
            }
        }
        private void OnAppearing()
        {
            Shell.Current.Navigating += Current_Navigating;
        }
        private void OnDisappearing()
        {
            Shell.Current.Navigating -= Current_Navigating;
        }
        private async void Current_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                e.Cancel();
                await GoBack();
            }
        }

        private async Task GoBack()
        {
            // display Alert for confirmation
            if (IsChanged)
            {

                PhotoValidator _validator = new PhotoValidator();
                PhotoValidator _fullvalidator = new PhotoValidator(true);

                ValidationResult validationResults = _validator.Validate(_photo);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_photo);

                ParseValidater _parser = new ParseValidater();
                (_photo.ERRORCOUNT, _photo.ERRORMSG) = _parser.Parse(fullvalidationResults);

//                PhotoValidator _validator = new PhotoValidator();
  //              ValidationResult validationResults = _validator.Validate(_photo);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
              //      await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Vegetation", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
          //      await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}