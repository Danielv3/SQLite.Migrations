using System;

namespace Kraai.SQLiteMigrations.Android.Host
{
	[AppVersion(1)]
	public class Migration1 : Migration
	{
		protected override void OnUpgrade (IDatabaseContext db)
		{
			db.ExecuteSql ("CREATE TABLE People (PersonID INTEGER PRIMARY KEY AUTOINCREMENT , FirstName ntext, LastName ntext)");
		}

		protected override void OnDowngrade (IDatabaseContext db)
		{
			db.ExecuteSql ("DROP TABLE People");
		}
	}

	[AppVersion(2)]
	public class Migration2 : IMigration
	{
		public int Version { get { return 2; } }

		public void Upgrade (IDatabaseContext db)
		{
			db.ExecuteSql ("CREATE TABLE People2 (PersonID INTEGER PRIMARY KEY AUTOINCREMENT , FirstName ntext, LastName ntext)");
		}

		public void Downgrade (IDatabaseContext db)
		{
			db.ExecuteSql ("DROP TABLE People2");
		}
	}
}

