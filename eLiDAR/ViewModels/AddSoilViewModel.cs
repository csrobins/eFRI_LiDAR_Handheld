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
    public class AddSoilViewModel: BaseSoilViewModel {
        private Utilities.Utils util = new Utilities.Utils(); 
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SoilHorizonCommand { get; private set; }
        public ICommand SoilStructureCommand { get; private set; }
        public ICommand ColourCommand { get; private set; }
        public ICommand MottleColourCommand { get; private set; }
        public ICommand GleyColourCommand { get; private set; }
        public ICommand TextureCommand { get; private set; }
        public List<PickerItemsString> ListPorePattern { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        private bool _AllowToLeave = false;

        public AddSoilViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _soil = new SOIL();
            _soil.SOILID  = selectedID;
            _soilRepository = new SoilRepository();
            _fk = selectedID;
            _soil.DEPTHFROM = 0;
            _soil.DEPTHTO = 0;
            
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListPorePattern = PickerService.PorePatternItems().ToList();
            SoilHorizonCommand = new Command(async () => await ShowSoilHorizon());
            SoilStructureCommand = new Command(async () => await ShowSoilStructure());
            ColourCommand = new Command(async () => await ShowColour());
            MottleColourCommand = new Command(async () => await ShowMottleColour());
            GleyColourCommand = new Command(async () => await ShowGleyColour());
            TextureCommand = new Command(async () => await ShowTexture());
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
            if (util.AllowAutoNumber) { _soil.HORIZONNUMBER = _soilRepository.GetNextNumber(_fk); }

        }
        public void Refresh()
        {
            NotifyPropertyChanged("HorizonButton");
            NotifyPropertyChanged("StructureButton");
            NotifyPropertyChanged("ColourButton");
            NotifyPropertyChanged("MottleColourButton");
            NotifyPropertyChanged("GleyColourButton");
            NotifyPropertyChanged("TextureButton");
        }
        async Task ShowColour()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new SoilColour(_soil, IsChanged ));
        }
        async Task ShowMottleColour()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new MottleColour(_soil, IsChanged));
        }
        async Task ShowGleyColour()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new GleyColour(_soil));
        }
        async Task ShowSoilStructure()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new SoilStructure(_soil));
        }
        async Task ShowSoilHorizon()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new SoilHorizon(_soil));
        }
        async Task ShowTexture()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new Texture(_soil));
        }
        public string ColourButton
        {
            get
            {
                if (COLOUR == null) { return "Matrix Colour"; }
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
                if (MOTTLE_COLOUR == null) { return "Mottle Colour"; }
                else { return MOTTLE_COLOUR; }
            }
            set
            {
            }
        }
        public string GleyColourButton
        {
            get
            {
                if (GLEYCOLOUR == null) { return "Gley Colour"; }
                else { return GLEYCOLOUR; }
            }
            set
            {
            }
        }
        public string TextureButton
        {
            get
            {
                if (TEXTURE == null) { return "Mineral Texture"; }
                else { return TEXTURE; }
            }
            set
            {
            }
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
              //  SetProperty(ref _selectedPorePattern, value);
                _soil.POREPATTERNCODE = _selectedPorePattern.ID;
                Utilities.Utils _util = new Utilities.Utils();
                _soil.POREPATTERNCODE = _util.getPorePattern(_soil);
                SetProperty(ref _selectedPorePattern, PickerService.GetItem(ListPorePattern, _soil.POREPATTERNCODE));
                NotifyPropertyChanged("SelectedPorePattern");
            }
        }
        void FetchDetails(string fk){
            _soil = _soilRepository.GetSoilData(fk);
        }
        private Task Update() {
            try
            {
              _soil.IsDeleted = "N";
              _soil.Created = System.DateTime.UtcNow;
              _soil.LastModified = _soil.Created;
              _soilRepository.InsertSoil(_soil,_fk);
                        //  This is just to slow down the database
                     _soilRepository.GetSoilData(_soil.SOILID);
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
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Soil Details", "Delete Soil Details", "OK", "Cancel");
            if (isUserAccept) {
                _AllowToLeave = true;
                _soilRepository.DeleteSoil(_soil);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Soil layer for plot " + _soilRepository.GetTitle(_fk);
            set
            {
            }
        }
        public string HorizonButton
        {
            get
            {
                if (HORIZON == "L" || HORIZON == "F" || HORIZON == "H" || HORIZON == "LM" || HORIZON == "Of" || HORIZON == "Of1" || HORIZON == "Of2" || HORIZON == "Of3" || HORIZON == "Of4" || HORIZON == "Om" || HORIZON == "Om1" || HORIZON == "Om2" || HORIZON == "Oh" || HORIZON == "Oh1" || HORIZON == "Oh2")
                {
                    IsOrganic = true;
                }
                else { IsOrganic = false; }
                if (HORIZON == null) { return "Horizon"; }
                else { return HORIZON; }
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
        private void OnAppearing()
        {
            _AllowToLeave = false;
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
                if (!_AllowToLeave) {
                    e.Cancel();
                    await GoBack();
                }
            }
        }
        private async Task GoBack()
        {
            // display Alert for confirmation
            if (IsChanged)
            {

                SoilValidator _validator = new SoilValidator();
                SoilValidator _fullvalidator = new SoilValidator(true);

                ValidationResult validationResults = _validator.Validate(_soil);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_soil);

                ParseValidater _parser = new ParseValidater();
                (_soil.ERRORCOUNT, _soil.ERRORMSG) = _parser.Parse(fullvalidationResults);


//                SoilValidator _validator = new SoilValidator();
  //              ValidationResult validationResults = _validator.Validate(_soil );
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
             //       await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Soil", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
        //        await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}