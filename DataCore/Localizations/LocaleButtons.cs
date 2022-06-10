// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleButtons
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleButtons _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleButtons Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string Abort => Lang == ShareEnums.Lang.English ? "Abort" : "Прервать";
        public string Apply => Lang == ShareEnums.Lang.English ? "Apply" : "Применить";
        public string Cancel => Lang == ShareEnums.Lang.English ? "Cancel" : "Отмена";
        public string Clear => Lang == ShareEnums.Lang.English ? "Clear" : "Очистить";
        public string Close => Lang == ShareEnums.Lang.English ? "Close" : "Закрыть";
        public string Custom => Lang == ShareEnums.Lang.English ? "Custom" : "Кастом";
        public string Enter => Lang == ShareEnums.Lang.English ? "Enter" : "Ввод";
        public string Ignore => Lang == ShareEnums.Lang.English ? "Ignore" : "Игнорировать";
        public string Next => Lang == ShareEnums.Lang.English ? "Next" : "Следующие";
        public string No => Lang == ShareEnums.Lang.English ? "No" : "Нет";
        public string Ok => Lang == ShareEnums.Lang.English ? "Ok" : "Ок";
        public string Previous => Lang == ShareEnums.Lang.English ? "Previous" : "Предыдущие";
        public string Retry => Lang == ShareEnums.Lang.English ? "Retry" : "Повторить";
        public string Yes => Lang == ShareEnums.Lang.English ? "Yes" : "Да";
        
        #endregion
    }
}
