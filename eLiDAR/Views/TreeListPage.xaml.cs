using System;
using System.Linq;
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
            MyListView.ItemsSource = _viewmodel.TreeStemListFull;
        }
        private TreeListViewModel _viewmodel;


        public TreeListPage(string plotID)
        {
            InitializeComponent();
            _viewmodel = new TreeListViewModel(Navigation, plotID);

            //this.BindingContext = new TreeListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
        }
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            MyListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                MyListView.ItemsSource = _viewmodel.TreeStemListFull;
            else
                MyListView.ItemsSource = _viewmodel.TreeStemListFull.Where(i => i.TREENUMBER.Equals(Int32.Parse( e.NewTextValue)));

            MyListView.EndRefresh();
        }
    }
    }
