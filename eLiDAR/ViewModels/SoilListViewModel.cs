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
    public class SoilListViewModel : BaseSoilViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public SoilListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _soilRepository = new SoilRepository();
            _fk = fk;
            _soil = new SOIL();
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<SOIL>(async (x) => await ShowSoil(x));
            FetchSoil();
        }

        public void FetchSoil(){
            if (_fk == "")
                SoilList = _soilRepository.GetAllData();
            else
                SoilList = _soilRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddSoil(""));
            }
            else { await _navigation.PushAsync(new AddSoil(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Soil List", "Delete All Soil Details?", "OK", "Cancel");
            if (isUserAccept){
                _soilRepository.DeleteAllSoil();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedSoilID){
            await _navigation.PushAsync(new SoilDetailsPage(selectedSoilID));
        }

        SOIL _selectedSoilItem;
        public SOIL SelectedSoilItem {
            get  {
                try
                {
                   return _selectedSoilItem;
                }
                catch (System.Exception ex) {
                    return null;
                }


            }
            set {
                if (value != null){
                    _selectedSoilItem = value;
                    NotifyPropertyChanged("SelectedSoilItem");
                    ShowDetails(value.SOILID);
                }
            }
        }
        async Task ShowSoil(SOIL _soil)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new SoilDetailsPage(_soil.SOILID));
        }
        public string Title
        {
            get => "Soil layer list for plot " + _soilRepository.GetTitle(_fk) + ".\n  " + SoilList.Count.ToString() + " soil layers.";
            set
            {
            }
        }
    }
}
