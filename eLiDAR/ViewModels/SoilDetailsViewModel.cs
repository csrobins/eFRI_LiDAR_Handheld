using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Validator;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class SoilDetailsViewModel: BaseSoilViewModel {

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListPorePattern { get; set; }

        public SoilDetailsViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _soil = new SOIL();
            _soil.SOILID  = selectedID;
            _soilRepository = new SoilRepository();
           //_fk = selectedID;
            _selectedid = selectedID;
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListPorePattern = PickerService.PorePatternItems().ToList();
 
            // Get the soil
           FetchDetails(_selectedid);
            
        }

        private PickerItems _selectedPorePattern = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedPorePattern
        {
            get
            {
                _selectedPorePattern = PickerService.GetItem(ListPorePattern, _soil.PORE_PATTERN);
                return _selectedPorePattern;
            }
            set
            {
                SetProperty(ref _selectedPorePattern, value);
                _soil.PORE_PATTERN  = (int)_selectedPorePattern.ID;
            }
        }
        void FetchDetails(string fk){
            _soil = _soilRepository.GetSoilData(fk);
        }
        async Task Update() {
            try
            {
                SoilValidator _soilValidator = new SoilValidator();
                ValidationResult validationResults = _soilValidator.Validate(_soil);

                if (validationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Soil Details", "Save Soil Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                        
                      _soilRepository.UpdateSoil(_soil);
                      await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Soil", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Soil Details", "Delete Soil Details", "OK", "Cancel");
            if (isUserAccept) {
                _soilRepository.DeleteSoil(_soil.SOILID );
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Soil layers for plot " + _soilRepository.GetTitle(_soil.PLOTID);
            set
            {
            }
        }
    }
}