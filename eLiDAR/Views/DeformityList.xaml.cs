using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DeformityList : ContentPage {
      
        public DeformityList(string treeID)
        {
            InitializeComponent();
            _viewmodel = new DeformityListViewModel(Navigation, treeID);
            this.BindingContext = _viewmodel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchDetails();
            MyListView.ItemsSource = _viewmodel.DeformityList ;
           
        }
        private DeformityListViewModel _viewmodel;
    }
    }
