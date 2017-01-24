using System;
using UnityEngine;

namespace Model.Paper
{
	/// <summary>
	/// 圆阵
	/// </summary>
	public class CircleFormationPaper : FormationPaper
	{
		/// <summary>
		/// 半径
		/// </summary>
		private float _Radius;

		/// <summary>
		/// 半径
		/// </summary>
		public float Radius {
			get { 
				return _Radius;
			}
		}
		
		public CircleFormationPaper ()
		{
			_Radius = 0;
		}
	}
}

