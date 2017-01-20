using System;
using System.Collections;


namespace Game
{
	/// <summary>
	/// 下载任务
	/// </summary>
	public class DownloadTask
	{
		/// <summary>
		/// 下载任务
		/// </summary>
		public DownloadItem Item;
		/// <summary>
		/// 是否下载完成
		/// </summary>
		public bool IsFinish;
		/// <summary>
		/// 是否正在下载
		/// </summary>
		public bool IsRunning;
		/// <summary>
		/// 已下载字节数
		/// </summary>
		public int BytesDownloaded;
		/// <summary>
		/// 总字节数
		/// </summary>
		public int Size;
		/// <summary>
		/// 下载进度
		/// </summary>
		public float Progress {
			get { 
				if (Size == 0) {
					return 0;
				}

				return BytesDownloaded / Size;
			}
		}
		/// <summary>
		/// 是否出错
		/// </summary>
		public bool IsError;

		public DownloadTask(DownloadItem item)
		{
			Item = item;
			IsFinish = false;
			IsRunning = false;
			IsError = false;
			BytesDownloaded = 0;
			Size = 0;
		}
	}

	/// <summary>
	/// 下载任务列表
	/// </summary>
	public class DownloadTaskSheet
	{		
		/// <summary>
		/// 下载队列
		/// </summary>
		private Queue _DownloadTasks;

		public DownloadTaskSheet ()
		{
			_DownloadTasks = new Queue ();
		}

		/// <summary>
		/// 添加下载任务
		/// </summary>
		/// <param name="item">DownloadItem.</param>
		public void AddTask(DownloadItem item)
		{
			if (item == null) {
				return;
			}

			DownloadTask task = new DownloadTask (item);
			_DownloadTasks.Enqueue (task);
		}

		/// <summary>
		/// 获取当前要执行的任务
		/// </summary>
		public DownloadTask Top()
		{
			if (_DownloadTasks.Count == 0) {
				return null;
			}

			return (DownloadTask)_DownloadTasks.Peek ();
		}

		/// <summary>
		/// 退出第一个
		/// </summary>
		public void Pop()
		{
			if (_DownloadTasks.Count == 0) {
				return;
			}

			_DownloadTasks.Dequeue ();
		}

		/// <summary>
		/// 清空下载任务
		/// </summary>
		public void Clear()
		{
			_DownloadTasks.Clear ();
		}
	}
}

