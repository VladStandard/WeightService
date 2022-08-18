//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using MdmControlBlazor.Utils;

//namespace MdmControlBlazor.Components
//{
//    public partial class Docs
//    {
//        #region Public and private fields and properties

//        private string SqlUserEng => JsonSettings.Trusted ? "windows-account" : $"sql-account: {JsonAppSettings.Username}";
//        private string SqlUserRus => JsonSettings.Trusted ? "windows-аккаунт" : $"sql-аккаунт: {JsonAppSettings.Username}";

//        public string GetDocEng()
//        {
//            return LocalizationStrings.HomeText + Environment.NewLine + $@"
//SQL-server: {JsonAppSettings.Server}
//SQL-DB: {JsonAppSettings.Db}
//{SqlUserEng}

//        public string GetDocRus()
//        {
//            return LocalizationStrings.HomeText + Environment.NewLine + $@"
//SQL-сервер: {JsonAppSettings.Server}
//SQL-БД: {JsonAppSettings.Db}
//{SqlUserRus}

//Горячие клавиши для любой страницы:
//- Alt + 1   -- Главная
//- Alt + 2   -- Номенклатура
//- Ctrl + R  -- обновить данные
//Горячие клавиши для страницы номенклатур:
//- F5        -- Создать мастер запись
//- F6        -- Включить ненормализованную запись в мастер
//- F8        -- Удалить мастер запись
//- Enter     -- Редактировать мастер запись
//Горячие клавиши для страницы номенклатуры:
//- Ctrl + S  -- Сохранить
//- Backspace -- Закрыть
//";
//        }

//        #endregion
//    }
//}