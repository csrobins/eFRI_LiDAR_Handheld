using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Validator;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class PlotDetailsViewModel: BasePlotViewModel {

        public ICommand UpdatePlotCommand { get; private set; }
        public ICommand DeletePlotCommand { get; private set; }

        public PlotDetailsViewModel(INavigation navigation, string selectedPlotID) {
            _navigation = navigation;
            _plot = new PLOT();
            _plot.PLOTID = selectedPlotID;
            _plotRepository = new PlotRepository();

            UpdatePlotCommand = new Command(async () => await UpdatePlot());
            DeletePlotCommand = new Command(async () => await DeletePlot());

            FetchPlotDetails();
        }

        void FetchPlotDetails(){
            _plot = _plotRepository.GetPlotData(_plot.PLOTID);
        }

        async Task UpdatePlot() {
           var validationResults = _plotValidator.Validate(_plot);

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
    }
}