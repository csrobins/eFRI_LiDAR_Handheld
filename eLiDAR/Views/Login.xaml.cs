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
     
    public partial class Login : ContentPage
    {
        private LoginViewModel _viewmodel;
       

        public Login()
        {
            InitializeComponent();
            _viewmodel = new LoginViewModel(Navigation);
            this.BindingContext = _viewmodel;
            
        }
    }
}