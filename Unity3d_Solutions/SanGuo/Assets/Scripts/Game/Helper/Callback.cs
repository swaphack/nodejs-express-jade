using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 参数为www的回调
	/// </summary>
	public delegate void OnWWWCallback(WWW www);
	/// <summary>
	///  参数为GameObject的回调
	/// </summary>
	public delegate void OnGameObjectCallback(GameObject value);
	/// <summary>
	/// 参数为string的回调
	/// </summary>
	public delegate void OnStringCallback(string value);
	/// <summary>
	/// 参数为bool的回调
	/// </summary>
	public delegate void OnBooleanCallback(bool value);
}

