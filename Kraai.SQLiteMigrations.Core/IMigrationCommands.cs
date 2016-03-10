using System;
using System.Collections.Generic;

namespace Kraai.SQLiteMigrations
{
	public interface IMigrationCommands
	{
		IList<string> Commands { get; }
	}
}

