namespace ASK.ServEasy.Windows.Services
{
	public class ServiceInstaller
	{
		private readonly ServiceController theServiceController = new ServiceController();

		public ServiceInstaller(ModuleInfo aModuleInfo)
		{
			ServiceName = aModuleInfo.ModuleName;
			Description = aModuleInfo.Description;
			AssemblyFileName = aModuleInfo.AssemblyFileName;
		}

		public string ServiceName { get; set; }
		public string Description { get; set; }
		public string AssemblyFileName { get; set; }

		public void InstallService(BuiltInAccount account, bool isAutoStart, string[] dependances)
		{
			theServiceController.InstallService(AssemblyFileName, ServiceName, ServiceName, account, Description, isAutoStart, dependances);
		}
		public void InstallService(string userName = null, string password = null, bool isAutoStart = false, string[] dependances = null)
		{
			theServiceController.InstallService(AssemblyFileName,ServiceName, ServiceName, userName, password, Description, isAutoStart, dependances);
		}
		public void UninstallService()
		{
			theServiceController.UninstallService(ServiceName);
		}
		public void StopService()
		{
			theServiceController.StopService(ServiceName);
		}
		public void StartService()
		{
			theServiceController.StartService(ServiceName);
		}
		public bool ServiceExists { get { return theServiceController.ServiceExists(ServiceName); } }
	}
}
