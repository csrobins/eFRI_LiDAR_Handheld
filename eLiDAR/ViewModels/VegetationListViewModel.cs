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
    public class VegetationListViewModel : BaseVegetationViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public VegetationListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _vegetationRepository = new VegetationRepository();
            _fk = fk;
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<VEGETATION>(async (x) => await ShowVegetation(x));
            FetchVegetation();
        }

        public void FetchVegetation(){
            if (_fk == "")
                VegetationList = _vegetationRepository.GetAllData();
            else
                VegetationList = _vegetationRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddVegetation(""));
            }
            else { await _navigation.PushAsync(new AddVegetation(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Vegetation List", "Delete All Vegetaiton Details?", "OK", "Cancel");
            if (isUserAccept){
                _vegetationRepository.DeleteAllVegetation();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedVegetationID){
            await _navigation.PushAsync(new VegetationDetailsPage(selectedVegetationID));
        }

        VEGETATION _selectedVegetationItem;
        public VEGETATION SelectedVegetationItem {
            get  {
                try
                {
                   return _selectedVegetationItem;
                }
                catch (System.Exception ex) {
                    return null;
                }
            }
            set {
                if (value != null){
                    _selectedVegetationItem = value;
                    NotifyPropertyChanged("SelectedVegetationItem");
                    ShowDetails(value.VEGETATIONID );
                }
            }
        }
        async Task ShowVegetation(VEGETATION  _vegetation)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new VegetationDetailsPage(_vegetation.VEGETATIONID));
        }
        public string Title
        {
            get => "Vegetation details for plot " + _vegetationRepository.GetTitle(_fk) + ".  " + VegetationList.Count.ToString() + " veg species.";
            set
            {
            }
        }
    }
}
