// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник UAC.
    /// </summary>
    public sealed class UacHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<UacHelper> _instance = new(() => new UacHelper());
        public static UacHelper Instance => _instance.Value;
        private UacHelper() { }

        #endregion

        private const string UacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string UacRegistryValue = "EnableLUA";

        private static readonly uint STANDARD_RIGHTS_READ = 0x00020000;
        private static readonly uint TOKEN_QUERY = 0x0008;
        private static readonly uint TOKEN_READ = STANDARD_RIGHTS_READ | TOKEN_QUERY;

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        public enum TOKEN_ELEVATION_TYPE
        {
            TokenElevationTypeDefault = 1,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }

        public bool IsUacEnabled
        {
            get
            {
                RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(UacRegistryKey, false);
                bool result = uacKey != null && uacKey.GetValue(UacRegistryValue).Equals(1);
                return result;
            }
        }

        public bool IsProcessElevated
        {
            get
            {
                if (IsUacEnabled)
                {
                    if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_READ, out IntPtr tokenHandle))
                    {
                        throw new ApplicationException("Could not get process token.  Win32 Error Code: " + Marshal.GetLastWin32Error());
                    }

                    TOKEN_ELEVATION_TYPE elevationResult = TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;

                    int elevationResultSize = Marshal.SizeOf((int)elevationResult);
                    IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);

                    bool success = GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevationType, elevationTypePtr, (uint)elevationResultSize, out _);
                    if (success)
                    {
                        elevationResult = (TOKEN_ELEVATION_TYPE)Marshal.ReadInt32(elevationTypePtr);
                        bool isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                        return isProcessAdmin;
                    }
                    else
                    {
                        throw new ApplicationException("Unable to determine the current elevation.");
                    }
                }
                else
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new(identity);
                    bool result = principal.IsInRole(WindowsBuiltInRole.Administrator);
                    return result;
                }
            }
        }
    }
}
