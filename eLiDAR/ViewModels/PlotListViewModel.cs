using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Views;

using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class PlotListViewModel : BasePlotViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
        public ICommand ShowSiteCommand { get; private set; }

        public PlotListViewModel(INavigation navigation) {
            _navigation = navigation;
            _plotRepository = new PlotRepository();
            _selectedprojectid = "";
            AddCommand = new Command(async () => await ShowAdd()); 
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<PLOT>(async (x) => await ShowTrees(x));
            ShowSiteCommand = new Command<PLOT>(async (x) => await ShowSite(x));
            FetchPlots();
        }
        public PlotListViewModel(INavigation navigation,string selectedprojectid )
        {
            _navigation = navigation;
            _plotRepository = new PlotRepository();
            _selectedprojectid = selectedprojectid;

            AddCommand = new Command(async () => await ShowAdd(_selectedprojectid));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<PLOT>(async (x) => await ShowTrees(x));
            ShowSiteCommand = new Command<PLOT>(async (x) => await ShowSite(x));

            FetchPlots();
        }

        void FetchPlots(){
            if (_selectedprojectid == "")
                PlotList = _plotRepository.GetAllData();
            else
                PlotList = _plotRepository.GetFilteredData(_selectedprojectid);
        }

        async Task ShowAdd() {
            await _navigation.PushAsync(new AddPlot ());
        }
        async Task ShowAdd(string fk)
        {
            await _navigation.PushAsync(new AddPlot(fk));
        }


        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Plot List", "Delete All Plot Details?", "OK", "Cancel");
            if (isUserAccept){
                _plotRepository.DeleteAllPlots();
                await _navigation.PushAsync(new AddPlot());
            }
        }

        async void ShowDetails(string selectedPlotID){
            await _navigation.PushAsync(new PlotDetailsPage(selectedPlotID));
        }

        PLOT _selectedPlotItem;
        public PLOT SelectedPlotItem {
            get => _selectedPlotItem;
            set {
                if (value != null){
                    _selectedPlotItem = value;
                    NotifyPropertyChanged("SelectedPlotItem");
                    ShowDetails(value.PLOTID);
                }
            }
        }
        async Task ShowTrees(PLOT _plot)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new TreeList(_plot.PLOTID));
        }
        async Task ShowSite(PLOT _plot)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new EcositeDetailsPage(_plot.PLOTID));
        }

        public string Title
        {
            get => "Plot List for " + _plotRepository.GetProjectTitle(_selectedprojectid);
            set
            {
            }
        }
    }
}
