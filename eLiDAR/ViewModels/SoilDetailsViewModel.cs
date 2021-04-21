using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class SoilDetailsViewModel: BaseSoilViewModel {

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SoilHorizonCommand { get; private set; }
        public ICommand SoilStructureCommand { get; private set; }
        public ICommand ColourCommand { get; private set; }
        public ICommand MottleColourCommand { get; private set; }

        public List<PickerItemsString> ListPorePattern { get; set; }

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
            SoilHorizonCommand = new Command(async () => await ShowSoilHorizon());
            SoilStructureCommand = new Command(async () => await ShowSoilStructure());
            ColourCommand = new Command(async () => await ShowColour());
            MottleColourCommand = new Command(async () => await ShowMottleColour());
            // Get the soil
            FetchDetails(_selectedid);
            
        }
        public void Refresh()
        {
            NotifyPropertyChanged("HorizonButton");
            NotifyPropertyChanged("StructureButton");
            NotifyPropertyChanged("ColourButton");
            NotifyPropertyChanged("MottleColourButton");

        }
        async Task ShowSoilStructure()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new SoilStructure(_soil));
        }
        async Task ShowColour()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new SoilColour(_soil));
        }
        async Task ShowMottleColour()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new MottleColour(_soil));
        }
        public string ColourButton
        {
            get
            {
                if (COLOUR == null) { return "Colour"; }
                else { return COLOUR; }
            }
            set
            {
            }
        }
        public string MottleColourButton
        {
            get
            {
                if (MOTTLE_COLOUR  == null) { return "Mottle Colour"; }
                else { return MOTTLE_COLOUR; }
            }
            set
            {
            }
        }
        public string StructureButton
        {
            get
            {
                if (STRUCTURE == null) { return "Structure"; }
                else { return STRUCTURE; }
            }
            set
            {
            }
        }
        public string HorizonButton
        {
            get
            {
                if (HORIZON == null) { return "Horizon"; }
                else { return HORIZON; }
            }
            set
            {
            }
        }
        async Task ShowSoilHorizon()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new SoilHorizon(_soil));
        }
        private PickerItemsString _selectedPorePattern = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedPorePattern
        {
            get
            {
                _selectedPorePattern = PickerService.GetItem(ListPorePattern, _soil.POREPATTERNCODE);
                return _selectedPorePattern;
            }
            set
            {
                SetProperty(ref _selectedPorePattern, value);
                _soil.POREPATTERNCODE  = _selectedPorePattern.ID;
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
                        _soil.LastModified = System.DateTime.UtcNow;
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