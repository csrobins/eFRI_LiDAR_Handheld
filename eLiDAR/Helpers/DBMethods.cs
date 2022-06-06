using System;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using System.Reflection;
using System.Diagnostics;

namespace eLiDAR.Helpers
{
    public class DatabaseMethods
    {
        static SQLiteConnection sqliteconnection;

        public DatabaseMethods()
        {
            sqliteconnection = DependencyService.Get<ISQLite>().GetConnection();
        
        }
          

    }
        public class EmbeddedSourceror
    {
        public static Xamarin.Forms.ImageSource SourceFor(string pclFilePathInResourceFormat)
        {
            var resources = typeof(EmbeddedSourceror).GetTypeInfo().Assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(pclFilePathInResourceFormat, StringComparison.OrdinalIgnoreCase));
            Debug.WriteLine("EmbeddedSourceror: resourceName string is " + resourceName);

            return Xamarin.Forms.ImageSource.FromResource(resourceName);
        }
    }
}