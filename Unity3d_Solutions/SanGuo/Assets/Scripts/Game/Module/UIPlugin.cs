using System;
using Foundation.Plugin;
using Game.Layer;
using UnityEngine;
using View;
using Game.Helper;

namespace Game.Module
{
	/// <summary>
	/// 用户界面插件
	/// </summary>
	public class UIPlugin : IPlugin
	{
		/// <summary>
		/// 用户接口
		/// </summary>
		private UI _UI;

		public UIPlugin ()
		{
			_UI = new UI ();
			_UI.ScaleType = UIScale.FixedHeight;
		}

		/// <summary>
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.UI;
			} 
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			_UI.UIRoot = GameObject.Find ("UI");
			if (_UI.UIRoot != null) {
				new ViewLayer ();
			} else {
				Log.Warning ("Not Exist UI Node, Name : 'UI'");
			}
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			_UI.Update (dt);
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_UI.Clear ();
		}
	}
}

