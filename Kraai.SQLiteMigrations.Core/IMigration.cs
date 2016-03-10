using System;

namespace Kraai.SQLiteMigrations
{
	public interface IMigration
	{
		int Version { get; }

		void Upgrade (IDatabaseContext db);
		void Downgrade (IDatabaseContext db);
	}
}

