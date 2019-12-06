using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class EcositeDetailsPage : ContentPage {

        public EcositeDetailsPage(string plotID) {
            InitializeComponent();
            this.BindingContext = new EcositeDetailsViewModel(Navigation,plotID);

        }
    }
}
