using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Validator;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class ProjectDetailsViewModel: BaseProjectViewModel {

        public ICommand UpdateProjectCommand { get; private set; }
        public ICommand DeleteProjectCommand { get; private set; }

        public ProjectDetailsViewModel(INavigation navigation, string selectedProjectID) {
            _navigation = navigation;
           // _contactValidator = new ContactValidator();
            _project = new PROJECT();
            _project.PROJECTID = selectedProjectID;
            _projectRepository = new ProjectRepository();

            UpdateProjectCommand = new Command(async () => await UpdateProject());
            DeleteProjectCommand = new Command(async () => await DeleteProject());

            FetchProjectDetails();
        }

        void FetchProjectDetails(){
            _project = _projectRepository.GetProjectData(_project.PROJECTID);
        }

        async Task UpdateProject() {
            //  var validationResults = _projectValidator.Validate(_project);
            ProjectValidator _projectValidator = new ProjectValidator();
            ValidationResult validationResults = _projectValidator.Validate(_project);


            if (validationResults.IsValid) {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Project Details", "Update Project Details", "OK", "Cancel");
                if (isUserAccept) {
                    _project.LastModified = System.DateTime.UtcNow;
                    _projectRepository.UpdateProject(_project);
                    await _navigation.PopAsync();
                }
            }
            else {
                await Application.Current.MainPage.DisplayAlert("Add Project", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        async Task DeleteProject() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Project Details", "Delete Project Details", "OK", "Cancel");
            if (isUserAccept) {
                _projectRepository.DeleteProject(_project.PROJECTID);
                await _navigation.PopAsync();
            }
        }
    }
}