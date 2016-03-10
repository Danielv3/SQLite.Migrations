using System;
using TinyIoC;

namespace Kraai.SQLiteMigrations
{
	public class DefaultContainer : IKraaiContainer
	{
		private TinyIoCContainer container;

		public DefaultContainer ()
		{
			this.container = new TinyIoCContainer ();
		}

		#region IKraaiContainer implementation

		public T GetService<T> () 
			where T : class
		{
			T service;
			return (this.container.TryResolve<T> (out service) ? service : null);
		}

		public void RegisterService<T, S> () 
			where T : class
			where S : class, T
		{
			this.container.Register<T, S> ();
		}

		public void RegisterService<T> (Func<T> serviceFactory) 
			where T : class
		{
			this.container.Register<T> ((c,p) => serviceFactory.Invoke() );
		}

		#endregion
	}
}

