#define USE_ASSET_BUNDLE

using UnityEngine;
using System.Collections.Generic;
using Data;

namespace Game.Listener
{
	/// <summary>
	/// 轨道
	/// </summary>
	public class TrackListener : MonoBehaviour
	{
		/// <summary>
		/// 原型
		/// </summary>
		private Dictionary<int, TrackData.PrefabItem> _PrefabItems;
		/// <summary>
		/// 赛道元素
		/// </summary>
		private List<TrackData.ElementItem> _ElementItems;
		/// <summary>
		/// 资源包名称
		/// </summary>
		private string _AssetBundle;
		/// <summary>
		/// 已加载数
		/// </summary>
		private int _LoadedCount;
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
			TrackData data = new TrackData (TrackConfigPath);
			if (!data.Load ()) {
				return;
			}

			_PrefabItems = data.PrefabItems;
			_ElementItems = data.ElementItems;
			_AssetBundle = data.AssetBundlePath;


#if USE_ASSET_BUNDLE
			if (!_bLoadedAssetBundle) {
				FileDataHelp.LoadAssetBundle (_AssetBundle, (bool status) => {
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
			LoadElement ();
		}

		/// <summary>
		/// 加载赛道元素
		/// </summary>
		private void LoadElement()
		{
			if (_ElementItems == null) {
				Log.Warning ("Not Exists Element Item");
				return;
			}
			if (_LoadedCount >= _ElementItems.Count) {
				return;
			}

			TrackData.ElementItem elementItem = _ElementItems [_LoadedCount];
			if (!_PrefabItems.ContainsKey (elementItem.PrefabID)) {
				_LoadedCount++;
				return;
			}
			TrackData.PrefabItem prefabItem = _PrefabItems [elementItem.PrefabID];
#if USE_ASSET_BUNDLE
			FileDataHelp.CreatePrefabFromAssetBundle (_AssetBundle, prefabItem.Path, (GameObject gameObj)=>{
				if (gameObj == null) {
					return;
				}

				GameObject instance = GameObject.Instantiate<GameObject> (gameObj);
				if (instance == null) {
					return;
				}
				instance.transform.name = elementItem.Name;
				instance.transform.SetParent (this.transform);
				instance.transform.localPosition = elementItem.Position;
				instance.transform.localRotation.Set (0, 0, 0, 1);
			});
#else
			FileDataHelp.CreatePrefabFromAsset (prefabItem.Path, (GameObject gameObj)=>{
				if (gameObj == null) {
					return;
				}

				GameObject instance = GameObject.Instantiate<GameObject> (gameObj);
				if (instance == null) {
					return;
				}
				instance.transform.name = elementItem.Name;
				instance.transform.SetParent (this.transform);
				instance.transform.localPosition = elementItem.Position;
				instance.transform.localRotation.Set (0, 0, 0, 1);
			});
#endif
			_LoadedCount++;
		}
	}
}