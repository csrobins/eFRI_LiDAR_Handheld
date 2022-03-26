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
    public class DWDListViewModel : BaseDWDViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand AddAccumCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public DWDListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _dwdRepository = new DWDRepository();
            _fk = fk;
            _dwd = new DWD();
            AddCommand = new Command(async () => await ShowAdd(_fk));
            AddAccumCommand = new Command(async () => await ShowAddAccum(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<DWD>(async (x) => await ShowDWD(x));
            FetchDetails();
        }

        public void FetchDetails(){
            if (_fk == "")
                DWDList = _dwdRepository.GetAllData();
            else
                DWDList = _dwdRepository.GetFilteredData(_fk);
        }

        async Task ShowAdd(string _fk) {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddDWD(""));
            }
            else { await _navigation.PushAsync(new AddDWD(_fk)); }
        }
        async Task ShowAddAccum(string _fk)
        {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddDWDAccum(""));
            }
            else { await _navigation.PushAsync(new AddDWDAccum(_fk)); }
        }

        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("DWD List", "Delete All DWD Details?", "OK", "Cancel");
            if (isUserAccept){
                _dwdRepository.DeleteAllDWD();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedDWDID, string IsAccum){
            if (IsAccum == "Y")
            {
                await _navigation.PushAsync(new DWDAccumDetailsPage(selectedDWDID));

            }
            else
            { 
                await _navigation.PushAsync(new DWDDetailsPage(selectedDWDID));
            }
        }

        DWD  _selectedDWDItem;
        public DWD SelectedDWDItem {
            get  {
                try
                {
                   return _selectedDWDItem;
                }
                catch (System.Exception ex) {
                    return null;
                }
            }
            set {
                if (value != null){
                    _selectedDWDItem = value;
                    NotifyPropertyChanged("SelectedDWDItem");
                    ShowDetails(value.DWDID, value.IS_ACCUM  );
                }
            }
        }
        async Task ShowDWD(DWD _dwd)
        {
            if (_dwd.IS_ACCUM == "Y")
            {
                // launch the form - filtered to a specific projectid
                await _navigation.PushAsync(new DWDDetailsPage(_dwd.DWDID));
            }
            else {
                await _navigation.PushAsync(new DWDAccumDetailsPage(_dwd.DWDID));
            }

        }
        public string Title
        {
            get => "DWD details for plot " + _dwdRepository.GetTitle(_fk) + ".  " + DWDList.Count.ToString() + " DWD items.";
            set
            {
            }
        }
    }
}
