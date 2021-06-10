using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class ProjectList : ContentPage {

        public ProjectList() {
            InitializeComponent();
            _viewmodel = new ProjectListViewModel(Navigation);
            this.BindingContext = _viewmodel;
            _viewmodel.FetchProjects();
        }
        private ProjectListViewModel _viewmodel;
        protected override void OnAppearing() {
            base.OnAppearing(); 
            MyListView.ItemsSource = null;
            _viewmodel.FetchProjects();
            if (_viewmodel.IsLoggedIn)
            {
                MyListView.ItemsSource = _viewmodel.ProjectList;

            }

            if (_viewmodel != null) { _viewmodel.Refresh(); }
        }
    }
}
