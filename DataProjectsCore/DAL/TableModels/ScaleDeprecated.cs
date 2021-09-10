//using System;
//using System.IO;
//using System.Net;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using System.Text;
//using System.Xml.Serialization;

//namespace DataProjectsCore.DAL.Entities
//{
//    [Serializable]
//    public class ScaleEntity
//    {
//        // Помощник приложения.
//        [XmlIgnoreAttribute]
//        private readonly AppHelper _app = AppHelper.Instance;

//        // Помощник SQL.
//        [XmlIgnoreAttribute]
//        private readonly SqlHelper _sql = SqlHelper.Instance;

//        //// Помощник GUID.
//        //[XmlIgnoreAttribute]
//        //public GuidHelper Guid { get; private set; } = GuidHelper.Instance;


//        #region Constructor

//        public ScaleEntity()
//        {
//            DeviceId = default(int);
//            Description = "";
//            RRefID = "";
//            DeviceIP = GetLocalIPAddress();
//            DevicePort = 5001;
//            DeviceMAC = GetMacAddress();
//            DeviceSendTimeout = 500;
//            DeviceReceiveTimeout = 1000;
//            DeviceComPort = "COM4";
//            ZebraIP = "192.168.7.127";
//            ZebraPort = 9100;
//            UseOrder = default(byte);
//            VerScalesUI = "";
//            //DeviceNumber = default(int);
//            FieldCount = 13;
//        }

//        public ScaleEntity(string guid) : this()
//        {
//            RRefID = guid;
//        }

//        public ScaleEntity(Guid guid) : this(guid.ToString()) { }

//        #endregion

//        #region Public fields and properties

//        //public TableField<int> DeviceId { get; set; }
//        //public TableField<string> Description { get; set; }
//        //public TableField<string> RRefID { get; set; }
//        //public TableField<string> DeviceIP { get; set; }
//        //public TableField<short> DevicePort { get; set; }
//        //public TableField<string> DeviceMAC { get; set; }
//        //public TableField<short> DeviceSendTimeout { get; set; }
//        //public TableField<short> DeviceReceiveTimeout { get; set; }
//        //public TableField<string> DeviceComPort { get; set; }
//        public int DeviceId { get; set; }
//        public string Description { get; set; }
//        public string RRefID { get; set; }
//        public string DeviceIP { get; set; }
//        public short DevicePort { get; set; }
//        public string DeviceMAC { get; set; }
//        public short DeviceSendTimeout { get; set; }
//        public short DeviceReceiveTimeout { get; set; }
//        public string DeviceComPort { get; set; }

//        ///// <summary>
//        ///// IP-адрес Зебры.
//        ///// </summary>
//        //public TableField<string> ZebraIP { get; set; }
//        ///// <summary>
//        ///// Порт Зебры.
//        ///// </summary>
//        //public TableField<short> ZebraPort { get; set; }
//        ///// <summary>
//        ///// Использовать заказ.
//        ///// </summary>
//        //public TableField<byte> UseOrder { get; set; }
//        //public TableField<string> _verScalesUI;
//        ///// <summary>
//        ///// Версия ScalesUI.
//        ///// </summary>
//        //public TableField<string> VerScalesUI 
//        //{
//        //    get => _verScalesUI;
//        //    set =>
//        //        _verScalesUI = string.IsNullOrEmpty(value.Value) 
//        //            ? new TableField<string>(value.Name, _app.GetCurrentVersion(ShareEnums.AppVerCountDigits.Use3)) 
//        //            : new TableField<string>(value.Name, value.Value);
//        //}
//        /// <summary>
//        /// IP-адрес Зебры.
//        /// </summary>
//        public string ZebraIP { get; set; }
//        /// <summary>
//        /// Порт Зебры.
//        /// </summary>
//        public short ZebraPort { get; set; }
//        /// <summary>
//        /// Использовать заказ.
//        /// </summary>
//        public byte UseOrder { get; set; }
//        public string _verScalesUI;
//        /// <summary>
//        /// Версия ScalesUI.
//        /// </summary>
//        public string VerScalesUI
//        {
//            get => _verScalesUI;
//            set =>
//                _verScalesUI = string.IsNullOrEmpty(value)
//                    ? _app.GetCurrentVersion(ShareEnums.AppVerCountDigits.Use3)
//                    : value;
//        }
//        //public int DeviceNumber { get; set; }
//        /// <summary>
//        /// Количество полей.
//        /// </summary>
//        public int FieldCount { get; }

//        #endregion

//        #region Public methods

//        /// <summary>
//        /// Получить локальный IP-адрес.
//        /// Редактировал: 2020-06-24 Морозов Дамиан.
//        /// </summary>
//        /// <returns></returns>
//        public string GetLocalIPAddress()
//        {
//            var host = Dns.GetHostEntry(Dns.GetHostName());
//            foreach (var ip in host.AddressList)
//            {
//                if (ip.AddressFamily == AddressFamily.InterNetwork)
//                {
//                    return ip.ToString();
//                }
//            }
//            throw new Exception("No network adapters with an IPv4 address in the system!");
//        }

//        /// <summary>
//        /// Получить MAC-адрес.
//        /// Редактировал: 2020-06-24 Морозов Дамиан.
//        /// </summary>
//        /// <returns></returns>
//        public string GetMacAddress()
//        {
//            string macAddresses = string.Empty;
//            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
//            {
//                if (nic.OperationalStatus == OperationalStatus.Up)
//                {
//                    macAddresses += nic.GetPhysicalAddress().ToString();
//                    break;
//                }
//            }
//            return macAddresses;
//        }

//        public string SerializeObject()
//        {
//            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScaleEntity));
//            using (StringWriter textWriter = new StringWriter())
//            {
//                xmlSerializer.Serialize(textWriter, this);
//                return textWriter.ToString();
//            }
//        }

//        /// <summary>
//        /// Обновить.
//        /// Редактировал: 2020-07-14 Морозов Дамиан.
//        /// </summary>
//        /// <returns></returns>
//        public int Update()
//        {
//            int result = int.MinValue;
//            try
//            {
//                if (Guid.Exists())
//                {
//                    using (var con = new SqlConnection(_sql.ConnectionString))
//                    {
//                        using (var cmd = new SqlCommand(UpdateQuery()))
//                        {
//                            cmd.Connection = con;
//                            cmd.Parameters.AddWithValue($"@Uuid", RRefID);  // @1CRRefID
//                            cmd.Parameters.AddWithValue($"@{nameof(Description)}", Description);
//                            cmd.Parameters.AddWithValue($"@IP", DeviceIP);
//                            cmd.Parameters.AddWithValue($"@Port", DevicePort);
//                            cmd.Parameters.AddWithValue($"@{nameof(DeviceMAC)}", DeviceMAC);
//                            cmd.Parameters.AddWithValue($"@{nameof(DeviceSendTimeout)}", DeviceSendTimeout);
//                            cmd.Parameters.AddWithValue($"@{nameof(DeviceReceiveTimeout)}", DeviceReceiveTimeout);
//                            cmd.Parameters.AddWithValue($"@{nameof(DeviceComPort)}", DeviceComPort);
//                            cmd.Parameters.AddWithValue($"@{nameof(ZebraIP)}", ZebraIP);
//                            cmd.Parameters.AddWithValue($"@{nameof(ZebraPort)}", ZebraPort);
//                            cmd.Parameters.AddWithValue($"@{nameof(UseOrder)}", UseOrder);
//                            cmd.Parameters.AddWithValue($"@{nameof(VerScalesUI)}", VerScalesUI);
//                            cmd.Parameters.AddWithValue($"@DeviceNumber", DeviceId);
//                            con.Open();
//                            result = Convert.ToInt32(cmd.ExecuteScalar());
//                            //DeviceId.Value = result;
//                            DeviceId = result;
//                            con.Close();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Ошибка обновления данных таблицы Scales!" + Environment.NewLine + ex.Message);
//            }
//            return result;
//        }

//        /// <summary>
//        /// Загрузить данные таблицы Scales.
//        /// Редактировал: 2020-07-14 Морозов Дамиан.
//        /// </summary>
//        /// <param name="guid"></param>
//        /// <param name="silentUI"></param>
//        /// <returns></returns>
//        public void Load(string guid, ProjectsEnums.SilentUI silentUI = ProjectsEnums.SilentUI.True)
//        {
//            try
//            {
//                _sql.Open(ShareEnums.SettingsStorage.UseRegistry);
//                using (var con = new SqlConnection(_sql.ConnectionString))
//                {
//                    using (var cmd = new SqlCommand(LoadQuery()))
//                    {
//                        var description = "Табличная функция [GetScaleByID].";
//                        cmd.Connection = con;
//                        cmd.Parameters.AddWithValue("@ScaleID", guid ?? string.Empty);
//                        con.Open();
//                        var reader = cmd.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            while (reader.Read())
//                            {
//                                if (reader.FieldCount >= FieldCount)
//                                {
//                                    DeviceId = _sql.GetValue<int>("ID", reader, silentUI, description);
//                                    Description = _sql.GetValue<string>(nameof(Description), reader, silentUI, description);
//                                    RRefID = _sql.GetValue<string>("1CRRefID", reader, silentUI, description);  // Uuid
//                                    DeviceIP = _sql.GetValue<string>(nameof(DeviceIP), reader, silentUI, description);
//                                    DevicePort = _sql.GetValue<short>(nameof(DevicePort), reader, silentUI, description);
//                                    DeviceMAC = _sql.GetValue<string>(nameof(DeviceMAC), reader, silentUI, description);
//                                    DeviceSendTimeout = _sql.GetValue<short>(nameof(DeviceSendTimeout), reader, silentUI, description);
//                                    DeviceReceiveTimeout = _sql.GetValue<short>(nameof(DeviceReceiveTimeout), reader, silentUI, description);
//                                    DeviceComPort = _sql.GetValue<string>(nameof(DeviceComPort), reader, silentUI, description);
//                                    ZebraIP = _sql.GetValue<string>(nameof(ZebraIP), reader, silentUI, description);
//                                    ZebraPort = _sql.GetValue<short>(nameof(ZebraPort), reader, silentUI, description);
//                                    UseOrder = _sql.GetValue<byte>(nameof(UseOrder), reader, silentUI, description);
//                                    VerScalesUI = _sql.GetValue<string>(nameof(VerScalesUI), reader, silentUI, description);
//                                    //DeviceNumber = _sql.GetValue<int>(nameof(DeviceNumber), reader, silentUI, description);
//                                }
//                            }
//                        }
//                        reader.Close();
//                        con.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Ошибка загрузки данных таблицы Scales!" + Environment.NewLine + ex.Message);
//            }
//        }

//        /// <summary>
//        /// Запрос загрузки данных таблицы Scales.
//        /// Редактировал: 2020-06-24 Морозов Дамиан.
//        /// </summary>
//        /// <returns></returns>
//        public string LoadQuery()
//        {
//            return string.Format(@"
//-- Загрузить данные таблицы Scales.
//-- Версия 0.0.20.
//SELECT
//	 [ID]
//	,[Description]
//	,[1CRRefID]
//	,[DeviceIP]
//	,[DevicePort]
//	,[DeviceMAC]
//	,[DeviceSendTimeout]
//	,[DeviceReceiveTimeout]
//	,[DeviceComPort]
//	,[ZebraIP]
//	,[ZebraPort]
//	,[UseOrder]
//	,[VerScalesUI]
//FROM [db_scales].[GetScaleByID] (@ScaleID)
//                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n'));
//        }

//        //public void Load()
//        //{
//        //    Load(Settings.Default.ScalesGuid);
//        //}

//        /// <summary>
//        /// Отправить на принтер.
//        /// Редактировал: 2020-06-24 Морозов Дамиан.
//        /// </summary>
//        /// <param name="zplCommand"></param>
//        public void SendToPrinter(string[] zplCommand)
//        {
//            SendToPrinter(ZebraIP, ZebraPort, zplCommand);
//        }

//        /// <summary>
//        /// Отправить на принтер.
//        /// Редактировал: 2020-06-24 Морозов Дамиан.
//        /// </summary>
//        /// <param name="ipAddress"></param>
//        /// <param name="port"></param>
//        /// <param name="zplCommand"></param>
//        public void SendToPrinter(string ipAddress, int port, string[] zplCommand)
//        {
//            var client = new TcpClient();
//            client.Connect(ipAddress, port);
//            var stream = client.GetStream();

//            foreach (var commandLine in zplCommand)
//            {
//                stream.Write(Encoding.ASCII.GetBytes(commandLine), 0, commandLine.Length);
//                stream.Flush();
//            }

//            stream.Close();
//            client.Close();
//        }

//        #endregion
//    }
//}
