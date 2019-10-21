using eLiDAR.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlotEdit : ContentPage
    {
        public ObservableCollection<FakePlot> Items { get; set; } = App.FakePlots;

        public PlotEdit()
        {
            InitializeComponent();
            this.BindingContext = Items;
        }
    }
}