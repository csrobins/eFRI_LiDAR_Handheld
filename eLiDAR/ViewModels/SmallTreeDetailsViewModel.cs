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
    public class SmallTreeDetailsViewModel: BaseSmallTreeViewModel {

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public SmallTreeDetailsViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _smallTree = new SMALLTREE();
            _smallTree.SMALLTREEID   = selectedID;
            _smallTreeRepository = new SmallTreeRepository();
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
                _selectedSpecies = PickerService.GetItem(ListSpecies, _smallTree.SPECIESCODE);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _smallTree.SPECIESCODE   = (int)_selectedSpecies.ID;
            }
        }
        void FetchDetails(string fk){
            _smallTree = _smallTreeRepository.GetSmallTreeData(fk);
        }
        private Task Update() {
            try
            {
              
                 _smallTree.LastModified = System.DateTime.UtcNow;
                 _smallTreeRepository.UpdateSmallTree(_smallTree);
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
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Shrub Details", "Delete Shrub Details", "OK", "Cancel");
            if (isUserAccept) {
                _smallTreeRepository.DeleteSmallTree(_smallTree);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Shrubs for plot " + _smallTreeRepository.GetTitle(_smallTree.PLOTID);
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
                SmallTreeValidator _validator = new SmallTreeValidator();
                SmallTreeValidator _fullvalidator = new SmallTreeValidator(true);

                ValidationResult validationResults = _validator.Validate(_smallTree);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_smallTree);

                ParseValidater _parser = new ParseValidater();
                (_smallTree.ERRORCOUNT, _smallTree.ERRORMSG) = _parser.Parse(fullvalidationResults);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
            //        await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Shrubs", validationResults.Errors[0].ErrorMessage, "Ok");
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