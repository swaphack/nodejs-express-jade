using System;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Helper;

namespace Game.Layer
{
	/// <summary>
	/// 界面缩放类型
	/// </summary>
	public enum UIScale
	{
		/// <summary>
		/// 固定
		/// </summary>
		Fixed = 0,
		/// <summary>
		/// 固定宽缩放
		/// </summary>
		FixedWidth = 1,
		/// <summary>
		/// 固定高缩放
		/// </summary>
		FixedHeight = 2,
		/// <summary>
		/// 宽高皆按屏幕缩放
		/// </summary>
		Expend = 3,
	}
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
		private Vector2 _DesignSize = new Vector2(1024, 768);
		/// <summary>
		/// 缩放类型
		/// </summary>
		private UIScale _ScaleType;

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

		/// <summary>
		/// 缩放类型
		/// </summary>
		/// <value>The type of the scale.</value>
		public UIScale ScaleType {
			get { 
				return _ScaleType;
			}
			set { 
				_ScaleType = value;
			}
		}

		public UI ()
		{
			_UILayers = new List<UILayer> ();

			s_UserInterface = this;
			_ScaleType = UIScale.FixedHeight;
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
				Log.Warning ("UI Root is Not Found");
				return;
			}

			if (layer.Root == null) {
				Log.Warning ("UI Layer is Not Find");
				return;
			}

			Transform transform = layer.Root.transform;
			if (transform == null) {
				Log.Warning ("UI Transform is Not Find");
				return;
			}

			RectTransform rect = layer.Root.GetComponent<RectTransform> ();
			if (rect == null) {
				Log.Warning ("UI RectTransform is Not Find");
				return;
			}

			transform.SetParent (_UIRoot.transform);
			rect.localPosition = Vector3.zero;
			rect.localScale = Vector3.one;
			layer.Visible = true;
			_UILayers.Add (layer);
		}

		/// <summary>
		/// 设置缩放比
		/// </summary>
		/// <param name="rect">Rect.</param>
		protected void AutoScale(RectTransform rect)
		{
			if (rect == null) {
				return;
			}

			Vector2 screenSize = Utility.GetScreenSize ();
			Vector2 designSize = _DesignSize;

			Vector3 scale = Vector3.one;

			switch (_ScaleType) {
			case UIScale.Fixed:
				break;
			case UIScale.FixedWidth:
				scale.y = scale.x = screenSize.x / designSize.x;
				break;
			case UIScale.FixedHeight:
				scale.x = scale.y = screenSize.y / designSize.y;
				break;
			case UIScale.Expend:
				scale.x = screenSize.x / designSize.x;
				scale.y = screenSize.y / designSize.y;
				break;
			default:
				break;
			}

			rect.localPosition = Vector3.zero;
			//rect.localScale = scale;
			//rect.sizeDelta = designSize;
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
			int count = _UILayers.Count;
			for (int i = 0; i < count; i++) {
				_UILayers[i].Update (dt);
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear()
		{
			int count = _UILayers.Count;
			for (int i = 0; i < count; i++) {
				_UILayers [i].Dispose ();
			}

			_UILayers.Clear ();
		}


		/// <summary>
		/// 显示界面
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Show<T>() where T : ILayer, new() 
		{
			int count = _UILayers.Count;
			for (int i = 0; i < count; i++) {
				if (typeof(T) == _UILayers[i].GetType ()) {
					_UILayers[i].Show ();
					return;
				}
			}

			T t = new T ();
			t.Show ();
		}
	}
}

