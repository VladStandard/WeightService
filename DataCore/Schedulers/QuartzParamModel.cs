// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Schedulers;

public class QuartzParamModel<T>
{
    public string Name { get; }
    public T Value { get; }

    public QuartzParamModel(string name, T value)
    {
        Name = name;
        Value = value;
    }
}
