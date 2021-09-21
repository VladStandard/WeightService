// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Sockets;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace WebApiTerra1000.Common
//{
//    public interface IZabbixSenderFactory
//    {
//        public ZabbixServer OpenSession();
//    }

//    public class ZabbixSenderFactory : IZabbixSenderFactory
//    {
//        private readonly ZabbixServer _archeType;

//        private static IZabbixSenderFactory _instance;
//        public static IZabbixSenderFactory Instance => LazyInitializer.EnsureInitialized(ref _instance);

//        public ZabbixServer OpenSession()
//        {
//            return new ZabbixServer(_archeType.Server, _archeType.Port, _archeType.Timeout);
//        }

//        public ZabbixSenderFactory() {}
//        public ZabbixSenderFactory(string server, int port = 10051, int timeOut = 500)
//        {
//            _archeType = new ZabbixServer(server, port, timeOut);
//        }

//    }

//    public class ZabbixServer : IDisposable
//    {
//        public string Server { get; set; }
//        public int Port { get; set; }
//        public int Timeout { get; set; }
//        public ZabbixServer(string server, int port = 10051, int timeOut = 500)
//        {
//            Server = server;
//            Port = port;
//            Timeout = timeOut;
//        }

//        public async void ZabbixNoticeAsync(string host, string key, IDictionary<string, string> value)
//        {
//            try { 
//                ZabbixRequest zabbixRequest = new ZabbixRequest(host, key, MyJsonConverter.Serialize<IDictionary<string, string>>(value));
//                ZabbixResponse resp = await zabbixRequest.SendAsync(this);
//            }
//            catch (Exception)
//            {
//                //
//            }
//        }

//        public void ZabbixNotice(string host, string key, IDictionary<string, string> value)
//        {
//            try
//            {
//                ZabbixRequest zabbixRequest = new ZabbixRequest(host, key, MyJsonConverter.Serialize<IDictionary<string, string>>(value));
//                ZabbixResponse resp = zabbixRequest.Send(this);
//            }
//            catch (Exception)
//            {
//                //
//            }
//        }

//        void IDisposable.Dispose()
//        {
//            //
//        }
//    }

//    [DataContract]
//    public class ZabbixData
//    {
//        [DataMember]
//        public string Host { get; set; }

//        [DataMember]
//        public string Key { get; set; }

//        [DataMember]
//        public string Value { get; set; }
//        public ZabbixData(string host, string key, string value)
//        {
//            Host = host;
//            Key = key;
//            Value = value;
//        }
//    }

//    [DataContract]
//    public class ZabbixResponse
//    {
//        [DataMember]
//        public string Response { get; set; }

//        [DataMember]
//        public string Info { get; set; }

//    }

//    [DataContract]
//    public class ZabbixRequest
//    {
//        [DataMember]
//        public string Request { get; set; }

//        [DataMember]
//        public ZabbixData[] Data { get; set; }

//        public ZabbixRequest(string zbxHost, string zbxKey, string zbxVal)
//        {
//            Request = "sender data";
//            Data = new ZabbixData[] { new ZabbixData(zbxHost, zbxKey, zbxVal) };
//        }

//        public async Task<ZabbixResponse> SendAsync(ZabbixServer zsrv)
//        {
//            ZabbixResponse x = await Task.Run(() => Send(zsrv));
//            return x;
//        }

//        public ZabbixResponse Send(ZabbixServer zsrv)
//        {
//            string jr = MyJsonConverter.Serialize<ZabbixRequest>(new ZabbixRequest(Data[0].Host, Data[0].Key, Data[0].Value));

//            using TcpClient lTcPc = new TcpClient(zsrv.Server, zsrv.Port);
//            using NetworkStream lStream = lTcPc.GetStream();
//            byte[] header = Encoding.ASCII.GetBytes("ZBXD\x01");
//            byte[] dataLen = BitConverter.GetBytes((long)jr.Length);
//            byte[] content = Encoding.ASCII.GetBytes(jr);
//            byte[] message = new byte[header.Length + dataLen.Length + content.Length];
//            Buffer.BlockCopy(header, 0, message, 0, header.Length);
//            Buffer.BlockCopy(dataLen, 0, message, header.Length, dataLen.Length);
//            Buffer.BlockCopy(content, 0, message, header.Length + dataLen.Length, content.Length);

//            lStream.Write(message, 0, message.Length);
//            lStream.Flush();
//            int counter = 0;
//            while (!lStream.DataAvailable)
//            {
//                if (counter < zsrv.Timeout / 50)
//                {
//                    counter++;
//                    _ = Task.Delay(50);
//                }
//                else
//                {
//                    // throw new TimeoutException();
//                }
//            }

//            byte[] resbytes = new byte[1024];
//            lStream.Read(resbytes, 0, resbytes.Length);
//            string s = Encoding.UTF8.GetString(resbytes);

//            int pFrom = s.IndexOf("{");
//            int pTo = s.LastIndexOf("}") + 1;

//            string jsonRes = s.Substring(pFrom, pTo - pFrom);
//            return MyJsonConverter.Deserialize<ZabbixResponse>(jsonRes);
//        }
//    }

//    public static class MyJsonConverter
//    {
//        public static T Deserialize<T>(string body)
//        {
//            using (MemoryStream stream = new MemoryStream())
//            using (StreamWriter writer = new StreamWriter(stream))
//            {
//                writer.Write(body);
//                writer.Flush();
//                stream.Position = 0;
//                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
//            }
//        }

//        public static string Serialize<T>(T item)
//        {
//            using (MemoryStream ms = new MemoryStream())
//            {
//                new DataContractJsonSerializer(typeof(T)).WriteObject(ms, item);
//                return Encoding.Default.GetString(ms.ToArray());
//            }
//        }
//    }
//}