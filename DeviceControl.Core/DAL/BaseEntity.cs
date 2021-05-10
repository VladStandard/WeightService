using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DeviceControl.Core.DAL
{
    public class BaseEntity : ICloneable
    {
        #region Public and private fields and properties

        public virtual int Id { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}. ";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual bool Equals(BaseEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Id.Equals(entity.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntity)obj);
        }

        public virtual bool EqualsDefault()
        {
            return Equals(Id, default(int));
        }

        public virtual bool EqualsEmpty()
        {
            if (Equals(default) || Equals(null))
                return true;
            return false;
        }

        public virtual object Clone()
        {
            return new BaseEntity
            {
                Id = Id,
            };
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
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            if (useBase64)
            {
                var base64String = Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.None);
                return Encoding.Default.GetBytes(base64String);
            }
            return memoryStream.ToArray();
        }

        public virtual Image GetImage(byte[] bytes, bool useBase64)
        {

            var ms = new MemoryStream(bytes, 0, bytes.Length);
            if (useBase64)
                ms.Write(Convert.FromBase64String(bytes.ToString()), 0, bytes.Length);
            else
                ms.Write(bytes, 0, bytes.Length);
            return Image.FromStream(ms, true);
        }

        #endregion
    }
}
