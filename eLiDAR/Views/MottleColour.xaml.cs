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
     
    public partial class MottleColour : ContentPage
    {
        private MottleColourViewModel _viewmodel;
  
        public MottleColour(SOIL _soil, bool ischanged)
        {
            InitializeComponent();
            _viewmodel = new MottleColourViewModel(Navigation, _soil, ischanged);
            this.BindingContext = _viewmodel;
            
        }
    }
}