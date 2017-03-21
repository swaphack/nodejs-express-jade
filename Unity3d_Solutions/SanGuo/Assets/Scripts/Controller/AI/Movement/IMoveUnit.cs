using UnityEngine;

namespace Controller.AI.Movement
{
	/// <summary>
	/// 单位状态
	/// </summary>
	public enum UnitState
	{
		/// <summary>
		/// 未在阵型中
		/// </summary>
		Broken,
		/// <summary>
		/// 调整中
		/// </summary>
		Forming,
		/// <summary>
		/// 调整完毕
		/// </summary>
		Formed,
	}

	/// <summary>
	/// 碰撞单位
	/// </summary>
	public interface IMoveUnit
	{
		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		int ID { get; }
		/// <summary>
		/// 单位状态
		/// </summary>
		/// <value>The state.</value>
		UnitState State { get; }
		/// <summary>
		/// 位置
		/// </summary>
		/// <value>The position.</value>
		Vector3 Position  { get; }
		/// <summary>
		/// 朝向
		/// </summary>
		/// <value>The orientation.</value>
		Vector3 Orientation { get; }
		/// <summary>
		/// 单位是否独立
		/// </summary>
		/// <value><c>true</c> if this instance is individual; otherwise, <c>false</c>.</value>
		bool IsIndividual { get; set; }
		/// <summary>
		/// 是否是碰撞体
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		bool IsCollider { get; }
		/// <summary>
		/// 移动速度
		/// </summary>
		/// <value>The move speed.</value>
		float MoveSpeed { get; }
		/// <summary>
		/// 小组移动速度
		/// </summary>
		/// <value>The group speed.</value>
		float GroupSpeed { get; set; }
		/// <summary>
		/// 是否与其他单位碰撞
		/// </summary>
		/// <returns><c>true</c> if this instance is collide the specified unit; otherwise, <c>false</c>.</returns>
		/// <param name="unit">Unit.</param>
		bool IsCollideWith (IMoveUnit unit);
		/// <summary>
		/// 移动到指定目标
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="bIndividual">If set to <c>true</c> b individual.</param>
		/// <param name="tag">Tag.</param>
		void MoveTo (Vector3 position, bool bIndividual = true, MoveType tag = MoveType.Normal);
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		void Update (float dt);
	}
}

