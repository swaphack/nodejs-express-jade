using System.Collections;
using System.Net.Sockets;
using System;

namespace Foundation.Net
{
	/// <summary>
	/// 连接状态
	/// </summary>
	public enum ConnectState 
	{
		/// <summary>
		/// 连接中
		/// </summary>
		Connected,
		/// <summary>
		/// 断开连接
		/// </summary>
		Disconnected,
	}

	/// <summary>
	/// 断开连接处理
	/// </summary>
	public delegate void ConnectStateHandler(ConnectState state);
	/// <summary>
	/// 接受数据处理
	/// </summary>
	public delegate void ReceiveHandler(byte[] data, int size);

	/// <summary>
	/// 客户端
	/// </summary>
	public class Client 
	{
		internal class ReceiveObject
		{
			public NetworkStream Stream;
		}
		/// <summary>
		/// 每次读取数据长度
		/// </summary>
		public const int ReadBufferSize = 1024;

		/// <summary>
		/// 客户接收端
		/// </summary>
		private TcpClient _TcpClient;
		/// <summary>
		/// 接收缓存
		/// </summary>
		private byte[] _ReceiveBuffer;
		/// <summary>
		/// 远程信息
		/// </summary>
		private Remote _Remote;

		public Client()
		{
			_TcpClient = new TcpClient ();
			_TcpClient.ReceiveTimeout = 20;

			_ReceiveBuffer = new byte[ReadBufferSize];

			_Remote = new Remote ();
		}

		/// <summary>
		/// 断开事件
		/// </summary>
		public event ConnectStateHandler OnConnect;
		/// <summary>
		/// 收到数据
		/// </summary>
		public event ReceiveHandler OnReceive;

		/// <summary>
		/// 连接服务器
		/// </summary>
		/// <param name="ip">地址</param>
		/// <param name="port">端口</param>
		public bool Connect(string ip, int port)
		{
			_Remote.IP = ip;
			_Remote.Port = port;

			return Reconnect ();
		}

		/// <summary>
		/// 重连
		/// </summary>
		public bool Reconnect()
		{
			if (String.IsNullOrEmpty (_Remote.IP) == true || _Remote.Port <= 0) 
			{
				return false;
			}

			_TcpClient.BeginConnect (_Remote.IP, _Remote.Port, (IAsyncResult ar)=>{
				if (ar.IsCompleted)
				{
					if (this.IsConnected == true)
					{
						OnConnect (ConnectState.Connected);
					}
					else
					{
						OnConnect (ConnectState.Disconnected);
					}
				}
			}, this);

			return true;
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="buffer">消息数据</param>
		public void Send (byte[] data)
		{
			if (data == null) 
			{
				return;
			}
			if (IsConnected == false) 
			{
				return;
			}
			_TcpClient.GetStream ().BeginWrite (data, 0, data.Length, null, null);
		}

		/// <summary>
		/// 定时更新，用于数据接收处理
		/// </summary>
		public void Update()
		{
			if (IsConnected == false) 
			{
				// 接收到数据
				OnConnect(ConnectState.Disconnected);
				return;
			}

			Array.Clear(_ReceiveBuffer, 0, _ReceiveBuffer.Length);

			try
			{
				ReceiveObject obj = new ReceiveObject();
				obj.Stream = _TcpClient.GetStream();
				_TcpClient.GetStream().BeginRead(_ReceiveBuffer, 0, ReadBufferSize, (IAsyncResult ar)=>{
					if (ar.IsCompleted)
					{
						ReceiveObject receiveObj = ar.AsyncState as ReceiveObject;
						int size = receiveObj.Stream.EndRead(ar);
						if (size > 0)
						{
							// 接收到数据
							OnReceive(_ReceiveBuffer, size);
						}	
					}
				}, obj);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// 断开连接
		/// </summary>
		public void Disconnect()
		{
			_TcpClient.EndConnect (null);
			_TcpClient.Close ();

			OnConnect (ConnectState.Disconnected);
		}

		/// <summary>
		/// 是否连接
		/// </summary>
		/// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
		public bool IsConnected 
		{
			get {  return _TcpClient.Connected; }			
		}
	}	
}
