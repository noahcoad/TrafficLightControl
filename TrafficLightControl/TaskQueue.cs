using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightControl
{
	class TaskQueue
	{
		private static TaskQueue _instance = null;
		private object syncRoot = new object();
		private Task latestTask;

		public void EnqueueAction(System.Action action)
		{
			lock (syncRoot)
			{
				if (latestTask == null) latestTask = Task.Factory.StartNew(action);
				else latestTask = latestTask.ContinueWith(tsk => action());
			}
		}

		public static TaskQueue Default
		{ get { if (_instance == null) _instance = new TaskQueue(); return _instance; } }
	}
}
