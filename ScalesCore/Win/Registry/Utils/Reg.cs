// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using ScalesCore.Win.Registry.Entities;

namespace ScalesCore.Win.Registry.Utils
{
    public static class Reg
    {
        public static bool Win64Platform { get; set; } = Environment.Is64BitOperatingSystem;

        /// <summary>
        /// Список разделов
        /// </summary>
        public static class ListNames
        {
            private static Record[] _names;

            /// <summary>
            /// Инициализация
            /// </summary>
            public static void Init()
            {
                try
                {
                    _names = new Record[0];
                }
                catch (Exception)
                {
                    //
                }
            }

            /// <summary>
            /// Добавить имя в с список
            /// </summary>
            public static void Add(string name, string owner, string deny, string allow)
            {
                try
                {
                    Array.Resize(ref _names, _names.Length + 1);
                    _names[_names.Length - 1] = new Record(name, owner, deny, allow);
                }
                catch (Exception)
                {
                    //
                }
            }

            /// <summary>
            /// Получить список
            /// </summary>
            public static Record[] Get()
            {
                try
                {
                    return _names;
                }
                catch (Exception)
                {
                    //
                }
                return null;
            }
        }

        /// <summary>
        /// Настройки
        /// </summary>
        public static class Setting
        {
//            /// <summary>
//            /// Менеджер ключей реестра
//            /// </summary>
//            public static class KeyManager
//            {
//                // Прочитать
//                public static string Load(string regPath, string paramName)
//                {
//                    string paramValue = null;
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey(regPath);
//                        paramValue = rk != null ? Convert.ToString(rk.GetValue(paramName)) : "";
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return paramValue;
//                }

//                // Записать
//                public static bool Save(string regPath, string paramName, string paramValue, bool question = true)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (string.IsNullOrEmpty(paramValue))
//                            {
//                                if (question)
//                                    if (UtilsMessageBoxAutoClosing.Show(
//                                        "Записать пустое значение ключа в реестр?" + Environment.NewLine +
//                                        Environment.NewLine +
//                                        paramName + " = " + paramValue, "Запись значения ключа в реестр", 10000,
//                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
//                                        MessageBoxDefaultButton.Button3) != DialogResult.Yes)
//                                        return false;

//                            }
//                            rk.SetValue(paramName, paramValue ?? string.Empty);
//                            return true;
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Создать
//                public static bool Create(string regPath, string paramName, string paramValue)
//                {
//                    var result = false;
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(paramName) == null)
//                                rk.SetValue(paramName, paramValue);
//                            else
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже создан.", "Редактор реестра");
//                            result = true;
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 5000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                    return result;
//                }

//                // Удалить
//                public static bool Delete(string regPath, string paramName)
//                {
//                    var result = false;
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(paramName) == null)
//                            {
//                                //UtilsMessageBoxAutoClosing.Show("Параметр уже удалён.", "Редактор реестра");
//                            }
//                            else
//                                rk.DeleteValue(paramName);
//                            result = true;
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 5000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                    return result;
//                }

//                // Бэкап
//                public static bool Backup(string regPathSource, string regPathDest, string paramName)
//                {
//                    try
//                    {
//                        var rkSource = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(regPathSource, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rkSource != null)
//                        {
//                            var rkDest = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                    OpenSubKey(regPathDest, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                            if (rkDest != null)
//                            {
//                                rkDest.SetValue(paramName, Load(regPathSource, paramName));
//                                return true;
//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Бэкап ключей внутри каталога
//                public static bool BackupInsidePath(string regPathSource, string regPathDest)
//                {
//                    try
//                    {
//                        var result = true;
//                        // ReSharper disable once LoopCanBePartlyConvertedToQuery
//                        foreach (var keyName in GetKeyNames(regPathSource))
//                        {
//                            if (!Backup(regPathSource, regPathDest, keyName))
//                            {
//                                result = false;
//                            }
//                        }
//                        return result;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Удалить ключи внутри каталога
//                public static bool DeleteInsidePath(string regPathSource)
//                {
//                    try
//                    {
//                        var result = true;
//                        // ReSharper disable once LoopCanBePartlyConvertedToQuery
//                        foreach (var keyName in GetKeyNames(regPathSource))
//                        {
//                            if (!Delete(regPathSource, keyName))
//                            {
//                                result = false;
//                            }
//                        }
//                        return result;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Восстановить
//                public static bool Restore(string regPathSource, string regPathDest, string paramName)
//                {
//                    try
//                    {
//                        var rkSource = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(regPathSource, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rkSource != null)
//                        {
//                            var rkDest = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                    OpenSubKey(regPathDest, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                            if (rkDest != null)
//                            {
//                                rkSource.SetValue(paramName, Load(regPathDest, paramName));
//                                return true;
//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Получить список
//                public static Collection<string> GetKeyNames(string regPath)
//                {
//                    var paramNames = new Collection<string>();
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey(regPath);

//                        if (rk != null)
//                        {
//                            foreach (var key in rk.GetValueNames())
//                            {
//                                paramNames.Add(key);
//                            }

//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return paramNames;
//                }

//                // Получить список
//                public static Collection<string> GetPathNames(string regPath)
//                {
//                    var paramNames = new Collection<string>();
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey(regPath);

//                        if (rk != null)
//                        {
//                            foreach (var path in rk.GetSubKeyNames())
//                            {
//                                paramNames.Add(path);
//                            }

//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return paramNames;
//                }

//                /// <summary>
//                /// Бэкап всех ключей
//                /// </summary>
//                /// <param name="showMsg"></param>
//                public static bool BackupAll(bool showMsg)
//                {
//                    try
//                    {
//                        #region Вопрос

//                        if (showMsg)
//                        {
//                            if (UtilsMessageBoxAutoClosing.Show("Выполнить резервирование в специальный раздел?", "Редактор реестра",
//                                10000, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
//                                return false;
//                        }

//                        #endregion

//                        var result = true;

//                        // Создать каталог бэкапа
//                        Functions.Path.Create(RegistryHive.LocalMachine, UtilsKey.Folder.SoftwareActiveSettingShareBak);
//                        // Бэкап ключей в корневом каталоге
//                        if (!BackupInsidePath(UtilsKey.Folder.SoftwareActiveSettingShare, UtilsKey.Folder.SoftwareActiveSettingShareBak))
//                            result = false;

//                        // Перебор вложенных каталогов
//                        foreach (var pathName in GetPathNames(UtilsKey.Folder.SoftwareActiveSettingShare))
//                        {
//                            // Создать каталог бэкапа
//                            Functions.Path.Create(RegistryHive.LocalMachine, UtilsKey.Folder.SoftwareActiveSettingShareBak + @"\" + pathName);
//                            // Бэкап ключей вложенных каталогов
//                            if (!BackupInsidePath(UtilsKey.Folder.SoftwareActiveSettingShare + @"\" + pathName,
//                                UtilsKey.Folder.SoftwareActiveSettingShareBak + @"\" + pathName))
//                                result = false;
//                        }

//                        return result;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                /// <summary>
//                /// Восстановить все ключи
//                /// </summary>
//                /// <param name="showMsg"></param>
//                public static bool RestoreAll(bool showMsg)
//                {
//                    try
//                    {
//                        #region Вопрос

//                        if (showMsg)
//                        {
//                            if (UtilsMessageBoxAutoClosing.Show("Выполнить восстановление из специального раздела?", "Редактор реестра",
//                                10000, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
//                                return false;
//                        }

//                        #endregion

//                        var result = true;

//                        // Создать каталог бэкапа
//                        Functions.Path.Create(RegistryHive.LocalMachine, UtilsKey.Folder.SoftwareActiveSettingShare);
//                        // Восстановление ключей в корневом каталоге
//                        if (!BackupInsidePath(UtilsKey.Folder.SoftwareActiveSettingShareBak, UtilsKey.Folder.SoftwareActiveSettingShare))
//                            result = false;

//                        // Перебор вложенных каталогов
//                        foreach (var pathName in GetPathNames(UtilsKey.Folder.SoftwareActiveSettingShareBak))
//                        {
//                            // Создать каталог бэкапа
//                            Functions.Path.Create(RegistryHive.LocalMachine, UtilsKey.Folder.SoftwareActiveSettingShare + @"\" + pathName);
//                            // Восстановление ключей вложенных каталогов
//                            if (!BackupInsidePath(UtilsKey.Folder.SoftwareActiveSettingShareBak + @"\" + pathName,
//                                UtilsKey.Folder.SoftwareActiveSettingShare + @"\" + pathName))
//                                result = false;
//                        }

//                        return result;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                /// <summary>
//                /// Удалить все ключи
//                /// </summary>
//                /// <param name="showMsg"></param>
//                public static bool DeleteAll(bool showMsg)
//                {
//                    try
//                    {
//                        #region Вопрос

//                        if (showMsg)
//                        {
//                            if (UtilsMessageBoxAutoClosing.Show("Выполнить удаление всех ключей и подразделов специального раздела?", "Редактор реестра",
//                                10000, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
//                                return false;
//                        }

//                        #endregion

//                        bool result = DeleteInsidePath(UtilsKey.Folder.SoftwareActiveSettingShareBak);

//                        // Перебор вложенных каталогов
//                        foreach (var pathName in GetPathNames(UtilsKey.Folder.SoftwareActiveSettingShareBak))
//                        {
//                            if (!DeleteInsidePath(UtilsKey.Folder.SoftwareActiveSettingShareBak + @"\" + pathName))
//                                result = false;
//                        }

//                        // Удаление каталогов
//                        var result2 = true;
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(UtilsKey.Folder.SoftwareActiveSetting, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                            rk?.DeleteSubKeyTree("Backup", false);
//                        }
//                        catch (Exception)
//                        {
//                            result2 = false;
//                        }

//                        if (!result2)
//                            result = false;

//                        return result;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }
//            }

//            /// <summary>
//            /// Общие настройки
//            /// </summary>
//            public static class Share
//            {
//                public static string ComputerDescription { get; set; } = "";

//                /// <summary>
//                /// Загрузить ключи
//                /// </summary>
//                public static bool LoadAll()
//                {
//                    ComputerDescription = KeyManager.Load(UtilsKey.Folder.SoftwareActiveSettingShare, "ComputerDescription");
//                    return !string.IsNullOrEmpty(ComputerDescription);
//                }

//                /// <summary>
//                /// Сохранить ключи
//                /// </summary>
//                public static bool SaveAll()
//                {
//                    var result = KeyManager.Save(UtilsKey.Folder.SoftwareActiveSettingShare, "ComputerDescription", ComputerDescription);
//                    return result;
//                }

//                public static void CreateParam(string param, string value, RegistryValueKind registryValueKind, bool showMessage)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                    OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingShare, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                rk.SetValue(param, value, registryValueKind);
//                                LoadAll();
//                            }
//                            else
//                            {
//                                if (showMessage) UtilsMessageBoxAutoClosing.Show("Параметр уже создан.", "Редактор реестра");
//                            }
//                        }
//                        else
//                        {
//                            if (showMessage) UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }

//                /// <summary>
//                /// Создать ключи
//                /// </summary>
//                public static void CreateAll()
//                {
//                    KeyManager.Create(UtilsKey.Folder.SoftwareActiveSettingShare, "ComputerDescription", ComputerDescription);
//                }

//                /// <summary>
//                /// Удалить ключи
//                /// </summary>
//                public static void DeleteAll()
//                {
//                    KeyManager.Delete(UtilsKey.Folder.SoftwareActiveSettingShare, "ComputerDescription");
//                }
//            }

//            /// <summary>
//            /// Настройки БД
//            /// </summary>
//            public static class Db
//            {
//                public static string Primary { get; set; } = "";
//                public static string DataSourcePrimary { get; set; } = "";
//                public static string DataSourceSecondary { get; set; } = "";
//                public static string InitialCatalog { get; set; } = "";
//                public static Collection<string> DataSourceRemote = new Collection<string>();
//                public static Collection<string> InitialCatalogRemote = new Collection<string>();
//                public static string Provider { get; set; } = "";
//                public static string UserId { get; set; } = "";
//                public static string PasswordNonCrypt { get; set; } = "";
//                public static string TimeOut { get; set; } = "";
//                public static string CryptAlgorithm { get; set; } = "0";

//                // Значения по умолчанию
//                public static void Default()
//                {
//                    try
//                    {
//                        Primary = "";
//                        DataSourcePrimary = "";
//                        DataSourceSecondary = "";
//                        InitialCatalog = "";
//                        DataSourceRemote = new Collection<string>() { "" };
//                        InitialCatalogRemote = new Collection<string>() { "" };
//                        Provider = "";
//                        UserId = "";
//                        PasswordNonCrypt = "";
//                        TimeOut = "";
//                        CryptAlgorithm = "0";
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                }

//                public static void Load()
//                {
//                    // Значения по умолчанию
//                    Default();

//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            Primary = Convert.ToString(rk.GetValue("Primary"));
//                            DataSourcePrimary = Convert.ToString(rk.GetValue("DataSource"));
//                            DataSourceSecondary = Convert.ToString(rk.GetValue("DataSourceSecondary"));
//                            #region DataSourceRemote
//                            try
//                            {
//                                DataSourceRemote.Clear();
//                                for (var i = 1; i < 100; i++)
//                                {
//                                    var keyName = "DataSourceRemote" + Convert.ToString(i);
//                                    // поиск ключа
//                                    foreach (var key in rk.GetValueNames())
//                                    {
//                                        if (key == keyName)
//                                        {
//                                            var dataSourceRemote = rk.GetValue(keyName).ToString();
//                                            if (!string.IsNullOrEmpty(dataSourceRemote))
//                                            {
//                                                DataSourceRemote.Add(dataSourceRemote);
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                            catch (Exception)
//                            {
//                                //
//                            }
//                            #endregion
//                            InitialCatalog = Convert.ToString(rk.GetValue("InitialCatalog"));
//                            #region InitialCatalogRemote
//                            try
//                            {
//                                InitialCatalogRemote.Clear();
//                                for (var i = 1; i < 100; i++)
//                                {
//                                    var keyName = "InitialCatalogRemote" + Convert.ToString(i);
//                                    // поиск ключа
//                                    foreach (var key in rk.GetValueNames())
//                                    {
//                                        if (key == keyName)
//                                        {
//                                            var initialCatalogRemote = rk.GetValue(keyName).ToString();
//                                            if (!string.IsNullOrEmpty(initialCatalogRemote))
//                                            {
//                                                InitialCatalogRemote.Add(initialCatalogRemote);
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                            catch (Exception)
//                            {
//                                //
//                            }
//                            #endregion
//                            Provider = Convert.ToString(rk.GetValue("Provider"));
//                            UserId = Convert.ToString(rk.GetValue("UserId"));
//                            TimeOut = Convert.ToString(rk.GetValue("TimeOut"));
//                            PasswordNonCrypt = UtilsCrypt.MethodM.Get(Convert.ToString(rk.GetValue("PasswordCrypt")));
//                            try
//                            {
//                                CryptAlgorithm = !string.IsNullOrEmpty(Convert.ToString(rk.GetValue("CryptAlgorithm")))
//                                    ? Convert.ToString(rk.GetValue("CryptAlgorithm")) : "0";
//                            }
//                            catch (Exception)
//                            {
//                                CryptAlgorithm = "0";
//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                }

//                public static bool Save()
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            // SQL
//                            rk.SetValue("Primary", Primary, RegistryValueKind.String);
//                            rk.SetValue("DataSource", DataSourcePrimary, RegistryValueKind.String);
//                            rk.SetValue("DataSourceSecondary", DataSourceSecondary, RegistryValueKind.String);
//                            try
//                            {
//                                if (DataSourceRemote.Count > 0)
//                                {
//                                    var i = 1;
//                                    foreach (var item in DataSourceRemote)
//                                    {
//                                        rk.SetValue("DataSourceRemote" + Convert.ToString(i), item, RegistryValueKind.String);
//                                        i++;
//                                    }
//                                }
//                            }
//                            catch (Exception)
//                            {
//                                //
//                            }
//                            rk.SetValue("InitialCatalog", InitialCatalog, RegistryValueKind.String);
//                            try
//                            {
//                                if (InitialCatalogRemote.Count > 0)
//                                {
//                                    var i = 1;
//                                    foreach (var item in InitialCatalogRemote)
//                                    {
//                                        rk.SetValue("InitialCatalogRemote" + Convert.ToString(i), item, RegistryValueKind.String);
//                                        i++;
//                                    }
//                                }
//                            }
//                            catch (Exception)
//                            {
//                                //
//                            }
//                            rk.SetValue("Provider", Provider, RegistryValueKind.String);
//                            rk.SetValue("UserId", UserId, RegistryValueKind.String);
//                            rk.SetValue("PasswordCrypt", UtilsCrypt.MethodM.Set(PasswordNonCrypt), RegistryValueKind.String);
//                            rk.SetValue("TimeOut", TimeOut, RegistryValueKind.String);
//                            rk.SetValue("CryptAlgorithm", CryptAlgorithm, RegistryValueKind.String);
//                            // Повторная загрузка
//                            Load();
//                            return true;
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                public static void DeleteRemoteDataSources()
//                {
//                    try
//                    {
//                        var rk32 =
//                            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb);
//                        var rk64 =
//                            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb);

//                        if (Win64Platform)
//                        {
//                            if (rk64 != null)
//                            {
//                                try
//                                {
//                                    DataSourceRemote?.Clear();
//                                    InitialCatalogRemote?.Clear();
//                                }
//                                catch (Exception)
//                                {
//                                    //
//                                }
//                                try
//                                {
//                                    for (var i = 1; i < 100; i++)
//                                    {
//                                        if (!string.IsNullOrEmpty(rk64.GetValue("DataSourceRemote" + Convert.ToString(i)).ToString()))
//                                        {
//                                            DeleteParam("DataSourceRemote" + Convert.ToString(i));
//                                        }
//                                        if (!string.IsNullOrEmpty(rk64.GetValue("InitialCatalogRemote" + Convert.ToString(i)).ToString()))
//                                        {
//                                            DeleteParam("InitialCatalogRemote" + Convert.ToString(i));
//                                        }
//                                    }
//                                }
//                                catch (Exception)
//                                {
//                                    //
//                                }
//                            }
//                        }
//                        else
//                        {
//                            if (rk32 != null)
//                            {
//                                try
//                                {
//                                    for (var i = 1; i < 100; i++)
//                                    {
//                                        if (!string.IsNullOrEmpty(rk32.GetValue("DataSourceRemote" + Convert.ToString(i)).ToString()))
//                                        {
//                                            DeleteParam("DataSourceRemote" + Convert.ToString(i));
//                                        }
//                                        if (!string.IsNullOrEmpty(rk32.GetValue("InitialCatalogRemote" + Convert.ToString(i)).ToString()))
//                                        {
//                                            DeleteParam("InitialCatalogRemote" + Convert.ToString(i));
//                                        }
//                                    }
//                                }
//                                catch (Exception)
//                                {
//                                    //
//                                }
//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                }

//                public static void DeleteRemoteDataSourcesNumber(int number)
//                {
//                    try
//                    {
//                        var rk32 =
//                            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb);
//                        var rk64 =
//                            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb);

//                        if (Win64Platform)
//                        {
//                            if (rk64 != null)
//                            {
//                                try
//                                {
//                                    DataSourceRemote?.Clear();
//                                    InitialCatalogRemote?.Clear();
//                                }
//                                catch (Exception)
//                                {
//                                    //
//                                }
//                                try
//                                {
//                                    if (!string.IsNullOrEmpty(rk64.GetValue("DataSourceRemote" + Convert.ToString(number)).ToString()))
//                                    {
//                                        DeleteParam("DataSourceRemote" + Convert.ToString(number));
//                                    }
//                                    if (!string.IsNullOrEmpty(rk64.GetValue("InitialCatalogRemote" + Convert.ToString(number)).ToString()))
//                                    {
//                                        DeleteParam("InitialCatalogRemote" + Convert.ToString(number));
//                                    }
//                                }
//                                catch (Exception)
//                                {
//                                    //
//                                }
//                            }
//                        }
//                        else
//                        {
//                            if (rk32 != null)
//                            {
//                                try
//                                {
//                                    if (!string.IsNullOrEmpty(rk32.GetValue("DataSourceRemote" + Convert.ToString(number)).ToString()))
//                                    {
//                                        DeleteParam("DataSourceRemote" + Convert.ToString(number));
//                                    }
//                                    if (!string.IsNullOrEmpty(rk32.GetValue("InitialCatalogRemote" + Convert.ToString(number)).ToString()))
//                                    {
//                                        DeleteParam("InitialCatalogRemote" + Convert.ToString(number));
//                                    }
//                                }
//                                catch (Exception)
//                                {
//                                    //
//                                }
//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                }

//                public static void DeleteParam(string param)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже удалён.", "Редактор реестра");
//                            }
//                            else
//                            {
//                                rk.DeleteValue(param);
//                                Load();
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }

//                public static void CreateParam(string param, string value, RegistryValueKind registryValueKind, bool showMessage)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingDb, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                rk.SetValue(param, value, registryValueKind);
//                                Load();
//                            }
//                            else
//                            {
//                                if (showMessage) UtilsMessageBoxAutoClosing.Show("Параметр уже создан.", "Редактор реестра");
//                            }
//                        }
//                        else
//                        {
//                            if (showMessage) UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }
//            }

//            /// <summary>
//            /// Настройки Windows аккаунта
//            /// </summary>
//            public static class Win
//            {
//                public static string User { get; set; } = "";
//                public static string PasswordNonCrypt { get; set; } = "";

//                // Чтение реестра
//                public static bool Load()
//                {
//                    try
//                    {
//                        var rk32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingWin);
//                        var rk64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingWin);
//                        if (Win64Platform)
//                        {
//                            if (rk64 != null)
//                            {
//                                User = Convert.ToString(rk64.GetValue("User"));
//                                PasswordNonCrypt = UtilsCrypt.MethodM.Get(Convert.ToString(rk64.GetValue("PasswordCrypt")));
//                            }
//                            else
//                            {
//                                User = "";
//                                PasswordNonCrypt = "";
//                            }
//                        }
//                        else
//                        {
//                            if (rk32 != null)
//                            {
//                                User = Convert.ToString(rk32.GetValue("User"));
//                                PasswordNonCrypt = UtilsCrypt.MethodM.Get(Convert.ToString(rk32.GetValue("PasswordCrypt")));
//                            }
//                            else
//                            {
//                                User = "";
//                                PasswordNonCrypt = "";
//                            }
//                        }
//                        if (!string.IsNullOrEmpty(User))
//                            return true;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Запись реестра
//                public static bool Save()
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingWin, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            rk.SetValue("User", User);
//                            rk.SetValue("PasswordCrypt", UtilsCrypt.MethodM.Set(PasswordNonCrypt));
//                            // Повторная загрузка
//                            Load();
//                            return true;
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Создать параметр
//                public static void CreateParam(string param, string value)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingWin, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                rk.SetValue(param, value);
//                                Load();
//                            }
//                            else
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже создан.", "Редактор реестра");
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }

//                // Удалить параметр
//                public static void DeleteParam(string param)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingWin, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже удалён.", "Редактор реестра");
//                            }
//                            else
//                            {
//                                rk.DeleteValue(param);
//                                Load();
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }
//            }

//            /// <summary>
//            /// Настройки отчётов
//            /// </summary>
//            public static class Report
//            {
//                public static string Dir { get; set; } = "";

//                // Чтение реестра
//                public static bool Load()
//                {
//                    try
//                    {
//                        var rk32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingReports);
//                        var rk64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingReports);
//                        Dir = Win64Platform
//                            ? (rk64 != null ? Convert.ToString(rk64.GetValue("Folder")) : "")
//                            : (rk32 != null ? Convert.ToString(rk32.GetValue("Folder")) : "");
//                        if (!string.IsNullOrEmpty(Dir))
//                            return true;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Запись реестра
//                public static bool Save()
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingReports, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            rk.SetValue("Folder", Dir);
//                            // Повторная загрузка
//                            Load();
//                            return true;
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Создать параметр
//                public static void CreateParam(string param, string value)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingReports, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                rk.SetValue(param, value);
//                                Load();
//                            }
//                            else
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже создан.", "Редактор реестра");
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }

//                // Удалить параметр
//                public static void DeleteParam(string param)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingReports, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже удалён.", "Редактор реестра");
//                            }
//                            else
//                            {
//                                rk.DeleteValue(param);
//                                Load();
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }
//            }

//            /// <summary>
//            /// Настройки SMTP
//            /// </summary>
//            public static class Smtp
//            {
//                public static string Server { get; set; } = "";
//                public static string Port { get; set; } = "";
//                public static string Priority { get; set; } = "";
//                public static bool Ssl { get; set; }
//                public static string Encoding { get; set; } = "";
//                public static string From { get; set; } = "";
//                public static string FromAlyas { get; set; } = "";
//                public static string PasswordNonCrypt { get; set; } = "";
//                public static string Mailto { get; set; } = "";
//                public static string TimeOut { get; set; } = "";
//                public static string Subject { get; set; } = "";

//                // Чтение реестра
//                public static bool Load()
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingSmtp);
//                        if (rk != null)
//                        {
//                            Server = Convert.ToString(rk.GetValue("Server"));
//                            Port = Convert.ToString(rk.GetValue("Port"));
//                            Priority = Convert.ToString(rk.GetValue("Priority"));
//                            Ssl = Convert.ToBoolean(rk.GetValue("Ssl"));
//                            Encoding = Convert.ToString(rk.GetValue("Encoding"));
//                            From = Convert.ToString(rk.GetValue("From"));
//                            FromAlyas = Convert.ToString(rk.GetValue("FromAlyas"));
//                            PasswordNonCrypt = UtilsCrypt.MethodM.Get(Convert.ToString(rk.GetValue("PasswordCrypt")));
//                            Mailto = Convert.ToString(rk.GetValue("Mailto"));
//                            TimeOut = Convert.ToString(rk.GetValue("TimeOut"));
//                            Subject = Convert.ToString(rk.GetValue("Subject"));
//                        }
//                        else
//                        {
//                            Server = "";
//                            Port = "";
//                            Priority = "";
//                            Ssl = false;
//                            Encoding = "";
//                            From = "";
//                            FromAlyas = "";
//                            PasswordNonCrypt = "";
//                            Mailto = "";
//                            TimeOut = "";
//                            Subject = "";
//                        }

//                        if (!string.IsNullOrEmpty(Server) && !string.IsNullOrEmpty(Port) && !string.IsNullOrEmpty(From) &&
//                            !string.IsNullOrEmpty(PasswordNonCrypt) && !string.IsNullOrEmpty(Mailto) && !string.IsNullOrEmpty(Subject))
//                            return true;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Запись реестра
//                public static bool Save()
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingSmtp, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            rk.SetValue("Server", Server);
//                            rk.SetValue("Port", Port);
//                            rk.SetValue("Priority", Priority);
//                            rk.SetValue("Ssl", Ssl);
//                            rk.SetValue("Encoding", Encoding);
//                            rk.SetValue("From", From);
//                            rk.SetValue("FromAlyas", FromAlyas);
//                            rk.SetValue("PasswordCrypt", UtilsCrypt.MethodM.Set(PasswordNonCrypt));
//                            rk.SetValue("Mailto", Mailto);
//                            rk.SetValue("TimeOut", TimeOut);
//                            rk.SetValue("Subject", Subject);
//                            // Повторная загрузка
//                            Load();
//                            return true;
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                // Создать параметр
//                public static void CreateParam(string param, string value)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingSmtp, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                rk.SetValue(param, value);
//                                Load();
//                            }
//                            else
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже создан.", "Редактор реестра");
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }

//                // Удалить параметр
//                public static void DeleteParam(string param)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(UtilsKey.Folder.SoftwareActiveSettingSmtp, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            if (rk.GetValue(param) == null)
//                            {
//                                UtilsMessageBoxAutoClosing.Show("Параметр уже удалён.", "Редактор реестра");
//                            }
//                            else
//                            {
//                                rk.DeleteValue(param);
//                                Load();
//                            }
//                        }
//                        else
//                        {
//                            UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание параметра реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                    }
//                }
//            }
        }

//        /// <summary>
//        /// Control Panel\Desktop
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="timeOut"></param>
//        /// <returns></returns>
//        public static bool SecuritySetupControlPanelDesktop(string fileName, string timeOut)
//        {
//            try
//            {
//                var makeJob = true;
//                var rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
//                    Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                    OpenSubKey(UtilsKey.Folder.ControlPanelDesktop, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                if (rk1 != null)
//                {
//                    rk1.SetValue("ScreenSaveActive", @"1", RegistryValueKind.String);
//                    rk1.SetValue("SCRNSAVE.EXE", fileName, RegistryValueKind.String);
//                    rk1.SetValue("ScreenSaverIsSecure", @"1", RegistryValueKind.String);
//                    rk1.SetValue("ScreenSaveTimeOut", timeOut, RegistryValueKind.String);
//                }
//                else
//                {
//                    makeJob = false;
//                    UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//                }
//                if (!makeJob)
//                    return false;
//                var rk2 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
//                    Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                    OpenSubKey(UtilsKey.Folder.SoftwarePoliciesMicrosoftWindowsControlPanelDesktop, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
//                if (rk2 == null)
//                {
//                    rk2 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
//                        Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                        CreateSubKey(UtilsKey.Folder.SoftwarePoliciesMicrosoftWindowsControlPanelDesktop, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                }
//                if (rk2 != null)
//                {
//                    rk2.SetValue("ScreenSaveActive", @"1", RegistryValueKind.String);
//                    rk2.SetValue("SCRNSAVE.EXE", fileName, RegistryValueKind.String);
//                    rk2.SetValue("ScreenSaverIsSecure", @"1", RegistryValueKind.String);
//                    rk2.SetValue("ScreenSaveTimeOut", timeOut, RegistryValueKind.String);
//                    return true;
//                }
//                UtilsMessageBoxAutoClosing.Show("Раздел реестра не найден!", "Редактор реестра");
//            }
//            catch (Exception exception)
//            {
//                UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000, MessageBoxButtons.OK,
//                    MessageBoxIcon.Error);
//            }
//            return false;
//        }

//        // Логирование
//        private static void WriteLog(ref RichTextBox log, string text)
//        {
//            try
//            {
//                if (log != null)
//                {
//                    if (!string.IsNullOrEmpty(text))
//                    {
//                        if (log.InvokeRequired)
//                        {
//                            var box = log;
//                            box.BeginInvoke(new Action(() =>
//                            {
//                                box.AppendText(text + Environment.NewLine);
//                            }));
//                        }
//                        else
//                        {
//                            log.AppendText(text + Environment.NewLine);
//                        }
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                //
//            }
//        }

//        /// <summary>
//        /// Владелец
//        /// </summary>
//        public static class Owner
//        {
//            /// <summary>
//            /// Прочитать владельца, с обходом ограничений ACL
//            /// </summary>
//            /// <param name="path"></param>
//            /// <param name="registryHive"></param>
//            /// <returns></returns>
//            public static string Get(string path, RegistryHive registryHive)
//            {
//                // Разрешить текущему процессу обходить ограничения ACL
//                UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeRestorePrivilege, true);
//                // Иногда это требуется, в других случаях работает без него
//                UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeTakeOwnershipPrivilege, true);

//                var result = @"<НЕТ ДОСТУПА>";
//                //try
//                //{
//                //TokenManipulator.AddPrivilege("SeRestorePrivilege");
//                //TokenManipulator.AddPrivilege("SeBackupPrivilege");
//                //TokenManipulator.AddPrivilege("SeTakeOwnershipPrivilege");

//                //var subKey = Registry.ClassesRoot.OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.TakeOwnership);
//                // code to change owner...
//                try
//                {
//                    //var privilegeType = Type.GetType("System.Security.AccessControl.Privilege");
//                    //if (privilegeType != null)
//                    //{
//                    //    var privilege = Activator.CreateInstance(privilegeType, "SeCreateGlobalPrivilege");
//                    //    try
//                    //    {
//                    //        // => privilege.Enable();
//                    //        var methodInfo = privilegeType.GetMethod("Enable");
//                    //        methodInfo?.Invoke(privilege, null);

//                    //        //var rk = RegistryKey.OpenBaseKey(registryHive,
//                    //        //    winPlatform64 ? RegistryView.Registry64 : RegistryView.Registry32)
//                    //        //    .OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.TakeOwnership);
//                    //        var rk = RegistryKey.OpenBaseKey(registryHive,
//                    //            winPlatform64 ? RegistryView.Registry64 : RegistryView.Registry32)
//                    //            .OpenSubKey(path, RegistryKeyPermissionCheck.Default, RegistryRights.TakeOwnership);
//                    //        if (rk != null)
//                    //        {
//                    //            var rSecurity = rk.GetAccessControl();
//                    //            var arc = rSecurity.GetOwner(typeof (NTAccount));
//                    //            result = arc.Value;
//                    //        }
//                    //    }
//                    //    catch (PrivilegeNotHeldException)
//                    //    {
//                    //        result = "PrivilegeNotHeldException";
//                    //    }
//                    //    finally
//                    //    {
//                    //        // =>  privilege.Revert();
//                    //        var methodInfo = privilegeType.GetMethod("Revert");
//                    //        methodInfo?.Invoke(privilege, null);
//                    //    }
//                    var rk = RegistryKey.OpenBaseKey(registryHive,
//                        Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32)
//                        .OpenSubKey(path, RegistryKeyPermissionCheck.Default, RegistryRights.TakeOwnership);
//                    if (rk != null)
//                    {
//                        var rSecurity = rk.GetAccessControl();
//                        //var arc = rSecurity.GetOwner(typeof(NTAccount));
//                        //result = arc.Value;
//                        result = rSecurity.GetOwner(typeof(SecurityIdentifier)).Translate(typeof(NTAccount)).Value;
//                    }
//                }
//                catch (Exception exception)
//                {
//                    result = exception.Message;
//                }

//                //}
//                //finally
//                //{
//                //    TokenManipulator.RemovePrivilege("SeRestorePrivilege");
//                //    TokenManipulator.RemovePrivilege("SeBackupPrivilege");
//                //    TokenManipulator.RemovePrivilege("SeTakeOwnershipPrivilege");
//                //}

//                return result;
//            }

//            /// <summary>
//            /// Задать владельца, с обходом ограничений ACL
//            /// </summary>
//            /// <param name="userName"></param>
//            /// <param name="path"></param>
//            /// <param name="registryHive"></param>
//            /// <param name="log"></param>
//            /// <param name="showMsg"></param>
//            /// <returns></returns>
//            public static void Set(string userName, string path, RegistryHive registryHive,
//                ref RichTextBox log, bool showMsg)
//            {
//                #region Проверки

//                if (string.IsNullOrEmpty(userName))
//                {
//                    UtilsMessageBoxAutoClosing.Show("Пользователь не указан!", "Редактор реестра", 10000,
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }

//                #endregion

//                #region Вопрос

//                if (showMsg)
//                {
//                    if (UtilsMessageBoxAutoClosing.Show(
//                        "Выполнить назначение владельца '" + userName + "'?" + Environment.NewLine +
//                        Environment.NewLine + path,
//                        "Редактор реестра", 10000, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
//                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
//                        return;
//                }

//                #endregion

//                // Разрешить текущему процессу обходить ограничения ACL
//                UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeRestorePrivilege, true);
//                // Иногда это требуется, в других случаях работает без него
//                UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeTakeOwnershipPrivilege, true);

//                var regType = (registryHive == RegistryHive.CurrentUser) ? @"HKEY_CURRENT_USER" : @"HKEY_LOCAL_MACHINE";
//                try
//                {
//                    var rk = RegistryKey.OpenBaseKey(registryHive,
//                        Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                        OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                    if (rk != null)
//                    {
//                        var rs = rk.GetAccessControl();
//                        rs.SetOwner(new NTAccount(userName));
//                        //rs.AddAccessRule(new RegistryAccessRule(userName, RegistryRights.FullControl,
//                        //    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
//                        //    PropagationFlags.None, AccessControlType.Allow));
//                        rk.SetAccessControl(rs);
//                        if (showMsg)
//                            UtilsMessageBoxAutoClosing.Show(path + Environment.NewLine + Environment.NewLine + @"Владелец '" + userName + @"' успешно назначен",
//                                "Редактор реестра: " + regType);
//                        else
//                            WriteLog(ref log,
//                                regType + @"\" + path + @"  - Владелец '" + userName + @"' успешно назначен");
//                    }
//                    else
//                    {
//                        if (showMsg)
//                            UtilsMessageBoxAutoClosing.Show(path + Environment.NewLine + Environment.NewLine +
//                                                        @"Раздел реестра не найден!", "Редактор реестра: " + regType);
//                        else
//                            WriteLog(ref log, regType + @"\" + path + @"  - Раздел реестра не найден!");
//                    }
//                }
//                catch (Exception exception)
//                {
//                    WriteLog(ref log, regType + @"\" + path + @"  -  Добавление прав: " + exception.Message);
//                    if (showMsg)
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Редактор реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                }
//            }
//        }

//        /// <summary>
//        /// Права доступа
//        /// </summary>
//        public static class AccessRights
//        {
//            /// <summary>
//            /// Удалить права доступа, с обходом ограничений ACL
//            /// </summary>
//            /// <param name="userName"></param>
//            /// <param name="path"></param>
//            /// <param name="registryHive"></param>
//            /// <param name="log"></param>
//            /// <param name="showMsg"></param>
//            public static void Remove(string userName, string path, RegistryHive registryHive,
//                ref RichTextBox log, bool showMsg)
//            {
//                #region Проверки

//                if (string.IsNullOrEmpty(userName))
//                {
//                    UtilsMessageBoxAutoClosing.Show("Пользователь не указан!", "Редактор реестра", 10000,
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }

//                #endregion

//                #region Вопрос

//                if (showMsg)
//                {
//                    if (UtilsMessageBoxAutoClosing.Show("Выполнить удаление прав '" + userName + "'?" +
//                                                    Environment.NewLine + Environment.NewLine + path,
//                        "Редактор реестра",
//                        10000, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
//                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
//                        return;
//                }

//                #endregion

//                var regType = (registryHive == RegistryHive.CurrentUser)
//                    ? @"HKEY_CURRENT_USER"
//                    : @"HKEY_LOCAL_MACHINE";
//                try
//                {
//                    // Разрешить текущему процессу обходить ограничения ACL
//                    UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeRestorePrivilege, true);
//                    // Иногда это требуется, в других случаях работает без него
//                    UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeTakeOwnershipPrivilege, true);

//                    var rk = RegistryKey.OpenBaseKey(registryHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                    if (rk != null)
//                    {
//                        var rs = rk.GetAccessControl();
//                        rs.RemoveAccessRule(new RegistryAccessRule(userName, RegistryRights.FullControl,
//                            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
//                            PropagationFlags.None, AccessControlType.Allow));
//                        rk.SetAccessControl(rs);
//                        if (showMsg)
//                            UtilsMessageBoxAutoClosing.Show(path + Environment.NewLine + Environment.NewLine +
//                                                        @"Права '" + userName + @"' успешно удалены",
//                                "Редактор реестра: " + regType);
//                        else
//                            WriteLog(ref log,
//                                regType + @"\" + path + @"  - Права '" + userName + @"' успешно удалены");
//                    }
//                    else
//                    {
//                        if (showMsg)
//                            UtilsMessageBoxAutoClosing.Show(path + Environment.NewLine + Environment.NewLine +
//                                                        @"Раздел реестра не найден!",
//                                "Редактор реестра: " + regType);
//                        else
//                            WriteLog(ref log, regType + @"\" + path + @"  - Раздел реестра не найден!");
//                    }
//                }
//                catch (Exception exception)
//                {
//                    WriteLog(ref log, regType + @"\" + path + @"  - Удаление прав: " + exception.Message);
//                    if (showMsg)
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Редактор реестра", 10000, MessageBoxButtons.OK,
//                            MessageBoxIcon.Error);
//                }

//            }

//            /// <summary>
//            /// Добавить права доступа, с обходом ограничений ACL
//            /// </summary>
//            /// <param name="userName"></param>
//            /// <param name="path"></param>
//            /// <param name="registryHive"></param>
//            /// <param name="log"></param>
//            /// <param name="showMsg"></param>
//            /// <param name="accessControlType"></param>
//            /// <param name="propagationFlags"></param>
//            public static void Add(string userName, string path, RegistryHive registryHive,
//                ref RichTextBox log, bool showMsg,
//                AccessControlType accessControlType = AccessControlType.Allow,
//                PropagationFlags propagationFlags = PropagationFlags.None)
//            {
//                #region Проверки

//                if (string.IsNullOrEmpty(userName))
//                {
//                    UtilsMessageBoxAutoClosing.Show("Пользователь не указан!", "Редактор реестра", 10000,
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }

//                #endregion

//                #region Вопрос

//                if (showMsg)
//                {
//                    if (UtilsMessageBoxAutoClosing.Show(
//                        "Выполнить добавление прав '" + userName + "'?" + Environment.NewLine +
//                        Environment.NewLine + path,
//                        "Редактор реестра", 10000, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
//                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
//                        return;
//                }

//                #endregion

//                var regType = (registryHive == RegistryHive.CurrentUser)
//                    ? @"HKEY_CURRENT_USER"
//                    : @"HKEY_LOCAL_MACHINE";
//                try
//                {
//                    // Разрешить текущему процессу обходить ограничения ACL
//                    UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeRestorePrivilege, true);
//                    // Иногда это требуется, в других случаях работает без него
//                    UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeTakeOwnershipPrivilege, true);

//                    var rk = RegistryKey.OpenBaseKey(registryHive,
//                        Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                        OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                    if (rk != null && !string.IsNullOrEmpty(userName))
//                    {
//                        var rs = rk.GetAccessControl();
//                        rs.AddAccessRule(new RegistryAccessRule(userName, RegistryRights.FullControl,
//                            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
//                            propagationFlags, accessControlType));
//                        rk.SetAccessControl(rs);
//                        WriteLog(ref log,
//                            regType + @"\" + path + @"  - Права '" + userName + @"' успешно добавлены");
//                        if (showMsg)
//                            UtilsMessageBoxAutoClosing.Show(path + Environment.NewLine + Environment.NewLine +
//                                                        @"Права '" + userName + @"' успешно добавлены",
//                                "Редактор реестра: " + regType);
//                    }
//                    else
//                    {
//                        if (showMsg)
//                            UtilsMessageBoxAutoClosing.Show(path + Environment.NewLine + Environment.NewLine +
//                                                        @"Раздел реестра не найден!",
//                                "Редактор реестра: " + regType);
//                        else
//                            WriteLog(ref log, regType + @"\" + path + @"  - Раздел реестра не найден!");
//                    }
//                }
//                catch (Exception exception)
//                {
//                    WriteLog(ref log, regType + @"\" + path + @"  -  Добавление прав: " + exception.Message);
//                    UtilsMessageBoxAutoClosing.Show(exception.Message, "Редактор реестра", 10000, MessageBoxButtons.OK,
//                        MessageBoxIcon.Error);
//                }
//            }

//            /// <summary>
//            /// Прочитать права доступа, с обходом ограничений ACL
//            /// </summary>
//            /// <param name="userName"></param>
//            /// <param name="path"></param>
//            /// <param name="registryHive"></param>
//            /// <param name="accessControlType"></param>
//            /// <returns></returns>
//            public static string Get(string userName, string path, RegistryHive registryHive,
//                string accessControlType)
//            {
//                #region Проверки

//                if (string.IsNullOrEmpty(userName))
//                {
//                    return @"<ОШИБКА ПОЛЬЗОВАТЕЛЯ>";
//                }

//                #endregion

//                try
//                {
//                    // Разрешить текущему процессу обходить ограничения ACL
//                    UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeRestorePrivilege, true);
//                    // Иногда это требуется, в других случаях работает без него
//                    UtilsPrivilege.ModifyPrivilege(EnumPrivilegeName.SeTakeOwnershipPrivilege, true);

//                    var rk = RegistryKey.OpenBaseKey(registryHive,
//                        Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                        OpenSubKey(path, RegistryKeyPermissionCheck.Default, RegistryRights.ReadPermissions);
//                    if (rk != null)
//                    {
//                        var rSecurity = rk.GetAccessControl();
//                        foreach (RegistryAccessRule ar in rSecurity.GetAccessRules(true, true, typeof(NTAccount)))
//                        {
//                            if (ar.IdentityReference.ToString().ToUpper().Contains(userName.ToUpper()))
//                            {
//                                if (ar.AccessControlType.ToString().Contains(accessControlType))
//                                {
//                                    return ar.RegistryRights.ToString();
//                                }
//                            }
//                        }
//                        return @""; //return "<ACCESS ERROR>";
//                    }
//                    return @"<ПУТЬ НЕ НАЙДЕН>"; //return "<PATH ERROR>";
//                }
//                catch (Exception exception)
//                {
//                    return exception.Message;
//                }
//            }
//        }

//        /// <summary>
//        /// Функции
//        /// </summary>
//        public static class Functions
//        {
//            // Путь в RegEdit
//            public static bool OpenRegEdit(RegistryHive regHive, string path)
//            {
//                try
//                {
//                    var rk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
//                        Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                        OpenSubKey(UtilsKey.Folder.SoftwareMicrosoftWindowsCurrentVersionAppletsRegedit,
//                            RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);
//                    if (rk != null)
//                    {
//                        switch (regHive)
//                        {
//                            case RegistryHive.CurrentUser:
//                                rk.SetValue("LastKey", @"HKEY_CURRENT_USER\" + path);
//                                break;
//                            case RegistryHive.LocalMachine:
//                                rk.SetValue("LastKey", @"HKEY_LOCAL_MACHINE\" + path);
//                                break;
//                            case RegistryHive.ClassesRoot:
//                                rk.SetValue("LastKey", @"HKEY_CLASSES_ROOT\" + path);
//                                break;
//                            case RegistryHive.Users:
//                                rk.SetValue("LastKey", @"HKEY_USERS\" + path);
//                                break;
//                            case RegistryHive.PerformanceData:
//                                rk.SetValue("LastKey", @"HKEY_PERFORMANCE_DATA\" + path);
//                                break;
//                            case RegistryHive.CurrentConfig:
//                                rk.SetValue("LastKey", @"HKEY_CURRENT_CONFIG\" + path);
//                                break;
//                            case RegistryHive.DynData:
//                                rk.SetValue("LastKey", @"HKEY_DYN_DATA\" + path);
//                                break;
//                        }
//                        return true;
//                    }
//                    switch (regHive)
//                    {
//                        case RegistryHive.CurrentUser:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_CURRENT_USER\" + path,
//                                "Редактор реестра");
//                            break;
//                        case RegistryHive.LocalMachine:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_LOCAL_MACHINE\" + path,
//                                "Редактор реестра");
//                            break;
//                        case RegistryHive.ClassesRoot:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_CLASSES_ROOT\" + path,
//                                "Редактор реестра");
//                            break;
//                        case RegistryHive.Users:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_USERS\" + path,
//                                "Редактор реестра");
//                            break;
//                        case RegistryHive.PerformanceData:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_PERFORMANCE_DATA\" + path,
//                                "Редактор реестра");
//                            break;
//                        case RegistryHive.CurrentConfig:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_CURRENT_CONFIG\" + path,
//                                "Редактор реестра");
//                            break;
//                        case RegistryHive.DynData:
//                            UtilsMessageBoxAutoClosing.Show(
//                                "Раздел реестра не найден!" + Environment.NewLine + @"HKEY_DYN_DATA\" + path,
//                                "Редактор реестра");
//                            break;
//                    }
//                }
//                catch (Exception exception)
//                {
//                    UtilsMessageBoxAutoClosing.Show(
//                        "Открытие редактора реестра" + Environment.NewLine + Environment.NewLine +
//                        exception.Message, "Редактор реестра", 10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//                return false;
//            }

//            /// <summary>
//            /// Ключ
//            /// </summary>
//            public static class Key
//            {
//                public static bool Remove(RegistryHive regHive, string path, string key,
//                    bool showMessage = false)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey);
//                        //UtilsMessageBoxAutoClosing.Show(regHive + @"\" + path + @"\" + key,
//                        //    "Удаление ключа реестра", 10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        rk?.DeleteValue(key);
//                        return true;
//                    }
//                    catch (Exception exception)
//                    {
//                        if (showMessage)
//                            UtilsMessageBoxAutoClosing.Show(
//                                exception.Message + Environment.NewLine + Environment.NewLine +
//                                regHive + @"\" + path + @"\" + key,
//                                "Удаление ключа реестра", 10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    return false;
//                }

//                public static bool RemoveInside(RegistryHive regHive, string path, string key,
//                    RegistryValueKind rkValueKind, string value)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);
//                        if (rkValueKind == rk?.GetValueKind(key))
//                        {
//                            if (rk.GetValue(key).ToString().Contains(value))
//                            {
//                                rk.SetValue(key, rk.GetValue(key).ToString().Replace(value, ""),
//                                    RegistryValueKind.String);
//                                return true;
//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //UtilsMessageBoxAutoClosing.Show(exception.Message, "Редактор реестра", 10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    return false;
//                }

//                public static bool RemoveDefault(RegistryHive regHive, string path,
//                    bool showMessage = false)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);
//                        rk?.DeleteValue(string.Empty);
//                        return true;
//                    }
//                    catch (Exception exception)
//                    {
//                        if (showMessage)
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Удаление ключа по умолчанию реестра",
//                                10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    return false;
//                }

//                public static class Add
//                {
//                    public static bool String(RegistryHive regHive, string path, string key,
//                        string value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(key, Convert.ToString(rk.GetValue(key)) + value,
//                                    RegistryValueKind.String);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }

//                    public static bool Dword(RegistryHive regHive, string path, string key,
//                        ulong value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(key, Convert.ToInt32(rk.GetValue(key)) +
//                                                    BitConverter.ToInt32(BitConverter.GetBytes(value), 0),
//                                    RegistryValueKind.DWord);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }

//                }

//                public static class Set
//                {
//                    public static bool DefaultString(RegistryHive regHive, string path,
//                        string value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(string.Empty, value, RegistryValueKind.String);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }

//                    public static bool String(RegistryHive regHive, string path, string key,
//                        string value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(key, value, RegistryValueKind.String);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }

//                    public static bool MultiString(RegistryHive regHive, string path, string key,
//                        string[] value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(key, value, RegistryValueKind.MultiString);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }

//                    public static bool Dword(RegistryHive regHive, string path, string key,
//                        ulong value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(key, BitConverter.ToInt32(BitConverter.GetBytes(value), 0),
//                                    RegistryValueKind.DWord);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }

//                    public static bool Binary(RegistryHive regHive, string path, string key,
//                        byte[] value)
//                    {
//                        try
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.SetValue);
//                            if (rk != null)
//                            {
//                                rk.SetValue(key, value, RegistryValueKind.Binary);
//                                return true;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Назначение параметра реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                        return false;
//                    }
//                }

//                public static string Get(RegistryHive regHive, string path, string key,
//                    RegistryValueKind rkValueKind, bool showMessage = false)
//                {
//                    try
//                    {
//                        //MessageBox.Show(@"Get: " + regHive + @"\" + path + @"\" + key + @" [" + rkValueKind + @"]");
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.QueryValues);
//                        if (rk?.GetValue(key) != null)
//                        {
//                            if (rk.GetValueKind(key) == rkValueKind)
//                            {
//                                switch (rkValueKind)
//                                {
//                                    case RegistryValueKind.Binary:
//                                        return "BIN";
//                                    case RegistryValueKind.DWord:
//                                        return Convert.ToString(rk.GetValue(key));
//                                    default:
//                                        return rk.GetValue(key).ToString();
//                                }
//                            }
//                            return "<TYPE ERROR>";
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        if (showMessage)
//                            UtilsMessageBoxAutoClosing.Show(
//                                exception.Message + Environment.NewLine + Environment.NewLine +
//                                regHive + @"\" + path + @"\" + key + @" [" + rkValueKind + @"]",
//                                "Чтение ключа реестра", 10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return exception.Message;
//                    }
//                    return "<NULL>";
//                }

//                public static bool Exists(RegistryHive regHive, string path, string key)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey);
//                        if (rk?.GetValue(key) != null)
//                            return true;
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }

//                public static bool Check(RegistryHive regHive, string path, string key,
//                    RegistryValueKind rkValueKind, bool showMessage = false)
//                {
//                    try
//                    {
//                        //MessageBox.Show(@"Check: " + regHive + @"\" + path + @"\" + key + @" [" + rkValueKind + @"]");
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey);
//                        //MessageBox.Show(@"Check: " + regHive + @"\" + path + @"\" + key + @" [" + rkValueKind + @"] = " + 
//                        //    ((rk?.GetValue(key) != null) ? @"'" + rk.GetValue(key) + @"'" : "<null>"));
//                        if (rk?.GetValue(key) != null && rk.GetValueKind(key) == rkValueKind)
//                            return true;
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message + Environment.NewLine + Environment.NewLine +
//                                                    regHive + @"\" + path + @"\" + key + @" [" + rkValueKind + @"]",
//                            "Проверка ключареестра", 10000, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    return false;
//                }

//                public static bool Contains(RegistryHive regHive, string path, string key,
//                    RegistryValueKind rkValueKind, string value)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey);
//                        if (rkValueKind == rk?.GetValueKind(key))
//                        {
//                            if (rk.GetValue(key).ToString().Contains(value))
//                                return true;
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                    return false;
//                }
//            }

//            /// <summary>
//            /// Раздел
//            /// </summary>
//            public static class Path
//            {
//                public static void Remove(RegistryHive regHive, string path, bool showMessage)
//                {
//                    try
//                    {
//                        #region Вопрос

//                        var action = false;
//                        if (showMessage)
//                        {
//                            if (UtilsMessageBoxAutoClosing.Show("Выполнить удаление раздела со вложенными подразделами?",
//                                "Редактор реестра", 10000, MessageBoxButtons.YesNoCancel,
//                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
//                                action = true;
//                        }
//                        else
//                            action = true;

//                        #endregion

//                        if (action)
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32)
//                                .OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.WriteKey);
//                            if (rk == null)
//                            {
//                                if (showMessage)
//                                    UtilsMessageBoxAutoClosing.Show("Раздел реестра уже удалён.", "Редактор реестра");
//                            }
//                            else
//                            {
//                                RegistryKey.OpenBaseKey(regHive,
//                                    Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                    DeleteSubKeyTree(path);
//                                if (showMessage)
//                                    UtilsMessageBoxAutoClosing.Show("Раздел реестра удалён успешно.", "Редактор реестра");
//                            }
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Удаление раздела реестра", 10000,
//                            MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }

//                public static void Create(RegistryHive regHive, string path, bool showMessage = false)
//                {
//                    try
//                    {
//                        #region Вопрос

//                        var action = false;
//                        if (showMessage)
//                        {
//                            if (UtilsMessageBoxAutoClosing.Show(
//                                "Выполнить создание раздела?" + Environment.NewLine + path, "Редактор реестра", 10000,
//                                MessageBoxButtons.YesNoCancel,
//                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
//                                action = true;
//                        }
//                        else
//                            action = true;

//                        #endregion

//                        if (action)
//                        {
//                            var rk = RegistryKey.OpenBaseKey(regHive,
//                                Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32)
//                                .OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree,
//                                    RegistryRights.CreateSubKey);
//                            if (rk != null)
//                            {
//                                if (showMessage)
//                                    UtilsMessageBoxAutoClosing.Show("Раздел реестра уже создан.", "Редактор реестра");
//                            }
//                            else
//                            {
//                                RegistryKey.OpenBaseKey(regHive,
//                                    Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                                    CreateSubKey(path);
//                                if (showMessage)
//                                    UtilsMessageBoxAutoClosing.Show("Раздел реестра создан успешно.", "Редактор реестра");
//                            }
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        if (showMessage)
//                            UtilsMessageBoxAutoClosing.Show(exception.Message, "Создание раздела реестра", 10000,
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }

//                public static void CreateSoftwareMicrosoftWindowsCurrentVersionExplorerAppKey(bool showMessage)
//                {
//                    try
//                    {
//                        for (var i = 1; i <= 60; i++)
//                        {
//                            Create(RegistryHive.LocalMachine,
//                                UtilsKey.Folder.SoftwareMicrosoftWindowsCurrentVersionExplorerAppKey + "\\" +
//                                Convert.ToString(i), showMessage);
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //
//                    }
//                }

//                public static bool CreateSubPath(RegistryHive regHive, string path,
//                    string subPath)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
//                        if (rk != null)
//                        {
//                            rk.CreateSubKey(subPath);
//                            return true;
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Создать подраздел реестра", 10000,
//                            MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    return false;
//                }

//                public static bool Exists(RegistryHive regHive, string path)
//                {
//                    try
//                    {
//                        var rk = RegistryKey.OpenBaseKey(regHive,
//                            Win64Platform ? RegistryView.Registry64 : RegistryView.Registry32).
//                            OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.QueryValues);
//                        if (rk != null)
//                        {
//                            return true;
//                        }
//                    }
//                    catch (Exception exception)
//                    {
//                        UtilsMessageBoxAutoClosing.Show(exception.Message, "Проверка наличия раздела реестра", 10000,
//                            MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    return false;
//                }
//            }

//            /// <summary>
//            /// Запрос значения
//            /// </summary>
//            public static class Query
//            {
//                public static bool Command(string query, string select = null, int timeOutSeconds = 60,
//                    bool kav = false)
//                {
//                    return UtilsWinCmd.Run.ExecCommand("reg", query, @select, timeOutSeconds, kav);
//                }

//                public static bool Cmd(string[] query)
//                {
//                    return UtilsWinCmd.Run.ExecCmd(query);
//                }
//            }
//        }
    }
}
