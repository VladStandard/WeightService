// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;

namespace WeightCore.Helpers
{
    public class WeighingSettingsEntity
    {
        #region Public and private fields and properties

        private byte KneadingMinValue => 1;
        private byte KneadingMaxValue => 140;
        private byte _kneading;
        public byte Kneading
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

        //public void RotatePalletSize(ProjectsEnums.Direction direction)
        //{
        //    if (direction == ProjectsEnums.Direction.Left)
        //    {
        //        CurrentLabelsCountMain--;
        //        if (CurrentLabelsCountMain < LabelsCountMinValue)
        //            CurrentLabelsCountMain = LabelsCountMinValue;

        //    }
        //    if (direction == ProjectsEnums.Direction.Right)
        //    {
        //        CurrentLabelsCountMain++;
        //        if (CurrentLabelsCountMain > LabelsCountMaxValue)
        //            CurrentLabelsCountMain = LabelsCountMaxValue;
        //    }
        //}

        #endregion

        #region Constructor and destructor

        public WeighingSettingsEntity()
        {
            Kneading = KneadingMinValue;
            LabelsCountMain = LabelsCountMinValue;
        }

        #endregion

        #region Public and private methods

        public void RotateKneading(ProjectsEnums.Direction direction)
        {
            if (direction == ProjectsEnums.Direction.Left)
            {
                Kneading--;
                if (Kneading < KneadingMinValue)
                    Kneading = KneadingMinValue;
            }
            if (direction == ProjectsEnums.Direction.Right)
            {
                Kneading++;
                if (Kneading > KneadingMaxValue)
                    Kneading = KneadingMaxValue;
            }
        }

        #endregion
    }
}
