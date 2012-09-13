using System;

namespace ASK.ServEasy
{
	public interface IModuleController : IDisposable
	{
		void Initialize(Module aModule);
	}
}
