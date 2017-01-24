using System;
using Foundation.DataBase;
using Game;
using Data;

namespace Control
{
	/// <summary>
	/// 服务器配置
	/// </summary>
	public class ServerConfig : IConfig
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
		/// 配置所在路径
		/// </summary>
		public string ConfigPath { 
			get { 
				return XmlFilePath.DataBaseConfigRemote;
			}
		}

		/// <summary>
		/// 加载
		/// </summary>
		public bool Load ()
		{
			DataTable table = XmlHelp.LoadSimpleXml (ConfigPath);
			if (table == null) {
				return false;
			}

			IDataRecord record = table.At (0);
			IPAddress = record.GetProperty ("IP");
			Port = int.Parse(record.GetProperty ("Port"));

			record = table.At (1);
			IsSocketEnable = record.GetProperty ("enable") == "true";

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