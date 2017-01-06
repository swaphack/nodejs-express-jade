using System;
using System.IO;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 记录
	/// </summary>
	public class Log
	{
		/// <summary>
		/// 文档名称
		/// 在WindowsPlayer模式下,是保存到本地文档中
		/// </summary>
		private const string _FilePath = "Logger.txt";
		
		private Log ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public static void Init()
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.Delete (_FilePath);
			} 
		}

		/// <summary>
		/// 写入消息
		/// 在WindowsPlayer模式下,是保存到本地文档中
		/// </summary>
		/// <param name="obj">string.</param>
		public static void Info(string obj)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.AppendAllText (_FilePath, "[Info]" + obj + "\r\n");
			} else {
				Debug.Log (obj);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在WindowsPlayer模式下,是保存到本地文档中
		/// </summary>
		/// <param name="obj">string.</param>
		public static void Warning(string obj)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.AppendAllText (_FilePath, "[Warning]" + obj + "\r\n");
			} else {
				Debug.LogWarning (obj);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在WindowsPlayer模式下,是保存到本地文档中
		/// </summary>
		/// <param name="exception">Exception.</param>
		public static void Exception(Exception exception)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.AppendAllText (_FilePath, "[Exception]" + exception.ToString() + "\r\n");
			} else {
				Debug.LogException (exception);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在WindowsPlayer模式下,是保存到本地文档中
		/// </summary>
		/// <param name="condition">boolean.</param>
		/// <param name="obj">string.</param>
		public static void Assert(bool condition, string obj)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				if (!condition) {
					File.AppendAllText (_FilePath, "[Exception]" + obj + "\r\n");
				}
			} else {
				Debug.Assert (condition, obj);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在WindowsPlayer模式下,是保存到本地文档中
		/// </summary>
		/// <param name="obj">string.</param>
		public static void Error(string obj)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.AppendAllText (_FilePath, "[Error]" + obj + "\r\n");
			} else {
				Debug.LogError (obj);
			}
		}
	}
}

