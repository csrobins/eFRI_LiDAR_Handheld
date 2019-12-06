using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeDetailsPage : ContentPage {

        public TreeDetailsPage(string treeID) {
            InitializeComponent();
            this.BindingContext = new TreeDetailsViewModel(Navigation,treeID);
        }
    }
}
