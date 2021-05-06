using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eLiDAR
{
   
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            { var error = ex.Message;
            }

        }
    }
}