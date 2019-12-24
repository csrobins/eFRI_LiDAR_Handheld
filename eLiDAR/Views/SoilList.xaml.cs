using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class SoilList : ContentPage {
      
        public SoilList(string plotID)
        {
            InitializeComponent();
            _viewmodel = new SoilListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchSoil();
            MyListView.ItemsSource = _viewmodel.SoilList;
           
        }
        private SoilListViewModel _viewmodel;
    }
    }
