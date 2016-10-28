using System;


namespace Foundation.Net
{
	/// <summary>
	/// 最后尝试连接处理
	/// </summary>
	public delegate void FinalTryConnectHandler ();
	/// <summary>
	/// 网络中心
	/// </summary>
	public class NetCenter
	{
		/// <summary>
		/// 客户端
		/// </summary>
		private Client _Client;
		/// <summary>
		/// 报文控制中心
		/// </summary>
		private PacketController _PacketController;
		/// <summary>
		/// 最大尝试连接次数
		/// </summary>
		private int _TryConnectMaxCount = -1;
		/// <summary>
		/// 尝试连接次数
		/// </summary>
		private int _TryConnectCount;
		/// <summary>
		/// 最后尝试连接处理
		/// </summary>
		public FinalTryConnectHandler FinalConnectHandler;

		/// <summary> 
		/// 报文控制中心
		/// </summary>
		public PacketController Packet {
			get { 
				return _PacketController; 
			}
		}

		/// <summary>
		/// 最大尝试连接次数 
		/// 负数标识不判断，每次断开都会重新连接
		/// </summary>
		public int TryConnectMaxCount {
			get { return _TryConnectMaxCount; }
			set { _TryConnectMaxCount = value; }
		}

		/// <summary>
		/// 客户端
		/// </summary>
		public Client Client {
			get { return _Client; }
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public NetCenter ()
		{
			_Client = new Client ();			
			_Client.OnReceive += OnReceiveBuffer;

			_PacketController = new PacketController ();
		}

		/// <summary>
		/// 初始化网络中
		/// </summary>
		/// <param name="ip">服务器地址</param>
		/// <param name="port">服务器端口</param>
		public void Init (string ip, int port)
		{
			_Client.Connect (ip, port);
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void Update ()
		{
			if (IsOverTryConnectCount == true) {
				FinalConnectHandler ();
				return;
			}
			if (_Client.IsConnected == false) {
				_TryConnectCount++;
				_Client.Reconnect ();
			} else {
				_Client.Update ();
			}
			 
		}

		/// <summary>
		/// 是否超出尝试连接次数
		/// </summary>
		/// <value><c>true</c> 尝试次数用完 <c>false</c>.</value>
		public bool IsOverTryConnectCount { 
			get { 
				if (_TryConnectMaxCount < 0) {
					return false;
				}
				return _TryConnectCount > _TryConnectMaxCount; 
			}
		}

		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="data">数据</param>
		/// <param name="size">长度</param>
		private void OnReceiveBuffer (byte[] data, int size)
		{
			_PacketController.AddBuffer (data, size);
		}
	}
}
