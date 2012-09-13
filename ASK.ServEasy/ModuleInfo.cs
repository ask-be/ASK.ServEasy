using System;
using System.Reflection;

namespace ASK.ServEasy
{
	public class ModuleInfo
	{
		private ModuleInfo()
		{
		}

		public static ModuleInfo FromEntryAssembly()
		{
			ModuleInfo info = new ModuleInfo();

			Assembly entryAssembly = Assembly.GetEntryAssembly();

			info.Version = entryAssembly.GetName().Version.ToString();

			string codeBase = entryAssembly.CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			info.AssemblyFileName = Uri.UnescapeDataString(uri.Path);

			AssemblyTitleAttribute title = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyTitleAttribute));
			if((title != null) && (!String.IsNullOrEmpty(title.Title)))
			{
				info.ModuleName = title.Title;
			}
			AssemblyDescriptionAttribute description = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyDescriptionAttribute));
			if ((description != null) && (!String.IsNullOrEmpty(description.Description)))
			{
				info.Description = description.Description;
			}
			AssemblyCompanyAttribute company = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCompanyAttribute));
			if((company != null) && (!String.IsNullOrEmpty(company.Company)))
			{
				info.Company = company.Company;
			}
			AssemblyCopyrightAttribute copyright = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCopyrightAttribute));
			if ((copyright != null) && (!String.IsNullOrEmpty(copyright.Copyright)))
			{
				info.Copyright = copyright.Copyright;
			}
			AssemblyTrademarkAttribute trademark = (AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyTrademarkAttribute));
			if ((trademark != null) && (!String.IsNullOrEmpty(trademark.Trademark)))
			{
				info.Trademark = trademark.Trademark;
			}
			AssemblyProductAttribute product = (AssemblyProductAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyProductAttribute));
			if((product != null) && (!String.IsNullOrEmpty(product.Product)))
			{
				info.Product = product.Product;
			}

			return info;
		}

		/// <summary>
		/// Module name
		/// </summary>
		public string ModuleName { get; set; }
		/// <summary>
		/// The Full filename of the Module.
		/// </summary>
		public string AssemblyFileName { get; set; }
		/// <summary>
		/// A short description of what this Module does.
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// Returns the current version of the Module, as it is defined by the <c>AssemblyVersion</c> attribute of AssemblyInfo.cs
		/// </summary>
		public string Version { get; set; }
		/// <summary>
		/// Returns the name of the company that wrote the Module, as it is defined by the <c>AssemblyCompany</c> attribute of AssemblyInfo.cs
		/// </summary>
		public string Company { get; set; }
		/// <summary>
		/// Returns the copyright text associated with the Module, as it is defined by the <c>AssemblyCopyright</c> attribute of AssemblyInfo.cs
		/// </summary>
		public string Copyright { get; set; }
		/// <summary>
		/// Returns the trademark text associated with the Module, as it is defined by the <c>AssemblyTrademark</c> attribute of AssemblyInfo.cs
		/// </summary>
		public string Trademark { get; set; }
		/// <summary>
		/// Returns the product text associated with the Module, as it is defined by the <c>AssemblyProduct</c> attribute of AssemblyInfo.cs
		/// </summary>
		public string Product { get; set; }
	}
}
