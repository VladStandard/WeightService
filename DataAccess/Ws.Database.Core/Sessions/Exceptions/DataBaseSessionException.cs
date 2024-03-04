namespace Ws.Database.Core.Sessions.Exceptions;

public class DataBaseSessionException()
    : Exception("Implicitly session usage not allowed. Please open session through UnitOfWorkFactory");