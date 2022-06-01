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
    public class AddVegetationViewModel: BaseVegetationViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItemsString> ListVeg { get; set; }
        public VEGETATIONCENSUS _vegetationcensus;
        public VegetationCensusRepository _vegetationcensusRepository;
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        private bool _AllowToLeave = false;
        public AddVegetationViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _vegetation = new VEGETATION();
            _vegetationcensus = new VEGETATIONCENSUS();
            _vegetation.PLOTID  = selectedID;
            _vegetationRepository = new VegetationRepository();
            _vegetationcensusRepository = new VegetationCensusRepository();

            _fk = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListVeg = PickerService.VegItems().ToList();
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
        private PickerItemsString _selectedVeg = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedVeg
        {
            get
            {
                _selectedVeg = PickerService.GetItem(ListVeg, _vegetation.VSNSPECIESCODE);
                return _selectedVeg ;
            }
            set
            {
                SetProperty(ref _selectedVeg, value);
                _vegetation.VSNSPECIESCODE = _selectedVeg.ID;
                
            }
        }
        void FetchDetails(string fk){
            _vegetation = _vegetationRepository.GetVegetationData(fk);
        }
        private Task Update() {
            try
            {

                _vegetation.IsDeleted = "N"; 
                _vegetation.Created = System.DateTime.UtcNow;
                _vegetation.LastModified = _vegetation.Created;
                _vegetationRepository.InsertVegetation(_vegetation,_fk);
                ////Add in the species to the VSN table
                ///// removed on May 5
                //_vegetationcensus.IsDeleted = "N";
                //_vegetationcensus.Created = _vegetation.Created;
                //_vegetationcensus.LastModified = _vegetation.Created;
                //_vegetationcensus.VSNSPECIESCODE = _vegetation.VSNSPECIESCODE;
                //_vegetationcensusRepository.InsertVegetation(_vegetationcensus, _fk);
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
                _AllowToLeave = true;
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Vegetation layer for plot " + _vegetationRepository.GetTitle(_fk);
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
                if (!_AllowToLeave)
                {
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

                VegetationValidator _validator = new VegetationValidator();
                VegetationValidator _fullvalidator = new VegetationValidator(true);

                ValidationResult validationResults = _validator.Validate(_vegetation);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_vegetation);

                ParseValidater _parser = new ParseValidater();
                (_vegetation.ERRORCOUNT, _vegetation.ERRORMSG) = _parser.Parse(fullvalidationResults);

 //               VegetationValidator _validator = new VegetationValidator();
   //             ValidationResult validationResults = _validator.Validate(_vegetation);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
           //         await Shell.Current.GoToAsync("..", true);
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
        //        await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}