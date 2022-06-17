using System;
using System.Linq;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotSynchList : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchPlots();

            UpdateList(this.MainSearch.Text); 

        }
        private PlotSynchViewModel _viewmodel;
    
        public PlotSynchList()
        {
            InitializeComponent();
            _viewmodel = new PlotSynchViewModel(Navigation);
            this.BindingContext = _viewmodel;
            _viewmodel.FetchPlots();

        }
        private void SynchRun_OnClick(object sender, TextChangedEventArgs e)
        {
//            UpdateList(e.NewTextValue);
            this.MyListView.ItemsSource = _viewmodel.PlotList;  
        }
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList(e.NewTextValue); 
        }
        private void UpdateList(string txtvalue)
        {
            MyListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(txtvalue))
                MyListView.ItemsSource = _viewmodel.PlotList;
            else
                MyListView.ItemsSource = _viewmodel.PlotList.Where(i => i.VSNPLOTNAME.Contains(txtvalue));
            MyListView.EndRefresh();
        }
    }
    }
