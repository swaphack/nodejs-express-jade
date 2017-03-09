using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Helper
{
	/// <summary>
	/// 字符串工具
	/// </summary>
	public class StringHelp
	{
		private StringHelp ()
		{
		}


		/// <summary>
		/// 将字符串转为Vector3
		/// 格式1,1,1
		/// </summary>
		/// <returns>The to vector3.</returns>
		/// <param name="value">Value.</param>
		public static Vector3 ConvertToVector3(string value)
		{
			if (string.IsNullOrEmpty (value)) {
				return Vector3.zero;
			}

			string[] parameters = value.Split (',');
			if (parameters == null || parameters.Length != 3) {
				return Vector3.zero;
			}

			float x = 0;
			float y = 0;
			float z = 0;

			if (!float.TryParse(parameters[0], out x)) {
				return Vector3.zero;
			}

			if (!float.TryParse(parameters[1], out y)) {
				return Vector3.zero;
			}

			if (!float.TryParse(parameters[2], out z)) {
				return Vector3.zero;
			}

			return new Vector3 (x, y, z);
		}

		/// <summary>
		/// 将字符串转成整形数组
		/// 格式1,1,1
		/// </summary>
		/// <returns>The to array.</returns>
		/// <param name="value">Value.</param>
		/// <typeparam name="">The 1st type parameter.</typeparam>
		public static List<int> ConvertToIntArray(string value)
		{
			if (string.IsNullOrEmpty (value)) {
				return null;
			}

			string[] parameters = value.Split (',');
			if (parameters == null || parameters.Length == 0) {
				return null;
			}

			List<int> result = new List<int> ();
			int x = 0;
			int length = parameters.Length;
			for (int i = 0; i < length; i++) {
				if (int.TryParse (parameters[i], out x)) {
					result.Add (x);
				} else {
					Log.Warning ("Convert string to array Error, Not ValueType!");
				}
			}

			return result;
		}
	}
}

