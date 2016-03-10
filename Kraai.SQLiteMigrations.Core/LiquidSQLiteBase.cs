using System;
using System.Linq;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public abstract class LiquidSQLiteBase : ILiquidSQLite
	{
		protected string DatabasePath { get; private set; }
		protected IKraaiContainer Container { get; private set; }

		protected List<IMigrationsStore> Stores { get; private set; }

		protected IVersionManager VersionManager { get { return this.Container.GetService<IVersionManager> (); } }
		protected IMigrationsProvider MigrationsProvider { get { return this.Container.GetService<IMigrationsProvider> (); } }
		protected IMigrationsInvoker MigrationsInvoker { get { return this.Container.GetService<IMigrationsInvoker> (); } }


		public LiquidSQLiteBase (string databasePath, IKraaiContainer container)
		{
			this.DatabasePath = databasePath;
			this.Stores = new List<IMigrationsStore> ();
			this.Container = container;

			this.RegiterDefaultServices ();
		}

		// TODO: Get results report
		public void MigrateDatabase()
		{
			if (!this.Stores.Any ()) {
				throw new InvalidOperationException ("There are not migration stores registered");
			}

			// TODO: Get versions from app services...
			int fromVersion = 3; //this.VersionManager.GetPreinstalledVersion ();
			int toVersion = 1; //this.VersionManager.GetInstalledVersion ();

			var migrations = this.MigrationsProvider.GetMigrations (this.Stores, fromVersion, toVersion);

			this.MigrationsInvoker.ExecuteMigrations (this.DatabasePath, migrations, fromVersion, toVersion);

			//this.VersionManager.SynchronizeVersions ();
		}

		public void RegisterMigrationStore(IMigrationsStore store)
		{
			if (this.Stores.Contains (store)) {
				throw new ArgumentException ("The migration store is already registered");
			}
			this.Stores.Add (store);
		}

		public void RegisterService<T>(Func<T> factory) 
			where T : class
		{
			// TODO: Verify how to override the default registered services...
			this.Container.RegisterService<T> (factory);
		}

		protected virtual void RegiterDefaultServices()
		{
			// Register all the default service implementations
			this.Container.RegisterService<IMigrationsProvider, MigrationsProvider> ();
			this.Container.RegisterService<IMigrationsInvoker, MigrationsInvoker> ();
		}
	}
}