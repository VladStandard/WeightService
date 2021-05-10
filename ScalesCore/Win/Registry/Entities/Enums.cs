// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace ScalesCore.Win.Registry.Entities
{
    public enum EnumRegRoot
    {
        /// <summary>
        /// HKEY_CLASSES_ROOT.
        /// </summary>
        ClassesRoot,
        /// <summary>
        /// HKEY_CURRENT_USER.
        /// </summary>
        CurrentUser,
        /// <summary>
        /// HKEY_LOCAL_MACHINE.
        /// </summary>
        LocalMachine,
        /// <summary>
        /// HKEY_USERS.
        /// </summary>
        Users,
        /// <summary>
        /// HKEY_CURRENT_CONFIG.
        /// </summary>
        CurrentConfig
    }
}