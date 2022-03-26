using System;  
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;
using eLiDAR.Helpers;
using eLiDAR.UWP;
using SQLite;  
using Windows.Storage;  
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlConnection))]  
  
namespace eLiDAR.UWP
{
    class SqlConnection : ISQLite
    {
        private string path;
        public SQLiteConnection GetConnection()
        {
            string documentPath = ApplicationData.Current.LocalFolder.Path;
            path = Path.Combine(documentPath, DatabaseHelper.DbFileName);
            //path = Path.Combine(documentPath, "test.sqlite");

            // Use this if I need to reset the datbase
            bool Isdebug = false;
            if (Isdebug == true)
            {
                File.Delete(path);
            }
            _ = CheckFile();

            try
            {
                return new SQLiteConnection(path);
            }
            catch (SQLiteException sqle)
            {
                Debug.WriteLine(sqle);
            }
            return null;
        }
        async Task<bool> CheckFile()
        {
            //var storageFile = IsolatedStorageFile.GetUserStoreForApplication();
            //if (!storageFile.FileExists(path))
            //{
            //    // copy storage file; replace if exists.
            //    var uri = new Uri("ms-appx:///Assets/eLiDAR.sqlite");
            //    try
            //    {
            //        //var fileToRead = await StorageFile.GetFileFromApplicationUriAsync(uri);
            //        StorageFile file1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/eLiDAR.sqlite"));
            //        await file1.CopyAsync(ApplicationData.Current.LocalFolder, "eLiDAR.sqlite");
            //        Debug.WriteLine(uri.ToString());
            //        //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //        //StorageFile fileCopy = await fileToRead.CopyAsync(storageFolder, DatabaseHelper.DbFileName, NameCollisionOption.ReplaceExisting);

            //        // Get the app's temporary folder.
            //        StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;

            //        // Create a sample file in the temporary folder.
            //        string newFileName = "test.txt";

            //        StorageFile newFile = await tempFolder.CreateFileAsync(newFileName);



            //    }
            //    catch (SQLiteException sqle)
            //    {
            //        Debug.WriteLine(sqle);
            //    }
            //}


            var dbFile = await ApplicationData.Current.LocalFolder.TryGetItemAsync("eLiDAR.sqlite") as StorageFile; 
            if (null == dbFile)
            {// first time ... copy the .db file from assets to local  folder
                var localFolder = ApplicationData.Current.LocalFolder;
                var originalDbFileUri = new Uri("ms-appx:///Assets/eLiDAR.sqlite");
                var originalDbFile = await StorageFile.GetFileFromApplicationUriAsync(originalDbFileUri);
                if(null != originalDbFile)
                {
                   dbFile = await originalDbFile.CopyAsync(localFolder, "eLiDAR.sqlite", NameCollisionOption.ReplaceExisting);
                }
            }

            return true;

        }
    }
}
