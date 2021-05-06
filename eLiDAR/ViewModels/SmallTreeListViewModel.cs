using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Views;

using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class SmallTreeListViewModel : BaseSmallTreeViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public SmallTreeListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _smallTreeRepository = new SmallTreeRepository();
            _fk = fk;
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<SMALLTREE>(async (x) => await ShowSmallTree(x));
            FetchSmallTree();
        }

        public void FetchSmallTree(){
            if (_fk == "")
                SmallTreeList = _smallTreeRepository.GetAllData();
            else
                SmallTreeList = _smallTreeRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddSmallTree(""));
            }
            else { await _navigation.PushAsync(new AddSmallTree(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Small Tree List", "Delete All Small Tree Details?", "OK", "Cancel");
            if (isUserAccept){
                _smallTreeRepository.DeleteAllSmallTree();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedSmallTreeID){
            await _navigation.PushAsync(new SmallTreeDetailsPage(selectedSmallTreeID));
        }

        SMALLTREE _selectedSmallTreeItem;
        public SMALLTREE SelectedSmallTreeItem {
            get  {
                try
                {
                   return _selectedSmallTreeItem;
                }
                catch (System.Exception ex) {
                    return null;
                }


            }
            set {
                if (value != null){
                    _selectedSmallTreeItem = value;
                    NotifyPropertyChanged("SelectedSmallTreeItem");
                    ShowDetails(value.SMALLTREEID);
                }
            }
        }
        async Task ShowSmallTree(SMALLTREE _smallTree)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new SmallTreeDetailsPage(_smallTree.SMALLTREEID ));
        }
        public string Title
        {
            get => "Smaller tree details for plot " + _smallTreeRepository.GetTitle(_fk) + ".  " + SmallTreeList.Count.ToString() + " species.";
            set
            {
            }
        }
    }
}
