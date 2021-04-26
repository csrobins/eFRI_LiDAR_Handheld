using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class VegetationCensusList : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewmodel.FetchVegetation();

        }
        private VegetationCensusListViewModel _viewmodel;

        public VegetationCensusList(string plotID)
        {
            InitializeComponent();
            _viewmodel = new VegetationCensusListViewModel(Navigation, plotID);

            this.BindingContext = _viewmodel;
        } 
        }
    }
