// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace ScalesUI.Utils
{
    public static class Messages
    {
        public static string Exception => @"Ошибка";
        public static string Registration => @"Регистрация устройства";
        public static string OperationControl => @"Контроль операций";
        public static string WeightControl => @"Вес выходит за границы!";
        public static readonly decimal MassaThreshold = 0.05M;
        public static string MassaCheck =>
            @"Разгрузите весовую платформу!" + Environment.NewLine + 
            $@"Пороговое значение: {MassaThreshold} кг." + Environment.NewLine + Environment.NewLine +
            @"  Yes - игнорировать и продолжить." + Environment.NewLine +
            @"  No - приостановить и разгрузить.";
    }
}