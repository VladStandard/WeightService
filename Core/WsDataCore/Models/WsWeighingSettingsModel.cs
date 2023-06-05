// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsPrintCore.Common;

namespace WsDataCore.Models;

public class WsWeighingSettingsModel
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
    private byte _labelsCountShipping;
    public byte LabelsCountShipping
    {
        get => _labelsCountShipping;
        set
        {
            if (value < KneadingMinValue)
                _labelsCountShipping = LabelsCountMinValue;
            else if (value > KneadingMaxValue)
                _labelsCountShipping = LabelsCountMaxValue;
            else
                _labelsCountShipping = value;
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsWeighingSettingsModel()
    {
        Kneading = KneadingMinValue;
        LabelsCountMain = LabelsCountMinValue;
    }

    #endregion

    #region Public and private methods

    public string GetPrintName(bool isMain, WsEnumPrintModel printBrand) => isMain
        ? printBrand switch
        {
            WsEnumPrintModel.Zebra => LocaleCore.Print.NameMainZebra,
            WsEnumPrintModel.Tsc => LocaleCore.Print.NameMainTsc,
            _ => LocaleCore.Print.DeviceName
        }
        : printBrand switch
        {
            WsEnumPrintModel.Zebra => LocaleCore.Print.NameShippingZebra,
            WsEnumPrintModel.Tsc => LocaleCore.Print.NameShippingTsc,
            _ => LocaleCore.Print.DeviceNameIsUnavailable
        };

    public string GetPrintDescription(bool isMain, WsEnumPrintModel printBrand, MdPrinterModel printer,
        bool isConnected, int scaleCounter, string deviceStatus, int labelPrintedCount, byte labelCount) =>
        $"{GetPrintName(isMain, printBrand)} | {printer.Ip} | " +
        //$"{LocaleCore.Table.Status}: {MdNetUtils.GetPingStatus(printer.PingStatus)} | " +
        $"{LocaleCore.Table.Status}: {(isConnected ? MdNetLocalization.Instance.StatusSuccess : MdNetLocalization.Instance.StatusUnknown)} | " +
        $"{LocaleCore.Table.LabelCounter}: {scaleCounter} | " +
        $"{deviceStatus} | " +
        $"{LocaleCore.Scales.Labels}: {labelPrintedCount} {LocaleCore.Strings.From} {labelCount}";

    #endregion
}