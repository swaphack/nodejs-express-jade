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
		/// <param name="packetID">Packet I.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T GetPacket<T> (PacketID packetID) where T : IPacket
		{
			PacketHeader header;
			header.PacketID = (int)packetID;
			header.Length = 0;

			T t = default(T);
			t.Header = header;
			return t;
		}
	}

}