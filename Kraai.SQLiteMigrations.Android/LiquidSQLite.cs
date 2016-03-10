using System;
using System.Linq;
using System.Collections.Generic;
using Android.Content;

namespace Kraai.SQLiteMigrations
{
	public class LiquidSQLite : LiquidSQLiteBase
	{
		private Context context;

		public LiquidSQLite (string databasePath, Context context) 
			: this(databasePath, context, new DefaultContainer())
		{
		}

		public LiquidSQLite (string databasePath, Context context, IKraaiContainer container)
			: base (databasePath, container)
		{
			this.context = context;
		}

		protected override void RegiterDefaultServices()
		{
			base.RegiterDefaultServices ();

			this.Container.RegisterService<IVersionManager> (() => new VersionManager (this.context));
			this.Container.RegisterService<ISQLiteDatabase, SQLiteDatabase> ();
		}
	}
}

