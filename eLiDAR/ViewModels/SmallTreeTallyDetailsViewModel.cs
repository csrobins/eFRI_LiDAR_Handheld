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

namespace eLiDAR.ViewModels
{
    internal class SmallTreeTallyDetailsViewModel : BaseSmallTreeTallyViewModel
    {
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public SmallTreeTallyDetailsViewModel(INavigation navigation, string selectedID)
        {
            _navigation = navigation;
            _smallTreeTally = new SMALLTREETALLY();
            _smallTreeTally.SMALLTREETALLYID = selectedID;
            _smallTreeTallyRepository = new SmallTreeTallyRepository();
            _fk = selectedID;
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListSpecies = PickerService.SmallTreeSpeciesItems().ToList().OrderBy(c => c.ID).ToList();
            FetchDetails(selectedID);
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }
        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _smallTreeTally.SPECIESCODE);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _smallTreeTally.SPECIESCODE = (int)_selectedSpecies.ID;
            }
        }
        void FetchDetails(string fk)
        {
            _smallTreeTally = _smallTreeTallyRepository.GetSmallTreeTallyData(fk);
        }
        private Task Update()
        {
            try
            {

                _smallTreeTally.LastModified = System.DateTime.UtcNow;
                _smallTreeTallyRepository.UpdateSmallTreeTally(_smallTreeTally);
                return Task.CompletedTask;

            }
            catch (Exception e)
            {
                var myerror = e.Message;
                return Task.CompletedTask;// error
                                          //  Log.Fatal(e);
            };
        }
        async Task Delete()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Small Tree Details", "Delete Small Tree Details", "OK", "Cancel");
            if (isUserAccept)
            {
                _smallTreeTallyRepository.DeleteSmallTreeTally(_smallTreeTally);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Small Tree Tallys for plot " + _smallTreeTallyRepository.GetTitle(_smallTreeTally.PLOTID);
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
                SmallTreeTallyValidator _validator = new SmallTreeTallyValidator();
                SmallTreeTallyValidator _fullvalidator = new SmallTreeTallyValidator(true);

                ValidationResult validationResults = _validator.Validate(_smallTreeTally);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_smallTreeTally);

                ParseValidater _parser = new ParseValidater();
                (_smallTreeTally.ERRORCOUNT, _smallTreeTally.ERRORMSG) = _parser.Parse(fullvalidationResults);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
                    //        await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Small Tree", validationResults.Errors[0].ErrorMessage, "Ok");
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