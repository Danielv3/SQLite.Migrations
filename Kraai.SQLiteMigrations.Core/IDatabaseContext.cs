using System;

namespace Kraai.SQLiteMigrations
{
	public interface IDatabaseContext
	{
		void ExecuteSql (string sql);

		ILiquidTable Table ();

	}

	public interface ILiquidTable
	{
		ILiquidTable From<T> ();
		ILiquidTable WithName (string name);
	}


}

