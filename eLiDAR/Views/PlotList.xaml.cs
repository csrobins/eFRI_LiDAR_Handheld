using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PlotList : ContentPage {
        public PlotList() {
            InitializeComponent();
            this.BindingContext = new PlotListViewModel(Navigation);
        }

        public PlotList(string projectID)
        {
            InitializeComponent();
            this.BindingContext = new PlotListViewModel(Navigation, projectID);
        } 
        }
    }
