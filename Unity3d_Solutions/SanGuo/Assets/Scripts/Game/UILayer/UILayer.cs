using System;
using UnityEngine;
using Game;
using Foundation.Net;

namespace Game
{
	/// <summary>
	/// UI层
	/// </summary>
	public class UILayer : MonoBehaviour
	{
		/// <summary>
		/// 网络
		/// </summary>
		private static UINet _Net;

		/// <summary>
		/// 网络
		/// </summary>
		public UINet Net {
			get { 
				if (_Net == null) {
					_Net = new UINet ();
				}
				return _Net; 
			}
		}

		public UILayer ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		void Start ()
		{
			InitUI ();
			InitText ();
			InitPacket ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		void Update ()
		{
		}

		/// <summary>
		/// 重新加载
		/// </summary>
		public virtual void Reload ()
		{
		
		}

		/// <summary>
		/// 初始化UI
		/// </summary>
		protected virtual void InitUI ()
		{
		}

		/// <summary>
		/// 初始化文本
		/// </summary>
		protected virtual void InitText ()
		{
		
		}

		/// <summary>
		/// 初始化报文监听
		/// </summary>
		protected virtual void InitPacket ()
		{

		}

		/// <summary>
		/// 关闭
		/// </summary>
		protected virtual void Close ()
		{
		}

		/// <summary>
		/// 获取本地文本
		/// </summary>
		/// <returns>文本</returns>
		/// <param name="textId">文本编号</param>
		public string GetLocalText(int textId)
		{
			return GameInstance.GetInstance ().Text.GetMessage(textId);
		}

		/// <summary>
		/// 查找控件
		/// </summary>
		/// <returns>The transform.</returns>
		/// <param name="fullCanvasPath">Full canvas path.</param>
		public Transform FindCanvas (string fullCanvasPath)
		{
			if (string.IsNullOrEmpty (fullCanvasPath) == true) {
				return null;
			}

			string[] canvasTree = fullCanvasPath.Split ('.');

			Transform parent = null;

			for (int i = 0; i < canvasTree.Length; i++) {
				if (parent == null) {
					GameObject parentObject = GameObject.Find (canvasTree [i]);
					if (parentObject == null) {
						return null;
					}
					parent = parentObject.transform;
				} else {
					parent = parent.transform.FindChild (canvasTree [i]);
				}

				if (parent == null) {
					return null;
				}
			}

			return parent;
		}

		/// <summary>
		/// 查找控件
		/// </summary>
		/// <returns>The canvas.</returns>
		/// <param name="fullCanvasPath">Full canvas path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T FindCanvas<T> (string fullCanvasPath)
		{
			Transform transform = FindCanvas (fullCanvasPath);
			if (transform == null) {
				return default(T);
			}

			return transform.GetComponent<T> ();
		}
	}
}