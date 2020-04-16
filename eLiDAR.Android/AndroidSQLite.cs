using System.IO;
using System.Threading.Tasks;
using eLiDAR;
using eLiDAR.Droid;
using eLiDAR.Helpers;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLite))]
namespace eLiDAR.Droid {
    public class AndroidSQLite : ISQLite {
        public SQLiteConnection GetConnection() {
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			// Documents folder
            var path = Path.Combine(documentsPath, DatabaseHelper.DbFileName);

            // Use this if I need to reset the datbase
            bool Isdebug = false;
            if (Isdebug == true)
                {
                File.Delete(path);
                }

            if ((!File.Exists(path)))
            {
                using var binaryReader = new BinaryReader(Android.App.Application.Context.Assets.Open(DatabaseHelper.DbFileName));
                using var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create));
                byte[] buffer = new byte[2048];
                int length = 0;
                while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    binaryWriter.Write(buffer, 0, length);
                }
                // intialize the synchonizer to the base azure web service
               // var result = task.Wait();

            }
            var conn = new SQLiteConnection( path);
			// Return the database connection
			return conn;
		}

   
    }
}
