using System;
using Android.Content;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public class AssetsMigrationsStore : IMigrationsStore
	{
		private Context context;

		public string FileName { get; private set; }

		public AssetsMigrationsStore (string fileName, Context context)
		{
			this.FileName = fileName;
			this.context = context;
		}

		#region IMigrationScriptStore implementation

		public IList<IMigration> GetMigrations (int fromVersion, int toVersion)
		{
			var stream = this.context.Assets.Open (this.FileName);

			throw new NotImplementedException ();
		}

		#endregion
	}
}

