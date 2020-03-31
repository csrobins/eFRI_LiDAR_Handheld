using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DWDList : ContentPage {
      
        public DWDList(string plotID)
        {
            InitializeComponent();
            _viewmodel = new DWDListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchDetails();
            MyListView.ItemsSource = _viewmodel.DWDList;
           
        }
        private DWDListViewModel _viewmodel;
    }
    }
