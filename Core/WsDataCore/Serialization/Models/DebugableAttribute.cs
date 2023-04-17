// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Serialization.Models;

[AttributeUsage(AttributeTargets.Property)]
public sealed class DebuggableAttribute : Attribute
{
    #region Public and private fields, properties, constructor

    public bool IsDebug { get; set; }

    public DebuggableAttribute()
    {
        IsDebug = false;
    }

    public DebuggableAttribute(bool isDebug)
    {
        IsDebug = isDebug;
    }

    #endregion
}
