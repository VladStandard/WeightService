namespace Ws.Database.EntityFramework.Models;

/// <summary>
/// LOGS_WEB_SERVICES reference
/// </summary>
public partial class LogsWebService
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime StampDt { get; set; }

    public string Version { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string DataRequest { get; set; } = null!;

    public string DataResponse { get; set; } = null!;

    public int CountAll { get; set; }

    public int CountSuccess { get; set; }

    public int CountError { get; set; }
}
