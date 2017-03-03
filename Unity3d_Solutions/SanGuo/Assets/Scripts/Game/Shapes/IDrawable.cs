using System;
using UnityEngine;

namespace Game.Shapes
{
	public interface IDrawable
	{
		/// <summary>
		/// 显示边界
		/// </summary>
		/// <value><c>true</c> if show line; otherwise, <c>false</c>.</value>
		bool EdgeVisible { get; set; }
		/// <summary>
		/// 边界颜色
		/// </summary>
		/// <value>The color of the edge.</value>
		Color EdgeColor { get; set; }
		/// <summary>
		/// 内部填充
		/// </summary>
		/// <value><c>true</c> if this instance is fill; otherwise, <c>false</c>.</value>
		bool IsFill { get; set; }
		/// <summary>
		/// 填充颜色
		/// </summary>
		/// <value>The color of the fill.</value>
		Color FillColor { get; set; }
		/// <summary>
		/// 绘制
		/// </summary>
		void Draw();
	}
}

