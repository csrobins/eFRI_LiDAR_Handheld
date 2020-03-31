using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class SmallTreeList : ContentPage {
      
        public SmallTreeList(string plotID)
        {
            InitializeComponent();
            _viewmodel = new SmallTreeListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
     //       MyListView.ItemsSource = null;
      //      _viewmodel.FetchSmallTree();
       //     MyListView.ItemsSource = _viewmodel.SmallTreeList;

        }
        private SmallTreeListViewModel _viewmodel;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            _viewmodel.FetchSmallTree();
        }


    }
}
