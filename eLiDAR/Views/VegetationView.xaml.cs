using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dotMorten.Xamarin.Forms;


namespace eLiDAR.Views
{
     
    public partial class VegetationView : ContentView
    {

        private List<string> ListPlant = new List<string>();

        public VegetationView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            using (var s = typeof(VegetationView).Assembly.GetManifestResourceStream("eLiDAR.Data.Genus.txt"))
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
