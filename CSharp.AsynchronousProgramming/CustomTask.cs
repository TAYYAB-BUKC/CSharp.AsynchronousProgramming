namespace CSharp.AsynchronousProgramming
{
	public class CustomTask
	{
		private readonly Lock _lock = new();
		private bool _completed;
		private Exception _exception;

		public bool IsCompleted 
		{
			get 
			{ 
				lock(_lock)
				{
					return _completed;
				}
			}
		}

		public void SetException(Exception exception)
		{
			lock (_lock)
			{
				if (_completed)
					throw new InvalidOperationException("CustomTask already completed. Cannot set an exception on the completed task.");

				_exception = exception;
			}
		}

		public void SetResult()
		{
			lock (_lock)
			{
				if (_completed)
					throw new InvalidOperationException("CustomTask already completed. Cannot set a result on the completed task.");
				
				_completed = true;
			}
		}

		public static CustomTask Run(Action action)
		{
			CustomTask task = new CustomTask();
			ThreadPool.QueueUserWorkItem(_ =>
			{
				try
				{
					action();
					task.SetResult();
				}
				catch (Exception ex)
				{
					task.SetException(ex);
				}
			});

			return task;
		}
	}
}