using System;
using UnityEngine;
using Game;
using Foundation.Net;

namespace Game
{
	/// <summary>
	/// UI层
	/// </summary>
	public class Layer : MonoBehaviour
	{
		/// <summary>
		/// 是否设置文本了
		/// </summary>
		private bool _IsInitText;

		public Layer ()
		{
			_IsInitText = false;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		void Start ()
		{
			InitUI ();
			InitPacket ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		void Update ()
		{
			if (_IsInitText == false) {
				InitText ();
				_IsInitText = true;
			}
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

		public virtual void Show()
		{
			if (GameInstance.GetInstance ().Platform != null) {
				GameInstance.GetInstance ().Platform.AddEscKeyHandler (EscapeKeyHandler);
			}
		}

		/// <summary>
		/// 关闭
		/// </summary>
		public virtual void Close ()
		{
			if (GameInstance.GetInstance ().Platform != null) {
				GameInstance.GetInstance ().Platform.AddEscKeyHandler (EscapeKeyHandler);
			}
		}

		/// <summary>
		/// 返回键处理
		/// </summary>
		public void EscapeKeyHandler()
		{
			OnEscapeHandler ();
		}

		protected virtual void OnEscapeHandler ()
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