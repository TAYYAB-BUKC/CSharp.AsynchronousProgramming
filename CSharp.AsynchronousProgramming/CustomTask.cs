using System;
using System.Runtime.ExceptionServices;

namespace CSharp.AsynchronousProgramming
{
	public class CustomTask
	{
		private readonly Lock _lock = new();
		private bool _completed;
		private Exception _exception;
		private Action? _action;
		private ExecutionContext _executionContext;

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

		public void SetException(Exception exception) => CompleteTask(exception);

		public void SetResult() => CompleteTask(null);

		public void CompleteTask(Exception? exception)
		{
			lock (_lock)
			{
				if (_completed)
					throw new InvalidOperationException("CustomTask already completed. Cannot perform operation on the completed task.");

				_completed = true;
				_exception = exception;

				if(_action is not null)
				{
					if(_executionContext is null)
					{
						_action.Invoke();
					}
					else
					{
						ExecutionContext.Run(_executionContext, state => ((Action?)state)?.Invoke(), _action);
					}
				}
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

		public CustomTask ContinueWith(Action action)
		{
			CustomTask task = new CustomTask();
			lock (_lock)
			{
				if (_completed)
				{
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
				}
				else
				{
					_action = action;
					_executionContext = ExecutionContext.Capture();
				}
			}

			return task;
		}

		public void Wait()
		{
			ManualResetEventSlim? resetEventSlim = null;

			lock (_lock)
			{
				if (!_completed)
				{
					resetEventSlim = new();
					ContinueWith(() =>
					{
						resetEventSlim.Set();
					});

				}
			}

			resetEventSlim?.Wait();

			if(_exception is not null)
			{
				ExceptionDispatchInfo.Throw(_exception);
			}
		}

		public static CustomTask Delay(TimeSpan timeSpan)
		{
			CustomTask task = new CustomTask();

			new Timer(_ => task.SetResult()).Change(timeSpan, Timeout.InfiniteTimeSpan);

			return task;
		}
	}
}