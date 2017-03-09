using System;
using Foundation.DataBase;
using Game.Helper;

namespace Data.Config
{
	/// <summary>
	/// 服务器配置
	/// </summary>
	public class RemoteConfig
	{
		/// <summary>
		/// 服务器地址
		/// </summary>
		public string IPAddress;
		/// <summary>
		/// 服务器端口
		/// </summary>
		public int Port;
		/// <summary>
		/// socket 是否可用
		/// </summary>
		public bool IsSocketEnable;
		/// <summary>
		/// 重连尝试次数
		/// </summary>
		public int TryConnectCount;
		/// <summary>
		/// 配置所在路径
		/// </summary>
		public string ConfigPath { 
			get { 
				return XmlFilePath.DataBaseConfigRemote;
			}
		}

		public RemoteConfig()
		{
			TryConnectCount = -1;
		}

		/// <summary>
		/// 加载
		/// </summary>
		public bool Load ()
		{
			IDataTable table = XmlHelp.LoadSimpleXml (ConfigPath);
			if (table == null) {
				return false;
			}

			IDataRecord record = table.At (0);
			IPAddress = record.GetProperty ("IP");
			Port = int.Parse(record.GetProperty ("Port"));

			record = table.At (1);
			IsSocketEnable = record.GetProperty ("enable") == "true";
			TryConnectCount = int.Parse(record.GetProperty ("TryConnectCount"));

			return true;
		}

		/// <summary>
		/// 清空
		/// </summary>
		public bool Clear ()
		{
			IPAddress = "";
			Port = 0;
			IsSocketEnable = false;

			return true;
		}
	}
}