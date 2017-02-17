using System;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Helper;

namespace Game.Layer
{
	/// <summary>
	/// 用户接口
	/// </summary>
	public class UI
	{
		/// <summary>
		/// ui根节点
		/// </summary>
		private GameObject _UIRoot;
		/// <summary>
		/// 界面集合
		/// </summary>
		private List<UILayer> _UILayers;

		/// <summary>
		/// 设计大小
		/// </summary>
		private Vector2 _DesignSize = new Vector2(1280, 720);

		/// <summary>
		/// 静态实例
		/// </summary>
		private static UI s_UserInterface;

		/// <summary>
		/// UI根目录
		/// </summary>
		/// <value>The user interface directory.</value>
		public string UIDirectory {
			get { 
				return "UI";
			}
		}

		/// <summary>
		/// 设计大小
		/// </summary>
		/// <value>The size of the design.</value>
		public Vector2 DesignSize {
			get { 
				return _DesignSize;
			}
		}

		/// <summary>
		/// 设置UI根节点
		/// </summary>
		/// <value>The user interface root.</value>
		public GameObject UIRoot {
			get { 
				return _UIRoot;
			}
			set { 
				_UIRoot = value;
			}
		}

		public UI ()
		{
			_UILayers = new List<UILayer> ();

			s_UserInterface = this;
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		public static UI GetInstance()
		{
			if (s_UserInterface == null) {
				return null;
			}

			return s_UserInterface;
		}

		/// <summary>
		/// 压入界面
		/// </summary>
		/// <param name="layer">Layer.</param>
		public void Push (UILayer layer)
		{
			if (layer == null || layer.Root == null || layer.Root.transform == null) {
				return;
			}

			if (_UIRoot == null) {
				Log.Warning ("UI Root is Not Find");
				return;
			}

			layer.Root.transform.SetParent (_UIRoot.transform);
			layer.Visible = true;

			Vector2 screenSize = Utility.GetScreenSize ();
			Vector2 designSize = DesignSize;

			RectTransform rect = layer.Root.GetComponent<RectTransform> ();
			rect.localScale = new Vector3 (screenSize.x / designSize.x, screenSize.y / designSize.y);
			rect.localPosition = new Vector3 (0,0,0);

			_UILayers.Add (layer);
		}

		/// <summary>
		/// 弹出当前界面
		/// </summary>
		public void Pop()
		{
			if (_UILayers.Count == 0) {
				return;
			}
			int index = _UILayers.Count - 1;
			UILayer layer = _UILayers [index];
			layer.Visible = false;

			_UILayers.RemoveAt (index);
		}

		/// <summary>
		/// 弹出界面
		/// </summary>
		public void Pop(UILayer layer)
		{
			if (layer == null) {
				return;
			}

			layer.Visible = false;
			_UILayers.Remove (layer);
		}

		/// <summary>
		/// 替换当前界面
		/// </summary>
		/// <param name="layer">Layer.</param>
		public void Replace(UILayer layer)
		{
			if (layer == null) {
				return;
			}

			Pop ();
			Push (layer);
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			foreach (UILayer layer in _UILayers) {
				layer.Update (dt);
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear()
		{
			foreach (UILayer layer in _UILayers) {
				layer.Dispose ();
			}

			_UILayers.Clear ();
		}


		/// <summary>
		/// 显示界面
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Show<T>() where T : ILayer, new() 
		{
			foreach (UILayer layer in _UILayers) {
				if (typeof(T) == layer.GetType ()) {
					layer.Show ();
					return;
				}
			}

			T t = new T ();
			t.Show ();
		}
	}
}

