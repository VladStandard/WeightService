// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace WeightCore.MassaK
{
    public class MassaConnectionException : Exception
    {
        public MassaConnectionException() : base("Failed connect to the scales") { }
        public MassaConnectionException(Exception e) : base("Failed connect to the scales", e) { }
    }
}
