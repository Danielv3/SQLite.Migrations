using System;
using Android.Content;

namespace Kraai.SQLiteMigrations
{
	public class VersionManager : IVersionManager
	{
		private Context context;

		public VersionManager (Context context)
		{
			this.context = context;
		}

		#region IVersionManager implementation

		public int GetInstalledVersion ()
		{
			// https://gist.github.com/Redth/5862119
			var package = this.context.PackageManager.GetPackageInfo (context.PackageName, 0);
			return package.VersionCode;
		}

		public int GetPreinstalledVersion ()
		{
			throw new NotImplementedException ();
		}

		public void SynchronizeVersions ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

