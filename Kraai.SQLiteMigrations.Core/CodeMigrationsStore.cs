using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Kraai.SQLiteMigrations
{
	public class CodeMigrationsStore : IMigrationsStore
	{
		public Assembly ScriptsAssembly { get; private set; }

		public CodeMigrationsStore (Assembly assembly)
		{
			if (assembly == null) {
				throw new ArgumentNullException ("The assembly cannot be null");
			}
			this.ScriptsAssembly = assembly;
		}

		#region IScriptStore implementation

		public IList<IMigration> GetMigrations (int fromVersion, int toVersion)
		{			
			var exports = this.ScriptsAssembly
				.ExportedTypes
				.Select (e => e.GetTypeInfo ());

			var migrations = exports
				.Where (t => (t.ImplementedInterfaces.Any (i => i == typeof(IMigration)) && !t.IsSubclassOf(typeof(Migration)))
					|| t.IsSubclassOf(typeof(Migration)))
				.Where (t => this.IsVersionInRange (t, fromVersion, toVersion))
				.Select (t => (IMigration)Activator.CreateInstance (t.AsType ()));

			return migrations.ToList ();
		}

		#endregion

		private bool IsVersionInRange(TypeInfo migrationType, int from, int to)
		{
			var attribute = migrationType.GetCustomAttribute<AppVersionAttribute> (true);
			if (attribute == null) {
				// When no attribute is found, the migration script is not valid and is not collected.
				// TODO: Output to the logs...
				return false;
			}

			int version = attribute.Version;	
			return (version > from && version <= to) || (version > to && version <= from);
			//return (version >= from && version <= to) || (version >= to && version <= from);
		}
	}
}

