// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsScheduleCore.Models;

public class WsQuartzParamModel<T>
{
    public string Name { get; }
    public T Value { get; }

    public WsQuartzParamModel(string name, T value)
    {
        Name = name;
        Value = value;
    }
}