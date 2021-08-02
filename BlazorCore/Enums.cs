namespace BlazorCore
{
    public enum EnumLang
    { 
        Eng,
        Rus
    }

    public enum EnumTable
    {
        Default,
        BarCodeTypes,
        Contragents,
        Hosts,
        Labels,
        Nomenclature,
        Orders,
        OrderStatus,
        OrderTypes,
        Plu,
        ProductionFacility,
        ProductSeries,
        Scales,
        SsccStorage,
        TemplateResources,
        Templates,
        WeithingFact,
        WorkShop,
        Printer,
        PrinterResourceRef,
        PrinterType,
        Logs,
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
