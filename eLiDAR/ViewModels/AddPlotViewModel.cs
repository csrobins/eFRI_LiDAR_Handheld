using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Validator;
using eLiDAR.Views;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class AddPlotViewModel : BasePlotViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand ViewAllCommand { get; private set; }

        public AddPlotViewModel(INavigation navigation){
            _navigation = navigation;
            _plotValidator = new PlotValidator();
            _plot = new PLOT();
            _plotRepository = new PlotRepository();
            AddCommand = new Command(async () => await AddPlot()); 
            ViewAllCommand = new Command(async () => await ShowList()); 
        }

       async Task ShowList(){ 
            await _navigation.PushAsync(new PlotList());
        }
        async Task AddPlot()
        {
            var validationResults = _plotValidator.Validate(_plot);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Project", "Do you want to save project details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _plotRepository.InsertPlot(_plot);
                    await _navigation.PushAsync(new ProjectList());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Project", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }
        public bool IsViewAll => _plotRepository.GetAllData().Count > 0 ? true : false;
    }
}
