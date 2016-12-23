using System;
using System.IO;
using UnityEngine;

namespace Game
{
	public class Log
	{
		private static string _FilePath = "Logger.txt";
		
		public Log ()
		{
		}

		public static void Init()
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) {
				File.Delete (_FilePath);
			} 
		}

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

