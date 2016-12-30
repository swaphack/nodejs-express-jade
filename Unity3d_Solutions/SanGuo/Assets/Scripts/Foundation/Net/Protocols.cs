using System;
using Foundation.Net;
using System.Runtime.InteropServices;

namespace Foundation.Net
{
	/// <summary>
	/// 协议
	/// </summary>
	public class Protocols
	{
		private Protocols ()
		{
		}

		/// <summary>
		/// 获取指定包
		/// </summary>
		/// <returns>The packet.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T GetPacket<T> () where T : IPacket
		{
			PacketHeader header;
			header.PacketID = 0;
			header.Length = 0;

			T t = default(T);
			t.Header = header;
			
			return t;
		}
	}

}