// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Win32;

namespace WsLabelCore.Helpers;

public sealed class WsHardwareHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsHardwareHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsHardwareHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public methods

    /// <summary>
    /// Methods return System manufacture, model and Bios version.
    /// </summary>
    /// <param name="infoSelect"></param>
    /// <returns></returns>
    public Dictionary<string, string> HardwareInfoSelect(string infoSelect)
    {
        Dictionary<string, string> systemInfo = new();
        SelectQuery query = new(@"Select * from " + infoSelect);

        using (ManagementObjectSearcher searcher = new(query))
        {
            try
            {
                // execute the query
                foreach (ManagementBaseObject proc in searcher.Get())
                {
                    // print system info
                    ((ManagementObject)proc).Get();
                    systemInfo.Add("Manufacturer", proc["Manufacturer"].ToString());
                    systemInfo.Add("Model", proc["Model"].ToString().Trim());
                    systemInfo.Add("ComputerName", proc["Caption"].ToString().Trim());
                    systemInfo.Add("Domain", proc["Domain"].ToString().Trim());
                    systemInfo.Add("UserName", proc["PrimaryOwnerName"].ToString().Trim());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        ManagementObjectSearcher searcher1 = new("SELECT * FROM Win32_BIOS");
        ManagementObjectCollection collection = searcher1.Get();

        foreach (ManagementBaseObject obj in collection)
        {
            ((ManagementObject)obj).Get();
            if (((string[])obj["BIOSVersion"]).Length > 1)
                systemInfo.Add("BIOSVersion", ((string[])obj["BIOSVersion"])[0] + " - " + ((string[])obj["BIOSVersion"])[1]);
            else
                systemInfo.Add("BIOSVersion", ((string[])obj["BIOSVersion"])[0]);
        }
        return systemInfo;
    }

    /// <summary>
    /// Method return instaled program in PC.
    /// </summary>
    /// <returns></returns>
    public List<string> ProgramPrint()
    {
        Stopwatch stopwatch1 = new();
        Stopwatch stopwatch2 = new();
        List<string> installed_program = new();

        string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
        {
            if (key is not null)
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    using RegistryKey subKey = key.OpenSubKey(subKeyName);
                    try
                    {
                        if (subKey is not null) installed_program.Add(subKey.GetValue("DisplayName").ToString());
                    }
                    catch (Exception)
                    {
                        //
                    }
                }
        }
        stopwatch1.Stop();
        Console.WriteLine(@"Time elapsed using: {0}", stopwatch1.Elapsed);
        Console.WriteLine(@"Time elapsed foreach: {0}", stopwatch2.Elapsed);
        return installed_program;
    }

    #endregion
}