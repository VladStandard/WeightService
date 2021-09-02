// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MdmControlCore
{
    public enum EnumTable
    {
        Default,
        NomenclatureNonNormalize,
        NomenclatureMaster,
        Nomenclature,
        InformationSystem,
    }

    public enum EnumAccessRights
    {
        Admin,
        User,
        Guest
    }

    public enum EnumTableAction
    {
        Add,
        Edit,
        Copy,
        Delete,
        Marked,
    }

    public enum EnumMemoryLimitAction
    {
        Exit,
        Restart
    }

    public enum EnumRelevanceStatus
    {
        Unknown = 0,
        Actual = 1,
        NoActual = 2,
    }

    public enum EnumNormilizationStatus
    {
        NotNormilized = 0,
        NormilizedFull = 1,
        NormilizedPart = 2,
        NotSubjectNormalization = 3,
    }
}
