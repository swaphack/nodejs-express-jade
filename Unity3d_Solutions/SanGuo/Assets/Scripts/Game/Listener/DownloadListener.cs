using UnityEngine;
using System.Collections;

using Game.DownLoad;

namespace Game.Listener
{
	public class DownloadListener : MonoBehaviour
	{
		string url = "https://fengche.co/users/c54220dddab7/tickets/c55219d69a16";

		string filePath = "1234.html";

		// Use this for initialization
		void Start ()
		{
			HttpDownloader.Init ();

			Downloader.GetInstance ().RunTaskHandler = WWWDownloader.RunTask;
			//Downloader.GetInstance ().RunTaskHandler = HttpDownloader.RunTask;

			Downloader.GetInstance ().addTask (url, filePath);
			Downloader.GetInstance ().Start ();
		}

		// Update is called once per frame
		void Update ()
		{
			Downloader.GetInstance ().Update ();
		}
	}

}
