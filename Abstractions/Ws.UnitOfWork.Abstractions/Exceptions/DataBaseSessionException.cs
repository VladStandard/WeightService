namespace Ws.UnitOfWork.Abstractions.Exceptions;

public class DataBaseSessionException() 
    : Exception("Implicitly session usage not allowed. Please open session through UnitOfWorkFactory");