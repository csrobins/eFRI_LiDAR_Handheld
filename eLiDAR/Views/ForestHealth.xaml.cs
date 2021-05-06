using eLiDAR.Models;
using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views
{
    public partial class ForestHealth : ContentPage
    {
        private ForestHealthViewModel _viewmodel;
        public ForestHealth(PLOT _plot)
        {
            try
            {
                InitializeComponent();
                _viewmodel = new ForestHealthViewModel(Navigation, _plot);
                this.BindingContext = _viewmodel;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}