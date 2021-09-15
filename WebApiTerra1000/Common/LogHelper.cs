// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System.IO;
    //using System.Threading;
    //namespace WebApiTerra1000.Common
    //{
    //    public class LogHelper
    //    {
    //        #region Design pattern "Lazy Singleton"

//        private static LogHelper _instance;
//        public static LogHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

//        #endregion

//        #region Public and private fields and properties

//        public string FileName { get; } = "WebApiTerra1000.log";
//        public string PathName => $"{Directory.GetCurrentDirectory()}\\{FileName}";
//        public FileStream FileStream { get; }
//        public StreamWriter StreamWriter { get; }

//        #endregion

//        #region Constructor and destructor

//        public LogHelper()
//        {
//            FileStream = new FileStream(PathName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
//            StreamWriter = new StreamWriter(FileStream);
//        }

//        ~LogHelper()
//        {
//            StreamWriter?.Close();
//            FileStream?.Close();
//        }

//        #endregion

//        #region Public and private methods

//        public void Write(string value)
//        {
//            StreamWriter.WriteLine(value);
//        }

//        #endregion
//    }
//}