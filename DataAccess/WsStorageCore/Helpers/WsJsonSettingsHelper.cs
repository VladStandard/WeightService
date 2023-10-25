namespace WsStorageCore.Helpers;

public sealed class WsJsonSettingsHelper
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
    public string FileNameReleaseVs => "appsettings.ReleaseVS.json";
    public string JsonFileName => WsDebugHelper.Instance.Config switch {
        WsEnumConfiguration.DevelopAleksandrov => FileNameDevelopAleksandrov,
        WsEnumConfiguration.DevelopMorozov => FileNameDevelopMorozov,
        WsEnumConfiguration.DevelopVS => FileNameDevelopVs,
        WsEnumConfiguration.ReleaseVS => FileNameReleaseVs,
        _ => FileNameDevelopVs };
    public string BinNetSubDir => WsDebugHelper.Instance.IsDevelop ? @"bin\Develop_x64\net7.0\" : @"bin\Release_x64\net7.0\";

	#endregion
}