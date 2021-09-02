// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace WebApiExample.Common
{
    public class ResponseItem
    {
        public Guid Id { get; set; }
        public StateRequest Status { get; set; }
        public string Message { get; set; }

    }

    public enum StateRequest
    {
        Error,
        ProcessCompleted,
        InQueue
    }
}