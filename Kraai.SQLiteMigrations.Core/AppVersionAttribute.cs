using System;

namespace Kraai.SQLiteMigrations
{
	public class AppVersionAttribute : Attribute
	{
		public int Version { get; private set; }

		public AppVersionAttribute (int version)
		{
			this.Version = version;
		}
	}
}

