using System;
using System.IO;
using System.Collections.Generic;
using Foundation;
using Foundation.Notify;

namespace Foundation.Net
{
	/// <summary>
	/// 报文派发处理
	/// </summary>
	public delegate void DispatchPacketHandler (byte[] bytes);

	/// <summary>
	/// 报文控制中心
	/// </summary>
	public class PacketController : IDisposable
	{
		/// <summary>
		/// 缓存数据流
		/// </summary>
		private MemoryStream _Stream;

		/// <summary>
		/// 包派发处理
		/// </summary>
		private Notifition<int> _OnPacketNotifition;
		/// <summary>
		/// 推送处理关联表
		/// </summary>
		private Dictionary<DispatchPacketHandler, NotifyHandlerWithParameter> _NotifyHandlerSheet;

		public PacketController ()
		{
			_Stream = new MemoryStream ();

			_OnPacketNotifition = new Notifition<int> ();

			_NotifyHandlerSheet = new Dictionary<DispatchPacketHandler, NotifyHandlerWithParameter> ();
		}

		/// <summary>
		/// 添加数据到缓存
		/// </summary>
		/// <param name="buffer">数据</param>
		/// <param name="size">长度</param>
		public void AddBuffer (byte[] buffer, int size)
		{
			if (buffer == null || size == 0) {
				return;
			}

			_Stream.Write (buffer, 0, size);
		}

		/// <summary>
		/// 添加报文派发处理
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="handler">处理</param>
		public void AddDispatcher (int packetID, DispatchPacketHandler handler)
		{
			if (handler == null) {
				return;
			}

			NotifyHandlerWithParameter notifyHandler = delegate (object parameter) {
				handler ((byte[])parameter);
			};

			_OnPacketNotifition.AddListener (packetID, notifyHandler);

			_NotifyHandlerSheet [handler] = notifyHandler;
		}

		/// <summary>
		/// 移除报文派发处理
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="handler">处理</param>
		public void RemoveDispatcher (int packetID, DispatchPacketHandler handler)
		{
			if (handler == null) {
				return;
			}

			if (_NotifyHandlerSheet.ContainsKey (handler) == false) {
				return;
			}

			_OnPacketNotifition.RemoveListener (packetID, _NotifyHandlerSheet [handler]);

			_NotifyHandlerSheet.Remove (handler);
		}

		/// <summary>
		/// 派发包
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="bytes">报文内容</param>
		public void DispatchPacket (int packetID, byte[] bytes)
		{
			_OnPacketNotifition.Notify (packetID, bytes);
		}

		/// <summary>
		/// 过滤包
		/// </summary>
		public void FliterPacket ()
		{
			if (_Stream.Length < 2 * sizeof(uint)) { // 数据长度不够
				return;
			}

			byte[] buffer = _Stream.ToArray ();

			int length = BitConverter.ToInt32 (buffer, 0);
			int packetID = BitConverter.ToInt32 (buffer, sizeof(int));

			if (buffer.Length < length) { // 报文长度不足
				return;
			}

			byte[] bytes = new byte[length];
			Array.Copy (buffer, bytes, length);

			_Stream.Seek (0, SeekOrigin.Begin);
			_Stream.SetLength (0);
			if (buffer.Length - length > 0) {
				_Stream.Write (buffer, length, buffer.Length - length);
			}

			// _Stream = new MemoryStream ();
			// _Stream.Write (buffer, length, buffer.Length - length);
	
			DispatchPacket (packetID, bytes);
		}

		/// <summary>
		/// Disponse this instance.
		/// </summary>
		public void Dispose()
		{
			_Stream.Close ();
		}
	}
}