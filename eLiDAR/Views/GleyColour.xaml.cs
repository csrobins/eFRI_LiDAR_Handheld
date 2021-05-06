using dotMorten.Xamarin.Forms;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
     
    public partial class GleyColour : ContentPage
    {
        private GleyColourViewModel _viewmodel;
  
        public GleyColour(SOIL _soil)
        {
            InitializeComponent();
            _viewmodel = new GleyColourViewModel(Navigation, _soil);
            this.BindingContext = _viewmodel;
            
        }
    }
}