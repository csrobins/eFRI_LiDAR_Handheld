using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views
{
    public partial class AddPlot : ContentPage {
        public AddPlot() {
            InitializeComponent();
            BindingContext = new AddPlotViewModel(Navigation);
        }
    }
}
