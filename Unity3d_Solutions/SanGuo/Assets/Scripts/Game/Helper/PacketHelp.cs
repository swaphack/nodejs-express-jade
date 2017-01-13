using System;
using UnityEngine;
using Game;
using Foundation;
using Foundation.Net;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;

namespace Game
{
	/// <summary>
	/// UI网络部分
	/// </summary>
	public class PacketHelp
	{
		private PacketHelp()
		{
		}
		/// <summary>
		/// 注册报文解析
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="handler">解析处理</param>
		public static void RegisterPacketHandler (PacketID packetID, DispatchPacketHandler handler)
		{
			GameInstance.GetInstance ().Net.AddPacketDispatcher ((int)packetID, handler);
		}

		/// <summary>
		/// 注销报文解析
		/// </summary>
		/// <param name="packetID">报文编号</param>
		/// <param name="handler">解析处理</param>
		public static void UnregisterPacketHandler (PacketID packetID, DispatchPacketHandler handler)
		{
			GameInstance.GetInstance ().Net.RemovePacketDispatcher ((int)packetID, handler);
		}

		/// <summary>
		/// 发送报文消息
		/// </summary>
		/// <param name="header">报文消息</param>
		public static void Send (IPacket packet)
		{
			if (packet == null) {
				return;
			}

			/*
			PacketHeader header;
			header = packet.Header;
			header.Length = Marshal.SizeOf (packet);
			packet.Header = header;
			*/
			packet.Init ();

			byte[] bytes = ConvertHelp.StructToBytes (packet);
			if (bytes == null || bytes.Length == 0) {
				return;
			}

			Send (bytes);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="msg">消息</param>
		public static void Send (string msg)
		{
			if (msg == null || msg.Length == 0) {
				return;
			}

			byte[] bytes = Encoding.UTF8.GetBytes (msg);

			Send (bytes);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="data">消息</param>
		public static void Send (byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0) {
				return;
			}

			GameInstance.GetInstance ().Net.SendMessage (bytes);
		}

		/// <summary>
		/// 将字符串转成网络字节数组
		/// </summary>
		/// <returns>网络字节数组</returns>
		/// <param name="value">字符串</param>
		/// <param name="size">数组长度</param>
		public static byte[] GetByteText (string value, int size = 20)
		{
			return ConvertHelp.GetByteText (value, size);
		}

		/// <summary>
		/// 将字符串转成字符数组
		/// </summary>
		/// <returns>网络字节数组</returns>
		/// <param name="value">字符串</param>
		/// <param name="size">数组长度</param>
		public static char[] GetCharText (string value, int size = 20)
		{
			return ConvertHelp.GetCharText (value, size);
		}

		/// <summary>
		/// 将字节数组转成字符串
		/// </summary>
		/// <returns>字符串</returns>
		/// <param name="bytes">字节数组</param>
		public static string GetStringText (byte[] bytes)
		{
			return ConvertHelp.GetStringText (bytes);
		}

		/// <summary>
		/// 将字节数组转成字符串
		/// </summary>
		/// <returns>字符串</returns>
		/// <param name="bytes">字节数组</param>
		public static string GetStringText (char[] bytes)
		{
			return new string (bytes);
		}

		/// <summary>
		/// 获取发送包
		/// </summary>
		/// <returns>The request packet.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T GetRequestPacket<T> () where T : IPacket
		{
			T t = Protocols.GetPacket<T> ();
			return t;
		}

		/// <summary>
		/// 获取接受包
		/// </summary>
		/// <returns>结构体</returns>
		/// <param name="bytes">二进制数组</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T GetResponsePacket<T> (byte[] bytes)
		{
			return ConvertHelp.BytesToStruct<T> (bytes);
		}
	}
}