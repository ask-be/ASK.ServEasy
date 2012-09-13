namespace ASK.ServEasy
{
	/// <summary>
	/// Describes the state of a Module or a ModuleThread (stopped, started, paused, etc...).
	/// </summary>
	public enum RunState
	{
		/// <summary>Thread is Not yet initialized.</summary>
		NotInitialized,
		/// <summary>Module or thread is in its initialization phas  (next state is Stopped)e</summary>
		Initializing,
		/// <summary>Module or thread is currently stopping (stop in progress).</summary>
		Stopping,
		/// <summary>Module or thread is currently stopped (but can still be present in memory).</summary>
		Stopped,
		/// <summary>Module or thread is currently being started (starting in progress).</summary>
		Starting,
		/// <summary>Module or thread is currently started and running.</summary>
		Started,
		/// <summary>Module or thread is being terminated (unloaded from memory).</summary>
		Terminating,
		/// <summary>Module or thread is terminated (unloaded from memory).</summary>
		Terminated
	}
}
