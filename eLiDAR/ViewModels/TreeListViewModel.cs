using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Utilities;
using eLiDAR.Views;


using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class TreeListViewModel : BaseTreeViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
        public ICommand ShowAgesCommand { get; private set; }
        public ICommand ShowDeformityCommand { get; private set; }

        public TreeListViewModel(INavigation navigation, string fk)
        {
            try
            {
                _navigation = navigation;
                _treeRepository = new TreeRepository();
                _fk = fk;
                _tree = new TREE();
                AddCommand = new Command(async () => await ShowAdd(_fk));
                DeleteAllCommand = new Command(async () => await DeleteAll());
                ShowFilteredCommand = new Command<TREE>(async (x) => await ShowStemMap(x));
                ShowDeformityCommand = new Command<TREE>(async (x) => await ShowDeformity(x));
                ShowAgesCommand = new Command<TREE>(async (x) => await ShowAges(x));

                FetchTrees();
            }
            catch (Exception ex)
            {
                var myerror = ex.Message;
            }
        }

        public void FetchTrees() {
           
            TreeStemListFull = _treeRepository.GetFilteredTreeStemDataFull(_fk, DefaultSort); 
            NotifyPropertyChanged("Title");
            NotifyPropertyChanged("SpeciesComp"); 
        }
        async Task ShowAges(TREE _tree)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new TreeAge(_tree.TREEID));
        }
        async Task ShowAdd(string _fk) {
            await _navigation.PushAsync(new AddTree(_fk)); 
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree List", "Delete All Tree Details?", "OK", "Cancel");
            if (isUserAccept){
                _treeRepository.DeleteAllTrees();
                await _navigation.PopAsync();
            }
        }
        private List<TREE> _treeStemList;
        public List<TREE> TreeStemList
        {
            get
            {
                if (_fk == "")
                    return _treeRepository.GetAllData();
                else
                    return _treeRepository.GetFilteredTreeStemData(_fk);
            }
            set
            {
                _treeStemList = value;
            }
        }
        private bool _defaultsort = false;
        public bool DefaultSort
        {
            get 
            {
                return _defaultsort;
            }
            set
            {
                _defaultsort = value;
                NotifyPropertyChanged("DefaultSort");
                FetchTrees(); 
            }
        }
        private List<TREELIST> _treeStemListFull;
        public List<TREELIST> TreeStemListFull
        {
            get
            {
                    return _treeRepository.GetFilteredTreeStemDataFull(_fk, DefaultSort);
            }
            set
            {
                _treeStemListFull = value;
                NotifyPropertyChanged("TreeStemListFull");
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
        async Task ShowDeformity(TREE _tree)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new DeformityList(_tree.TREEID));
        }
        public string SpeciesComp
        {
            get
            {
                List<TREE> _list = _treeRepository.GetFilteredData(_fk);
                Utils util = new Utils();
                return util.getSppComp(_list); 
            }
        }
        public string Title
        {
            get => "Tree List for plot " + _treeRepository.GetPlotTitle(_fk) + ".  " + TreeStemListFull.Count.ToString() + " trees.";
            set
            {
            }
        }

        public Int32 GetAzimuth
        {
            get => _treeRepository.GetAzimuth(_tree.TREEID);
            set
            {
            }
        }
        public Double GetDistance
        {
            get => _treeRepository.GetDistance(_tree.TREEID);
            set
            {
            }
        }
        public String GetLocation
        {
            get
            {
                //   double dist = GetDistance;
                if (GetDistance != 0)
                {
                    return "Az: " + GetAzimuth.ToString() + "\nDist: " + GetDistance.ToString();
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
