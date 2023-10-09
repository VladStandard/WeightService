using MDSoft.NetUtils;

namespace WsDataCore.Models;

public sealed class WsWeighingSettingsModel
{
    #region Public and private fields and properties

    private byte KneadingMinValue => 1;
    private byte KneadingMaxValue => 140;
    private short _kneading;
    public short Kneading
    {
        get => _kneading;
        set
        {
            if (value < KneadingMinValue)
                _kneading = KneadingMinValue;
            else if (value > KneadingMaxValue)
                _kneading = KneadingMaxValue;
            else
                _kneading = value;
        }
    }
    private byte LabelsCountMinValue => 1;
    private byte LabelsCountMaxValue => 130;
    private byte _labelsCountMain;
    public byte LabelsCountMain
    {
        get => _labelsCountMain;
        set
        {
            if (value < KneadingMinValue)
                _labelsCountMain = LabelsCountMinValue;
            else if (value > KneadingMaxValue)
                _labelsCountMain = LabelsCountMaxValue;
            else
                _labelsCountMain = value;
        }
    }

    public WsWeighingSettingsModel()
    {
        Kneading = KneadingMinValue;
        LabelsCountMain = LabelsCountMinValue;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Получить наименование принтера.
    /// </summary>
    private string GetPrintName(WsEnumPrintModel printBrand) =>
        printBrand switch
        {
            WsEnumPrintModel.Zebra => WsLocaleCore.Print.NameMainZebra,
            WsEnumPrintModel.Tsc => WsLocaleCore.Print.NameMainTsc,
            _ => WsLocaleCore.Print.DeviceName
        };

    public string GetPrintDescription(MdPrinterModel printer,
        bool isConnected, int scaleCounter, int labelPrintedCount, byte labelCount) =>
        $"{printer.Name} | {printer.Ip} | " +
        $"{(isConnected ? "Подключен" : "Отключен")} | " +
        $"{WsLocaleCore.Table.LabelCounter}: {scaleCounter} | " +
        $"{WsLocaleCore.LabelPrint.Labels}: {labelPrintedCount} {WsLocaleCore.Strings.From} {labelCount}";

    #endregion
}