using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotDetailsPage : ContentPage {
        //private string pROJECTID;

        //public PlotDetailsPage(string pROJECTID)
        //{
        //    this.pROJECTID = pROJECTID;
        //}

        public PlotDetailsPage(string plotID) {
            InitializeComponent();
            this.BindingContext = new PlotDetailsViewModel(Navigation,plotID);
            //this.BindingContext = new PlotDetailsViewModel(Navigation);

            //picker.ItemsSource = GFG.My_list;
        }
    }
}
