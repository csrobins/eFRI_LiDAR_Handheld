using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dotMorten.Xamarin.Forms;
using eLiDAR.Models;
using eLiDAR.Services;

namespace eLiDAR.Views
{
     
    public partial class VegetationCensusView : ContentView
    {

        private List<string> ListPlant = new List<string>();

        public VegetationCensusView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            using (var s = typeof(VegetationCensusView).Assembly.GetManifestResourceStream("eLiDAR.Data.Genus.txt"))
            {
                ListPlant = new StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
            }
        }

        private void TxtVeg_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            AutoSuggestBox box = (AutoSuggestBox)sender;
            // Filter the list based on text input
            box.ItemsSource = GetPlantSuggestions(box.Text);
        }
         private List<string> GetPlantSuggestions(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : ListPlant.Where(s => s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

    }
}
