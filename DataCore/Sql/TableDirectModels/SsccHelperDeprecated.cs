// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.TableDirectModels;

//[Serializable]
//    public class SsccHelper
//    {
//        #region Design pattern "Singleton"

//        private readonly Lazy<SsccHelper> _instance = new Lazy<SsccHelper>(() => new SsccHelper());
//        public SsccHelper Instance => _instance.Value;
//        protected SsccHelper()
//        {
//            if (_ssccStack.Count() < 5)
//            {
//                RequestArraySerialShippingContainerCodes(GLN, CashSize);
//            }
//        }

//        #endregion

//        #region Private fields and properties

//        // Помощник SQL.
//        private SqlHelper _sql { get; set; } = SqlHelper.Instance;
//        private const string GLN = "460710023";
//        private const int CashSize = 20;
//        private readonly Stack<SsccEntity> _ssccStack = new Stack<SsccEntity>();

//        #endregion

//        #region Public methods

//        public SsccEntity GetSerialShippingContainerCode()
//        {
//            try
//            {
//                if (_ssccStack.Count() < 5)
//                {
//                    RequestArraySerialShippingContainerCodes(GLN, CashSize);
//                }
//                return _ssccStack.Pop();

//            }
//            catch
//            {
//                return null;
//            }
//        }

//        public void RequestArraySerialShippingContainerCodes(string gln, short el = 10, byte unitType = 1)
//        {
//            using (var con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query = "EXECUTE [db_sscc].[GetSSCCList] (@GLN, @UnitType, @Count);";
//                using (var cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@GLN", gln);
//                    cmd.Parameters.AddWithValue("@UnitType", unitType);
//                    cmd.Parameters.AddWithValue("@Count", el);

//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            var item = new SsccEntity()
//                            {
//                                SSCC = reader.GetString(0),
//                                GLN = reader.GetString(1),
//                                UnitID = reader.GetInt32(2),
//                                UnitType = reader.GetByte(3),
//                                SynonymSSCC = reader.GetString(4),
//                                Check = reader.GetInt32(5)
//                            };
//                            _ssccStack.Push(item);
//                        }
//                    }
//                }
//                con.Close();
//            }
//        }

//        //var binding = new BasicHttpBinding()
//        //{
//        //    Name = "vsExchangeSoapBinding",
//        //    MaxBufferSize = 2147483647,
//        //    MaxReceivedMessageSize = 2147483647
//        //};

//        //binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
//        //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

//        //var client = new vsSSCCServiceRefer.vsExchangePortTypeClient(binding, new EndpointAddress(Settings.Default.ServerURI));

//        //if (!string.IsNullOrEmpty(Settings.Default.ServerUser))
//        //{
//        //    client.ClientCredentials.UserName.UserName = Settings.Default.ServerUser;
//        //    client.ClientCredentials.UserName.Password = Settings.Default.ServerPassword;
//        //}

//        ////vsSSCCServiceRefer.vsSSCCRequest request = new vsSSCCServiceRefer.vsSSCCRequest();
//        //var request = new vsSSCCServiceRefer.vsSSCCRequest();
//        //request.Count    = el.ToString();
//        //request.GLN      = GLN;
//        //request.UnitType = "0";
//        //request.Source   = "0";

//        //try
//        //{
//        //    string msg = "";
//        //    client.Open();
//        //    //vsSSCCServiceRefer.vsSSCCBarcode[] ssccList = client.GetSSCCBarcodeList(request, ref msg);
//        //    var ssccList = client.GetSSCCBarcodeList(request, ref msg);

//        //    if (ssccList is not null)
//        //    {
//        //        for (int i = 0; i < ssccList.Length; i++)
//        //        {
//        //            ssccStack.Push(ssccList[i].SSCC);
//        //        }
//        //    }
//        //}
//        //catch (Exception ex)
//        //{
//        //    log.Error(ex);
//        //}
//        //finally
//        //{
//        //    client.Close();
//        //}

//        #endregion
//    }
