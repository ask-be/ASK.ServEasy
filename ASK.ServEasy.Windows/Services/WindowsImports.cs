using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace ASK.ServEasy.Windows.Services
{
	public enum ServiceState
	{
		CONTINUE_PENDING = 0x5, // The service continue is pending.
		PAUSE_PENDING = 0x6, // The service pause is pending.
		PAUSED = 0x7, // The service is paused.
		RUNNING = 0x4, // The service is running.
		START_PENDING = 0x2, // The service is starting.
		STOP_PENDING = 0x3, // The service is stopping.
		STOPPED = 0x1  // The service is not running.
	}

	/// <summary>
	/// Summary description for ServiceInstaller.
	/// </summary>
	internal static class WindowsImports
	{
		internal class ServiceManagerHandle
		{
			private IntPtr theHandle;
			public ServiceManagerHandle(IntPtr hdl)
			{
				theHandle = hdl;
			}
			internal IntPtr Handle
			{
				get { return theHandle; }
			}
		}


		#region DLLImport
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern IntPtr OpenSCManager(string lpMachineName, string lpSCDB, int scParameter);

		[DllImport("Advapi32.dll", SetLastError = true)]
		private static extern IntPtr CreateService(IntPtr SC_HANDLE, string lpSvcName, string lpDisplayName,
			int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpPathName,
			string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern void CloseServiceHandle(IntPtr SCHANDLE);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool ControlService(IntPtr SVHANDLE, int cardinal, ref ServiceStatus status);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, int dwNumServiceArgs);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool DeleteService(IntPtr SVHANDLE);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool QueryServiceStatus(IntPtr SVHANDLE, ref ServiceStatus status);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool ChangeServiceConfig2(IntPtr SVHANDLE, uint dwInfoLevel, ref ServiceDescription lpInfo);

		// the version, the sample is built upon:
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern uint FormatMessage(uint dwFlags, IntPtr lpSource,uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer,uint nSize, IntPtr pArguments);

		[DllImport("kernel32.dll")]
		static extern IntPtr LocalFree(IntPtr hMem);

		private struct ServiceStatus
		{
			public int dwServiceType;
			public ServiceState dwCurrentState;
			public int dwControlsAccepted;
			public int dwWin32ExitCode;
			public int dwServiceSpecificExitCode;
			public int dwCheckPoint;
			public int dwWaitHint;
		}

		private struct ServiceDescription
		{
			public string Description;
		}

		#endregion DLLImport

		#region Constants declaration.

		private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
		private const int SERVICE_QUERY_CONFIG = 0x0001;
		private const int SERVICE_CHANGE_CONFIG = 0x0002;
		private const int SERVICE_QUERY_STATUS = 0x0004;
		private const int SERVICE_ENUMERATE_DEPENDENTS = 0x0008;
		private const int SERVICE_START = 0x0010;
		private const int SERVICE_STOP = 0x0020;
		private const int SERVICE_PAUSE_CONTINUE = 0x0040;
		private const int SERVICE_INTERROGATE = 0x0080;
		private const int SERVICE_USER_DEFINED_CONTROL = 0x0100;

		private const int SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
			SERVICE_QUERY_CONFIG |
			SERVICE_CHANGE_CONFIG |
			SERVICE_QUERY_STATUS |
			SERVICE_ENUMERATE_DEPENDENTS |
			SERVICE_START |
			SERVICE_STOP |
			SERVICE_PAUSE_CONTINUE |
			SERVICE_INTERROGATE |
			SERVICE_USER_DEFINED_CONTROL);

		private const int SC_MANAGER_ALL_ACCESS = 0xf003f;
		private const int SERVICE_WIN32_OWN_PROCESS = 0x10;
		private const int SERVICE_WIN32_SHARE_PROCESS = 0x20;
		private const int SERVICE_WIN32 = 0x30;
		private const int SERVICE_INTERACTIVE_PROCESS = 0x100;
		private const int SERVICE_BOOT_START = 0x0;
		private const int SERVICE_SYSTEM_START = 0x1;
		private const int SERVICE_AUTO_START = 0x2;
		private const int SERVICE_DEMAND_START = 0x3;
		private const int SERVICE_DISABLED = 0x4;
		private const int SERVICE_DELETE = 0x10000;
		private const int SERVICE_CONTROL_STOP = 0x1;
		private const int SERVICE_CONTROL_PAUSE = 0x2;
		private const int SERVICE_CONTROL_CONTINUE = 0x3;
		private const int SERVICE_CONTROL_INTERROGATE = 0x4;
		private const int SERVICE_STOPPED = 0x1;
		private const int SERVICE_START_PENDING = 0x2;
		private const int SERVICE_STOP_PENDING = 0x3;
		private const int SERVICE_RUNNING = 0x4;
		private const int SERVICE_CONTINUE_PENDING = 0x5;
		private const int SERVICE_PAUSE_PENDING = 0x6;
		private const int SERVICE_PAUSED = 0x7;
		private const int SERVICE_ERROR_NORMAL = 0x00000001;
		private const int SERVICE_CONFIG_DESCRIPTION = 1;
		#endregion

		private static void ThrowLastError()
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		private static void CheckHandleAndThrowLastError(IntPtr ptr)
		{
			if (ptr.ToInt32() == 0)
				ThrowLastError();
		}

		internal static ServiceManagerHandle OpenServiceManager()
		{
			IntPtr hdl = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
			CheckHandleAndThrowLastError(hdl);

			return new ServiceManagerHandle(hdl);
		}

		internal static ServiceManagerHandle OpenServiceManager(string machineName)
		{
			IntPtr hdl = OpenSCManager(machineName, null, SC_MANAGER_ALL_ACCESS);

			CheckHandleAndThrowLastError(hdl);

			return new ServiceManagerHandle(hdl);
		}

		internal static void CloseServiceManagerHandle(ServiceManagerHandle scHandle)
		{
			CloseServiceHandle(scHandle.Handle);
		}

		internal static bool StartService(ServiceManagerHandle smhandle, string serviceName)
		{
			IntPtr srvHandle = OpenService(smhandle.Handle, serviceName, SERVICE_START);
			CheckHandleAndThrowLastError(srvHandle);

			bool result = StartService(srvHandle, 0, null);
			CloseServiceHandle(srvHandle);

			return result;
		}
		internal static bool StopService(ServiceManagerHandle smhandle, string serviceName)
		{
			IntPtr srvHandle = OpenService(smhandle.Handle, serviceName, SERVICE_STOP);
			CheckHandleAndThrowLastError(srvHandle);

			ServiceStatus status = new ServiceStatus();
			if (!ControlService(srvHandle, SERVICE_CONTROL_STOP, ref status))
			{
				ThrowLastError();
			}

			CloseServiceHandle(srvHandle);
			return true;
		}
		internal static bool PauseService(ServiceManagerHandle smhandle, string serviceName)
		{
			IntPtr srvHandle = OpenService(smhandle.Handle, serviceName, SERVICE_PAUSE_CONTINUE);
			CheckHandleAndThrowLastError(srvHandle);

			ServiceStatus status = new ServiceStatus();
			if (!ControlService(srvHandle, SERVICE_CONTROL_PAUSE, ref status))
			{
				ThrowLastError();
			}

			CloseServiceHandle(srvHandle);
			return true;
		}
		internal static bool ContinueService(ServiceManagerHandle smhandle, string serviceName)
		{
			IntPtr srvHandle = OpenService(smhandle.Handle, serviceName, SERVICE_PAUSE_CONTINUE);
			CheckHandleAndThrowLastError(srvHandle);

			ServiceStatus status = new ServiceStatus();
			if (!ControlService(srvHandle, SERVICE_CONTROL_CONTINUE, ref status))
			{
				ThrowLastError();
			}

			CloseServiceHandle(srvHandle);
			return true;
		}

		internal static ServiceState GetServiceState(ServiceManagerHandle smHandle, string serviceName)
		{
			IntPtr srvHandle = OpenService(smHandle.Handle, serviceName, SERVICE_QUERY_STATUS);
			CheckHandleAndThrowLastError(srvHandle);

			ServiceStatus status = new ServiceStatus();
			if (!QueryServiceStatus(srvHandle, ref status))
			{
				ThrowLastError();
			}

			CloseServiceHandle(srvHandle);

			return status.dwCurrentState;
		}

		internal static bool ServiceExists(ServiceManagerHandle smHandle, string serviceName)
		{
			IntPtr srvHandle = OpenService(smHandle.Handle, serviceName, SERVICE_QUERY_STATUS);
			if (srvHandle.ToInt32() != 0)
			{
				CloseServiceHandle(srvHandle);
				return true;
			}
			return false;
		}

		internal static void InstallService(ServiceManagerHandle smHandle, string svcPath, string svcName, string svcDispName, string svcUserName, string svcPassword, bool autoStart, string[] dependencies)
		{
			StringBuilder dependenciesBuilder = new StringBuilder();
			if (dependencies != null)
			{
				for (int i = 0; i < dependencies.Length; i++)
				{
					dependenciesBuilder.Append(dependencies[i]);
					dependenciesBuilder.Append('\0');
				}
			}
			else
			{
				dependenciesBuilder.Append('\0');
			}

			IntPtr srvHandle = CreateService(
				smHandle.Handle,
				svcName,
				svcDispName,
				SERVICE_ALL_ACCESS,
				SERVICE_WIN32_OWN_PROCESS,
				autoStart ? SERVICE_AUTO_START : SERVICE_DEMAND_START,
				SERVICE_ERROR_NORMAL,
				svcPath,
				null,
				0,
				dependenciesBuilder.ToString(),
				svcUserName,
				svcPassword);

			CheckHandleAndThrowLastError(srvHandle);
			CloseServiceHandle(srvHandle);
		}

		internal static bool UnInstallService(ServiceManagerHandle smHandle, string svcName)
		{
			if (GetServiceState(smHandle, svcName) != ServiceState.STOPPED)
			{
				StopService(smHandle, svcName);
			}

			IntPtr srvHandle = OpenService(smHandle.Handle, svcName, SERVICE_DELETE);
			CheckHandleAndThrowLastError(srvHandle);

			bool result = DeleteService(srvHandle);
			CloseServiceHandle(srvHandle);
			return result;
		}
		internal static bool SetServiceDescription(ServiceManagerHandle smhandle, string serviceName, string description)
		{
			IntPtr srvHandle = OpenService(smhandle.Handle, serviceName, SERVICE_START | SERVICE_CHANGE_CONFIG);
			CheckHandleAndThrowLastError(srvHandle);

			ServiceDescription sd = new ServiceDescription();
			sd.Description = description;

			bool result = ChangeServiceConfig2(srvHandle, SERVICE_CONFIG_DESCRIPTION, ref sd);
			if (!result)
			{
				ThrowLastError();
			}
			CloseServiceHandle(srvHandle);
			return result;
		}

		//
		//      private bool StopService(string svcName)
		//      {
		//         bool result = false;
		//
		//         IntPtr sc_hndl = OpenServiceManager();
		//         if(sc_hndl.ToInt32() !=0)
		//         {
		//            IntPtr svc_hndl = OpenService(sc_hndl,svcName,SERVICE_STOP);
		//            if(svc_hndl.ToInt32() != 0)
		//            {
		//               ServiceStatus status = new ServiceStatus();
		//               result = ControlService(svc_hndl,SERVICE_CONTROL_STOP,ref status);
		//               CloseServiceHandle(svc_hndl);
		//            }
		//            CloseServiceHandle(sc_hndl);
		//         }
		//         return result;
		//      }
		//
		//      private bool StartService(string svcName)
		//      {
		//         bool result = false;
		//
		//         IntPtr sc_hndl = OpenServiceManager();
		//         if(sc_hndl.ToInt32() !=0)
		//         {
		//            IntPtr svc_hndl = OpenService(sc_hndl,svcName,SERVICE_START);
		//            if(svc_hndl.ToInt32() != 0)
		//            {
		//               result = StartService(svc_hndl,0,null);
		//               CloseServiceHandle(svc_hndl);
		//            }
		//            CloseServiceHandle(sc_hndl);
		//         }
		//         return result;
		//      }
		//
		//      private bool IsServiceRunning(string svcName)
		//      {
		//         bool result = false;
		//         IntPtr sc_hndl = OpenServiceManager();
		// 
		//         if(sc_hndl.ToInt32() !=0)
		//         {
		//            IntPtr svc_hndl = OpenService(sc_hndl,svcName,SERVICE_QUERY_STATUS);
		//            if(svc_hndl.ToInt32() != 0)
		//            {
		//               ServiceStatus status = new ServiceStatus();
		//               if(QueryServiceStatus(svc_hndl,ref status))
		//               {
		//                  result =(status.dwCurrentState == SERVICE_RUNNING);
		//               }
		//               CloseServiceHandle(svc_hndl);
		//            }
		//            CloseServiceHandle(sc_hndl);
		//         }
		//         return result;
		//      }
		//      private string theServiceName;
		//      private string theServicePath;
		//

		//
		//      public W32()
		//      {
		//         //Retrieve Service Name
		//         theServiceName = "";
		//         try
		//         {
		//            AssemblyTitleAttribute serviceName = (AssemblyTitleAttribute) Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(),typeof(AssemblyTitleAttribute));
		//            theServiceName = serviceName.Title;
		//         }
		//         catch{}
		//         finally
		//         {
		//            if(theServiceName.Length == 0)
		//            {
		//               theServiceName = System.IO.Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
		//            }
		//         }
		//         
		//         //Retrieve Service Path
		//         theServicePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + AppDomain.CurrentDomain.FriendlyName;
		//      }
		//
		//      public void InstallService()
		//      {
		//         this.InstallService(theServicePath,theServiceName,theServiceName,null,null);
		//      }
		//
		//      public void InstallService(BuiltInAccount account)
		//      {
		//         string builtInName = null;
		//         switch(account)
		//         {
		//            case BuiltInAccount.LocalService:
		//               builtInName = @"NT AUTHORITY\LocalService";
		//               break;
		//            case BuiltInAccount.NetworkService:
		//               builtInName = @"NT AUTHORITY\NetworkService";
		//               break;
		//         }
		//         this.InstallService(theServicePath,theServiceName,theServiceName,builtInName,null);
		//      }
		//
		//      public void InstallService(string userName,string password)
		//      {
		//         this.InstallService(theServicePath,theServiceName,theServiceName,userName,password);
		//      }
		//      public void UninstallService()
		//      {
		//         this.UnInstallService(theServiceName);
		//      }
		//
		//      public void StopService()
		//      {
		//         this.StopService(theServiceName);
		//      }
		//
		//      public void StartService()
		//      {
		//         this.StartService(theServiceName);
		//      }
	}
}