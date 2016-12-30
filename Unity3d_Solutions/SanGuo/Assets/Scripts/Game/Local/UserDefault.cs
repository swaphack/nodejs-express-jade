using System;

namespace Game
{
	/// <summary>
	/// 用户本地配置
	/// </summary>
	public class UserDefault
	{
		/// <summary>
		/// 文档名称
		/// </summary>
		public const string _FilePath = "UserDefault.xml";

		private static UserDefault sUserDefault;

		private UserDefault ()
		{
		}

		public static UserDefault GetInstance() 
		{
			if (sUserDefault == null) {
				sUserDefault = new UserDefault ();
			}			

			return sUserDefault;
		}

		public void set(string key, string value) 
		{
			
		}

		public string get(string key)
		{
			
		}
	}
}

