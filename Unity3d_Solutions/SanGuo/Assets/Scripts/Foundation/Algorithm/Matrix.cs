using System.Collections.Generic;
using System;

namespace Foundation.Algorithm
{
	public delegate void OnMatrixForeachHandler(int i, int j, float value);
	/// <summary>
	/// 矩阵
	/// </summary>
	public class Matrix
	{
		/// <summary>
		/// 数组数据
		/// </summary>
		private float[,] _Data;
		/// <summary>
		/// 边长
		/// </summary>
		private int _Length;
		/// <summary>
		/// 边长
		/// </summary>
		public int Length {
			get  { 
				return _Length;
			}
		}

		public Matrix(int length)
		{
			if (length <= 0) {
				return;
			}
			_Length = length;
			_Data = new float[length, length];
		}

		public Matrix (float[,] values)
		{
			if (Math.Sqrt (values.Length) != (int)Math.Sqrt (values.Length)) {
				return;
			}
			_Length = (int)Math.Sqrt (values.Length);
			_Data = values;

		}

		/// <summary>
		/// 遍历
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void Foreach(OnMatrixForeachHandler handler)
		{
			if (handler == null) {
				return;
			}

			int count = _Length;
			for (int i = 0; i < count; i++) {
				for (int j = 0; j < count; j++) {
					handler (i, j, _Data [i, j]);
				}
			}
		}

		public float this[int i, int j]
		{
			get { 
				return _Data [i, j];
			} 
		}
	
		/// <summary>
		/// 剔除执行行列
		/// </summary>
		/// <param name="column">Column.</param>
		/// <param name="row">Row.</param>
		public Matrix Eliminate (int column, int row)
		{
			if (column < 0 || column >= _Length
				|| row < 0 || row >_Length) {
				return null;
			}

			if (_Length == 1) {
				return null;
			}

			int count = _Length - 1;
			float[,] values = new float[count, count];

			int column0 = 0;
			int row0 = 0;
			for (int i = 0; i < count; i++) {
				for (int j = 0; j < count; j++) {
					column0 = i >= column ? i + 1 : i;
					row0 = j >= row ? j + 1 : j;
					values [i, j] = _Data [column0, row0];
				}
			}

			return new Matrix (values);
		}

		/// <summary>
		/// 获取最小值
		/// </summary>
		/// <returns><c>true</c>, if minimum was gotten, <c>false</c> otherwise.</returns>
		/// <param name="column">Column.</param>
		/// <param name="row">Row.</param>
		/// <param name="value">Value.</param>
		public bool GetMinimum(out int column, out int row, out float value)
		{
			column = 0;
			row = 0;
			value = 0;
			if (_Data == null || _Length == 0) {
				return false;
			}

			int column0 = 0;
			int row0 = 0;
			float value0 = 0;
			Foreach((int i, int j, float val)=>{
				if (value0 == 0 || val > value0) {
					value0 = val;
					column0 = i;
					row0 = j;
				}
			});

			column = column0;
			row = row0;
			value = value0;

			return true;
		}

		/// <summary>
		/// 筛选最小值
		/// 把每次筛选出的结果的所在行列剔除掉
		/// </summary>
		/// <returns>The minimum pairs.</returns>
		/// <param name="values">Values.</param>
		public static Dictionary<int, int> FilterMinPairs(float[,] values)
		{
			if (values == null || values.Length == 0) {
				return null;
			}

			Dictionary<int, int> pairs = new Dictionary<int, int> ();

			Matrix mat = new Matrix (values);
			int count = mat.Length;
			int column, row;
			int offsetColumn, offsetRow;
			float minValue;
			for (int i = 0; i < count; i++) {
				if (mat != null && mat.GetMinimum (out column, out row, out minValue)) {
					offsetColumn = 0;
					offsetRow = 0;
					foreach (KeyValuePair<int, int> item in pairs) {
						if (column >= item.Key) {
							offsetColumn++;
						}
						if (row >= item.Value) {
							offsetRow++;
						}
					}
					mat = mat.Eliminate (column, row);
					column += offsetColumn;
					row += offsetRow;
					pairs [column] = row;
				} else {
					break;
				}
			}
			return pairs;
		}
	}
}

