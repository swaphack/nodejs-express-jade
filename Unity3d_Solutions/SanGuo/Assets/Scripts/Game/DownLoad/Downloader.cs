using System;
using System.Collections;
using Foundation.Interface;

namespace Game.DownLoad
{
	/// <summary>
	/// 下载回调
	/// </summary>
	public delegate void OnDownloadCallback(DownloadTask task);

	/// <summary>
	/// 下载任务
	/// </summary>
	public delegate void OnRunTaskMethod(DownloadTask task);

	/// <summary>
	/// 下载接口
	/// </summary>
	public class Downloader
	{
		/// <summary>
		/// 下载队列
		/// </summary>
		private DownloadTaskSheet _DownloadSheet;

		/// <summary>
		/// 下载进行中
		/// </summary>
		public OnDownloadCallback OnProgressHandler;
		/// <summary>
		/// 下载成功
		/// </summary>
		public OnDownloadCallback OnSuccessHandler;
		/// <summary>
		/// 下载失败
		/// </summary>
		public OnDownloadCallback OnFailureHandler;
		/// <summary>
		/// 执行下载任务
		/// </summary>
		public OnRunTaskMethod RunTaskHandler;

		/// <summary>
		/// 是否正在运行
		/// </summary>
		private bool _Running;

		/// <summary>
		/// 静态实例
		/// </summary>
		private static Downloader sDownloader;
		/// <summary>
		/// 获取静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		public static Downloader GetInstance()
		{
			if (sDownloader == null) {
				sDownloader = new Downloader ();

			}

			return sDownloader;
		}

		private Downloader ()
		{
			_DownloadSheet = new DownloadTaskSheet ();
		}
			
		/// <summary>
		/// 创建一个下载任务
		/// </summary>
		/// <param name="task">Task.</param>
		public void addTask(string url, string filePath)
		{
			if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(filePath)) {
				return;
			}

			_DownloadSheet.AddTask(new DownloadItem(url, filePath));
		}

		/// <summary>
		/// 开始任务
		/// </summary>
		public void Start()
		{
			_Running = true;
		}

		/// <summary>
		/// 开始任务
		/// </summary>
		public void Stop()
		{
			_Running = false;
			_DownloadSheet.Clear ();
		}

		public void Update()
		{
			if (_Running) {
				if (RunTaskHandler == null) {
					return;
				}

				DownloadTask task = _DownloadSheet.Top ();
				if (task == null) {
					return;
				}

				// 下载完毕
				if (task.IsFinish == true) {
					OnSuccess(task);
					_DownloadSheet.Pop ();
					return;
				}

				// 开始下载
				if (task.IsRunning == false) {
					task.IsRunning = true;
					RunTaskHandler (task);
					return;
				}

				// 下载进行中
				if (task.IsError) {
					// 下载出现错误
					OnFailure (task);
					_DownloadSheet.Pop ();
				} else {
					// 下载中
					OnProgress (task);
				}
			}
		}

		private void OnDownload(DownloadTask task)
		{
			if (task != null && RunTaskHandler != null) {
				RunTaskHandler (task);
			}
		}

		/// <summary>
		/// 下载成功
		/// </summary>
		/// <param name="task">Task.</param>
		private void OnSuccess(DownloadTask task)
		{
			if (task != null && OnSuccessHandler != null) {
				OnSuccessHandler (task);
			}
			
		}

		/// <summary>
		/// 下载失败
		/// </summary>
		/// <param name="task">Task.</param>
		private void OnFailure(DownloadTask task)
		{
			if (task != null && OnFailureHandler != null) {
				OnFailureHandler (task);
			}
		}

		/// <summary>
		/// 下载中
		/// </summary>
		/// <param name="task">Task.</param>
		private void OnProgress(DownloadTask task)
		{
			if (task != null && OnProgressHandler != null) {
				OnProgressHandler (task);
			}
		}
	}
}

