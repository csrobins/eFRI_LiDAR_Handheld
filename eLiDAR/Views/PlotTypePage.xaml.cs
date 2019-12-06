using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
//using eLiDAR;

namespace eLiDAR.Views
{
    public partial class PlotTypePage : ContentPage
    {
        public ObservableCollection<PlotType> Items { get; set; } = App.PlotTypes;

        public PlotTypePage()
        {
            InitializeComponent();

            Shell.SetTitleColor(this, Color.Blue);
            Shell.SetSearchHandler(this, new FeedSearchHandler());
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            Title = "eLiDARs";
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.AddPlot());
        }

    }

    public class FeedSearchHandler : SearchHandler
    {
        public FeedSearchHandler()
        {
            SearchBoxVisibility = SearchBoxVisibility.Expanded;
            IsSearchEnabled = true;
            ShowsResults = true;
            Placeholder = "Find a seashell...";
            CancelButtonColor = Color.White;
            TextColor = Color.White;
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrEmpty(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var results = new List<string>();
                results = App.Seashells.Where(x => x.Text.IndexOf(newValue, StringComparison.InvariantCultureIgnoreCase) > -1).Select(x => x.Text).ToList<string>();
                ItemsSource = results;
            }
        }
    }

    public class Seashell
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string Image
        {
            get
            {
                return $"https://loremflickr.com/320/240/{Text}%20shell";
            }
        }
    }

    public class PlotType
    {
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public ImageSource Image => Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(ImageName) : ImageSource.FromFile("Images/" + ImageName);
    }
    public class FakeTree
    {
        public long TreeID { get; set; }
        public long TreeNum { get; set; }
        public string Spp { get; set; }
        public double Dbh { get; set; }
        public double Ht { get; set; }
        public string Status { get; set; }
        
    }
    public class FakePlot
    {
        public long PlotID { get; set; }
        public long PlotNum { get; set; }
        public string PlotType { get; set; }
        public DateTime Plot_Date { get; set; }
        public string Crew { get; set; }
     
    }

}


