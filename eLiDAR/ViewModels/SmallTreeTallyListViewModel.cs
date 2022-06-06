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
namespace eLiDAR.ViewModels
{
    internal class SmallTreeTallyListViewModel : BaseSmallTreeTallyViewModel
    {
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }

        public SmallTreeTallyListViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _smallTreeTallyRepository = new SmallTreeTallyRepository();
            _fk = fk;
            _smallTreeTally = new SMALLTREETALLY();
            AddCommand = new Command(async () => await ShowAdd(_fk));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<SMALLTREETALLY>(async (x) => await ShowSmallTreeTally(x));
            FetchSmallTreeTally();
        }

        public void FetchSmallTreeTally()
        {
            SmallTreeTallyList = _smallTreeTallyRepository.GetFilteredDataFull(_fk);
        }

        async Task ShowAdd(string _fk)
        {
            if (_fk == "")
            {
                await _navigation.PushAsync(new AddSmallTreeTally(""));
            }
            else { await _navigation.PushAsync(new AddSmallTreeTally(_fk)); }
        }

        async Task DeleteAll()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Small Tree List", "Delete All Small Tree Details?", "OK", "Cancel");
            if (isUserAccept)
            {
                _smallTreeTallyRepository.DeleteAllSmallTreeTally();
                await _navigation.PopAsync();
            }
        }

        async void ShowDetails(string selectedSmallTreeTallyID)
        {
            await _navigation.PushAsync(new SmallTreeTallyDetailsPage(selectedSmallTreeTallyID));
        }

        SMALLTREETALLY _selectedSmallTreeItemTally;
        public SMALLTREETALLY SelectedSmallTreeItemTally
        {
            get
            {
                try
                {
                    return _selectedSmallTreeItemTally;
                }
                catch (System.Exception ex)
                {
                    return null;
                }


            }
            set
            {
                if (value != null)
                {
                    _selectedSmallTreeItemTally = value;
                    NotifyPropertyChanged("SelectedSmallTreeItemTally");
                    ShowDetails(value.SMALLTREETALLYID);
                }
            }
        }
        async Task ShowSmallTreeTally(SMALLTREETALLY _smallTreeTally)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new SmallTreeTallyDetailsPage(_smallTreeTally.SMALLTREETALLYID));
        }
        public string Title
        {
            get => "Smaller tree details for plot " + _smallTreeTallyRepository.GetTitle(_fk) + ".  " + SmallTreeTallyList.Count.ToString() + " species.";
            set
            {
            }
        }
    }
}
