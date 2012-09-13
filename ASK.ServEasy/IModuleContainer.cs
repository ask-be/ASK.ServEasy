using System;

namespace ASK.ServEasy
{
	public interface IModuleContainer: IDisposable
	{
		void Initialize(Module aModule);
	}
}
