// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

public enum SqlFieldIdentityEnum
{
	Empty,
    Id,
    Uid,
	Test,
}

public enum SqlFieldComparerEnum
{
	Empty,
	Equal,
	NotEqual,
	More,
	Less,
}

public enum SqlFieldOrderEnum
{
	Empty,
	Asc,
	Desc
}
