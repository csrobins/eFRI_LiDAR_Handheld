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
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
            _viewmodel = new AboutViewModel(Navigation);
            this.BindingContext = _viewmodel;
        }
        private AboutViewModel _viewmodel;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
      
            _viewmodel.FetchSettings();
         

        }
        
    }
}