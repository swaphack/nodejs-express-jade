using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Foundation.Base
{
	/// <summary>
	/// 对象辅助工具
	/// </summary>
	public class ObjectHelp
	{
		private ObjectHelp ()
		{
		}

		/// <summary>
		/// 深度复制
		/// </summary>
		/// <returns>The clone.</returns>
		/// <param name="obj">Object.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T DeepClone<T>(T obj)
		{
			MemoryStream stream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, obj);
			stream.Position = 0;
			T result = (T)formatter.Deserialize (stream);
			stream.Close ();
			stream.Dispose ();

			return result;
		}
	}
}

