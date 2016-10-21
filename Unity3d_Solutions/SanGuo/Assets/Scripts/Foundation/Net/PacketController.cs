using System;
using System.IO;
using System.Collections.Generic;
using Foundation;

namespace Foundation.Net
{
	/// <summary>
	/// 报文派发处理
	/// </summary>
	public delegate void DispatchPacketHandler(byte[] bytes);

	/// <summary>
	/// 报文控制中心
	/// </summary>
	public class PacketController
	{
		/// <summary>
		/// 缓存数据流
		/// </summary>
		private MemoryStream _Stream;

		/// <summary>
		/// 包派发处理
		/// </summary>
		private Dictionary<int, DispatchPacketHandler> _OnDispatchPacket;

		public PacketController ()
		{
			_Stream = new MemoryStream ();

			_OnDispatchPacket = new Dictionary<int, DispatchPacketHandler> ();
		}

		~PacketController()
		{
			_Stream.Close ();
		}

		/// <summary>
		/// 添加数据到缓存
		/// </summary>
		/// <param name="buffer">数据</param>
		/// <param name="size">长度</param>
		public void AddBuffer(byte[] buffer, int size)
		{
			if (buffer == null || size == 0) 
			{
				return;
			}

			_Stream.Write (buffer, 0, size);

			FliterPacket ();
		} 

		/// <summary>
		/// 添加报文派发处理
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="handler">处理</param>
		public void AddDispatcher(int packetID, DispatchPacketHandler handler)
		{
			if (handler == null) 
			{
				return;
			}
			_OnDispatchPacket[packetID] = handler;
		}

		/// <summary>
		/// 移除报文派发处理
		/// </summary>
		/// <param name="packetID">报文编号</param>
		public void RemoveDispatcher(int packetID)
		{
			if (_OnDispatchPacket.ContainsKey (packetID)) 
			{
				_OnDispatchPacket.Remove (packetID);
			}
		}

		/// <summary>
		/// 派发包
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="bytes">报文内容</param>
		public void DispatchPacket(int packetID, byte[] bytes)
		{
			if (_OnDispatchPacket.ContainsKey (packetID) == false) 
			{
				return;
			}

			_OnDispatchPacket [packetID] (bytes);
		}

		/// <summary>
		/// 过滤包
		/// </summary>
		private void FliterPacket()
		{
			if (_Stream.Length < 2 * sizeof(uint)) 
			{ // 数据长度不够
				return;
			}

			byte[] buffer = _Stream.ToArray ();

			int length = BitConverter.ToInt32 (buffer, 0);
			int packetID = BitConverter.ToInt32 (buffer, sizeof(int));

			if (buffer.Length < length) 
			{ // 报文长度不足
				return;
			}

			byte[] bytes = new byte[length];
			Array.Copy (buffer, bytes, length);

			_Stream.Flush ();
			_Stream.Write (buffer, length, buffer.Length - length);
	
			DispatchPacket (packetID, bytes);
		}
	}
}