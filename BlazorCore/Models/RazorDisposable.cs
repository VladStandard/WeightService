// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace BlazorCore.Models
{
    public class RazorDisposable : RazorBase, IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            lock (this)
            {
                DialogService?.Dispose();
                TooltipService?.Dispose();
                //AppSettings.HotKeysContextItem?.Dispose();
                //AppSettings.CoreSettings = null;
                //AppSettings.IdentityItem = null;
                //AppSettings.HotKeysItem.DisposeAsync();
                //AppSettings.HotKeysContextItem.Dispose();
                //AppSettings.Memory.Close();
                //AppSettings.Memory = null;

                // Disable the garbage collector from calling the destructor.
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
