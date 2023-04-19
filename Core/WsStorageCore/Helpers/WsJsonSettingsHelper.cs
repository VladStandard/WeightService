// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

public class WsJsonSettingsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsJsonSettingsHelper _instance;
#pragma warning restore CS8618
    public static WsJsonSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public bool IsRemote { get; set; }
    private WsJsonSettingsModel? _local;
    public WsJsonSettingsModel Local
    {
        get
        {
            if (_local is null) throw new ArgumentNullException(nameof(Local));
            _local.CheckProperties(true);
            return _local;
        }
        set => _local = value;
    }
    private WsJsonSettingsModel? _remote;
    public WsJsonSettingsModel Remote
    {
        get
        {
            if (_remote is null) throw new ArgumentNullException(nameof(Remote));
            _remote.CheckProperties(true);
            return _remote;
        }
        set => _remote = value;
    }
	private string _remoteDir = string.Empty;

    public string RemoteDir
    {
	    get
	    {
		    if (!string.IsNullOrEmpty(_remoteDir) && Directory.Exists(_remoteDir))
			    return _remoteDir;
		    string tempDir = @"\\palych\Install\VSSoft\appsettings\";
		    if (Directory.Exists(tempDir))
			    _remoteDir = tempDir;
		    else
		    {
			    tempDir = @"h:\Install\VSSoft\appsettings\";
			    if (Directory.Exists(tempDir))
				    _remoteDir = tempDir;
		    }
		    return _remoteDir;
	    }
    }

    public string FileNameDevelopAleksandrov => "appsettings.DevelopAleksandrov.json";
    public string FileNameDevelopMorozov => "appsettings.DevelopMorozov.json";
    public string FileNameDevelopVs => "appsettings.DevelopVS.json";
    public string FileNameReleaseAleksandrov => "appsettings.ReleaseAleksandrov.json";
    public string FileNameReleaseMorozov => "appsettings.ReleaseMorozov.json";
    public string FileNameReleaseVs => "appsettings.ReleaseVS.json";
    public string JsonFileName =>
        DebugHelper.Instance.Config switch {
            WsConfiguration.DevelopAleksandrov => FileNameDevelopAleksandrov,
            WsConfiguration.DevelopMorozov => FileNameDevelopMorozov,
            WsConfiguration.DevelopVS => FileNameDevelopVs,
            WsConfiguration.ReleaseAleksandrov => FileNameReleaseAleksandrov,
            WsConfiguration.ReleaseMorozov => FileNameReleaseMorozov,
            WsConfiguration.ReleaseVS => FileNameReleaseVs,
            _ => FileNameDevelopVs,
        };
    //public string BlazorSubDir => DebugHelper.Instance.IsDevelop ? @"bin\x64\Debug\net7.0\" : @"bin\x64\Release\net7.0\";
    public string BlazorSubDir => DebugHelper.Instance.IsDevelop ? @"bin\Develop\x64\net7.0\" : @"bin\Release\x64\net7.0\";

	#endregion
}