using System;
using System.Linq;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public class MigrationsProvider : IMigrationsProvider
	{
		#region IMigrationsProvider implementation
		public IList<IMigration> GetMigrations (IList<IMigrationsStore> stores, int fromVersion, int toVersion)
		{
			var migrations = new List<IMigration> ();

			// Mix migration scripts from distinct stores...
			foreach (var store in stores.Reverse ()) {
				var partialMigrations = store.GetMigrations (fromVersion, toVersion);

				migrations = migrations.Union (partialMigrations, new MigrationScriptComparer ()).ToList ();
			}

			return migrations;
		}
		#endregion


		private class MigrationScriptComparer : IEqualityComparer<IMigration>
		{
			public bool Equals(IMigration migration1, IMigration migration2)
			{
				return migration1.Version == migration2.Version;
			}

			public int GetHashCode(IMigration migration)
			{
				return migration.Version;
			}
		}
	}
}

