// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataShareCore.DAL.Models
{
    public class BaseEntity : IBaseEntity, ICloneable
    {
        #region Public and private fields and properties

        public virtual PrimaryColumnEntity PrimaryColumn { get; set; } = new PrimaryColumnEntity(0);
        public virtual int Id { get => PrimaryColumn.Id; set { PrimaryColumn.Id = value; } }
        public virtual Guid Uid { get => PrimaryColumn.Uid; set { PrimaryColumn.Uid = value; } }

        #endregion

        #region Public and private methods

        public virtual bool EqualsEmpty() => PrimaryColumn == null;

        public virtual byte[] CloneBytes(byte[] bytes)
        {
            byte[] result = new byte[bytes.Length];
            bytes.CopyTo(result, 0);
            return result;
        }

        public virtual string GetBytesLength(byte[] bytes)
        {
            return bytes == null ? "Объём данных: 0 байт" :
                Encoding.Default.GetString(bytes).Length > 1024
                    ? $"Объём данных: {(float)Encoding.Default.GetString(bytes).Length / 1024:### ###.###} Кбайт"
                    : $"Объём данных: {Encoding.Default.GetString(bytes).Length:### ###} байт";
        }

        public virtual object? GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            return null;
        }

        public virtual async Task<byte[]> GetBytes(Stream stream, bool useBase64)
        {
            MemoryStream memoryStream = new();
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

            MemoryStream ms = new(bytes, 0, bytes.Length);
            ms.Write(useBase64 ? Convert.FromBase64String(bytes.ToString()) : bytes, 0, bytes.Length);
            return Image.FromStream(ms, true);
        }

        #endregion

        #region Public and private methods - override

        public override string ToString() => PrimaryColumn == null ? string.Empty : PrimaryColumn.ToString();

        public override int GetHashCode() => PrimaryColumn == null ? -1 : PrimaryColumn.GetHashCode();

        public virtual bool Equals(BaseEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return PrimaryColumn != null && PrimaryColumn.Equals(entity.PrimaryColumn);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntity)obj);
        }

        public virtual bool EqualsDefault() => PrimaryColumn == null || PrimaryColumn.EqualsDefault();

        public virtual object Clone() => PrimaryColumn == null ? new object() : PrimaryColumn.Clone();
        
        #endregion
    }
}
