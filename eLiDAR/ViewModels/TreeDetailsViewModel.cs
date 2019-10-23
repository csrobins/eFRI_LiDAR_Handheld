using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Validator;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class TreeDetailsViewModel: BaseTreeViewModel {

        public ICommand UpdateTreeCommand { get; private set; }
        public ICommand DeleteTreeCommand { get; private set; }

        public TreeDetailsViewModel(INavigation navigation, string selectedTreeID) {
            _navigation = navigation;
            _tree = new TREE();
            _tree.TREEID = selectedTreeID;
            _treeRepository = new TreeRepository();

            UpdateTreeCommand = new Command(async () => await UpdateTree());
            DeleteTreeCommand = new Command(async () => await DeleteTree());

            FetchTreeDetails();
        }

        void FetchTreeDetails(){
            _tree = _treeRepository.GetTreeData(_tree.TREEID);
        }

        async Task UpdateTree() {
           var validationResults = _treeValidator.Validate(_tree);

             if (validationResults.IsValid) {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Details", "Update Tree Details", "OK", "Cancel");
                if (isUserAccept) {
                    _treeRepository.UpdateTree(_tree);
                    await _navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Tree", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        async Task DeleteTree() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Details", "Delete Tree Details", "OK", "Cancel");
            if (isUserAccept) {
                _treeRepository.DeleteTree(_tree.TREEID);
                await _navigation.PopAsync();
            }
        }
    }
}