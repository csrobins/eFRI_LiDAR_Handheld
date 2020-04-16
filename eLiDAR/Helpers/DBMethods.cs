using System;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models; 

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
}