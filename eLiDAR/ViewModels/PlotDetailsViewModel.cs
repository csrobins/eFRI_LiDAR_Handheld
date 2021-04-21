using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Services;
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
            StandInfoCommand = new Command(async () => await ShowStandInfo());
            ForestHealthCommand = new Command(async () => await ShowForestHealth());
            PlotCrewCommand = new Command(async () => await ShowPlotCrew());
            ListGrowthPlot = PickerService.GrowthPlotItems().OrderBy(c => c.NAME).ToList();

            FetchPlotDetails();

            ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
                   }
        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new PlotComments(_plot));
        }
        async Task ShowStandInfo()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new StandInfo(_plot));
        }
        async Task ShowForestHealth()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new ForestHealth(_plot));
        }
        async Task ShowPlotCrew()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new PlotCrew(_plot));
        }
        private List<PickerItemsString> FillPersonPicker()
        {
            var list = new List<PickerItemsString>();
            foreach (var newperson in _plotRepository.GetPersonList(_plot.PROJECTID))
            {
                var newitem = new PickerItemsString() { ID = newperson.LASTNAME + ", " + newperson.FIRSTNAME, NAME = newperson.LASTNAME + ", " + newperson.FIRSTNAME };
                list.Add(newitem);
            };
            return list;
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
        private PickerItems _selectedCanopyOrigin = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCanopyOrigin
        {
            get
            {
                _selectedCanopyOrigin = PickerService.GetItem(ListCanopyOrigin, _plot.ORIGIN);
                return _selectedCanopyOrigin;
            }
            set
            {
                SetProperty(ref _selectedCanopyOrigin, value);
                _plot.ORIGIN = (int)_selectedCanopyOrigin.ID;
            }
        }
        private PickerItemsString _selectedMaturityClass = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaturityClass
        {
            get
            {
                _selectedMaturityClass = PickerService.GetItem(ListMaturityClass, _plot.MATURITYCLASSCODE);
                return _selectedMaturityClass;
            }
            set
            {
                SetProperty(ref _selectedMaturityClass, value);
                _plot.MATURITYCLASSCODE = _selectedMaturityClass.ID;
            }
        }
        private PickerItemsString _selectedCanopyStructure = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCanopyStructure
        {
            get
            {
                _selectedCanopyStructure = PickerService.GetItem(ListCanopyStructure, _plot.CANOPYSTRUCTURECODE);
                return _selectedCanopyStructure;
            }
            set
            {
                SetProperty(ref _selectedCanopyStructure, value);
                _plot.CANOPYSTRUCTURECODE = _selectedCanopyStructure.ID;
            }
        }

        //private PickerItems _selectedFMU = new PickerItems { ID = 0, NAME = "" };
        //public PickerItems SelectedFMU
        //{
        //    get
        //    {
        //        //_selectedSpecies.ID = PickerService.GetIndex(ListSpecies, _tree.SPECIES);
        //        //_selectedSpecies.NAME = PickerService.GetValue(ListSpecies, _tree.SPECIES);
        //        _selectedFMU = PickerService.GetItem(ListFMU, _plot.FMU);
        //        return _selectedFMU;
        //    }
        //    set
        //    {
        //        SetProperty(ref _selectedFMU, value);
        //        _plot.FMU = (int)_selectedFMU.ID;
        //    }
        //}
        void FetchPlotDetails(){
            _plot = _plotRepository.GetPlotData(_plot.PLOTID);
        }

        async Task UpdatePlot() {
            //  var validationResults = _projectValidator.Validate(_project);
            PlotValidator _plotValidator = new PlotValidator();
            ValidationResult validationResults = _plotValidator.Validate(_plot);
 
            if (validationResults.IsValid) {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Plot Details", "Update Plot Details", "OK", "Cancel");
                if (isUserAccept) {
                    _plot.LastModified = System.DateTime.UtcNow;
                    _plotRepository.UpdatePlot(_plot);
                    await _navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Plot", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        async Task DeletePlot() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Plot Details", "Delete Plot Details", "OK", "Cancel");
            if (isUserAccept) {
                _plotRepository.DeletePlot(_plot.PLOTID);
                await _navigation.PopAsync();
            }
        }
        public string PlotTitle
        {
            get => "Plot Details for plot " + _plot.VSNPLOTNAME;
            set{
            }
        }
    }
}