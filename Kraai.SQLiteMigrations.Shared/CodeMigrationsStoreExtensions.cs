using System;
using System.Reflection;

namespace Kraai.SQLiteMigrations
{
	public static class CodeMigrationsStoreExtensions
	{
		public static void UseMigrationsFromExecutingAssembly (this ILiquidSQLite liquidSQLite)
		{
			liquidSQLite.RegisterMigrationStore (new CodeMigrationsStore (Assembly.GetCallingAssembly ()));
		}

		public static void UseMigrationsFromAssembly (this ILiquidSQLite liquidSQLite, Assembly assembly)
		{
			liquidSQLite.RegisterMigrationStore (new CodeMigrationsStore (assembly));
		}
	}
}

