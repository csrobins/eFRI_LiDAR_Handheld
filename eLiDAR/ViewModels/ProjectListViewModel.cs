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
    public class ProjectListViewModel : BaseProjectViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllProjectsCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand ShowCrewCommand { get; private set; }
        private Utilities.Utils util = new Utils();
        public ProjectListViewModel(INavigation navigation) {
            _navigation = navigation;
            _projectRepository = new ProjectRepository();

            AddCommand = new Command(async () => await ShowAddProject());
            LoginCommand = new Command(async () => await ShowLogin());

            DeleteAllProjectsCommand = new Command(async () => await DeleteAllProjects());
            ShowFilteredCommand = new Command<PROJECT>(async (x) =>  await ShowPlots(x));
            ShowCrewCommand = new Command<PROJECT>(async (x) => await ShowCrew(x));
            FetchProjects();
        }
        public void Refresh()
        {
            NotifyPropertyChanged("IsLoggedIn");
            NotifyPropertyChanged("ProjectList");
            NotifyPropertyChanged("Login");
        }
        public bool IsLoggedIn
        {
            get
            {
                return util.IsLoggedIn;
            }
            set
            {
                util.IsLoggedIn = value;
                NotifyPropertyChanged("IsLoggedIn");
                NotifyPropertyChanged("ProjectList");
                NotifyPropertyChanged("Login");
            }
        }
        private string _login;
        public string Login
        {
            get
            {
                if (IsLoggedIn) { _login = util.LoggedInAs + ", Sign Out"; }
                else { _login = "Sign In"; }
                return _login;
            }
            set
            {
                if (IsLoggedIn) { _login = util.LoggedInAs + ", Sign Out"; }
                else { _login = "Sign In"; }
                NotifyPropertyChanged("Login");
            }
        }
        public void FetchProjects(){
            if (IsLoggedIn)
            {
                ProjectList = _projectRepository.GetAllProjectData();
            }
            else
            {
                ProjectList = null;
            }
            NotifyPropertyChanged("ProjectList");
            NotifyPropertyChanged("IsLoggedIn");
        }

        async Task ShowAddProject() {
            await _navigation.PushAsync(new AddProject ());
        }
        async Task ShowLogin()
        {
            if (!util.IsLoggedIn) { await _navigation.PushAsync(new Login()); }
            else
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Sign Out", "Do you want to log out of eVSN?", "OK", "Cancel");
                if (isUserAccept)
                {
                    util.IsLoggedIn = false;
                    IsLoggedIn = false;
                    NotifyPropertyChanged("ProjectList");
                    NotifyPropertyChanged("IsLoggedIn");
                    NotifyPropertyChanged("Login");

                }
            }
        }
        async Task DeleteAllProjects(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Project List", "Delete All Project Details ?", "OK", "Cancel");
            if (isUserAccept){
                _projectRepository.DeleteAllProjects();
                await _navigation.PushAsync(new AddProject());
            }
        }

        async void ShowProjectDetails(string selectedProjectID){
            await _navigation.PushAsync(new ProjectDetailsPage(selectedProjectID));
        }

        PROJECT _selectedProjectItem;
        public PROJECT SelectedProjectItem {
            get => _selectedProjectItem;
            set {
                if (value != null){
                    _selectedProjectItem = value;
                    NotifyPropertyChanged("SelectedProjectItem");
                    ShowProjectDetails(value.PROJECTID);
                }
            }
        }

        async Task  ShowPlots(PROJECT _project)
        {
            // launch the plot form - filtered to a specific projectid
            await _navigation.PushAsync(new PlotList( _project.PROJECTID));
           // await _navigation.PushAsync(new PlotDetailsPage("1"));

        }
        async Task ShowCrew(PROJECT _project)
        {
            // launch the crew form - filtered to a specific projectid
            await _navigation.PushAsync(new PersonList(_project.PROJECTID));
   
        }
        public List<PROJECT> ProjectList
        {
            get
            {
                if (IsLoggedIn) { return _projectRepository.GetAllProjectData(); }
                else { return null; }
            }

            set
            {
            }
        }

    }
}
