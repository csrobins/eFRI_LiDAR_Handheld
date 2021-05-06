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
     
    public partial class SoilStructure : ContentPage
    {
        private SoilStructureViewModel _viewmodel;
  
        public SoilStructure(SOIL _soil)
        {
            InitializeComponent();
            _viewmodel = new SoilStructureViewModel(Navigation, _soil);
            this.BindingContext = _viewmodel;
            
        }
    }
}