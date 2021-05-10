// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace ZabbixAgentLib
{
    /// <summary>
    /// Сбор данных.
    /// </summary>
    public class HealthDataCollectorStatus : IHealthDataCollector
    {
        #region Public and private fields and properties

        public Dictionary<string, string> Dict { get; set; }

        public DateTime StartDateTime { get; }

        public int RequestCount { get ; private set; }

        #endregion

        #region Constructor and destructor

        public HealthDataCollectorStatus()
        {
            StartDateTime = DateTime.Now;
            RequestCount = 0;
            Dict = new Dictionary<string, string>();
        }

        #endregion

        #region Public and private methods

        public StringBuilder Response()
        {
            var result = new StringBuilder();
            foreach (var v in Dict)
            {
                result.AppendLine($"{v.Key}={v.Value};");
            }

            var dt = DateTime.Now;
            result.AppendLine($"CurrentTime={dt.Year}-{dt.Month}-{dt.Day} {dt.Hour}:{dt.Minute}:{dt.Second};");
            var interval = DateTime.Now - StartDateTime;
            result.AppendLine($"TimePassed={interval};");
            result.AppendLine($"RequestCount={++RequestCount};");
            return result;
        }

        public void LoadValues(Dictionary<string, string> dict)
        {
            Dict = dict;
        }

        #endregion
    }
}
