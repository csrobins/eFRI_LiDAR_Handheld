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
    public partial class TreeView : ContentPage
    {
        public ObservableCollection<FakeTree> Items { get; set; } = App.FakeTrees;

        public TreeView()
        {
            InitializeComponent();
                   
            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //var strText = sender.ToString;
            //string strItem = e.Item.ToString; 
            await DisplayAlert("Item Tapped", "A tree item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
