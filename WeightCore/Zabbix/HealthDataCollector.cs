// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeightCore.Zabbix
{
    /// <summary>
    /// Сбор данных.
    /// </summary>
    public class HealthDataCollector : IHealthDataCollector
    {
        #region Public and private fields and properties

        public Dictionary<string, string> Dict { get; set; }

        public DateTime StartDateTime { get; }

        public int RequestCount { get ; private set; }

        #endregion

        #region Constructor and destructor

        public HealthDataCollector()
        {
            StartDateTime = DateTime.Now;
            RequestCount = 0;
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
            var result = new StringBuilder();
            foreach (var v in Dict)
            {
                result.AppendLine($"{v.Key}={v.Value};");
            }
            result.AppendLine($"CurrentTime={DateTime.Now.ToString(CultureInfo.InvariantCulture)};");
            var interval = DateTime.Now - StartDateTime;
            result.AppendLine($"TimePassed={interval};");
            result.AppendLine($"RequestCount={++RequestCount};");
            return result;
        }

        public void LoadValues(bool dbOk, bool zebraOk, bool massaOk)
        {
            Dict.Clear();
            Dict.Add("DataBase", dbOk ? "OK" : "ERROR");
            Dict.Add("Zebra", zebraOk ? "OK" : "ERROR");
            Dict.Add("Massa-K", massaOk ? "OK" : "ERROR");
        }

        #endregion
    }
}
