using System;

namespace ASK.ServEasy.Windows.Services
{
	public enum BuiltInAccount
	{
		LocalSystem = 0,
		LocalService = 1,
		NetworkService = 2
	}

	/// <summary>
	/// Summary description for ServiceController.
	/// </summary>
	public class ServiceController : IDisposable
	{
		private WindowsImports.ServiceManagerHandle theScHandle;

		public ServiceController(string machineName)
		{
			theScHandle = WindowsImports.OpenServiceManager(machineName);
		}

		public ServiceController()
		{
			theScHandle = WindowsImports.OpenServiceManager();
		}

		public bool StartService(string serviceName)
		{
			return WindowsImports.StartService(theScHandle, serviceName);
		}

		public bool StartServiceAndWait(string serviceName, TimeSpan timeout)
		{
			return StartService(serviceName) && WaitForState(serviceName, ServiceState.RUNNING, timeout);
		}

		public bool StopService(string serviceName)
		{
			return WindowsImports.StopService(theScHandle, serviceName);
		}

		public bool StopServiceAndWait(string serviceName, TimeSpan timeout)
		{
			return StopService(serviceName) && WaitForState(serviceName, ServiceState.STOPPED, timeout);
		}

		public bool PauseService(string serviceName)
		{
			return WindowsImports.PauseService(theScHandle, serviceName);
		}

		public bool ContinueService(string serviceName)
		{
			return WindowsImports.ContinueService(theScHandle, serviceName);
		}

		public bool Restart(string serviceName)
		{
			StopService(serviceName);
			return StartService(serviceName);
		}

		public ServiceState GetServiceState(string serviceName)
		{
			return WindowsImports.GetServiceState(theScHandle, serviceName);
		}

		public bool ServiceExists(string serviceName)
		{
			return WindowsImports.ServiceExists(theScHandle, serviceName);
		}

		public void InstallService(string serviceFileName, string serviceName, string displayName, string userName, string password, string description, bool autoStart, string[] dependencies)
		{
			WindowsImports.InstallService(theScHandle, serviceFileName, serviceName, displayName, userName, password, autoStart, dependencies);
			if (!String.IsNullOrEmpty(description))
			{
				WindowsImports.SetServiceDescription(theScHandle, serviceName, description);
			}
		}

		public void InstallService(string serviceFileName, string serviceName, string displayName, BuiltInAccount builtInAccount, string description, bool autoStart, string[] dependencies)
		{
			switch (builtInAccount)
			{
				case BuiltInAccount.LocalSystem:
					WindowsImports.InstallService(theScHandle, serviceFileName, serviceName, displayName, null, null, autoStart, dependencies);
					break;
				case BuiltInAccount.LocalService:
				case BuiltInAccount.NetworkService:
					WindowsImports.InstallService(theScHandle, serviceFileName, serviceName, displayName, GetBuiltInAccountName(builtInAccount), null, autoStart, dependencies);
					break;
			}

			if (!String.IsNullOrEmpty(description))
			{
				WindowsImports.SetServiceDescription(theScHandle, serviceName, description);
			}
		}

		public bool UninstallService(string serviceName)
		{
			return !ServiceExists(serviceName) || WindowsImports.UnInstallService(theScHandle, serviceName);
		}

		private string GetBuiltInAccountName(BuiltInAccount account)
		{
			switch (account)
			{
				case BuiltInAccount.LocalService:
					return @"NT AUTHORITY\LocalService";
				case BuiltInAccount.NetworkService:
					return @"NT AUTHORITY\NetworkService";
			}
			return null;
		}

		private bool WaitForState(string serviceName, ServiceState state, TimeSpan timeout)
		{
			DateTime max = DateTime.Now.Add(timeout);

			while ((GetServiceState(serviceName) != state) && (DateTime.Now < max))
			{
				System.Threading.Thread.Sleep(250);
			}

			return GetServiceState(serviceName) == state;
		}
		#region IDisposable Members

		public void Dispose()
		{
			if (theScHandle != null)
			{
				WindowsImports.CloseServiceManagerHandle(theScHandle);
			}
		}

		#endregion
	}
}
