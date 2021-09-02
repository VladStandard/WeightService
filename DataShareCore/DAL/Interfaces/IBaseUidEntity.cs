// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Entities;
using System;

namespace DataShareCore.DAL.Interfaces
{
    public interface IBaseUidEntity
    {
        Guid Uid { get; set; }

        object Clone();
        bool Equals(BaseUidEntity entity);
        bool Equals(object obj);
        bool EqualsDefault();
        int GetHashCode();
        string ToString();
    }
}