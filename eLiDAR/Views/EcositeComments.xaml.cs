using eLiDAR.Models;
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
     
    public partial class EcositeComments : ContentPage
    {
        private EcositeCommentsViewModel _viewmodel;
        public EcositeComments(ECOSITE  _ecosite)
        {

            try
            {
                InitializeComponent();
                _viewmodel = new EcositeCommentsViewModel(Navigation, _ecosite);
                this.BindingContext = _viewmodel;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}