using System;

namespace Foundation.Net
{
	/// <summary>
	/// 远程信息
	/// </summary>
	public class EndPoint
	{
		/// <summary>
		/// IP地址
		/// </summary>
		public string IP;
		/// <summary>
		/// 端口
		/// </summary>
		public int Port;

		public EndPoint()
		{
			IP = "";
			Port = 0;
		}
	}
}

