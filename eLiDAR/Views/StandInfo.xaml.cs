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
     
    public partial class StandInfo : ContentPage
    {
        private StandInfoViewModel _viewmodel;
        public StandInfo(PLOT _plot)
        {

            try
            {
                InitializeComponent();
 //               _viewmodel = new StandInfoViewModel(Navigation, _plot);
                this.BindingContext = new StandInfoViewModel(Navigation, _plot);
  //              this.BindingContext = _viewmodel;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                ((StandInfoViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((StandInfoViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}