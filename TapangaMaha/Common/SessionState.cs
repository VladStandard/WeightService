using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EntitiesLib;
using Hardware.Zebra;
using Hardware.Zpl;
using log4net;

namespace  TapangaMaha.Common
{
    public class SessionState
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Lazy<SessionState> _instance = new Lazy<SessionState>(() => new SessionState() );
        public static SessionState Instance => _instance.Value;

        public bool IsAdmin
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        private SessionState()
        {
            this.ID = Properties.Settings.Default.CurrentScaleId;
            SqlConnectFactory.GetConnection(Properties.Settings.Default.ConnectionString.ToString());
            this.CurrentScale = new ScaleEntity(this.ID);
            this.CurrentScale.Load();

            this.PluList = new List<PluEntity>();
            this.PluList = PluEntity.GetPLUList(this.CurrentScale);

            this.Kneading = KneadingMinValue;
            this.ProductDate = DateTime.Now;

            // контейнер пока не используем
            // оставим для бурного роста
            //ZebraDeviceСontainer = ZebraDeviceСontainer.Instance;
            //ZebraDeviceСontainer.AddDevice(this.CurrentScale.ZebraIP, this.CurrentScale.ZebraPort);
            //ZebraDeviceСontainer.CheckDeviceStatusOn();
            // создаем устройство ZEBRA
            // с необходимым крннектором (т.е. TCP, а можно и через USB)
            DeviceSocketTcp zplDeviceSocket =
                new DeviceSocketTcp(this.CurrentScale.ZebraPrinter.Ip, this.CurrentScale.ZebraPrinter.Port);
            zebraDeviceEntity = new DeviceEntity(zplDeviceSocket, Guid.NewGuid());
            // тут запускается поток 
            // который разбирает очередь 
            // т.к. команды пишутся не напрямую, а в очередь
            // а из нее потом доправляются на устройство
            zebraDeviceEntity.CheckDeviceStatusOn();

            // тут запускается процесс отправляющий комманды проверки состояния устройства
            ZplCommander = new ZplCommander(zplDeviceSocket.DeviceIP, zebraDeviceEntity, ZplPipeClass.ZplHostQuery());
        }
        //public ZebraDeviceСontainer ZebraDeviceСontainer { get; private set; }

        public ZplCommander ZplCommander { get; private set; }

        public DeviceEntity zebraDeviceEntity { get; private set; }




        public List<PluEntity> PluList = null;
        public int ID { get; private set; }

        public static readonly int KneadingMaxValue = 120;
        public static readonly int KneadingMinValue = 1;

        public static readonly int PalletSizeMaxValue = 130;
        public static readonly int PalletSizeMinValue = 1;

        public static readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+3);
        public static readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-1);


        public Int32 Kneading { get; set; }
        public DateTime ProductDate { get; set; } = DateTime.Now;
        public Int32 PalletSize { get; set; }


        [XmlElement(IsNullable = true)]
        public PluEntity CurrentPLU { get; set; }

        [XmlElement(IsNullable = true)]
        public ScaleEntity CurrentScale { get; set; }

        [XmlElement(IsNullable = true)]
        public WeighingFactEntity CurrentWeighingFact { get; set; }




    }
}
