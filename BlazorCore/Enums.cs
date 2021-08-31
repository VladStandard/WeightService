// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore
{
    public enum EnumLang
    { 
        English,
        Russian
    }

    public enum EnumTableSystem
    {
        Accesses,
        Logs,
    }

    public enum EnumTableScale
    {
        BarcodeTypes,
        Contragents,
        Hosts,
        Labels,
        Nomenclatures,
        OrderStatuses,
        OrderTypes,
        Orders,
        Plus,
        Printers,
        PrinterResources,
        PrinterTypes,
        ProductSeries,
        ProductionFacilities,
        Scales,
        TemplateResources,
        Templates,
        WeithingFacts,
        Workshops,
    }

    public enum EnumAccessRights
    {
        Admin,
        User,
        Guest
    }

    public enum EnumTableAction
    {
        New,
        Edit,
        Copy,
        Mark,
        Delete,
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

    public enum EnumDataLoad
    {
        None,
        Loading,
        Success,
        Error,
    }

    public enum EnumDb
    {
        Debug,
        Release,
    }

    public enum EnumField
    {
        Uid,
        Id,
        Name,
        Value,
        Description,
        ScaleId,
        CategoryId,
        PrinterId,
        Title,
        CreateDate,
        ModifiedDate,
        Type,
        Plu,
        Marked,
        GoodsName,
        WeithingDate
    }

    public enum EnumOrderDirection
    {
        Asc,
        Desc
    }
}
