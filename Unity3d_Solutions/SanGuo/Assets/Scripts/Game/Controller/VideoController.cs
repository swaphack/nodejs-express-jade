using UnityEngine;
using System.Collections;
using System;
using Game.Local;

namespace Game.Controller
{
	public class VideoController : MonoBehaviour
	{
		/// <summary>
		/// 视频纹理
		/// </summary>
		private MovieTexture MovTexture;
		/// <summary>
		/// 音频
		/// </summary>
		private AudioSource AudioSrc;
		/// <summary>
		/// 文件名称
		/// </summary>
		public String FileName;

		// Use this for initialization
		void Start ()
		{
			MovTexture = Resources.Load<MovieTexture> (FileName);
			AudioClip audioClip = Resources.Load<AudioClip> (FileName);

			GetComponent<MeshRenderer> ().material.mainTexture = MovTexture;
			GetComponent<AudioSource> ().clip = audioClip;
			AudioSrc = GetComponent<AudioSource> ();

			MovTexture.loop = true;
			AudioSrc.loop = true;

			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.F1, Game.Platform.KeyPhase.Began, OnControllerVideo);
		}


		void OnDestory()
		{
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.F1, Game.Platform.KeyPhase.Began, OnControllerVideo);
		}

		/// <summary>
		/// 控制视频播放
		/// </summary>
		protected void OnControllerVideo()
		{
			if (IsPlaying) {
				Pause ();
			} else {
				Resume ();
			}
		}

		/// <summary>
		/// 是否正在播放
		/// </summary>
		/// <value><c>true</c> if this instance is playing; otherwise, <c>false</c>.</value>
		public bool IsPlaying {
			get { 
				if (MovTexture != null) {
					return MovTexture.isPlaying;
				}

				if (AudioSrc != null) {
					return MovTexture.isPlaying;
				}

				return false;
			}
		}

		/// <summary>
		/// 播放
		/// </summary>
		public void Play()
		{
			if (MovTexture != null) {
				MovTexture.Play ();
			}

			if (AudioSrc != null) {
				AudioSrc.Play ();
			}
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Pause()
		{
			if (MovTexture != null) {
				MovTexture.Pause();
			}

			if (AudioSrc != null) {
				AudioSrc.Pause ();
			}
		}

		/// <summary>
		/// 继续
		/// </summary>
		public void Resume()
		{
			if (MovTexture != null) {
				MovTexture.Play();
			}

			if (AudioSrc != null) {
				AudioSrc.Play ();
			}
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Stop()
		{
			if (MovTexture != null) {
				MovTexture.Stop();
			}

			if (AudioSrc != null) {
				AudioSrc.Stop ();
			}
		}
	}
}


