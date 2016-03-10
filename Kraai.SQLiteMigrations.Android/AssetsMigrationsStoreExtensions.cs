using System;
using Android.Content;

namespace Kraai.SQLiteMigrations
{
	public static class AssetsMigrationsStoreExtensions
	{
		public static void UseMigrationsFromAssets (this ILiquidSQLite liquidSQLite, string fileName, Context context)
		{
			liquidSQLite.RegisterMigrationStore (new AssetsMigrationsStore (fileName, context));
		}
	}
}

