using System;
using UnityEngine;

namespace Game.Layer
{
	/// <summary>
	/// 层接口
	/// </summary>
	public interface ILayer : IDisposable
	{
		/// <summary>
		/// 文件名称
		/// </summary>
		/// <value>The name of the file.</value>
		string FileName { get; }
		/// <summary>
		/// 是否可见
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		bool Visible { get; set; }
		/// <summary>
		/// 根节点
		/// </summary>
		/// <value>The root.</value>
		GameObject Root { get; }
		/// <summary>
		/// 显示
		/// </summary>
		void Show();
		/// <summary>
		/// 隐藏
		/// </summary>
		void Close();
		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		void Update(float dt);
	}
}

