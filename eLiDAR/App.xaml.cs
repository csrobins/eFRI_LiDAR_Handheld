using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eLiDAR.Views;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eLiDAR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

                //   MainPage = new eLiDAR.AppShell();
             MainPage = new MainShell();       
        }
       

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static ObservableCollection<Seashell> Seashells = new ObservableCollection<Seashell>{
                new Seashell {
                    Text = "Auger"
                },
                new Seashell {
                    Text = "Abalone"
                },
                new Seashell {
                    Text = "Strombus"
                },
                new Seashell {
                    Text = "Sundial"
                },
                new Seashell {
                    Text = "Tibia"
                },
                new Seashell {
                    Text = "Tonna"
                },
                new Seashell {
                    Text = "Top"
                },
                new Seashell {
                    Text = "Triton"
                },
                new Seashell {
                    Text = "Turritella"
                },
                new Seashell {
                    Text = "Turbo"
                },
                new Seashell {
                    Text = "Umbonium"
                },
                new Seashell {
                    Text = "Volute"
                }
            };

        public static ObservableCollection<PlotType> PlotTypes = new ObservableCollection<PlotType>{
                new PlotType {
                    Text = "Type A",
                    Description = "Simple Plot",
                    ImageName = "pine.jpg"
                },
                new PlotType {
                    Text = "Type B",
                    Description = "A detailed plot",
                    ImageName = "spruce.png"
                },
             };
        public static ObservableCollection<FakeTree> FakeTrees = new ObservableCollection<FakeTree>{
                new FakeTree {
                    TreeID = 1,
                    TreeNum = 1,
                    Spp = "Pw",
                    Dbh = 56.5,
                    Ht = 25.6,
                    Status = "L"
                },
                new FakeTree {
                    TreeID = 2,
                    TreeNum = 2,
                    Spp = "Pr",
                    Dbh = 54.5,
                    Ht = 21.6,
                    Status = "L"
                },
                new FakeTree {
                    TreeID = 3,
                    TreeNum = 3,
                    Spp = "Pw",
                    Dbh = 66.5,
                    Ht = 31.6,
                    Status = "L"
                },
                new FakeTree {
                    TreeID = 4,
                    TreeNum = 4,
                    Spp = "Pj",
                    Dbh = 34.5,
                    Ht = 18.6,
                    Status = "L"
                },
                new FakeTree {
                    TreeID = 5,
                    TreeNum = 5,
                    Spp = "Sb",
                    Dbh = 28.6,
                    Status = "D"
                },
             };
        public static ObservableCollection<FakePlot> FakePlots = new ObservableCollection<FakePlot>{
                new FakePlot {
                    PlotID = 1,
                    PlotNum = 1,
                    PlotType = "A",
                    Crew = "Jim and Craig",
                },
                new FakePlot {
                    PlotID = 2,
                    PlotNum = 2,
                    PlotType = "A",
                    Crew = "John and Craig",
                },
                new FakePlot {
                    PlotID = 3,
                    PlotNum = 4,
                    PlotType = "B",
                    Crew = "Craig. Christine",
                },
                new FakePlot {
                    PlotID = 4,
                    PlotNum = 5,
                    PlotType = "A,B",
                    Crew = "Jim and Craig",
                },
                new FakePlot {
                    PlotID = 5,
                    PlotNum = 10,
                    PlotType = "A",
                    Crew = "Jim and Christine",
                },
             };

    }
}
