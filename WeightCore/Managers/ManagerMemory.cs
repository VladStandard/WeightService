// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Memory;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WeightCore.Helpers;
using LocalizationCore = DataCore.Localization.Core;

namespace WeightCore.Managers
{
    public class ManagerMemory : ManagerBase
    {
        #region Public and private fields and properties

        private Label FieldMemoryManagerTotal { get; set; }
        private Label FieldTasks { get; set; }
        private ProgressBar FieldMemoryProgress { get; set; }
        public MemorySizeEntity MemorySize { get; private set; }

        #endregion

        #region Constructor and destructor

        public ManagerMemory() : base()
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(Label fieldMemoryManagerTotal, Label fieldTasks, ProgressBar fieldMemoryProgress)
        {
            Init(ProjectsEnums.TaskType.MemoryManager,
                () =>
                {
                    MemorySize = new();
                    FieldMemoryManagerTotal = fieldMemoryManagerTotal;
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMemoryManagerTotal, $"{LocalizationCore.Scales.Memory}");
                    FieldTasks = fieldTasks;
                    FieldMemoryProgress = fieldMemoryProgress;
                },
                1_000, 5_000, 5_000, 1_000, 1_000);
        }

        public new void Open()
        {
            try
            {
                Open(
                () =>
                {
                    MemorySize.Open();
                    OpenMemory();
                },
                null,
                null);
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        private void OpenMemory()
        {
            if (SessionStateHelper.Instance.SqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMemoryManagerTotal,
                    $"{LocalizationCore.Scales.Memory} | " +
                    $"{LocalizationCore.Scales.MemoryFree}: " +
                        (MemorySize.PhysicalFree != null ? $"{MemorySize.PhysicalFree.MegaBytes:N0} MB" : $"- MB") +
                    $" | {LocalizationCore.Scales.MemoryBusy}: " + 
                        (MemorySize.PhysicalCurrent != null ? $"{MemorySize.PhysicalCurrent.MegaBytes:N0} MB" : $"- MB") +
                    $" | {LocalizationCore.Scales.MemoryAll}: " +
                        (MemorySize.PhysicalTotal != null ? $"{MemorySize.PhysicalTotal.MegaBytes:N0} MB" : $"- MB")
                    );

                MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(FieldMemoryProgress,
                    MemorySize.PhysicalTotal != null
                    ? (int)MemorySize.PhysicalTotal.MegaBytes : 0);
                MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(FieldMemoryProgress, 0);
                MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(FieldMemoryProgress,
                    MemorySize.PhysicalTotal != null && MemorySize.PhysicalFree != null
                    ? (int)(MemorySize.PhysicalTotal.MegaBytes - MemorySize.PhysicalFree.MegaBytes) : 0);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTasks, $"{LocalizationCore.Scales.Threads}: {Process.GetCurrentProcess().Threads.Count}");
            }
        }

        public new void Close()
        {
            base.Close();
        }

        public new void ReleaseManaged()
        {
            base.ReleaseManaged();

            if (MemorySize != null)
            {
                MemorySize.Close();
                MemorySize.Dispose(false);
                MemorySize = null;
            }
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();
        }

        #endregion
    }
}
