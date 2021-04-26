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
    public class ProjectListViewModel : BaseProjectViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllProjectsCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
        public ICommand ShowCrewCommand { get; private set; }

        public ProjectListViewModel(INavigation navigation) {
            _navigation = navigation;
            _projectRepository = new ProjectRepository();

            AddCommand = new Command(async () => await ShowAddProject()); 
            DeleteAllProjectsCommand = new Command(async () => await DeleteAllProjects());
            ShowFilteredCommand = new Command<PROJECT>(async (x) =>  await ShowPlots(x));
            ShowCrewCommand = new Command<PROJECT>(async (x) => await ShowCrew(x));
            FetchProjects();
        }

        public void FetchProjects(){
            ProjectList = _projectRepository.GetAllProjectData();
            NotifyPropertyChanged("ProjectList");
        }

        async Task ShowAddProject() {
            await _navigation.PushAsync(new AddProject ());
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
            get => _projectRepository.GetAllProjectData();
            set
            {
            }
        }

    }
}
