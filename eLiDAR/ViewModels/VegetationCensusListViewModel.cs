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
    public class VegetationCensusListViewModel : BaseVegetationCensusViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public VegetationCensusListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _vegetationCensusRepository = new VegetationCensusRepository();
            _fk = fk;
            _vegetation = new VEGETATIONCENSUS();
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<VEGETATIONCENSUS>(async (x) => await ShowVegetation(x));
            FetchVegetation();
        }

        public void FetchVegetation(){
            if (_fk == "")
                VegetationList = _vegetationCensusRepository.GetAllData();
            else
                VegetationList = _vegetationCensusRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddVegetationCensus(""));
            }
            else { await _navigation.PushAsync(new AddVegetationCensus(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Vegetation Census List", "Delete All Vegetaiton Details?", "OK", "Cancel");
            if (isUserAccept){
                _vegetationCensusRepository.DeleteAllVegetation();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedVegetationID){
            await _navigation.PushAsync(new VegetationCensusDetailsPage(selectedVegetationID));
        }

        VEGETATIONCENSUS _selectedVegetationItem;
        public VEGETATIONCENSUS SelectedVegetationItem {
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
                    ShowDetails(value.VEGETATIONCENSUSID );
                }
            }
        }
        async Task ShowVegetation(VEGETATIONCENSUS  _vegetation)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new VegetationCensusDetailsPage(_vegetation.VEGETATIONCENSUSID));
        }
        public string Title
        {
            get => "Vegetation Census list for plot " + _vegetationCensusRepository.GetTitle(_fk) + ".  " + VegetationList.Count.ToString() + " veg species.";
            set
            {
            }
        }
    }
}
