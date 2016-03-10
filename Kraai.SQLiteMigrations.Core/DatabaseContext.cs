using System;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public class DatabaseContext : IDatabaseContext, IMigrationCommands
	{
		private IList<string> commands = new List<string> ();

		#region IDatabase implementation

		public void ExecuteSql (string sql)
		{
			this.commands.Add (sql);
		}

		public ILiquidTable Table ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IMigrationCommands implementation

		IList<string> IMigrationCommands.Commands { get { return this.commands; } }

		#endregion
	}
}