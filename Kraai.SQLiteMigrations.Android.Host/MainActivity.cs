using Android.App;
using Android.Widget;
using Android.OS;
using System.Reflection;
using System.IO;

namespace Kraai.SQLiteMigrations.Android.Host
{
	[Activity (Label = "SQLite Migrations", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

			var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data.db");

			// TEST
			ILiquidSQLite db = new LiquidSQLite(path, this);
			db.UseMigrationsFromExecutingAssembly ();

			db.MigrateDatabase ();
		}
	}
}


