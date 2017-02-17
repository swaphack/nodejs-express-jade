using System;
using UnityEngine;
using Game.Helper;

namespace Game.Layer
{
	/// <summary>
	/// UI界面
	/// </summary>
	public class UILayer : ILayer
	{
		/// <summary>
		/// 文件名称
		/// </summary>
		private string _FileName;
		/// <summary>
		/// 根节点
		/// </summary>
		private GameObject _Root;

		/// <summary>
		/// 文件名称
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName {
			get { 
				return _FileName;
			}
			protected set { 
				_FileName = value;
			}
		}

		/// <summary>
		/// 文件路径
		/// </summary>
		/// <value>The file path.</value>
		public string FilePath {
			get { 
				return UI.GetInstance ().UIDirectory + "/" + FileName;
			}
		}

		/// <summary>
		/// 是否可见
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public bool Visible {
			get { 
				return _Root != null && _Root.activeSelf;
			}
			set { 
				if (_Root != null) {
					_Root.SetActive (value);
				}
			}
		}

		/// <summary>
		/// 根节点
		/// </summary>
		/// <value>The root.</value>
		public GameObject Root {
			get { 
				return _Root;
			}
		}

		public UILayer ()
		{
		}

		/// <summary>
		/// 显示
		/// </summary>
		public void Show() 
		{
			if (_Root == null) {
				InitUI ();
			} else {
				OnEnter ();
			}
		}

		/// <summary>
		/// 关闭
		/// </summary>
		public void Close() 
		{
			if (_Root == null) {
				return;
			}

			OnExit ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			UpdateUI (dt);
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			if (_Root == null) {
				return;
			}

			Utility.Destory (_Root);
			_Root = null;
		}

		/// <summary>
		/// 初始化UI
		/// </summary>
		protected void InitUI()
		{
			FileDataHelp.CreatePrefabFromAsset (FilePath, (GameObject gameObject) => {
				if (gameObject == null) {
					Log.Warning("Not Exist UI File Path " + FilePath);
					return;
				}

				_Root = Utility.Clone(gameObject);
				if (_Root == null) {
					return;
				}
				_Root.name = FileName;

				this.Show();
			});
		}

		/// <summary>
		/// 更新UI
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected virtual void UpdateUI(float dt)
		{
			
		}

		/// <summary>
		/// 进入视野前的处理
		/// </summary>
		protected virtual void OnEnter()
		{
			UI.GetInstance ().Push (this);
		}
		/// <summary>
		/// 离开视野前的处理
		/// </summary>
		protected virtual void OnExit()
		{
			UI.GetInstance ().Pop (this);
		}
		/// <summary>
		/// 查找控件
		/// </summary>
		/// <returns>The transform.</returns>
		/// <param name="fullCanvasPath">Full canvas path.</param>
		protected Transform FindCanvas (string fullCanvasPath)
		{
			if (string.IsNullOrEmpty (fullCanvasPath) == true) {
				return null;
			}

			if (_Root == null) {
				return null;
			}

			string[] canvasTree = fullCanvasPath.Split ('.');

			Transform target = null;

			for (int i = 0; i < canvasTree.Length; i++) {
				if (target == null) {
					Transform parentObject = _Root.transform.Find (canvasTree [i]);
					if (parentObject == null) {
						return null;
					}
					target = parentObject;
				} else if (target.transform != null) {
					target = target.transform.FindChild (canvasTree [i]);
				}

				if (target == null) {
					return null;
				}
			}

			return target;
		}

		/// <summary>
		/// 获取本地文本
		/// </summary>
		/// <returns>文本</returns>
		/// <param name="textId">文本编号</param>
		protected string GetLocalText(int textId)
		{
			return GameInstance.GetInstance ().Text.GetMessage(textId);
		}

		/// <summary>
		/// 查找控件
		/// </summary>
		/// <returns>The canvas.</returns>
		/// <param name="fullCanvasPath">Full canvas path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected T FindCanvas<T> (string fullCanvasPath)
		{
			Transform transform = FindCanvas (fullCanvasPath);
			if (transform == null) {
				return default(T);
			}

			return transform.GetComponent<T> ();
		}
	}
}

