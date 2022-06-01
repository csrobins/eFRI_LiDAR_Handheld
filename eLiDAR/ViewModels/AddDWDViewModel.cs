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
    public class AddDWDViewModel: BaseDWDViewModel {
        private Utilities.Utils util = new Utilities.Utils();  
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
        public AddDWDViewModel(INavigation navigation, string selectedID, bool IsAccumulation = false)
        {
            _navigation = navigation;
            _dwd = new DWD();
            _isaccum = IsAccumulation; 
            _dwd.DWDID  = selectedID;
            _dwdRepository = new DWDRepository();
            _fk = selectedID;
            _dwd.BURNED = "N";
            _dwd.HOLLOW = "N";
            AddCommand = new Command(async () => await Update());
            AddAccumCommand = new Command(async () => await Update(IsAccumulation));

            DeleteCommand = new Command(async () => await Delete());
            ListSpecies = PickerService.SpeciesItems().ToList();
            ListOrigin = PickerService.OriginItems().ToList();
            ListDecompClass = PickerService.DecompClassItems().ToList();
            ListLine = PickerService.LineItems().ToList();
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
        private void GetNextNum()
        {
            if (util.AllowAutoNumber && !_isaccum && DWDNUM == 0 && LINE > 0) { DWDNUM = _dwdRepository.GetNextNumber(_fk); }
        }
        //public int LINE
        //{
        //    get => _dwd.LINENUMBER;
        //    set
        //    {
        //        _dwd.LINENUMBER = value;
        //        NotifyPropertyChanged("LINE");
        //        IsChanged = true;
        //        GetNextNum();
        //    }
        //}

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
                _dwd.LINENUMBER  = (int)_selectedLine.ID;
                GetNextNum();
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
                _dwd.SPECIESCODE  = (int)_selectedSpecies.ID;
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
                _dwd.DOWNWOODYDEBRISORIGINCODE  = _selectedOrigin.ID;
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



        void FetchDetails(string fk){
            _dwd = _dwdRepository.GetDWDData(fk);
        }
        private Task Update() {
            try
            {
                
                _dwd.Created = System.DateTime.UtcNow;
                _dwd.IsDeleted = "N";
                _dwd.IS_ACCUM = "N";
                _dwd.LastModified = _dwd.Created;
                _dwdRepository.InsertDWD(_dwd,_fk);
                        //  This is just to slow down the database
                _dwdRepository.GetDWDData (_dwd.DWDID);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                return Task.CompletedTask;                         //  Log.Fatal(e);
            };
        }
        private Task Update(bool IsAccum)
        {
            try
            {
                _dwd.IS_ACCUM = "Y";
                _dwd.IsDeleted = "N";
                _dwd.PLOTID = _fk;
                _dwd.Created = System.DateTime.UtcNow;
                _dwd.LastModified = _dwd.Created;
                _dwdRepository.InsertDWD(_dwd, _fk);
                //  This is just to slow down the database
                 _dwdRepository.GetDWDData(_dwd.DWDID);
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
                if (_isaccum) { return "DWD Accumulation details for plot " + _dwdRepository.GetTitle(_fk); }
                else { return "DWD details for plot " + _dwdRepository.GetTitle(_fk); }
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




//                DWDValidator _validator = new DWDValidator();
  //              ValidationResult validationResults = _validator.Validate(_dwd);
                if (validationResults.IsValid)
                {
                    if (_isaccum) { _ = Update(true); }
                    else { _ = Update(); }
                    Shell.Current.Navigating -= Current_Navigating;
            //        await Shell.Current.GoToAsync("..", true);
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
      //          await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}