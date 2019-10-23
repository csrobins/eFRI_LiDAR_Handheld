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
    public class TreeListViewModel : BaseTreeViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }

        public TreeListViewModel(INavigation navigation) {
            _navigation = navigation;
            _treeRepository = new TreeRepository();

            AddCommand = new Command(async () => await ShowAdd()); 
            DeleteAllCommand = new Command(async () => await DeleteAll());

            FetchTrees();
        }

        void FetchTrees(){
            TreeList = _treeRepository.GetAllData();
        }

        async Task ShowAdd() {
            await _navigation.PushAsync(new AddPlot ());
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree List", "Delete All Tree Details?", "OK", "Cancel");
            if (isUserAccept){
                _treeRepository.DeleteAllTrees();
                await _navigation.PushAsync(new AddPlot());
            }
        }

        async void ShowDetails(string selectedTreeID){
            await _navigation.PushAsync(new PlotDetailsPage(selectedTreeID));
        }

        TREE _selectedTreeItem;
        public TREE SelectedTreeItem {
            get => _selectedTreeItem;
            set {
                if (value != null){
                    _selectedTreeItem = value;
                    NotifyPropertyChanged("SelectedTreeItem");
                    ShowDetails(value.TREEID);
                }
            }
        }
    }
}
