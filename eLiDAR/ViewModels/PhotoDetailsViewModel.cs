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
    public class PhotoDetailsViewModel: BasePhotoViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public PhotoDetailsViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _photo = new PHOTO();
            _photo.PHOTOID  = selectedID;
            _photoRepository = new PhotoRepository();
            _selectedplotid = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
            FetchDetails(_selectedplotid);
            IsChanged = false;
        }
 
        void FetchDetails(string fk){
            _photo = _photoRepository.GetPhotoData(fk);
        }
        private Task Update() {
            try
            {
               
                _photo.LastModified = System.DateTime.UtcNow;
                _photoRepository.UpdatePhoto(_photo);
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
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Photo Details", "Delete Photo Details", "OK", "Cancel");
            if (isUserAccept) {
                _photoRepository.DeletePhoto (_photo );
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Photo info for plot " + _photoRepository.GetPlotTitle(_photo.PLOTID);
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
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
            //        await Shell.Current.GoToAsync("..", true);
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