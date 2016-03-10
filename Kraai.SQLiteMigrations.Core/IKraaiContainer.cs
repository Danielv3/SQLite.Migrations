using System;

namespace Kraai.SQLiteMigrations
{
	public interface IKraaiContainer
	{
		T GetService<T> () 
			where T : class;
		
		void RegisterService<T,S> () 
			where T : class
			where S : class, T;
		
		void RegisterService<T> (Func<T> serviceFactory)
			where T : class;
	}
}

