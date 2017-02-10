using System;
using UnityEngine;

namespace Game.Helper
{
	/// <summary>
	/// 矩阵工具
	/// </summary>
	public class MatrixHelp
	{
		private MatrixHelp ()
		{
		}

		public static Quaternion QuaternionFromMatrix(Matrix4x4 m) 
		{ 
			return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1)); 
		}
	}
}

