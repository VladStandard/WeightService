// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Interfaces;

// ReSharper disable once InconsistentNaming
public interface ISqlTable1c : ISqlTable
{
   public Guid Uid1c { get; set; }

   public void UpdateProperties(ISqlTable1c item);
}