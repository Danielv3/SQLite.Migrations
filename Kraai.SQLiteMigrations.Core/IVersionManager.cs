using System;

namespace Kraai.SQLiteMigrations
{
	public interface IVersionManager
	{
		int GetInstalledVersion();
		int GetPreinstalledVersion();

		void SynchronizeVersions();
	}
}

