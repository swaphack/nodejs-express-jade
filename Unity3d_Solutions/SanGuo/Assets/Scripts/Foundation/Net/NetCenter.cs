using System;


namespace Foundation.Net
{
	/// <summary>
	/// 超出尝试连接次数后的处理
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
		/// 超出尝试连接次数后的处理
		/// </summary>
		public FinalTryConnectHandler FinalConnectHandler;

		/// <summary>
		/// 最大尝试连接次数 
		/// 负数标识不判断，每次断开都会重新连接
		/// </summary>
		public int TryConnectMaxCount {
			get { return _TryConnectMaxCount; }
			set { _TryConnectMaxCount = value; }
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

			_PacketController.FliterPacket ();
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
		/// 重置尝试连接次数
		/// </summary>
		public void ResetTryConnectCount()
		{
			_TryConnectCount = 0;
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

		/// <summary>
		/// 添加报文派发处理
		/// </summary>
		/// <param name="packetID">Packet I.</param>
		/// <param name="handler">Handler.</param>
		public void AddPacketDispatcher(int packetID, DispatchPacketHandler handler)
		{
			if (handler == null) {
				return;
			}
			_PacketController.AddDispatcher (packetID, handler);
		}

		/// <summary>
		/// 添加报文派发处理
		/// </summary>
		/// <param name="packetID">Packet I.</param>
		public void RemovePacketDispatcher(int packetID)
		{
			_PacketController.RemoveDispatcher (packetID);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="bytes">Bytes.</param>
		public void SendMessage(byte[] bytes)
		{
			if (bytes == null) {
				return;
			}

			_Client.Send (bytes);
		}

		/// <summary>
		/// 添加连接状态改变时的回调
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void AddConnectStateListener(ConnectStateHandler handler)
		{
			if (handler == null) {
				return;
			}

			_Client.OnConnect += handler;
		}
	}
}
