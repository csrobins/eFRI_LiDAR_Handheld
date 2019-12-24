using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeDetailsPage : ContentPage {
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
        public TreeDetailsPage(string treeID) {
            InitializeComponent();
            this.BindingContext = new TreeDetailsViewModel(Navigation,treeID);
        }
    }
}
