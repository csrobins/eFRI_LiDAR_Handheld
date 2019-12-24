using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeListPage : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.FetchTrees();
            MyListView.ItemsSource = _viewmodel.TreeStemList;

        }
        private TreeListViewModel _viewmodel;

        public TreeListPage() {
            InitializeComponent();
            //this.BindingContext = new TreeListViewModel(Navigation);
            _viewmodel = new TreeListViewModel(Navigation);
            this.BindingContext = _viewmodel;
        }

        public TreeListPage(string plotID)
        {
            InitializeComponent();
            _viewmodel = new TreeListViewModel(Navigation, plotID);

            //this.BindingContext = new TreeListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
        } 
        }
    }
