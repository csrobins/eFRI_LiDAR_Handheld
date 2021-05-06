using eLiDAR.Models;
using eLiDAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
     
    public partial class PlotComments : ContentPage
    {
        private PlotCommentsViewModel _viewmodel;
        public PlotComments(PLOT _plot)
        {

            try
            {
                InitializeComponent();
                _viewmodel = new PlotCommentsViewModel(Navigation, _plot);
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