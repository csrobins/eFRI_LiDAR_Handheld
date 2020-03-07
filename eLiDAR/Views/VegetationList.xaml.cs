using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class VegetationList : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchVegetation();
            MyListView.ItemsSource = _viewmodel.VegetationList;

        }
        private VegetationListViewModel _viewmodel;

        //public VegetationList() {
        //    InitializeComponent();
        //    //this.BindingContext = new TreeListViewModel(Navigation);
        //    _viewmodel = new VegetationListViewModel(Navigation);
        //    this.BindingContext = _viewmodel;
        //}

        public VegetationList(string plotID)
        {
            InitializeComponent();
            _viewmodel = new VegetationListViewModel(Navigation, plotID);

            //this.BindingContext = new TreeListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
        } 
        }
    }
