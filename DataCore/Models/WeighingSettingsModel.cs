// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Protocols;
using DataCore.Sql.TableScaleModels.Printers;
using MDSoft.BarcodePrintUtils.Enums;

namespace DataCore.Models;

public class WeighingSettingsModel
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
	public WeighingSettingsModel()
    {
        Kneading = KneadingMinValue;
        LabelsCountMain = LabelsCountMinValue;
    }

    #endregion

    #region Public and private methods

    public string GetPrintName(bool isMain, PrintBrand printBrand)
    {
        return isMain
            ? printBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameMainZebra,
                PrintBrand.TSC => LocaleCore.Print.NameMainTsc,
                _ => LocaleCore.Print.DeviceName
            }
            : printBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameShippingZebra,
                PrintBrand.TSC => LocaleCore.Print.NameShippingTsc,
                _ => LocaleCore.Print.DeviceNameIsUnavailable
            };
    }

    public string GetPrintDescription(bool isMain, PrintBrand printBrand, PrinterModel printer, int scaleCounter,
        string deviceStatus, int labelPrintedCount, byte labelCount) =>
        $"{GetPrintName(isMain, printBrand)} | " +
        $"{LocaleCore.Table.DeviceIp}: {printer.Ip} | " +
        $"{LocaleCore.Table.Status}: {NetUtils.GetPingStatus(printer.PingStatus)} | " +
        $"{LocaleCore.Table.LabelCounter}: {scaleCounter} | " +
        $"{deviceStatus} | " +
        $"{LocaleCore.Scales.Labels}: {labelPrintedCount} {LocaleCore.Strings.From} {labelCount}";

    #endregion
}