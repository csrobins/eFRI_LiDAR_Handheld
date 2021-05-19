using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeAge : ContentPage {
 
        public TreeAge(string treeID) {
            InitializeComponent();
            this.BindingContext = new TreeAgeViewModel(Navigation,treeID);
        }

        public TreeAge(TREE tree)
        {
            InitializeComponent();
            this.BindingContext = new TreeAgeViewModel(Navigation, tree);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((TreeAgeViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((TreeAgeViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}
