using System;
using System.Reflection;

namespace Kraai.SQLiteMigrations
{
	public abstract class Migration : IMigration
	{
		public int Version 
		{ 
			get { return this.GetType ().GetTypeInfo ().GetCustomAttribute<AppVersionAttribute>(true).Version; }
		}

		public void Upgrade (IDatabaseContext db)
		{
			this.OnUpgrade (db);
		}

		public void Downgrade (IDatabaseContext db)
		{
			this.OnDowngrade (db);
		}

		protected abstract void OnUpgrade (IDatabaseContext db);
		protected abstract void OnDowngrade (IDatabaseContext db);
	}
}

