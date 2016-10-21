using System;
using Foundation;
using System.Runtime.InteropServices;

namespace Foundation.Net
{
	/// <summary>
	/// 报文头部
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
	public struct PacketHeader
	{
		/// <summary>
		/// 长度
		/// </summary>
		public int Length;
		/// <summary>
		/// 编号
		/// </summary>
		public int PacketID;
	}
}

