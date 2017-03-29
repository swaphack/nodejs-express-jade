using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Game.Local
{
	/// <summary>
	/// 文件路径加载
	/// </summary>
	public class FilePathLoader
	{
		public class LoadItem
		{
			/// <summary>
			/// 是否已加载
			/// </summary>
			public bool IsLoaded;
			/// <summary>
			/// 文件路径
			/// </summary>
			public string FilePath;
			/// <summary>
			/// The www.
			/// </summary>
			public WWW www;

			private event OnWWWCallback _WWWEvent;

			public void AddEvent(OnWWWCallback handler)
			{
				if (handler != null) {
					_WWWEvent += handler;
				}
			}

			public void RemoveEvent(OnWWWCallback handler)
			{
				if (handler != null) {
					_WWWEvent -= handler;
				}
			}


			public void HandEvent()
			{
				_WWWEvent (www);
			}
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		private static FilePathLoader sFilePathLoader;

		/// <summary>
		/// 加载项
		/// </summary>
		private Dictionary<string, LoadItem> _LoadItems;

		private FilePathLoader ()
		{
			_LoadItems = new Dictionary<string, LoadItem> ();
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		public static FilePathLoader GetInstance()
		{
			if (sFilePathLoader == null) {
				sFilePathLoader = new FilePathLoader ();
			}

			return sFilePathLoader;
		}

		/// <summary>
		/// 加载资源
		/// </summary>
		/// <param name="filePath">FilePath.</param>
		/// <param name="handler">OnWWWCallback.</param>
		public void Load(string filePath, OnWWWCallback handler) {
			if (handler == null) {
				return;
			}
			if (string.IsNullOrEmpty (filePath)) {
				handler (null);
			}

			LoadItem item;

			// 已有此路径
			if (_LoadItems.ContainsKey (filePath)) {
				item = _LoadItems [filePath];
				if (item.IsLoaded) {
					// 已加载
					handler (item.www);
				} else {
					// 正在加载中
					item.AddEvent (handler);
				}
				return;
			}

			// 创建新任务
			item = new LoadItem ();
			item.FilePath = filePath;
			item.AddEvent (handler);
			_LoadItems [filePath] = item;

			GetWWW (filePath, (WWW www) => {
				item.IsLoaded = true;
				item.www = www;

				item.HandEvent();
			});
		}

		/// <summary>
		/// 清空所有项
		/// </summary>
		public void Clear()
		{
			foreach (KeyValuePair<string, LoadItem> item in _LoadItems) {
				if (item.Value.www != null) {
					item.Value.www.Dispose ();
				}
			}
			_LoadItems.Clear ();
		}


		/// <summary>
		/// 通过WWW获取资源
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="handler">Handler.</param>
		private void GetWWW(string url, OnWWWCallback handler)
		{
			if (handler == null) {
				return;
			}
			if (string.IsNullOrEmpty (url)) {
				handler (null);
			}

			Utility.RunCoroutine (CheckExistsWWW (url, handler));
		}

		/// <summary>
		/// WWW获取资源的函数
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="handler">Handler.</param>
		private IEnumerator CheckExistsWWW(string filepath, OnWWWCallback handler)
		{
			WWW www = new WWW (FilePathHelp.WWWMark + filepath);
			yield return www;
			if (www.isDone) {
				if (string.IsNullOrEmpty (www.error)) {
					handler (www);
				} else {
					handler (null);
				}
			}
		}
	}
}

