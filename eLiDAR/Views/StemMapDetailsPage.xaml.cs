using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class StemMapDetailsPage : ContentPage {

        public StemMapDetailsPage(string treeID) {
            InitializeComponent();
            this.BindingContext = new StemMapDetailsViewModel(Navigation,treeID);
        }
    }
}
