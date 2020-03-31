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
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class AddDWDViewModel: BaseDWDViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand AddAccumCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItemsString> ListOrigin { get; set; }
        public List<PickerItems> ListDecompClass { get; set; }
        public List<PickerItems> ListLine { get; set; }
        public AddDWDViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _dwd = new DWD();
            _dwd.DWDID  = selectedID;
            _dwdRepository = new DWDRepository();
            _fk = selectedID;
            AddCommand = new Command(async () => await Update());
            AddAccumCommand = new Command(async () => await Update(true));

            DeleteCommand = new Command(async () => await Delete());
            ListSpecies = PickerService.SpeciesItems().ToList();
            ListOrigin = PickerService.OriginItems().ToList();
            ListDecompClass = PickerService.DecompClassItems().ToList();
            ListLine = PickerService.LineItems().ToList();
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
        private PickerItems _selectedLine = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedLine
        {
            get
            {
                _selectedLine = PickerService.GetItem(ListLine, _dwd.LINE);
                return _selectedLine;
            }
            set
            {
                SetProperty(ref _selectedLine, value);
                _dwd.LINE  = (int)_selectedLine.ID;
            }
        }

        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _dwd.SPECIES);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _dwd.SPECIES  = (int)_selectedSpecies.ID;
            }
        }
        private PickerItemsString _selectedOrigin = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedOrigin
        {
            get
            {
                _selectedOrigin = PickerService.GetItem(ListOrigin, _dwd.ORIGIN );
                return _selectedOrigin;
            }
            set
            {
                SetProperty(ref _selectedOrigin, value);
                _dwd.ORIGIN  = _selectedOrigin.ID;
            }
        }
        private PickerItems _selectedDecompClass = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDecompClass
        {
            get
            {
                _selectedDecompClass = PickerService.GetItem(ListDecompClass, _dwd.DECOMP_CLASS );
                return _selectedDecompClass;
            }
            set
            {
                SetProperty(ref _selectedDecompClass, value);
                _dwd.DECOMP_CLASS  = (int)_selectedDecompClass.ID;
            }
        }



        void FetchDetails(string fk){
            _dwd = _dwdRepository.GetDWDData(fk);
        }
        async Task Update() {
            try
            {
                DWDValidator _dwdValidator = new DWDValidator();
                _dwd.PLOTID = _fk;
                ValidationResult validationResults = _dwdValidator.Validate(_dwd);

                if (validationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("DWD Details", "Save DWD Details", "OK", "Cancel");
                    if (isUserAccept)
                    {

                        _dwdRepository.InsertDWD(_dwd,_fk);
                        //  This is just to slow down the database
                     _dwdRepository.GetDWDData (_dwd.DWDID);
                     await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add DWD", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task Update(bool IsAccum)
        {
            try
            {
                DWDValidator _dwdValidator = new DWDValidator();
                ValidationResult validationResults = _dwdValidator.Validate(_dwd);

                if (validationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("DWD Accumulation Details", "Save DWD Accumulation Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                        _dwd.IS_ACCUM = "Y";
                        _dwd.PLOTID = _fk;
                        _dwdRepository.InsertDWD(_dwd, _fk);
                        //  This is just to slow down the database
                        _dwdRepository.GetDWDData(_dwd.DWDID);
                        await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add DWD Accumulation", validationResults.Errors[0].ErrorMessage, "Ok");
                }
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
                _dwdRepository.DeleteDWD(_dwd.DWDID );
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "DWD Accumulation details for plot " + _dwdRepository.GetTitle(_fk);
            set
            {
            }
        }
        private List<IAutoDropItem> _ItemDemo;
        public List<IAutoDropItem> ItemDemo
        {
            get => _ItemDemo;
            set
            {
                _ItemDemo = value;
                OnPropertyChanged();
            }
        }
    }
}