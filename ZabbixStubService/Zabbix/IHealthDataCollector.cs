// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Text;

namespace ZabbixStubService.Zabbix
{
    public interface IHealthDataCollector
    {
        Dictionary<string, string> Dict { get; set; }
        DateTime StartDateTime { get; }
        int RequestCount { get; }
        StringBuilder Response();
    }
}
