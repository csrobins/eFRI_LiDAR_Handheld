using eLiDAR.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlotView : ContentPage
    {
        public ObservableCollection<FakePlot> Items { get; set; } = App.FakePlots;

        public PlotView()
        {
            InitializeComponent();

       
            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new TreeView());
          //  await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void Edit_Plot(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new PlotEdit());
           
        }
        async void View_Trees(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new TreeView());

        }
    }
}
