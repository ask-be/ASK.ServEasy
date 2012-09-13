namespace ASK.ServEasy
{
	public class Module : ModuleThread
	{
		#region Logger Declaration
		private static readonly Logger.ILog theLogger = Logger.CreateLog();
		#endregion


		private Controller theController;

		protected Module():base("Main Thread", autoStart:false)
		{ 
			ModuleInfo = ModuleInfo.FromEntryAssembly();
		}

		internal void Initialize(Controller aController)
		{
			Initialize();
			theController = aController;
		}

		public ModuleInfo ModuleInfo { get; private set; }

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				theLogger.Debug("Disposing Module...");
				theController.Dispose();
				theLogger.Debug("Module Disposed");
			}
		}
	}
}
