using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotList : ContentPage {
        public PlotList() {
            InitializeComponent();
        }

		protected override void OnAppearing() {
            this.BindingContext = new PlotListViewModel(Navigation);
		}
    }
}
