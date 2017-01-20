using System;
using UnityEngine;

namespace Model.Paper
{
	/// <summary>
	/// 方阵
	/// </summary>
	public class SquareFormationPaper : FormationPaper
	{
		/// <summary>
		/// 长
		/// </summary>
		private float _Length;
		/// <summary>
		/// 宽
		/// </summary>
		private float _Width;
		/// <summary>
		/// 长
		/// </summary>
		public float Length {
			get { 
				return _Length;
			}
		}
		/// <summary>
		/// 宽
		/// </summary>
		public float Width {
			get { 
				return _Width;
			}
		}

		public SquareFormationPaper ()
		{
		}
	}
}

