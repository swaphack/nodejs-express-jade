using System.Collections.Generic;
using UnityEngine;

namespace Controller.AI.Movement
{
	/// <summary>
	/// 命令队列
	/// </summary>
	public class MoveCommand
	{
		/// <summary>
		/// 命令项
		/// </summary>
		internal struct CommandItem
		{
			/// <summary>
			/// 目的地
			/// </summary>
			public Vector3 Destination;
			/// <summary>
			/// 是否独立
			/// </summary>
			public bool IsIndividual;
			/// <summary>
			/// 是否抵达目标
			/// </summary>
			public bool IsArrived; 
			/// <summary>
			/// 标记
			/// </summary>
			public MoveType Tag;

			public CommandItem(Vector3 destination, bool bIndividual = true, MoveType tag = MoveType.Normal)
			{
				Destination = destination;
				IsArrived = false;
				IsIndividual = bIndividual;
				Tag = tag;
			}
		}

		/// <summary>
		/// 命令队列
		/// </summary>
		private Queue<CommandItem> _CommandItems;

		/// <summary>
		/// 队列是否为空
		/// </summary>
		/// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
		public bool Empty {
			get { 
				return _CommandItems.Count == 0;
			}
		}

		public MoveCommand ()
		{
			_CommandItems = new Queue<CommandItem> ();
		}

		/// <summary>
		/// 获取下一个目的地
		/// </summary>
		/// <returns><c>true</c>, if destination was gotten, <c>false</c> otherwise.</returns>
		/// <param name="destination">Destination.</param>
		/// <param name="bIndividual">B individual.</param>
		/// <param name="tag">Tag.</param>
		public bool GetDestination(out Vector3 destination, out bool bIndividual, out MoveType tag)
		{
			destination = Vector3.zero;
			bIndividual = true;
			tag = 0;

			if (_CommandItems.Count == 0) {
				return false;
			}

			CommandItem item = _CommandItems.Peek ();
			if (item.IsArrived) {
				_CommandItems.Dequeue ();
				return GetDestination (out destination, out bIndividual, out tag);
			}

			destination = item.Destination;
			bIndividual = item.IsIndividual;
			tag = item.Tag;

			return true;
		}

		/// <summary>
		/// 添加目的地
		/// </summary>
		/// <param name="destination">Destination.</param>
		/// <param name="bIndividual">If set to <c>true</c> b individual.</param>
		/// <param name="tag">Tag.</param>
		public void AddDestination(Vector3 destination, bool bIndividual, MoveType tag)
		{
			_CommandItems.Enqueue (new CommandItem (destination, bIndividual, tag));
		}

		/// <summary>
		/// 抵达目标
		/// </summary>
		public void ArrivedDestination()
		{
			if (_CommandItems.Count == 0) {
				return;
			}

			CommandItem item = _CommandItems.Peek ();
			if (!item.IsArrived) {
				item.IsArrived = true;
			}
		}

		/// <summary>
		/// 清空目的地
		/// </summary>
		public void Clear()
		{
			_CommandItems.Clear ();
		}
	}
}

