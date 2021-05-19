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
     
    public partial class Texture : ContentPage
    {
        private TextureViewModel _viewmodel;
       

        public Texture(SOIL soil)
        {
            InitializeComponent();
            _viewmodel = new TextureViewModel(Navigation, soil);
            this.BindingContext = _viewmodel;
            
        }
        public Texture(ECOSITE ecosite)
        {
            InitializeComponent();
            _viewmodel = new TextureViewModel(Navigation, ecosite);
            this.BindingContext = _viewmodel;

        }
    }
}