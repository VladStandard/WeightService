namespace DeviceControl.Core.Models
{
    public enum EnumTable
    {
        Default,
        AttributeDefinationList,
        AttributeValues,
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
        ZebraPrinter,
        ZebraPrinterResourceRef,
        ZebraPrinterType,
        PrinterType,
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
}
