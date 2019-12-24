using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotFilterList : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchPlots();

            MyListView.ItemsSource = _viewmodel.PlotList;
        }
        private PlotListViewModel _viewmodel;
        public PlotFilterList() {
            InitializeComponent();
            this.BindingContext = new PlotListViewModel(Navigation);
        }

        public PlotFilterList(string projectID)
        {
            InitializeComponent();
            this.BindingContext = new PlotListViewModel(Navigation, projectID);
        } 
        }

}

