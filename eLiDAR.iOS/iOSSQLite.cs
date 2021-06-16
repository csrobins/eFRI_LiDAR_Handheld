using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using elidar.iOS;
using eLiDAR.Helpers;
using Foundation;
using SQLite;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IOSSQLite))]
namespace elidar.iOS
{
    public class IOSSQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            // Documents folder

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            string dbPath = Path.Combine(libFolder, DatabaseHelper.DbFileName);

            if (!Directory.Exists(libFolder))
            {
                dbPath = FileAccessHelper.GetLocalFilePath(DatabaseHelper.DbFileName);
            }

            var conn = new SQLiteConnection(dbPath);
            // Return the database connection
            return conn;
        }
        public class FileAccessHelper
        {
            public static string GetLocalFilePath(string filename)
            {
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

                if (!Directory.Exists(libFolder))
                {
                    Directory.CreateDirectory(libFolder);
                }

                string dbPath = Path.Combine(libFolder, filename);

                CopyDatabaseIfNotExists(dbPath);

                return dbPath;
            }

            private static void CopyDatabaseIfNotExists(string dbPath)
            {
                bool debug = false;
                if (debug)
                {
                    File.Delete(dbPath); 
                }
                if (!File.Exists(dbPath))
                {
                    var existingDb = NSBundle.MainBundle.PathForResource("eLiDAR", "sqlite");
                    File.Copy(existingDb, dbPath);
                }
            }

        }
    }
}