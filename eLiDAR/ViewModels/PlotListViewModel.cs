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

        public PlotListViewModel(INavigation navigation) {
            _navigation = navigation;
            _plotRepository = new PlotRepository();

            AddCommand = new Command(async () => await ShowAdd()); 
            DeleteAllCommand = new Command(async () => await DeleteAll());

            FetchPlots();
        }

        void FetchPlots(){
            PlotList = _plotRepository.GetAllData();
        }

        async Task ShowAdd() {
            await _navigation.PushAsync(new AddPlot ());
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
    }
}
