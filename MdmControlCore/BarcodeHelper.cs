// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Threading;

namespace MdmControlCore
{
    public enum EnumGtinVariant
    {
        Var1,
        Var2,
        Var3,
    }

    public class BarcodeHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static BarcodeHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static BarcodeHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private methods

        public int GetEanCheckDigit(string code)
        {
            if (code.Length != 12)
                throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 12 characters!");

            var sum = 0;
            // Calculate the checksum digit here.
            for (var i = code.Length; i >= 1; i--)
            {
                var digit = Convert.ToInt32(code.Substring(i - 1, 1));
                // This appears to be backwards but the EAN-13 checksum must be calculated this way to be compatible with UPC-A.
                if (i % 2 == 0) // odd
                    sum += digit * 3;
                else            // even
                    sum += digit * 1;
            }
            return (10 - sum % 10) % 10;
        }

        public int GetGtinCheckDigitV1(string code)
        {
            if (code.Length != 13)
                throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 13 characters!");

            var sum = 0;
            for (var i = 0; i < code.Length; i++)
            {
                var n = int.Parse(code.Substring(code.Length - 1 - i, 1));
                sum += i % 2 == 0 ? n * 3 : n;
            }
            return sum % 10 == 0 ? 0 : 10 - sum % 10;
        }

        public int GetGtinCheckDigitV2(string code)
        {
            if (code.Length != 13)
                throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 13 characters!");
            
            var sum = 0;
            var list = code.Reverse().ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                var n = (int)char.GetNumericValue(list[i]);
                sum += i % 2 == 0 ? n * 3 : n;
            }
            return sum % 10 == 0 ? 0 : 10 - sum % 10;
        }

        public int GetGtinCheckDigitV3(string code)
        {
            if (code.Length != 13)
                throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 13 characters!");
            
            var sum = code.Reverse().Select((c, i) => (int)char.GetNumericValue(c) * (i % 2 == 0 ? 3 : 1)).Sum();
            return (10 - sum % 10) % 10;
        }

        public string GetGtin(string code, EnumGtinVariant gtinVariant = EnumGtinVariant.Var3)
        {
            if (string.IsNullOrEmpty(code))
                return string.Empty;

            return gtinVariant switch
            {
                EnumGtinVariant.Var1 => $"{code}{GetGtinCheckDigitV3(code)}",
                EnumGtinVariant.Var2 => $"{code}{GetGtinCheckDigitV2(code)}",
                _ => $"{code}{GetGtinCheckDigitV3(code)}"
            };
        }

        #endregion
    }
}