using System;

namespace Foundation.Net
{
	/// <summary>
	/// 报文接口
	/// </summary>
	public interface IPacket
	{
		/// <summary>
		/// 报文头部
		/// </summary>
		/// <value>报文头部</value>
		PacketHeader Header { get ; set; }
	}
}
