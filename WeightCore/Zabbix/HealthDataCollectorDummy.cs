// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeightCore.Zabbix
{
    /// <summary>
    /// Сбор данных пустышка.
    /// </summary>
    public class HealthDataCollectorDummy : IHealthDataCollector
    {
        #region Public and private fields and properties

        public Dictionary<string, string> Dict { get; set; }

        public DateTime StartDateTime { get; }

        private int _requestCount;
        public int RequestCount { get => _requestCount; }

        #endregion

        #region Constructor and destructor

        public HealthDataCollectorDummy()
        {
            StartDateTime = DateTime.Now;
            _requestCount = 0;
            Dict = new Dictionary<string, string>();
        }

        #endregion

        #region Public and private methods

        [Obsolete(@"Используй ResponseStatus")]
        public string ResponseBuilderFunc()
        {
            return Response().ToString();
        }

        public StringBuilder Response()
        {
            StringBuilder result = new();
            foreach (KeyValuePair<string, string> v in Dict)
            {
                result.AppendLine($"{v.Key}={v.Value};");
            }
            result.AppendLine($"CurrentTime={DateTime.Now.ToString(CultureInfo.InvariantCulture)};");
            TimeSpan interval = DateTime.Now - StartDateTime;
            result.AppendLine($"TimePassed={interval};");
            result.AppendLine($"RequestCount={++_requestCount};");
            return result;
        }

        public void LoadValues()
        {
            Dict.Clear();
            string result = string.Empty;
            Random random = new();
            for (int i = 0; i < 100; i++)
            {
                int prob = random.Next(100);
                result = (prob <= 50) ? "OK" : "ERROR";
            }
            Dict.Add("Printer", result);
            for (int i = 0; i < 100; i++)
            {
                int prob = random.Next(100);
                result = (prob <= 80) ? "OK" : "ERROR";
            }
            Dict.Add("DataBase", result);
            for (int i = 0; i < 100; i++)
            {
                int prob = random.Next(100);
                result = (prob <= 20) ? "OK" : "ERROR";
            }
            Dict.Add("Platform", result);
        }

        #endregion
    }
}
