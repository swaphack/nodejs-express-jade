using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Paper
{	
	/// <summary>
	/// 阵型说明
	/// </summary>
	public class FormationPaper
	{
		/// <summary>
		/// 单位说明
		/// </summary>
		private List<UnitPaper> _UnitPapers;

		/// <summary>
		/// 拥有单位说明
		/// </summary>
		/// <value>The unit papers.</value>
		public List<UnitPaper> UnitPapers {
			get { 
				return _UnitPapers;
			}
		}

		public FormationPaper ()
		{
			_UnitPapers = new List<UnitPaper>();
		}

		/// <summary>
		/// 添加单位说明
		/// </summary>
		/// <param name="unit">Unit.</param>
		public void AddUnit(UnitPaper unit)
		{
			if (unit == null) {
				return;
			}

			_UnitPapers.Add (unit);
		}
	}
}

