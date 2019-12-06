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
            AddCommand = new Command(async () => await AddPlot("")); 
            ViewAllCommand = new Command(async () => await ShowList()); 
        }

        public AddPlotViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _plotValidator = new PlotValidator();
            _plot = new PLOT();
            _plotRepository = new PlotRepository();
            _selectedprojectid = fk;
            AddCommand = new Command(async () => await AddPlot(_selectedprojectid));
            ViewAllCommand = new Command(async () => await ShowList());
        }

        async Task ShowList(){ 
            await _navigation.PushAsync(new PlotList());
        }
        async Task AddPlot(string fk)
        {
            var validationResults = _plotValidator.Validate(_plot);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Plot", "Do you want to save plot details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _plotRepository.InsertPlot(_plot, fk);
                    await _navigation.PushAsync(new PlotList(fk));
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Plot", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }
        public bool IsViewAll => _plotRepository.GetAllData().Count > 0 ? true : false;
    }
}
