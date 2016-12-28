﻿using System;
using Foundation.DataBase;
using Game;

namespace Logic
{
	/// <summary>
	/// 服务器配置
	/// </summary>
	public class ServerConfig
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
		public const string ConfigPath = "DataBase/Config/Remote";

		/// <summary>
		/// 加载
		/// </summary>
		public bool Load ()
		{
			DataTable table = XmlHelp.LoadXml (ConfigPath);
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