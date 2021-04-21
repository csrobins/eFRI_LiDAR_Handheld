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
using eLiDAR.Utilities;
using System.Linq;
using System.Collections.ObjectModel;

namespace eLiDAR.ViewModels {
    public class PersonListViewModel : BasePersonViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
     
   
        public PersonListViewModel(INavigation navigation,string selectedprojectid )
        {
            _navigation = navigation;
            _personRepository = new PersonRepository();
            _selectedprojectid = selectedprojectid;

            AddCommand = new Command(async () => await ShowAdd(_selectedprojectid));
            DeleteAllCommand = new Command(async () => await DeleteAll());
        //    ShowFilteredCommand = new Command<PLOT>(async (x) => await ShowTrees(x));
           //  SearchCommand = new Command<string>(async (text) => await Search(text));
            Fetch();
        }

 
        public void Fetch(){
            if (_selectedprojectid == "")
              //  PlotList = "No project selected";
                PersonList = _personRepository.GetAllPersonData();
            else
         //       PlotList = _plotRepository.GetFilteredData(_selectedprojectid);
                PersonList = _personRepository.GetFilteredData(_selectedprojectid);

        }
       
     
        async Task ShowAdd(string fk)
        {
            await _navigation.PushAsync(new AddPerson(fk));
        }


        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Person List", "Delete All Crew Details?", "OK", "Cancel");
            if (isUserAccept){
                _personRepository.DeleteAllPersons();
                await _navigation.PopAsync(true);
            }
        }

        async void ShowDetails(string selectedPersonID){
            await _navigation.PushAsync(new PersonDetailsPage(selectedPersonID));
        }

        bool _isselected;
        public bool IsSelected
        {
            get
            {
                if (_selectedprojectid.Length > 1)
                {
                    _isselected = true;
                    return _isselected;
                }
                else { return false; }
            }
            set
            {
               _isselected = value;
                NotifyPropertyChanged("IsSelected");
                
            }
        }

        PERSON _selectedPersonItem;
        public PERSON SelectedPersonItem {
            get => _selectedPersonItem;
            set {
                if (value != null){
                    _selectedPersonItem = value;
                    NotifyPropertyChanged("SelectedPersonItem");
                    ShowDetails(value.PERSONID);
                }
            }
        }
        
        public string Title
        {
            get => "Personnel List for " + _personRepository.GetProjectTitle(_selectedprojectid) + ".\n  " + PersonList.Count.ToString()+ " people.";
            set
            {
            }
        }
        public List<PERSON> PersonList
        {
            get => _personRepository.GetFilteredData(_selectedprojectid);
            set
            {
            }
        }
    
    }
}
