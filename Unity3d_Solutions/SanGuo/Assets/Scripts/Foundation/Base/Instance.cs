using System;

namespace Foundation.Base
{
	public class Instance<T> where T : new()
	{
		private static T s_T = new T ();

		private Instance ()
		{
		}

		public static T GetInstance()
		{
			return s_T;
		}
	}
}

