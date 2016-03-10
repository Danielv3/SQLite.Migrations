using System;

namespace Kraai.SQLiteMigrations
{
	public interface ILiquidSQLite
	{
		void MigrateDatabase ();

		void RegisterMigrationStore (IMigrationsStore store);
		void RegisterService<T> (Func<T> factory)
			where T : class;
	}
}

