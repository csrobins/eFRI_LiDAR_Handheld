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
    public class VegetationDetailsViewModel: BaseVegetationViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItemsString> ListVeg { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public VegetationDetailsViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _vegetation = new VEGETATION();
            //_vegetation.VEGETATIONID  = selectedID;
            _vegetationRepository = new VegetationRepository();
            _fk = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListVeg = PickerService.VegItems().ToList();
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
            FetchDetails(selectedID);
            IsChanged = false;
        }
       
        private bool _IsValidSingle;
        public bool IsValidSingle
        {
            get => _IsValidSingle;
            set
            {
                _IsValidSingle = value;
                OnPropertyChanged();
            }
        }
        void FetchDetails(string fk){
            _vegetation = _vegetationRepository.GetVegetationData(fk);
        }
        private Task Update() {
            try
            {
     
                _vegetation.LastModified = System.DateTime.UtcNow;
                _vegetationRepository.UpdateVegetation (_vegetation);
                //  This is just to slow down the database
                _vegetationRepository.GetVegetationData(_vegetation.VEGETATIONID );
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
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Vegetation Details", "Delete Vegetation Details", "OK", "Cancel");
            if (isUserAccept) {
                _vegetationRepository.DeleteVegetation (_vegetation);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Vegetation layer for plot " + _vegetationRepository.GetTitle(_vegetation.PLOTID);
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
                VegetationValidator _validator = new VegetationValidator();
                ValidationResult validationResults = _validator.Validate(_vegetation);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
                    await Shell.Current.GoToAsync("..", true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Vegetation", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
                await Shell.Current.GoToAsync("..", true);
            }
        }
    }
}