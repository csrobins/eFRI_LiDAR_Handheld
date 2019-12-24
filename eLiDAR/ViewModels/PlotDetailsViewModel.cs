using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Validator;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class PlotDetailsViewModel: BasePlotViewModel {

        public ICommand UpdatePlotCommand { get; private set; }
        public ICommand DeletePlotCommand { get; private set; }
        public List<PickerItems> ListFMU { get; set; }

        public PlotDetailsViewModel(INavigation navigation, string selectedPlotID)
        {
            _navigation = navigation;
            _plot = new PLOT();
            _plot.PLOTID = selectedPlotID;
            _plotRepository = new PlotRepository();

            UpdatePlotCommand = new Command(async () => await UpdatePlot());
            DeletePlotCommand = new Command(async () => await DeletePlot());
            ListFMU = PickerService.ForestItems().OrderBy(c => c.NAME).ToList();
            FetchPlotDetails();
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