using System;
using Foundation.Net;
using Foundation.Plugin;
using Data;

namespace Game.Module
{
	/// <summary>
	/// 超出尝试连接次数后的处理
	/// </summary>
	public delegate void FinalTryConnectHandler ();
	/// <summary>
	/// 网络中心
	/// </summary>
	public class NetPlugin : IPlugin
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
		/// 网络是否可用
		/// </summary>
		private bool _IsNetEnable;

		/// <summary>
		/// 最大尝试连接次数 
		/// 负数标识不判断，每次断开都会重新连接
		/// </summary>
		public int TryConnectMaxCount {
			get { 
				return _TryConnectMaxCount; 
			}
			set {
				_TryConnectMaxCount = value; 
			}
		}

		/// <summary>
		/// 网络是否可用
		/// </summary>
		/// <value><c>true</c> if this instance is net enable; otherwise, <c>false</c>.</value>
		public bool IsNetEnable {
			get { 
				return _IsNetEnable; 
			} 
			set { 
				_IsNetEnable = value;
			} 
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public NetPlugin ()
		{
			_Client = new Client ();			
			_Client.OnReceive += OnReceiveBuffer;

			_PacketController = new PacketController ();
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
		/// 移除报文派发处理
		/// </summary>
		/// <param name="packetID">Packet I.</param>
		/// <param name="handler">Handler.</param>
		public void RemovePacketDispatcher(int packetID, DispatchPacketHandler handler)
		{
			_PacketController.RemoveDispatcher (packetID, handler);
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
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.Net;
			} 
		}

		/// <summary>
		/// 初始化网络中
		/// </summary>
		public void Init ()
		{
			RemoteConfig config = new RemoteConfig ();
			if (config.Load () == true) {
				IsNetEnable = config.IsSocketEnable;
				TryConnectMaxCount = config.TryConnectCount;

				_Client.SetEndPoint (config.IPAddress, config.Port);
				_Client.OnConnect += OnConnectState;
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void Update (float dt)
		{
			if (!IsNetEnable) {
				return;
			}
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
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_Client.Disconnect ();
			_PacketController.Dispose ();
		}

		/// <summary>
		/// 尝试连接失败回调处理
		/// </summary>
		private void FinalConnectHandler()
		{
			//Log.Info ("Over Connect Server Count");
		}

		/// <summary>
		/// 连接状态回调处理
		/// </summary>
		/// <param name="state">连接状态</param>
		private void OnConnectState(ConnectState state)
		{
			if (state == ConnectState.Disconnected) 
			{
				//Log.Info ("Disconnect Server");
			} 
			else
			{
				//Log.Info ("Connect Server");
			}
		}
	}
}
