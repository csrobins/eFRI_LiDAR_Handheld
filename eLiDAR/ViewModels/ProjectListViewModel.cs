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
    public class ProjectListViewModel : BaseProjectViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllProjectsCommand { get; private set; }

        public ProjectListViewModel(INavigation navigation) {
            _navigation = navigation;
            _projectRepository = new ProjectRepository();

            AddCommand = new Command(async () => await ShowAddProject()); 
            DeleteAllProjectsCommand = new Command(async () => await DeleteAllProjects());

            FetchProjects();
        }

        void FetchProjects(){
            ProjectList = _projectRepository.GetAllProjectData();
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
    }
}
