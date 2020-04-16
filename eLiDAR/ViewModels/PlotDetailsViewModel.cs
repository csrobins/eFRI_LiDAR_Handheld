using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
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
        public List<PickerItems> ListFMU { get; set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItems> ListCanopyOrigin { get; set; }
        public List<PickerItemsString> ListCanopyStructure { get; set; }
        public List<PickerItemsString> ListMaturityClass { get; set; }
        public List<PickerItems> ListNonStandardType { get; set; }


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
            FetchPlotDetails();
        }
        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new PlotComments(_plot));
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
                _selectedMaturityClass = PickerService.GetItem(ListMaturityClass, _plot.MATURITY);
                return _selectedMaturityClass;
            }
            set
            {
                SetProperty(ref _selectedMaturityClass, value);
                _plot.MATURITY = _selectedMaturityClass.ID;
            }
        }
        private PickerItemsString _selectedCanopyStructure = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCanopyStructure
        {
            get
            {
                _selectedCanopyStructure = PickerService.GetItem(ListCanopyStructure, _plot.CANOPY_STRUCTURE);
                return _selectedCanopyStructure;
            }
            set
            {
                SetProperty(ref _selectedCanopyStructure, value);
                _plot.CANOPY_STRUCTURE = _selectedCanopyStructure.ID;
            }
        }

        private PickerItems _selectedFMU = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedFMU
        {
            get
            {
                //_selectedSpecies.ID = PickerService.GetIndex(ListSpecies, _tree.SPECIES);
                //_selectedSpecies.NAME = PickerService.GetValue(ListSpecies, _tree.SPECIES);
                _selectedFMU = PickerService.GetItem(ListFMU, _plot.FMU);
                return _selectedFMU;
            }
            set
            {
                SetProperty(ref _selectedFMU, value);
                _plot.FMU = (int)_selectedFMU.ID;
            }
        }
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
            get => "Plot Details for plot " + _plot.PLOTNUM;
            set{
            }
        }
    }
}