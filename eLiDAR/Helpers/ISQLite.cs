using System;
using SQLite;

namespace eLiDAR.Helpers
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}
