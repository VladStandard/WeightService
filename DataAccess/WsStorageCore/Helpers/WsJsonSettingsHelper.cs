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
    
	#endregion
}