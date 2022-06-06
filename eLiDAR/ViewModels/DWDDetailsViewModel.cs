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
    public class DWDDetailsViewModel: BaseDWDViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand AddAccumCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItemsString> ListOrigin { get; set; }
        public List<PickerItems> ListDecompClass { get; set; }
        public List<PickerItems> ListLine { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        private bool _isaccum;
        public DWDDetailsViewModel(INavigation navigation, string selectedID, bool IsAccumulation = false) {
            _navigation = navigation;
            _dwd = new DWD ();
            //_vegetation.VEGETATIONID  = selectedID;
            _dwdRepository = new DWDRepository();
            _fk = selectedID;
            _isaccum = IsAccumulation; 
            AddCommand = new Command(async () => await Update(false));
            AddAccumCommand = new Command(async () => await Update(true));

            DeleteCommand = new Command(async () => await Delete());
            ListSpecies = PickerService.SpeciesItems().ToList();
            ListOrigin = PickerService.OriginItems().ToList();
            ListDecompClass = PickerService.DecompClassItems().ToList();
            ListLine = PickerService.LineItems().ToList();
            FetchDetails(selectedID);
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
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
            _dwd = _dwdRepository.GetDWDData (fk);
        }
        private PickerItems _selectedLine = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedLine
        {
            get
            {
                _selectedLine = PickerService.GetItem(ListLine, _dwd.LINENUMBER);
                return _selectedLine;
            }
            set
            {
                SetProperty(ref _selectedLine, value);
                _dwd.LINENUMBER = (int)_selectedLine.ID;
            }
        }

        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _dwd.SPECIESCODE);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _dwd.SPECIESCODE = (int)_selectedSpecies.ID;
            }
        }
        private PickerItemsString _selectedOrigin = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedOrigin
        {
            get
            {
                _selectedOrigin = PickerService.GetItem(ListOrigin, _dwd.DOWNWOODYDEBRISORIGINCODE);
                return _selectedOrigin;
            }
            set
            {
                SetProperty(ref _selectedOrigin, value);
                _dwd.DOWNWOODYDEBRISORIGINCODE = _selectedOrigin.ID;
            }
        }
        private PickerItems _selectedDecompClass = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDecompClass
        {
            get
            {
                _selectedDecompClass = PickerService.GetItem(ListDecompClass, _dwd.DECOMPOSITIONCLASS );
                return _selectedDecompClass;
            }
            set
            {
                SetProperty(ref _selectedDecompClass, value);
                _dwd.DECOMPOSITIONCLASS = (int)_selectedDecompClass.ID;
            }
        }
        async Task Update(bool IsAccum) {
            try
            {   
                        if (IsAccum)
                        {
                            _dwd.IS_ACCUM = "Y";
                        }
                        else {
                            _dwd.IS_ACCUM = "N";
                        }
                        _dwd.LastModified = System.DateTime.UtcNow;
                        _dwdRepository.UpdateDWD (_dwd);
                        //  This is just to slow down the database
                     _dwdRepository.GetDWDData(_dwd.DWDID );
                  }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("DWD Details", "Delete DWD Details", "OK", "Cancel");
            if (isUserAccept) {
                _dwdRepository.DeleteDWD(_dwd);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get
            {
                if (_isaccum) { return "DWD Accumulation details for plot " + _dwdRepository.GetTitle(_dwd.PLOTID); }
                else { return "DWD details for plot " + _dwdRepository.GetTitle(_dwd.PLOTID ); }
            }
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
                DWDValidator _validator = new DWDValidator();
                DWDValidator _fullvalidator = new DWDValidator(true);

                ValidationResult validationResults = _validator.Validate(_dwd);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_dwd);

                ParseValidater _parser = new ParseValidater();
                (_dwd.ERRORCOUNT, _dwd.ERRORMSG) = _parser.Parse(fullvalidationResults);
                if (validationResults.IsValid)
                {
                    _ = Update(_isaccum);
                    Shell.Current.Navigating -= Current_Navigating;
               //     await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update DWD", validationResults.Errors[0].ErrorMessage, "Ok");
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