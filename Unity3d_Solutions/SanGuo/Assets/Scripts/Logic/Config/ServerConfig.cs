using System;
using Foundation.DataBase;

namespace Logic
{
	/// <summary>
	/// 服务器配置
	/// </summary>
	public class ServerConfig : IConfig
	{
		/// <summary>
		/// 是否已加载
		/// </summary>
		private bool _IsLoad;
		/// <summary>
		/// 配置是否有误
		/// </summary>
		private bool _IsError;
		/// <summary>
		/// 服务器地址
		/// </summary>
		public string IPAddress;
		/// <summary>
		/// 服务器端口
		/// </summary>
		public int Port;

		/// <summary>
		/// 配置文件路径
		/// </summary>
		/// <value>The filepath.</value>
		public string FilePath { get { return "DataBase/Config/Remote.xml"; } }

		/// <summary>
		/// 是否已加载
		/// </summary>
		/// <value><c>true</c> if this instance is load; otherwise, <c>false</c>.</value>
		public bool IsLoad { get { return _IsLoad; } }

		/// <summary>
		/// 配置是否有错误
		/// </summary>
		/// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
		public bool IsError { get { return _IsError; } }

		public ServerConfig ()
		{
		
		}

		/// <summary>
		/// 加载
		/// </summary>
		public bool Load ()
		{
			if (_IsLoad == true) {
				return _IsError;
			}

			_IsLoad = true;

			string tableName = "ServerConfig";

			DataTable table = new DataTable (tableName);
			DataLoadStep loadStep = new DataLoadStep (tableName, FilePath, table);
			loadStep.Load ();

			IDataRecord record = table.At (0);
			IPAddress = record.GetProperty ("IP");
			Port = record.GetProperty<int> ("Port");
			_IsError = false;

			return _IsError;
		}

		/// <summary>
		/// 清空
		/// </summary>
		public bool Clear ()
		{
			IPAddress = "";
			Port = 0;
			_IsLoad = false;
			_IsError = false;

			return true;
		}
	}

}