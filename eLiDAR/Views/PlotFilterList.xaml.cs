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
            _viewmodel = new PlotListViewModel(Navigation);
            this.BindingContext = _viewmodel;
        }

        public PlotFilterList(string projectID)
        {
            InitializeComponent();
            _viewmodel = new PlotListViewModel(Navigation);
            this.BindingContext = _viewmodel;
        } 
        }

}

