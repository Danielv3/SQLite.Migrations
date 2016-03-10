using System;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public interface IMigrationsInvoker
	{
		void ExecuteMigrations (string databasePath, IList<IMigration> migrations, int fromVersion, int toVersion);
	}
}

