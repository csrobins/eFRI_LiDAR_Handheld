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
        public ICommand ShowFilteredCommand { get; private set; }
  
        public TreeListViewModel(INavigation navigation) {
            _navigation = navigation;
            _treeRepository = new TreeRepository();
            _fk = "";
            AddCommand = new Command(async () => await ShowAdd("")); 
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<TREE>(async (x) => await ShowStemMap(x));
            FetchTrees();
        }
        public TreeListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _treeRepository = new TreeRepository();
            _fk = fk;
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<TREE>(async (x) => await ShowStemMap(x));
            FetchTrees();
        }

        void FetchTrees(){
            if (_fk == "")
                TreeList = _treeRepository.GetAllData();
            else
                TreeList = _treeRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddTree());
            }
            else { await _navigation.PushAsync(new AddTree(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree List", "Delete All Tree Details?", "OK", "Cancel");
            if (isUserAccept){
                _treeRepository.DeleteAllTrees();
                await _navigation.PushAsync(new AddPlot());
            }
        }

        async void ShowDetails(string selectedTreeID){
            await _navigation.PushAsync(new TreeDetailsPage(selectedTreeID));
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
        async Task ShowStemMap(TREE _tree)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new StemMapDetailsPage(_tree.TREEID));
        }
        public string Title
        {
            get => "Tree List for plot " + _treeRepository.GetPlotTitle(_fk);
            set
            {
            }
        }
        public int GetAzimuth
        {
            get => _treeRepository.GetAzimuth(_tree.TREEID);
            set
            {
            }
        }
        public  double GetDistance
        {
            get =>  _treeRepository.GetDistance(_tree.TREEID);
            set
            {
            }
        }
        public string GetLocation
        {
            get  {
                double dist = GetDistance;
                if (dist != 0)
                {
                    return "Az: " + GetAzimuth.ToString() + "\nDist: " + dist.ToString();
                }
                else
                {
                    return "";
                }
            }
            set { }
        }
    }
}
