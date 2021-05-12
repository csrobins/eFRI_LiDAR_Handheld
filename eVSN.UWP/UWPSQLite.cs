using System;  
using System.Collections.Generic;
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

            // Use this if I need to reset the datbase
            bool Isdebug = false;
            if (Isdebug == true)
            {
                File.Delete(path);
            }
            _ = CheckFile();

            return new SQLiteConnection(path);
        }
       async Task<bool> CheckFile()
        {
            var storageFile = IsolatedStorageFile.GetUserStoreForApplication();
            if (!storageFile.FileExists(path))
            {
                // copy storage file; replace if exists.
                var fileToRead = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///" + DatabaseHelper.DbFileName, UriKind.Absolute));
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile fileCopy = await fileToRead.CopyAsync(storageFolder, DatabaseHelper.DbFileName, NameCollisionOption.ReplaceExisting);
            }
            return true;
        }
    }
}
