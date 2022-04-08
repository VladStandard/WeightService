// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Utils;
using System.Text;

namespace DataCore.DAL.Models
{
    public class ImageDataEntity
    {
        #region Public and private fields and properties

        public virtual byte[] Value { get; set; }
        
        public virtual string ValueAscii
        {
            get => Value == null || Value.Length == 0 ? string.Empty : Encoding.Default.GetString(Value);
            set => Value = Encoding.Default.GetBytes(value);
        }
        
        public virtual string ValueUnicode
        {
            get => Value == null || Value.Length == 0 ? string.Empty : Encoding.Unicode.GetString(Value);
            set => Value = Encoding.Unicode.GetBytes(value);
        }

        public string Info
        {
            get => DataUtils.GetBytesLength(Value);
            set => _ = value;
        }

        #endregion

        #region Constructor and destructor

        public ImageDataEntity()
        {
            Value = new byte[0];
        }

        public ImageDataEntity(byte[] value)
        {
            Value = DataUtils.CloneBytes(value);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(Info)}: {Info}. ";
        }

        public virtual bool Equals(ImageDataEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Equals(Value, entity.Value);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ImageDataEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ImageDataEntity());
        }

        public virtual bool EqualsDefault()
        {
            return Equals(Value, new byte[0]);
        }

        public void SetTemplateValue()
        {
            ValueUnicode = @"
<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet version=""2.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
<xsl:output method=""text"" encoding=""UTF-16"" omit-xml-declaration=""no""/>
<xsl:template match=""/"">

<xsl:text>^XA

^CI28
^CWK,E:COURB.TTF
^CWL,E:COURBI.TTF
^CWM,E:COURBD.TTF
^CWN,E:COUR.TTF
^CWZ,E:ARIAL.TTF
^CWW,E:ARIALBI.TTF
^CWE,E:ARIALBD.TTF
^CWR,E:ARIALI.TTF

^LH0,30
^FWR
</xsl:text>

<xsl:variable name=""length"" select=""50"" />
<xsl:variable name=""width"" select=""30"" />

<!-- Дата изготовления: метка -->
<xsl:text>
</xsl:text>

<!-- Дата изготовления: значение -->
<xsl:text>
</xsl:text>

<!-- Масса нетто: метка -->
</xsl:text>

<!-- Масса нетто: значение -->
</xsl:text>

<!-- Bar Code  -->
<xsl:text>

^PQ1

^XZ</xsl:text>

</xsl:template>
</xsl:stylesheet>
                ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        }

        #endregion
    }
}
