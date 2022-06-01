using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Utilities;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class PlotDetailsViewModel: BasePlotViewModel {

        public ICommand UpdatePlotCommand { get; private set; }
        public ICommand DeletePlotCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public ICommand StandInfoCommand { get; private set; }
        public ICommand ForestHealthCommand { get; private set; }
        public ICommand PlotCrewCommand { get; private set; }
        public ICommand PhotoCommand { get; private set; }
        public ICommand ValidateCommand { get; private set; }
        public ICommand LocationCommand { get; private set; }
        public List<PickerItems> ListFMU { get; set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItems> ListCanopyOrigin { get; set; }
        public List<PickerItemsString> ListCanopyStructure { get; set; }
        public List<PickerItemsString> ListMaturityClass { get; set; }
        public List<PickerItems> ListNonStandardType { get; set; }
        public List<PickerItemsString> ListMeasurementType { get; set; }
        public List<PickerItemsString> ListNonStandardTypeCode { get; set; }
        public List<PickerItemsString> ListPerson { get; set; }
        public List<PickerItems> ListGrowthPlot { get; set; }
        public List<PickerItemsString> ListGrowthPlotType { get; set; }
        public List<PickerItems> ListAccessCondition { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        private bool _AllowToLeave = false;
        public PlotDetailsViewModel(INavigation navigation, string selectedPlotID)
        {
            _navigation = navigation;
            _plot = new PLOT();
            _plot.PLOTID = selectedPlotID;
            _plotRepository = new PlotRepository();

            UpdatePlotCommand = new Command(async () => await UpdatePlot());
            DeletePlotCommand = new Command(async () => await DeletePlot());
            ListFMU = PickerService.ForestItems().OrderBy(c => c.NAME).ToList();
            CommentsCommand = new Command(async () => await ShowComments());
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            ListCanopyOrigin = PickerService.CanopyOriginItems().OrderBy(c => c.NAME).ToList();
            ListCanopyStructure = PickerService.CanopyStructureItems().OrderBy(c => c.NAME).ToList();
            ListMaturityClass = PickerService.MaturityClassItems().OrderBy(c => c.NAME).ToList();
            ListMeasurementType = PickerService.MeasurementTypeItems().OrderBy(c => c.NAME).ToList();
            ListNonStandardType = PickerService.NonStandardTypeItems().OrderBy(c => c.NAME).ToList();
            ListAccessCondition = PickerService.AccessConditionItems().OrderBy(c => c.NAME).ToList();
            StandInfoCommand = new Command(async () => await ShowStandInfo());
            ForestHealthCommand = new Command(async () => await ShowForestHealth());
            PlotCrewCommand = new Command(async () => await ShowPlotCrew());
            PhotoCommand = new Command(async () => await ShowPhoto());
            LocationCommand = new Command(async () => await DoLocation());
            ValidateCommand = new Command(async () => await DoValidate());

            ListGrowthPlot = PickerService.GrowthPlotItems().OrderBy(c => c.NAME).ToList();
            ListGrowthPlotType = PickerService.GrowthPlotTypeItems().OrderBy(c => c.NAME).ToList();
            FetchPlotDetails();
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
//            ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
            ListPerson = PickerService.FillPersonPicker(_plotRepository.GetPersonList(_plot.PROJECTID)).OrderBy(c => c.NAME).ToList();
        }
        public bool AllowPlotDeletion
        {
            get
            { 
                Utils util = new Utils();
                return util.AllowPlotDeletion; 
            }
            set
            {
            }
        }
        async Task ShowComments()
        {
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                // launch the form - filtered to a specific tree
                await _navigation.PushAsync(new PlotComments(_plot));
            }

        }
        async Task ShowStandInfo()
        {
            // launch the form - filtered to a specific tree
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new StandInfo(_plot));
            }
        }
        async Task DoValidate()
        {
            _ = UpdatePlot(); 
            FullValidater _validater = new FullValidater(_plot);
            if (_validater.ValidAll())
            {
                await Application.Current.MainPage.DisplayAlert("Validater", "No errors found", "OK", "Cancel");
            }
            else
            {
                string errmsg = _validater.msg;
                Utils util = new Utils();
                util.ErrorList = errmsg;
                await Application.Current.MainPage.DisplayAlert("Validation Errors", "Errors found. Go to the Info tab for error details", "OK", "Cancel");
            }
        }

        async Task ShowForestHealth()
        {
            // launch the form - filtered to a specific tree
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new ForestHealth(_plot));
            }
        }
        async Task ShowPlotCrew()
        {
            // launch the form - filtered to a specific tree
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new PlotCrew(_plot));
            }
        }
        async Task ShowPhoto()
        {
            // launch the form - filtered to a specific photo list
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new PhotoList(_plot.PLOTID));
            }
        }
        private async Task<bool> TrySave()
        {
            PlotValidator _validator = new PlotValidator();
            ValidationResult validationResults = _validator.Validate(_plot);
            if (validationResults.IsValid)
            {
                _ = UpdatePlot();
                return true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Update Plot", validationResults.Errors[0].ErrorMessage, "Ok");
                return false;
            }
        }
        private List<PickerItemsString> FillPersonPicker()
        {
            var list = new List<PickerItemsString>();
            foreach (var newperson in _plotRepository.GetPersonList(_plot.PROJECTID))
            {
                var newitem = new PickerItemsString() { ID = newperson.LASTNAME + " " + newperson.FIRSTNAME, NAME = newperson.LASTNAME + ", " + newperson.FIRSTNAME };
                list.Add(newitem);
            };
            return list;
        }
        private PickerItems _selectedAccessCondition = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedAccessCondition
        {
            get
            {
                _selectedAccessCondition = PickerService.GetItem(ListAccessCondition, _plot.ACCESSCONDITIONCODE);
                return _selectedAccessCondition;
            }
            set
            {
                SetProperty(ref _selectedAccessCondition, value);
                _plot.ACCESSCONDITIONCODE = (int)_selectedAccessCondition.ID;
            }
        }

        private PickerItemsString _selectedCrew1 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrew1
        {
            get
            {
                _selectedCrew1 = PickerService.GetItem(ListPerson, _plot.FIELD_CREW1);
                return _selectedCrew1;
            }
            set
            {
                SetProperty(ref _selectedCrew1, value);
                _plot.FIELD_CREW1 = _selectedCrew1.ID;
            }
        }
        private PickerItemsString _selectedCrew2 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrew2
        {
            get
            {
                _selectedCrew2 = PickerService.GetItem(ListPerson, _plot.FIELD_CREW2);
                return _selectedCrew2;
            }
            set
            {
                SetProperty(ref _selectedCrew2, value);
                _plot.FIELD_CREW2 = _selectedCrew2.ID;
            }
        }
        private PickerItemsString _selectedCrew3 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrew3
        {
            get
            {
                _selectedCrew3 = PickerService.GetItem(ListPerson, _plot.FIELD_CREW3);
                return _selectedCrew3;
            }
            set
            {
                SetProperty(ref _selectedCrew3, value);
                _plot.FIELD_CREW3 = _selectedCrew3.ID;
            }
        }
        private PickerItemsString _selectedCrew4 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrew4
        {
            get
            {
                _selectedCrew4 = PickerService.GetItem(ListPerson, _plot.FIELD_CREW4);
                return _selectedCrew4;
            }
            set
            {
                SetProperty(ref _selectedCrew4, value);
                _plot.FIELD_CREW4 = _selectedCrew4.ID;
            }
        }
        private PickerItemsString _selectedCrew5 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrew5
        {
            get
            {
                _selectedCrew5 = PickerService.GetItem(ListPerson, _plot.FIELD_CREW5);
                return _selectedCrew5;
            }
            set
            {
                SetProperty(ref _selectedCrew5, value);
                _plot.FIELD_CREW5 = _selectedCrew5.ID;
            }
        }
        private PickerItemsString _selectedCrew6 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrew6
        {
            get
            {
                _selectedCrew6 = PickerService.GetItem(ListPerson, _plot.FIELD_CREW6);
                return _selectedCrew6;
            }
            set
            {
                SetProperty(ref _selectedCrew6, value);
                _plot.FIELD_CREW6 = _selectedCrew6.ID;
            }
        }


        private PickerItemsString _selectedMeasurementType = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMeasurementType
        {
            get
            {
                _selectedMeasurementType = PickerService.GetItem(ListMeasurementType, _plot.MEASURETYPECODE);
                return _selectedMeasurementType;
            }
            set
            {
                SetProperty(ref _selectedMeasurementType, value);
                _plot.MEASURETYPECODE = _selectedMeasurementType.ID;
            }
        }
        private PickerItems _selectedNonStandardType = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedNonStandardType
        {
            get
            {
                _selectedNonStandardType = PickerService.GetItem(ListNonStandardType, _plot.NONSTANDARDTYPECODE);
                return _selectedNonStandardType;
            }
            set
            {
                SetProperty(ref _selectedNonStandardType, value);
                _plot.NONSTANDARDTYPECODE = (int)_selectedNonStandardType.ID;
            }
        }
        private PickerItems _selectedGrowthPlot = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedGrowthPlot
        {
            get
            {
                _selectedGrowthPlot = PickerService.GetItem(ListGrowthPlot, _plot.GROWTHPLOTNUMBER);
                return _selectedGrowthPlot;
            }
            set
            {
                SetProperty(ref _selectedGrowthPlot, value);
                _plot.GROWTHPLOTNUMBER = (int)_selectedGrowthPlot.ID;
            }
        }
        private PickerItemsString _selectedGrowthPlotType = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedGrowthPlotType
        {
            get
            {
                _selectedGrowthPlotType = PickerService.GetItem(ListGrowthPlotType, _plot.EXISTINGPLOTTYPECODE);
                return _selectedGrowthPlotType;
            }
            set
            {
                SetProperty(ref _selectedGrowthPlotType, value);
                _plot.EXISTINGPLOTTYPECODE = _selectedGrowthPlotType.ID;
            }
        }
        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _plot.LEAD_SPP);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _plot.LEAD_SPP = (int)_selectedSpecies.ID;
            }
        }
      
    

   
        void FetchPlotDetails(){
            _plot = _plotRepository.GetPlotData(_plot.PLOTID);
        }

        private Task UpdatePlot()
        {  
            _plot.LastModified = System.DateTime.UtcNow;
            _plotRepository.UpdatePlot(_plot);
            return Task.CompletedTask;

        }
        async Task DoLocation()
        {
            // launch the form - filtered to a specific photo list

            Location _location = new Location();
            var tuple = await _location.GetCurrentLocation();
            UTM_EASTING = tuple.Item1;
            UTM_NORTHING = tuple.Item2;
            UTM_ZONE = tuple.Item3;

            //     await Application.Current.MainPage.DisplayAlert("Location", str, "Ok");
        }
        async Task DeletePlot() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Plot Details", "Delete Plot Details", "OK", "Cancel");
            if (isUserAccept) {
                if (_plotRepository.AllowDelete(_plot))
                {
                    _plotRepository.DeletePlot(_plot);
                    _AllowToLeave = true;
                    await _navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Cannot delete plot", "This plot has related trees, small trees, dwd, veg, soil or ecosites.  Delete those records first before deleting the plot.", "OK", "Cancel");
                }
            }
        }
        public string PlotTitle
        {
            get => "Plot Details for plot " + _plot.VSNPLOTNAME;
            set{
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

                PlotValidator _validator = new PlotValidator();
                PlotValidator _fullvalidator = new PlotValidator(true);

                ValidationResult validationResults = _validator.Validate(_plot);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_plot);

                ParseValidater _parser = new ParseValidater();
                (_plot.ERRORCOUNT, _plot.ERRORMSG) = _parser.Parse(fullvalidationResults);

//                PlotValidator _Validator = new PlotValidator();
  //              ValidationResult validationResults = _Validator.Validate(_plot);
                if (validationResults.IsValid)
                {
                    _ = UpdatePlot();
                    Shell.Current.Navigating -= Current_Navigating;
// await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Plot", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
             //   await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}