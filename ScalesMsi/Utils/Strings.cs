// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Text;
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo

namespace ScalesMsi.Utils
{
    internal static class Strings
    {
        #region Public fields and properties - Dirs

        private static string _dirProgram = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86) + @"\VladimirStandardCorp";
        /// <summary>
        /// Каталог установки.
        /// </summary>
        public static string DirProgram
        {
            get => _dirProgram;
            set
            {
                if (value.EndsWith(@"\"))
                    value = value.Substring(0, value.Length - 1);
                _dirProgram = value;
            }
        }

        /// <summary>
        /// Главный каталог ScalesUI.
        /// </summary>
        public static string DirProgramScalesUI =>
            string.IsNullOrEmpty(DirProgram) ? string.Empty
            : DirProgram.EndsWith(@"\") ? DirProgram + @"ScalesUI" : DirProgram + @"\ScalesUI";

        /// <summary>
        /// Главный каталог Massa-K.
        /// </summary>
        public static string DirProgramMassa =>
            string.IsNullOrEmpty(DirProgram) ? string.Empty
            : DirProgram.EndsWith(@"\") ? DirProgram + @"Massa-K" : DirProgram + @"\Massa-K";

        /// <summary>
        /// Каталог документации ScalesUI.
        /// </summary>
        public static string DirProgramScalesUIDocs =>
            string.IsNullOrEmpty(DirProgramScalesUI) ? string.Empty
            : DirProgramScalesUI.EndsWith(@"\") ? DirProgramScalesUI + @"Docs" : DirProgramScalesUI + @"\Docs";

        /// <summary>
        /// Каталог руководств ScalesUI.
        /// </summary>
        public static string DirProgramScalesUIManuals =>
            string.IsNullOrEmpty(DirProgramScalesUI) ? string.Empty
            : DirProgramScalesUI.EndsWith(@"\") ? DirProgramScalesUI + @"Manuals" : DirProgramScalesUI + @"\Manuals";

        /// <summary>
        /// Главный каталог TapangaMaha.
        /// </summary>
        public static string DirProgramTapangaMaha =>
            string.IsNullOrEmpty(DirProgram) ? string.Empty
            : DirProgram.EndsWith(@"\") ? DirProgram + @"TapangaMaha" : DirProgram + @"\TapangaMaha";

        /// <summary>
        /// Главный каталог LabelPrint.
        /// </summary>
        public static string DirProgramLabelPrint =>
            string.IsNullOrEmpty(DirProgram) ? string.Empty
            : DirProgram.EndsWith(@"\") ? DirProgram + @"LabelPrint" : DirProgram + @"\LabelPrint";

        /// <summary>
        /// Каталог документации LabelPrint.
        /// </summary>
        public static string DirProgramLabelPrintDocs =>
            string.IsNullOrEmpty(DirProgramLabelPrint) ? string.Empty
            : DirProgramLabelPrint.EndsWith(@"\") ? DirProgramLabelPrint + @"Docs" : DirProgramLabelPrint + @"\Docs";

        /// <summary>
        /// Каталог драйверов ScalesUI.
        /// </summary>
        public static string DirProgramMassaDrivers =>
            string.IsNullOrEmpty(DirProgramMassa) ? string.Empty
            : DirProgramMassa.EndsWith(@"\") ? DirProgramMassa + @"Drivers" : DirProgramMassa + @"\Drivers";

        /// <summary>
        /// Исходный каталог документации ScalesUI.
        /// </summary>
        public static string DirSourceScalesUIDocs => @"Docs";

        /// <summary>
        /// Исходный каталог STM-драйвера ScalesUI.
        /// </summary>
        public static string DirSourceScalesUIDrivers
        {
            get
            {
#if BRANCH
                return @"\\palych\VladimirStandardCorp\Drivers\";
#else 
                return @"\\palych\VladimirStandardCorp\Drivers\";
#endif
            }
        }

        /// <summary>
        /// Исходный каталог руководств ScalesUI.
        /// </summary>
        public static string DirSourceScalesUIManuals
        {
            get
            {
#if BRANCH
                return @"..\Resources\Manuals";
#else 
                return @"..\Resources\Manuals";
#endif
            }
        }

        /// <summary>
        /// Исходный каталог ScalesUI.
        /// </summary>
        public static string DirSourceScalesUI
        {
            get
            {
#if BRANCH
                return @"..\ScalesUI2\bin\x64\Debug";
#elif DEBUG
                return @"..\ScalesUI2\bin\x64\Debug";
#else
                return @"..\ScalesUI2\bin\x64\Release";
#endif
            }
        }

        /// <summary>
        /// Меню Windows VladimirStandardCorp.
        /// </summary>
        public static string MenuVladimirStandardCorp { get; } = @"%ProgramMenu%\VladimirStandardCorp";

        /// <summary>
        /// Исходный каталог TapangaMaha.
        /// </summary>
        public static string DirSourceTapangaMaha
        {
            get
            {
#if BRANCH
                return @"..\TapangaMaha\bin\x64\Debug";
#elif DEBUG
                return @"..\TapangaMaha\bin\x64\Debug";
#else
                return @"..\TapangaMaha\bin\x64\Release";
#endif
            }
        }

        /// <summary>
        /// Исходный каталог Massa-K.
        /// </summary>
        public static string DirSourceMassa => @"\\DS2\Common\VladimirStandardCorp\Massa-K";

        /// <summary>
        /// Исходный каталог LabelPrint.
        /// </summary>
        public static string DirSourceLabelPrint
        {
            get
            {
#if BRANCH
                return @"..\LabelPrint\bin\x64\Debug";
#elif DEBUG
                return @"..\LabelPrint\bin\x64\Debug";
#else
                return @"..\LabelPrint\bin\x64\Release";
#endif
            }
        }

        #endregion

        #region Public fields and properties

        public static string PropInstallDir => "INSTALLDIR";
        public static string PropInstallDirLabelPrint => "INSTALLDIR_LABELPRINT";
        public static string PropInstallDirLabelPrintDocs => "INSTALLDIR_LABELPRINT_DOCS";
        public static string PropInstallDirScalesUI => "INSTALLDIR_SCALESUI";
        public static string PropInstallDirScalesUIDocs => "INSTALLDIR_SCALESUI_DOCS";
        public static string PropInstallDirScalesUIManuals => "INSTALLDIR_SCALESUI_MANUALS";
        public static string PropInstallDirTapangaMaha => "INSTALLDIR_TAPANGAMAHA";
        public static string PropInstallDirMassa => "INSTALLDIR_MASSA";
        public static string PropInstallDirMassaDriver => "INSTALLDIR_MASSA_DRIVER";

        #endregion

        #region Public fields and properties - ScalesUI lists

        /// <summary>
        /// Список бинарных файлов.
        /// </summary>
        public static List<string> ListScalesBin { get; } = new List<string>()
        {
            "CHANGELOG.md",
            "EntitiesLib.dll",
            "WeightCore.dll",
            "log4net.dll",
            "Microsoft.Bcl.AsyncInterfaces.dll",
            "Microsoft.DotNet.PlatformAbstractions.dll",
            "Microsoft.Extensions.DependencyModel.dll",
            "Newtonsoft.Json.dll",
            "ScalesUI.exe.config",
            "SdkApi.Core.dll",
            "SdkApi.Desktop.dll",
            "System.Buffers.dll",
            "System.Memory.dll",
            "System.Numerics.Vectors.dll",
            "System.Runtime.CompilerServices.Unsafe.dll",
            "System.Text.Encodings.Web.dll",
            "System.Text.Json.dll",
            "System.Threading.Tasks.Extensions.dll",
            "System.ValueTuple.dll",
            "tsclibnet.dll",
            "UICommon.dll",
            "UtilsLib.dll",
            "ZebraPrinterSdk.dll",
        };

        /// <summary>
        /// Список файлов документации.
        /// </summary>
        public static List<string> ListScalesDocs { get; } = new List<string>() { "CHANGELOG.md", "README.md", "License.rtf" };

        /// <summary>
        /// Список руководств.
        /// </summary>
        public static List<string> ListScalesManuals { get; } = new List<string>() { "Руководство пользователя.docx" };

        #endregion

        #region Public fields and properties - TapangaMaha lists

        /// <summary>
        /// Список бинарных файлов.
        /// </summary>
        public static List<string> ListTapangaMaha { get; } = new List<string>() 
        {
            //"CHANGELOG.md",
            //"EntitiesLib.dll",
            //"log4net.dll",
            //"Newtonsoft.Json.dll",
            //"SdkApi.Core.dll",
            //"SdkApi.Desktop.dll",
            //"TapangaMaha.exe.config",
            //"UICommon.dll",
            //"UtilsLib.dll",
            //"ZabbixAgentLib.dll",
            //"ZplCommonLib.dll",
        };

        #endregion

        #region Public fields and properties - LabelPrint lists

        /// <summary>
        /// Список бинарных файлов.
        /// </summary>
        public static List<string> ListLabelPrint { get; } = new List<string>()
        {
            "LabelPrint.exe.config",
            "WPF.Utils.dll",
        };

        /// <summary>
        /// Список документации.
        /// </summary>
        public static List<string> ListLabelPrintDocs { get; } = new List<string>()
        {
            "CHANGELOG.md",
        };

        #endregion

        #region Public and private fields and properties - Massa

        /// <summary>
        /// Список файлов драйверов для установки.
        /// </summary>
        public static List<string> ListMassaDrivers { get; } = new List<string>()
        {
            "en.stsw-stm32102.zip",
        };

        /// <summary>
        /// Список файлов драйверов для удаления.
        /// </summary>
        public static List<string> ListMassaDriversForDelete { get; } = new List<string>()
        {
            "readme.txt", 
            "version.txt", 
            "VCP_V1.5.0_Setup_W7_x64_64bits.exe", 
            "VCP_V1.5.0_Setup_W7_x86_32bits.exe", 
            "VCP_V1.5.0_Setup_W8_x64_64bits.exe", 
            "VCP_V1.5.0_Setup_W8_x86_32bits.exe",
        };

        /// <summary>
        /// Список файлов Масса-К для установки.
        /// </summary>
        public static List<string> ListMassa { get; } = new List<string>()
        {
            "ScalesTerminalSetup_V1.3.191.exe",
        };

        #endregion

        #region Public fields and properties - Msi lists

        /// <summary>
        /// Список dll.
        /// </summary>
        public static List<string> ListMsiLib { get; } = new List<string>()
        {
            "UACHelper.dll",
        };

        /// <summary>
        /// Имя программы драйвера.
        /// </summary>
        public static string AppDriverName => "Virtual Comport Driver";
        /// <summary>
        /// Проивзодитель драйвера.
        /// </summary>
        public static string AppDriverVendor => "STMicroelectronics";

        #endregion

        #region Public methods

        /// <summary>
        /// Конверт кириллицы в транслит.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertCyryllicToTranslit(string input)
        {
            var sb = new StringBuilder();
            input = input.ToLower();
            var engChars = new[] { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "c", "ch", "sh", "shch", "j", "i", "j", "e", "yu", "ya", "_" };
            var rusChars = new[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ' ' };
            foreach (char x in input)
            {
                var position_s = System.Array.IndexOf(rusChars, x);
                if (position_s == -1)
                    sb.Append(x);
                else
                    sb.Append(engChars[position_s]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Получить Id для Feature.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetFeatureId(string input)
        {
            input = ConvertCyryllicToTranslit(input);
            return input.Replace("-", "").Replace(" ", "").Replace("_", "").Replace(@"\", "");
        }


        #endregion
    }
}
