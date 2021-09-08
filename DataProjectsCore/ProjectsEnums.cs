// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataProjectsCore
{
    public class ProjectsEnums
    {
        public enum TaskType
        {
            DeviceManager,
            MassaManager,
            MemoryManager,
            PrintManager,
            ZabbixManager,
        }

        public enum DefaultSetting
        {
            All,
            ComPortName,
            SendTimeout,
            ReceiveTimeout,
            ZebraTcpAddress,
            ZebraTcpPort,
            Description,
            Guid,
            ConnectionString
        }

        public enum SilentUI
        {
            True,
            False,
        }

        public enum Direction
        {
            Forward,
            Back
        }

        public enum Page
        {
            Default,
            PluList,
            SqlSettings
        }

        public enum OrderStatus
        {
            New = 0,
            InProgress = 1,
            Paused = 2,
            Performed = 3,
            Canceled = 4
        }

        public enum TableSystem
        {
            Accesses,
            Logs,
        }

        public enum TableScale
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
    }
}
