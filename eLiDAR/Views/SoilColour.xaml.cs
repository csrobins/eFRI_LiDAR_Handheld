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
     
    public partial class SoilColour : ContentPage
    {
        private SoilColourViewModel _viewmodel;
  
        public SoilColour(SOIL _soil,  bool ischanged)
        {
            InitializeComponent();
            _viewmodel = new SoilColourViewModel(Navigation, _soil, ischanged);
            this.BindingContext = _viewmodel;
        }
    }
}