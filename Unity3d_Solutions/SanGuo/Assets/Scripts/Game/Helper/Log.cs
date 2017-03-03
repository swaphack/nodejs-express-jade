using System;
using System.IO;
using UnityEngine;

namespace Game.Helper
{
	/// <summary>
	/// 记录
	/// </summary>
	public class Log
	{
		/// <summary>
		/// 文档名称
		/// 在保存模式下,是保存到本地文档中
		/// </summary>
		public const string FileName = "Logger.txt";

		/// <summary>
		/// 保存的路径
		/// </summary>
		public static string _FilePath = "";
		/// <summary>
		/// 是否写入文件
		/// </summary>
		private static bool _IsWriteToFile = false;
		
		private Log ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public static void Init()
		{
			_FilePath = FilePathHelp.GetWritableFilePath (FileName);
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				_IsWriteToFile = true;
			}
			// 删除旧有数据
			if (_IsWriteToFile) {
				File.Delete (_FilePath);
			} 
		}

		/// <summary>
		/// 写入消息
		/// 在保存模式下,是保存到本地文档中
		/// </summary>
		/// <param name="obj">string.</param>
		public static void Info(string obj)
		{
			if (_IsWriteToFile) {
				File.AppendAllText (_FilePath, "[Info]" + obj + "\r\n");
			} else {
				Debug.Log (obj);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在保存模式下,是保存到本地文档中
		/// </summary>
		/// <param name="obj">string.</param>
		public static void Warning(string obj)
		{
			if (_IsWriteToFile) {
				File.AppendAllText (_FilePath, "[Warning]" + obj + "\r\n");
			} else {
				Debug.LogWarning (obj);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在保存模式下,是保存到本地文档中
		/// </summary>
		/// <param name="exception">Exception.</param>
		public static void Exception(Exception exception)
		{
			if (_IsWriteToFile) {
				File.AppendAllText (_FilePath, "[Exception]" + exception.ToString() + "\r\n");
			} else {
				Debug.LogException (exception);
			}
		}

		/// <summary>
		/// 写入消息
		/// 在保存模式下,是保存到本地文档中
		/// </summary>
		/// <param name="condition">boolean.</param>
		/// <param name="obj">string.</param>
		public static void Assert(bool condition, string obj)
		{
			if (_IsWriteToFile) {
				if (!condition) {
					File.AppendAllText (_FilePath, "[Assert]" + obj + "\r\n");
				}
			} 

			Debug.Assert (condition, obj);
		}

		/// <summary>
		/// 写入消息
		/// 在保存模式下,是保存到本地文档中
		/// </summary>
		/// <param name="obj">string.</param>
		public static void Error(string obj)
		{
			if (_IsWriteToFile) {
				File.AppendAllText (_FilePath, "[Error]" + obj + "\r\n");
			} else {
				Debug.LogError (obj);
			}
		}
	}
}

