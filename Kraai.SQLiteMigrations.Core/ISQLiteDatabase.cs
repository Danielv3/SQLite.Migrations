using System;

namespace Kraai.SQLiteMigrations
{
	public interface ISQLiteDatabase
	{
		void BeginTransaction ();
		void CommitTransaction ();
		void RollbackTransaction ();

		void Open (string databasePath);
		void Close ();

		void ExecuteSql (string sql);
	}
}

