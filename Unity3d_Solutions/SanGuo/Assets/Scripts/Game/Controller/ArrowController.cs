using UnityEngine;

namespace Game.Controller
{
	[RequireComponent(typeof(LineRenderer))]
	public class ArrowController : MonoBehaviour
	{
		/// <summary>
		/// 起始点
		/// </summary>
		public Vector3 SrcPosition;
		/// <summary>
		/// 终止点
		/// </summary>
		public Vector3 DestPosition;
		/// <summary>
		/// 分割点数
		/// </summary>
		public int SplitCount = 2;
		/// <summary>
		/// 高度
		/// </summary>
		public float Height = 2;

		private void CalLineRender()
		{
			if (SplitCount < 2 || Height == 0) {
				return;
			}

			// 长度
			float length = Vector3.Distance (SrcPosition, DestPosition);
			// 垂直中心距离两端点的距离
			float height = (length * 0.5f * length * 0.5f - Height * Height) / (2 * Height);

			// 中心点位置
			Vector3 center = (SrcPosition + DestPosition) * 0.5f;
			// 球心位置
			Vector3 verticalCenter = center;
			verticalCenter.y -= height;

			// 方向向量
			Vector3 srcVector = SrcPosition - verticalCenter;
			Vector3 destVector = DestPosition - verticalCenter;

			this.GetComponent<LineRenderer> ().numPositions = SplitCount + 1;
			this.GetComponent<LineRenderer> ().SetPosition (0, SrcPosition);
			this.GetComponent<LineRenderer> ().SetPosition (SplitCount, DestPosition);
			// 球面插值
			Vector3 position = Vector3.zero;
			for (int i = 1; i < SplitCount; i++) {
				position = Vector3.Slerp (srcVector, destVector, 1.0f * i / SplitCount);
				position += verticalCenter;

				this.GetComponent<LineRenderer> ().SetPosition (i, position);
			}
		}

		void Start()
		{
			CalLineRender ();
		}

		void Update()
		{
		}
	}
}

