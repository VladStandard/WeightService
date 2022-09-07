// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace WeightCore.Helpers;

public class WeighingSettingsEntity
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
    public byte CurrentLabelsCountShipping { get; set; }

	//public void RotatePalletSize(Direction direction)
	//{
	//    if (direction == Direction.Left)
	//    {
	//        CurrentLabelsCountMain--;
	//        if (CurrentLabelsCountMain < LabelsCountMinValue)
	//            CurrentLabelsCountMain = LabelsCountMinValue;

	//    }
	//    if (direction == Direction.Right)
	//    {
	//        CurrentLabelsCountMain++;
	//        if (CurrentLabelsCountMain > LabelsCountMaxValue)
	//            CurrentLabelsCountMain = LabelsCountMaxValue;
	//    }
	//}

	/// <summary>
	/// Constructor.
	/// </summary>
	public WeighingSettingsEntity()
    {
        Kneading = KneadingMinValue;
        LabelsCountMain = LabelsCountMinValue;
    }

    #endregion

    #region Public and private methods

    public void RotateKneading(DirectionEnum direction)
    {
        if (direction == DirectionEnum.Left)
        {
            Kneading--;
            if (Kneading < KneadingMinValue)
                Kneading = KneadingMinValue;
        }
        if (direction == DirectionEnum.Right)
        {
            Kneading++;
            if (Kneading > KneadingMaxValue)
                Kneading = KneadingMaxValue;
        }
    }

    #endregion
}
