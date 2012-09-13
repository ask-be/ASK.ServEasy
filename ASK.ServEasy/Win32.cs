using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ASK.ServEasy
{
	internal static class Win32
	{
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);

		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_PROVIDER_DEFAULT = 0;

		internal static WindowsImpersonationContext Impersonate(string aUserName, string aDomain, string aPassword)
		{
			WindowsImpersonationContext context = null;
			WindowsIdentity tempWindowsIdentity = null;
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;
			try
			{
				if (RevertToSelf())
				{
					if (LogonUser(aUserName, aDomain, aPassword, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
					{
						if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
						{
							tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
							context = tempWindowsIdentity.Impersonate();
						}
						else
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
					}
					else
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (token != IntPtr.Zero)
				{
					CloseHandle(token);
				}
				if (tokenDuplicate != IntPtr.Zero)
				{
					CloseHandle(tokenDuplicate);
				}
			}
			return context;
		}
	}
}
