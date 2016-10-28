using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Foundation
{
	/// <summary>
	/// 转换辅助工具
	/// </summary>
	public class ConvertHelp
	{
		private ConvertHelp ()
		{
		}

		/// <summary>
		/// 结构体转二进制数组
		/// </summary>
		/// <returns>二进制数组</returns>
		/// <param name="structure">结构体</param>
		public static byte[] StructToBytes (Object structure)
		{
			Int32 size = Marshal.SizeOf (structure);
			IntPtr buffer = Marshal.AllocHGlobal (size);
			try {
				Marshal.StructureToPtr (structure, buffer, false);
				byte[] bytes = new byte[size];
				Marshal.Copy (buffer, bytes, 0, size);
				return bytes;
			} finally {
				Marshal.FreeHGlobal (buffer);
			}
		}

		/// <summary>
		/// 二进制数组转结构体
		/// </summary>
		/// <returns>结构体</returns>
		/// <param name="bytes">二进制数组</param>
		/// <param name="strcutType">结构体</param>
		public static Object BytesToStruct (byte[] bytes, Type strcutType)
		{
			Int32 size = Marshal.SizeOf (strcutType);
			IntPtr buffer = Marshal.AllocHGlobal (size);
			try {
				Marshal.Copy (bytes, 0, buffer, size);
				return Marshal.PtrToStructure (buffer, strcutType);
			} finally {
				Marshal.FreeHGlobal (buffer);
			}
		}

		/// <summary>
		/// 二进制数组转结构体
		/// </summary>
		/// <returns>结构体</returns>
		/// <param name="bytes">二进制数组</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T BytesToStruct<T> (byte[] bytes)
		{
			T t = (T)BytesToStruct (bytes, typeof(T));
			return t;
		}

		/// <summary>
		/// 将字符串转成网络字节数组
		/// </summary>
		/// <returns>网络字节数组</returns>
		/// <param name="value">字符串</param>
		/// <param name="size">数组长度</param>
		public static byte[] GetByteText (string value, int size)
		{
			byte[] data = new byte[size];
			byte[] bytes = Encoding.UTF8.GetBytes (value);
			Array.Copy (bytes, data, bytes.Length);
			return data;
		}

		/// <summary>
		/// 将字符串转成字符数组
		/// </summary>
		/// <returns>网络字节数组</returns>
		/// <param name="value">字符串</param>
		/// <param name="size">数组长度</param>
		public static char[] GetCharText (string value, int size)
		{
			char[] data = new char[size];
			byte[] bytes = Encoding.UTF8.GetBytes (value);
			Array.Copy (bytes, data, bytes.Length);
			return data;
		}

		/// <summary>
		/// 将字节数组转成字符串
		/// </summary>
		/// <returns>字符串</returns>
		/// <param name="bytes">字节数组</param>
		public static string GetStringText (byte[] bytes)
		{
			string data = Encoding.UTF8.GetString (bytes);
			return data;
		}
	}
}

