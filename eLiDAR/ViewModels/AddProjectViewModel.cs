using System;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class AddProjectViewModel : BaseProjectViewModel {

        public ICommand AddProjectCommand { get; private set; }
        public ICommand ViewAllProjectsCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public AddProjectViewModel(INavigation navigation){
            _navigation = navigation;
            _projectValidator = new ProjectValidator();
            _project = new PROJECT();
            _projectRepository = new ProjectRepository();
            AddProjectCommand = new Command(() =>  AddProject()); 
            ViewAllProjectsCommand = new Command(async () => await ShowProjectList());
            _project.PROJECT_DATE = DateTime.Now;
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }

        async Task ShowProjectList(){ 
            await _navigation.PushAsync(new ProjectList());
        }
        private void AddProject()
        {         
            _project.Created = System.DateTime.UtcNow;
            _project.LastModified = _project.Created;
            _project.IsDeleted = "N"; 
            _projectRepository.InsertProject(_project);
        }
        public bool IsViewAll => _projectRepository.GetAllProjectData().Count > 0 ? true : false;
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
                ProjectValidator _projectValidator = new ProjectValidator();
                ValidationResult validationResults = _projectValidator.Validate(_project);
                if (validationResults.IsValid)
                {
                    AddProject();
                    Shell.Current.Navigating -= Current_Navigating;
            //        await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Project", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
         //       await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}
