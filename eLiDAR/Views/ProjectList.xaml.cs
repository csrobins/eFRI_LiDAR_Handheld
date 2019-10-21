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

		protected override void OnAppearing() {
            this.BindingContext = new ProjectListViewModel(Navigation);
		}
    }
}
