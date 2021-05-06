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
     
    public partial class EcositeCode : ContentPage
    {
        private EcositeCodeViewModel _viewmodel;
        private List<string> ListPlant = new List<string>();

        public EcositeCode(ECOSITE _ecosite)
        {
            InitializeComponent();
            _viewmodel = new EcositeCodeViewModel(Navigation, _ecosite);
            this.BindingContext = _viewmodel;
            Initialize();
        }

        private void Initialize()
        {
            using (var s = typeof(EcositeCode).Assembly.GetManifestResourceStream("eLiDAR.Data.Ecosites.txt"))
            {
                ListPlant = new StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
            }
        }

        private void TxtEco_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            AutoSuggestBox box = (AutoSuggestBox)sender;
            // Filter the list based on text input
        box.ItemsSource = GetSuggestions(box.Text);
        }
        private List<string> GetSuggestions(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : ListPlant.Where(s => s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
}