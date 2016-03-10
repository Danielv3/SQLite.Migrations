using NUnit.Framework;
using System;
using Moq;
using System.Reflection;

namespace Kraai.SQLiteMigrations.Tests
{
	[TestFixture ()]
	public class CodeScriptStoreTest
	{
		[Test ()]
		public void GetMigrationsWhenFromVersionIsLessThanToVersion ()
		{
			// ARRANGE
			var store = new CodeMigrationsStore (Assembly.GetExecutingAssembly ());

			// ACT
			var migrations = store.GetMigrations (1, 2);

			// ASSERT
			Assert.IsTrue(migrations.Count == 1);
		}

		[Test ()]
		public void GetMigrationsWhenFromVersionIsEqualThanToVersion ()
		{
			// ARRANGE
			var store = new CodeMigrationsStore (Assembly.GetExecutingAssembly ());

			// ACT
			var migrations = store.GetMigrations (1, 1);

			// ASSERT
			Assert.IsTrue(migrations.Count == 0);
		}

		[Test ()]
		public void GetMigrationsWhenFromVersionIsGreaterThanToVersion ()
		{
			// ARRANGE
			var store = new CodeMigrationsStore (Assembly.GetExecutingAssembly ());

			// ACT
			var migrations = store.GetMigrations (3, 1);

			// ASSERT
			Assert.IsTrue(migrations.Count == 1);
		}
	}


	// Test classes implementing Migration Scripts: 

	[AppVersion(1)]
	public class Migration1 : Migration
	{
		protected override void OnUpgrade (IDatabaseContext db)
		{
			db.ExecuteSql ("CREATE TABLE Person ...");
		}

		protected override void OnDowngrade (IDatabaseContext db)
		{
			db.ExecuteSql ("DROP TABLE Person ...");
		}
	}

	[AppVersion(2)]
	public class Migration2 : IMigration
	{
		public int Version { get { return 2; } }

		public void Upgrade (IDatabaseContext db)
		{
			db.ExecuteSql ("CREATE TABLE Person ...");
		}

		public void Downgrade (IDatabaseContext db)
		{
			db.ExecuteSql ("DROP TABLE Person ...");
		}
	}
}