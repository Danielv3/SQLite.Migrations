using System;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public interface IMigrationsStore
	{
		IList<IMigration> GetMigrations (int fromVersion, int toVersion);
	}
}

