using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class ProjectList : ContentPage {

        public ProjectList() {
            InitializeComponent();
        }
        private ProjectListViewModel _viewmodel;
        protected override void OnAppearing() {
            _viewmodel = new ProjectListViewModel(Navigation);
            this.BindingContext = _viewmodel;
            MyListView.ItemsSource = null;
            MyListView.ItemsSource = _viewmodel.ProjectList;
            _viewmodel.FetchProjects(); 
		}
    }
}
