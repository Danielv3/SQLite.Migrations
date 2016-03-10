using System;
using System.Linq;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public class MigrationsInvoker : IMigrationsInvoker
	{
		private readonly ISQLiteDatabase database;

		public MigrationsInvoker (ISQLiteDatabase database)
		{
			this.database = database;
		}

		#region IMigrationsInvoker implementation
		public void ExecuteMigrations (string databasePath, IList<IMigration> migrations, int fromVersion, int toVersion)
		{
			try {
				this.database.Open (databasePath);
				this.database.BeginTransaction ();

				// Execute migration scripts in order...
				if (fromVersion < toVersion) {
					foreach (var migration in migrations.OrderBy (m => m.Version)) {

						var context = new DatabaseContext ();
						migration.Upgrade (context);

						foreach (var command in ((IMigrationCommands)context).Commands) {
							this.database.ExecuteSql(command);
						}
					}
				} else if (fromVersion > toVersion) {
					foreach (var migration in migrations.OrderByDescending (m => m.Version)) {
						
						var context = new DatabaseContext ();
						migration.Downgrade (context);

						foreach (var command in ((IMigrationCommands)context).Commands) {
							this.database.ExecuteSql(command);
						}
					}
				}

				this.database.CommitTransaction ();
			}
			catch(Exception exception){
				this.database.RollbackTransaction ();
			}
			finally {
				this.database.Close ();
			}

		}
		#endregion
	}
}

