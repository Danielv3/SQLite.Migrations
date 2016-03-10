using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using System.Reflection;

namespace Kraai.SQLiteMigrations.Tests
{
	[TestFixture ()]
	public class MigrationsProviderTest
	{
		[Test ()]
		public void GetMigrationsWithOneStore ()
		{
			// ARRANGE
			var store = GetMigrationStore (new [] {
				GetMigrationScript (1),
				GetMigrationScript (2)
			});

			var provider = new MigrationsProvider ();

			// ACT
			var migrations = provider.GetMigrations (new [] { store }.ToList (), 1, 2);

			// ASSERT
			Assert.AreEqual (migrations.Count, 2);
		}

		[Test ()]
		public void GetMigrationsWithTwoStoresAndNotOverridenScripts ()
		{
			// ARRANGE
			var store1 = GetMigrationStore (new [] {
				GetMigrationScript (1),
				GetMigrationScript (2)
			});

			var store2 = GetMigrationStore (new [] {
				GetMigrationScript (3),
				GetMigrationScript (4)
			});

			var provider = new MigrationsProvider ();

			// ACT
			var migrations = provider.GetMigrations (new [] { store1, store2 }.ToList (), 1, 4);

			// ASSERT
			Assert.AreEqual (migrations.Count, 4);
		}

		[Test ()]
		public void GetMigrationsWithTwoStoresAndOverridenScripts ()
		{
			// ARRANGE
			var migrationVersion1ForStore1 = GetMigrationScript (1);
			var migrationVersion2ForStore1 = GetMigrationScript (2);

			var store1 = GetMigrationStore (new [] {
				migrationVersion1ForStore1,
				migrationVersion2ForStore1
			});

			var migrationVersion2ForStore2 = GetMigrationScript (2);
			var migrationVersion3ForStore2 = GetMigrationScript (3);

			var store2 = GetMigrationStore (new [] {
				migrationVersion2ForStore2,
				migrationVersion3ForStore2
			});

			// ACT
			var provider = new MigrationsProvider ();
			var migrations = provider.GetMigrations (new [] { store1, store2 }.ToList (), 1, 3);

			// ASSERT
			Assert.AreEqual (migrations.Count, 3);
			Assert.AreNotSame (migrationVersion2ForStore1, migrations.Single (m => m.Version == 2));
			Assert.AreSame (migrationVersion2ForStore2, migrations.Single (m => m.Version == 2));
		}

		private IMigration GetMigrationScript (int version)
		{
			var migration = new Mock<IMigration> ();
			migration.Setup (m => m.Version).Returns (version);

			return migration.Object;
		}

		private IMigrationsStore GetMigrationStore (IMigration[] migrationScripts)
		{
			var store = new Mock<IMigrationsStore> ();
			store.Setup (s => s.GetMigrations (It.IsAny<int> (), It.IsAny<int> ())).Returns (migrationScripts.ToList ());

			return store.Object;
		}
	}
}