using System;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace Kraai.SQLiteMigrations
{
	public class SQLiteDatabase : ISQLiteDatabase
	{
		private SqliteConnection connection;
		private SqliteTransaction transaction;

		#region ISQLiteDatabase implementation

		public void BeginTransaction ()
		{
			if (this.transaction == null) {
				this.transaction = this.connection.BeginTransaction ();
			}
		}

		public void CommitTransaction ()
		{
			if (this.transaction != null) {
				this.transaction.Commit ();
			}
		}

		public void RollbackTransaction ()
		{
			if (this.transaction != null) {
				this.transaction.Rollback ();
			}
		}

		public void Open (string databasePath)
		{
			try
			{
				if (!File.Exists (databasePath)) {
					SqliteConnection.CreateFile (databasePath);
				}
				string connectionString = string.Format ("Data Source={0};Version=3;", databasePath);
				this.connection = new SqliteConnection (connectionString);
				this.connection.Open ();
			}
			catch (Exception exception) {
				// TODO: Log it!
				throw new Exception ("Database cannot be opened", exception);
			}
		}

		public void Close ()
		{
			this.connection.Close ();
		}

		public void ExecuteSql (string sql)
		{
			if (!string.IsNullOrWhiteSpace (sql)) {
				try {
					using (var command = new SqliteCommand (this.connection)) {
						command.CommandType = CommandType.Text;
						command.CommandText = sql;

						command.ExecuteNonQuery ();
					}
				} catch (SqliteException exception) {
					// TODO: Log it!
					throw new Exception ("Error while executing SQL command", exception);
				}
			}
		}

		#endregion
	}
}

/*

var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
var path = System.IO.Path.Combine(documents, "data.db");
if (!System.IO.File.Exists (path)) {
	Mono.Data.Sqlite.SqliteConnection.CreateFile (path);
}

var connectionString = string.Format ("Data Source={0};Version=3;", path);

using (var connection = new Mono.Data.Sqlite.SqliteConnection (connectionString)) {
	connection.Open ();
	var transaction = connection.BeginTransaction ();
	using (var command = new Mono.Data.Sqlite.SqliteCommand (connection)) {
		command.CommandType = System.Data.CommandType.Text;
		command.CommandText = "CREATE TABLE People (PersonID INTEGER PRIMARY KEY AUTOINCREMENT , FirstName ntext, LastName ntext)";

		command.ExecuteNonQuery ();
	}
	transaction.Commit ();
}

*/