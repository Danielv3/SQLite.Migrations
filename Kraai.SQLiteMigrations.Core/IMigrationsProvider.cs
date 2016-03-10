using System;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public interface IMigrationsProvider
	{
		IList<IMigration> GetMigrations (IList<IMigrationsStore> stores, int fromVersion, int toVersion);
	}
}

