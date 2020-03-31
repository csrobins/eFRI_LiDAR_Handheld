using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
    public partial class CameraPage : ContentPage
    {
        public CameraPage(ECOSITE _ecosite)
        {
            InitializeComponent();
            BindingContext = new CameraViewModel(_ecosite);
        }


    }
}