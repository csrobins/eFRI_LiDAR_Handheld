using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotDetailsPage : ContentPage {
        
        public PlotDetailsPage(string projectID) {
            InitializeComponent();
            this.BindingContext = new PlotDetailsViewModel(Navigation, projectID);
        }
    }
}
