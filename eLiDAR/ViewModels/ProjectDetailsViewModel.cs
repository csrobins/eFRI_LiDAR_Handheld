using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Utilities;
using eLiDAR.Validator;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class ProjectDetailsViewModel: BaseProjectViewModel {

        public ICommand UpdateProjectCommand { get; private set; }
        public ICommand DeleteProjectCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public ProjectDetailsViewModel(INavigation navigation, string selectedProjectID) {
            _navigation = navigation;
           // _contactValidator = new ContactValidator();
            _project = new PROJECT();
            _project.PROJECTID = selectedProjectID;
            _projectRepository = new ProjectRepository();

            UpdateProjectCommand = new Command(() => Update());
            DeleteProjectCommand = new Command(async () => await DeleteProject());

            FetchProjectDetails();
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }
        public bool AllowProjectDeletion
        {
            get
            {
                Utils util = new Utils();
                return util.AllowProjectDeletion;
            }
            set
            {
            }
        }
        void FetchProjectDetails(){
            _project = _projectRepository.GetProjectData(_project.PROJECTID);
        }

        private Task Update()
        {

            _project.LastModified = System.DateTime.UtcNow;
            _projectRepository.UpdateProject(_project);
            return Task.CompletedTask; 
        }

        async Task DeleteProject() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Project Details", "Delete Project Details", "OK", "Cancel");
            if (isUserAccept) {
                _projectRepository.DeleteProject(_project);
                await _navigation.PopAsync();
            }
        }
        private void OnAppearing()
        {
            Shell.Current.Navigating += Current_Navigating;
        }
        private void OnDisappearing()
        {
            Shell.Current.Navigating -= Current_Navigating;
        }
        private async void Current_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                e.Cancel();
                await GoBack();
            }
        }
        private async Task GoBack()
        {
            // display Alert for confirmation
            if (IsChanged)
            {
                ProjectValidator _Validator = new ProjectValidator();
                ValidationResult validationResults = _Validator.Validate(_project);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    //just to slow things down
                    _project = _projectRepository.GetProjectData(_project.PROJECTID);
                    Shell.Current.Navigating -= Current_Navigating;
                  //  await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Project", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
             //   await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}