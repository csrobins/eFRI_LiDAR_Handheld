using System;
using System.Linq;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotList : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchPlots();

            UpdateList(this.MainSearch.Text); 
        }
        private PlotListViewModel _viewmodel;
    
        public PlotList(string projectID)
        {
            InitializeComponent();
            _viewmodel = new PlotListViewModel(Navigation, projectID);
            this.BindingContext = _viewmodel;
            _viewmodel.FetchPlots();

        }
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList(e.NewTextValue); 
            //MyListView.BeginRefresh();

            //if (string.IsNullOrWhiteSpace(e.NewTextValue))
            //    MyListView.ItemsSource = _viewmodel.PlotListFull;
            //else
            //    MyListView.ItemsSource = _viewmodel.PlotListFull.Where(i => i.VSNPLOTNAME.Contains(e.NewTextValue));

            //MyListView.EndRefresh();
        }
        private void UpdateList(string txtvalue)
        {
            MyListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(txtvalue))
                MyListView.ItemsSource = _viewmodel.PlotListFull;
            else
                MyListView.ItemsSource = _viewmodel.PlotListFull.Where(i => i.VSNPLOTNAME.Contains(txtvalue));
            MyListView.EndRefresh();
        }
    }
    }
