using System;
using Xamarin.Forms;
using eLiDAR.ViewModels;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eLiDAR.Views
{
     
    public partial class DefaultPage : ContentPage
    {
        public DefaultPage()
        {
            InitializeComponent();
        
        }
        private DefaultViewModel _viewmodel;
        protected override void OnAppearing()
        {
            try
            {
                _viewmodel = new DefaultViewModel(Navigation);
                this.BindingContext = _viewmodel;
            }
            catch (Exception ex) 
            { 
                var msg = ex.Message;
            }
            
        }
    }
}