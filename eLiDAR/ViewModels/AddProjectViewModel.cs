using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Validator;
using eLiDAR.Views;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class AddProjectViewModel : BaseProjectViewModel {

        public ICommand AddProjectCommand { get; private set; }
        public ICommand ViewAllProjectsCommand { get; private set; }

        public AddProjectViewModel(INavigation navigation){
            _navigation = navigation;
            _projectValidator = new ProjectValidator();
            _project = new PROJECT();
            _projectRepository = new ProjectRepository();
            AddProjectCommand = new Command(async () => await AddProject()); 
            ViewAllProjectsCommand = new Command(async () => await ShowProjectList()); 
        }

       async Task ShowProjectList(){ 
            await _navigation.PushAsync(new PlotList());
        }
        async Task AddProject()
        {
            var validationResults = _projectValidator.Validate(_project);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Project", "Do you want to save project details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _projectRepository.InsertProject(_project);
                    await _navigation.PushAsync(new ProjectList());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Project", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }
        public bool IsViewAll => _projectRepository.GetAllProjectData().Count > 0 ? true : false;
    }
}
