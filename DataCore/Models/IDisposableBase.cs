// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models;

public interface IDisposableBase
{
    public void Close();

    public void Dispose(bool disposing);

    public void ReleaseManaged();

    public void ReleaseUnmanaged();
}