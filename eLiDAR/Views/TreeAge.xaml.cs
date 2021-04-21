using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeAge : ContentPage {
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
        public TreeAge(string treeID) {
            InitializeComponent();
            this.BindingContext = new TreeAgeViewModel(Navigation,treeID);
        }
    }
}
