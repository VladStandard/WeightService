using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCore.DAL
{
    public class BaseEntity
    {
        #region Public and private methods

        public virtual bool EqualsEmpty()
        {
            if (Equals(default) || Equals(null))
                return true;
            return false;
        }

        public virtual byte[] CloneBytes(byte[] bytes)
        {
            byte[] result = bytes != null ? new byte[(int)bytes?.Length] : null;
            bytes?.CopyTo(result, 0);
            return result;
        }

        public virtual string GetBytesLength(byte[] bytes)
        {
            return bytes == null ? "Объём данных: 0 байт" :
                Encoding.Default.GetString(bytes).Length > 1024
                    ? $"Объём данных: {(float)(Encoding.Default.GetString(bytes).Length) / 1024:### ###.###} Кбайт"
                    : $"Объём данных: {Encoding.Default.GetString(bytes).Length:### ###} байт";
        }

        public virtual object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            return null;
        }

        public virtual async Task<byte[]> GetBytes(Stream stream, bool useBase64)
        {
            MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            if (useBase64)
            {
                string base64String = Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.None);
                return Encoding.Default.GetBytes(base64String);
            }
            return memoryStream.ToArray();
        }

        public virtual Image GetImage(byte[] bytes, bool useBase64)
        {

            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            ms.Write(useBase64 ? Convert.FromBase64String(bytes.ToString()) : bytes, 0, bytes.Length);
            return Image.FromStream(ms, true);
        }

        #endregion
    }
}
