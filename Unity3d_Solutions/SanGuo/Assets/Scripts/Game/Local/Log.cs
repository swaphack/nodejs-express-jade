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
		/// <param name="obj">Object.</param>
		public static void Write(string obj)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.AppendAllText (_FilePath, obj + "\r\n");
			} else {
				Debug.Log (obj);
			}
		}
	}
}

