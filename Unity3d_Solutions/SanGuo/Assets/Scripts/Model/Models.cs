using System;
using System.Collections.Generic;
using Model.Base;
using Model.Sheet;

namespace Model
{
	/// <summary>
	/// 模型数据
	/// </summary>
	public class Models
	{
		/// <summary>
		/// 模型实例
		/// </summary>
		private Dictionary<ModelType, IModelSample> _ModelSamples;

		private static Models _Models;

		private Models ()
		{
			_ModelSamples = new Dictionary<ModelType, IModelSample> ();
		}

		public static Models GetInstance()
		{
			if (_Models == null) {
				_Models = new Models ();
			}

			return _Models;
		}

		/// <summary>
		/// 添加实例
		/// </summary>
		/// <param name="sample">Sample.</param>
		public void AddSample(IModelSample sample)
		{
			if (sample == null) {
				return;
			}

			_ModelSamples[sample.ID] = sample;
		}

		/// <summary>
		/// 移除实例
		/// </summary>
		/// <param name="id">Identifier.</param>
		public void RemoveSample(ModelType id)
		{
			_ModelSamples.Remove (id);
		}

		/// <summary>
		/// 查找实例
		/// </summary>
		/// <returns>The sample.</returns>
		/// <param name="id">Identifier.</param>
		public IModelSample FindSample(ModelType id)
		{
			if (!_ModelSamples.ContainsKey(id)) {
				return null;
			}

			return _ModelSamples [id];
		}

		/// <summary>
		/// 清空所有实例
		/// </summary>
		public void Clear()
		{
			_Models.Clear ();
		}

		/// <summary>
		/// 任务
		/// </summary>
		/// <value>The task.</value>
		public TaskSheet Task {
			get { 
				return (TaskSheet)FindSample (ModelType.Task);
			}
		}
	}
}

