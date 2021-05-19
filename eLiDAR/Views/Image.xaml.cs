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
     
    public partial class ImagePage : ContentPage
    {
        private ImageViewModel _viewmodel;
        public ImagePage()
        {
            InitializeComponent();
            _viewmodel = new ImageViewModel(Navigation);
            this.BindingContext = _viewmodel;
        }
    }
}