using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Validator;
using eLiDAR.Views;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class AddTreeViewModel : BaseTreeViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand ViewAllCommand { get; private set; }

        public AddTreeViewModel(INavigation navigation){
            _navigation = navigation;
            _treeValidator = new TreeValidator();
            _tree = new TREE();
            _treeRepository = new TreeRepository();
            AddCommand = new Command(async () => await AddTree()); 
            ViewAllCommand = new Command(async () => await ShowList()); 
        }

       async Task ShowList(){ 
            await _navigation.PushAsync(new PlotList());
        }
        async Task AddTree()
        {
            var validationResults = _treeValidator.Validate(_tree);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Tree", "Do you want to save tree details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _treeRepository.InsertTree(_tree);
                    await _navigation.PushAsync(new ProjectList());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Project", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }
        public bool IsViewAll => _treeRepository.GetAllData().Count > 0 ? true : false;
    }
}
