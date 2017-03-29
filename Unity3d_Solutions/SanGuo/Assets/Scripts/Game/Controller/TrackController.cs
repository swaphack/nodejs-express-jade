#define USE_ASSET_BUNDLE

using UnityEngine;
using System.Collections.Generic;
using Data.Map;
using Game.Helper;

namespace Game.Controller
{
	/// <summary>
	/// 轨道
	/// </summary>
	public class TrackController : MonoBehaviour
	{
		/// <summary>
		/// 原型
		/// </summary>
		TrackMap _TrackMap;
		/// <summary>
		/// 已加载数
		/// </summary>
		private int _LoadedCount;
		/// <summary>
		/// 已加角色
		/// </summary>
		private int _LoadedRole;
		/// <summary>
		/// 是否已经加载了资源包
		/// </summary>
		private bool _bLoadedAssetBundle;
		/// <summary>
		/// 配置所在路径
		/// </summary>
		public const string TrackConfigPath = XmlFilePath.DataBaseMapTrack;

		// Use this for initialization
		void Start ()
		{
			_TrackMap = new TrackMap (TrackConfigPath);
			if (!_TrackMap.Load ()) {
				return;
			}

#if USE_ASSET_BUNDLE
			if (!_bLoadedAssetBundle) {
				FileDataHelp.LoadAssetBundle (_TrackMap.AssetBundlePath, (bool status) => {
					if (!status) {
						Log.Warning("Not exists assetbundle, path:" + _TrackMap.AssetBundlePath);
						return;
					}
					_bLoadedAssetBundle = true;
				});
			}
#else
			_bLoadedAssetBundle = true;
#endif
		}

		// Update is called once per frame
		void Update ()
		{
			if (!_bLoadedAssetBundle) {
				return;
			}
			LoadLines ();

			LoadRole ();
		}

		/// <summary>
		/// 加载赛道元素
		/// </summary>
		private void LoadLines()
		{
			if (_TrackMap.LineItems == null) {
				Log.Warning ("Not Exists Element Item");
				return;
			}

			if (_LoadedCount >= _TrackMap.LineItems.Count) {
				return;
			}

			TrackMap.LineItem item = _TrackMap.LineItems [_LoadedCount];
			if (!_TrackMap.PrefabItems.ContainsKey (item.PrefabID)) {
				_LoadedCount++;
				return;
			}
			TrackMap.PrefabItem prefabItem = _TrackMap.PrefabItems [item.PrefabID];
#if USE_ASSET_BUNDLE
			FileDataHelp.CreatePrefabFromAssetBundle (_TrackMap.AssetBundlePath, prefabItem.Path, (GameObject gameObj)=>{
				if (gameObj == null) {
					return;
				}

				LoadLineItems(item, gameObj);
			});
#else
			FileDataHelp.CreatePrefabFromAsset (prefabItem.Path, (GameObject gameObj)=>{
				if (gameObj == null) {
					return;
				}

			LoadLineItems(item, gameObj);
			});
#endif

			_LoadedCount++;
		}

		private void LoadLineItems(TrackMap.LineItem item, GameObject gameObj)
		{
			if (gameObj == null) {
				return;
			}

			int count = (int)Vector3.Distance (item.Src, item.Dest);
			Vector3 different = item.Dest - item.Src;
			for (int i = 0; i <= count; i++) {
				GameObject instance = Utility.Clone (gameObj);
				if (instance == null) {
					return;
				}
				instance.transform.SetParent (this.transform);
				instance.transform.localPosition = item.Src + (1.0f * i / count) * different;
				instance.transform.localRotation.Set (0, 0, 0, 1);
			}
		}

		/// <summary>
		/// 加载角色
		/// </summary>
		private void LoadRole()
		{
			if (_LoadedRole >= 1) {
				return;
			}

			TrackMap.PrefabItem prefabItem = _TrackMap.PrefabItems [_TrackMap.Role.PrefabID];
			FileDataHelp.CreatePrefabFromAssetBundle (_TrackMap.AssetBundlePath, prefabItem.Path, (GameObject gameObj)=>{
				if (gameObj == null) {
					return;
				}

				GameObject instance = Utility.Clone (gameObj);
				if (instance == null) {
					return;
				}
				instance.transform.SetParent (this.transform.parent);
				instance.transform.localPosition = _TrackMap.Role.Position;
				instance.transform.localRotation.Set (0, 0, 0, 1);
				instance.AddComponent<AudioListener>();
				instance.AddComponent<MoveController>();
				instance.AddComponent<ThirdPersonController>();
				instance.AddComponent<ViewController>();
			});

			_LoadedRole++;
		}
	}
}