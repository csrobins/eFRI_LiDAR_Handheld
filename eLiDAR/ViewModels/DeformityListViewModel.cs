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
    public class DeformityListViewModel : BaseDeformityViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public DeformityListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _deformityRepository = new DeformityRepository();
            _fk = fk;
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<DEFORMITY>(async (x) => await ShowDeformity(x));
            FetchDetails();
        }

        public void FetchDetails(){
            if (_fk == "")
                DeformityList = _deformityRepository.GetAllData();
            else
                DeformityList = _deformityRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddDeformity(""));
            }
            else { await _navigation.PushAsync(new AddDeformity(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Deformity List", "Delete All Deformity Details?", "OK", "Cancel");
            if (isUserAccept){
                _deformityRepository.DeleteAllDeformity ();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedDeformityID){
            await _navigation.PushAsync(new DeformityDetailsPage(selectedDeformityID));
        }

        DEFORMITY  _selectedDeformityItem;
        public DEFORMITY SelectedDeformityItem {
            get  {
                try
                {
                   return _selectedDeformityItem;
                }
                catch (System.Exception ex) {
                    return null;
                }
            }
            set {
                if (value != null){
                    _selectedDeformityItem = value;
                    NotifyPropertyChanged("SelectedDeformityItem");
                    ShowDetails(value.DEFORMITYID  );
                }
            }
        }
        async Task ShowDeformity(DEFORMITY  _deformity)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new DeformityDetailsPage(_deformity.DEFORMITYID ));
        }
        public string Title
        {
            get => "Deformity details for tree " + _deformityRepository.GetTitle(_fk) + ".  " + DeformityList.Count.ToString() + " deformities.";
            set
            {
            }
        }
    }
}
