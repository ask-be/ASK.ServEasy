using System;
using System.IO;
using System.Reflection;

namespace ASK.ServEasy
{
	public class Loader
	{
		#region Logger Declaration
		private static readonly Logger.ILog theLogger = Logger.CreateLog();
		#endregion

		static Loader()
		{
			// Ensure that CurrentDirectory is set to the path of the .exe by default, even when ru as a service
			Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			log4net.Config.XmlConfigurator.ConfigureAndWatch(
				new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
		}

		public static void LoadModule<T>() where T: Module,new()
		{
			LoadModule(new T());
		}

		public static void LoadModule(Module aModule)
		{
			try
			{
				AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

				IModuleContainer container;

				if(Environment.UserInteractive)
				{
					theLogger.Info("Loading WinformContainer...");
					container =
						(IModuleContainer)
						Activator.CreateInstance(Type.GetType("ASK.ServEasy.Windows.Containers.WinFormContainer, ASK.ServEasy.Windows"));
				}
				else
				{
					theLogger.Info("Loading ServiceContainer...");
					container =
						(IModuleContainer)
						Activator.CreateInstance(Type.GetType("ASK.ServEasy.Windows.Containers.ServiceContainer, ASK.ServEasy.Windows"));
				}

				Controller controller = new Controller(aModule, container);
				
				theLogger.Info("Starting controller...");
				controller.Run();
				// Will not return until the module terminates
				theLogger.Info("Controller stopped...");
			}
			catch (Exception e)
			{
				theLogger.Fatal("Error while Loading Module. Aborted", e);
				throw;
			}

		}

		static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			theLogger.Fatal("Unhandled exception", e.ExceptionObject as Exception);
		}
	}
}
