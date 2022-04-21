// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Printers".
    /// </summary>
    public class PrinterEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string Ip { get; set; }
        public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
        public virtual short Port { get; set; }
        public virtual string Password { get; set; }
        public virtual PrinterTypeEntity PrinterType { get; set; }
        public virtual MacAddressEntity MacAddress { get; set; }
        public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; }
        public virtual bool PeelOffSet { get; set; }
        public virtual short DarknessLevel { get; set; }
        [XmlIgnore] public virtual System.Net.HttpStatusCode HttpStatusCode { get; set; }
        [XmlIgnore] public virtual Exception? HttpStatusException { get; set; }

        #endregion

        #region Constructor and destructor

        public PrinterEntity() : this(0)
        {
            //
        }

        public PrinterEntity(long id) : base(id)
        {
            Name = string.Empty;
            Ip = string.Empty;
            Port = 0;
            Password = string.Empty;
            PrinterType = new PrinterTypeEntity();
            MacAddress = new();
            PeelOffSet = false;
            DarknessLevel = 0;
            HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
            HttpStatusException = null;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strPrinterType = PrinterType != null ? PrinterType.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(Port)}: {Port}. " +
                   $"{nameof(Password)}: {Password}. " +
                   $"{nameof(PrinterType)}: {strPrinterType}. " +
                   $"{nameof(MacAddress)}: {MacAddress}. " +
                   $"{nameof(PeelOffSet)}: {PeelOffSet}. " +
                   $"{nameof(DarknessLevel)}: {DarknessLevel}. " +
                   $"{nameof(HttpStatusCode)}: {HttpStatusCode}. " + 
                   $"{nameof(HttpStatusException)}: {HttpStatusException}. ";
        }

        public virtual bool Equals(PrinterEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(Ip, item.Ip) &&
                   Equals(Port, item.Port) &&
                   Equals(Password, item.Password) &&
                   Equals(PrinterType, item.PrinterType) &&
                   Equals(MacAddress, item.MacAddress) &&
                   Equals(PeelOffSet, item.PeelOffSet) &&
                   Equals(DarknessLevel, item.DarknessLevel) &&
                   Equals(HttpStatusCode, item.HttpStatusCode) &&
                   Equals(HttpStatusException, item.HttpStatusException);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PrinterEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new PrinterEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (PrinterType != null && !PrinterType.EqualsDefault())
                return false;
            if (MacAddress != null && !MacAddress.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Name, string.Empty) &&
                   Equals(Ip, string.Empty) &&
                   Equals(Port, 0) &&
                   Equals(Password, string.Empty) &&
                   Equals(PeelOffSet, false) &&
                   Equals(DarknessLevel, 0) &&
                   Equals(HttpStatusCode, System.Net.HttpStatusCode.BadRequest) &&
                   Equals(HttpStatusException, null);
        }

        public override object Clone()
        {
            PrinterEntity item = (PrinterEntity)base.Clone();
            item.Name = Name;
            item.Ip = Ip;
            item.Port = Port;
            item.Password = Password;
            item.PrinterType = (PrinterTypeEntity)PrinterType.Clone();
            item.MacAddress = (MacAddressEntity)MacAddress.Clone();
            item.PeelOffSet = PeelOffSet;
            item.DarknessLevel = DarknessLevel;
            item.HttpStatusCode = HttpStatusCode;
            item.HttpStatusException = HttpStatusException;
            return item;
        }

        public virtual void SetHttpStatus(int timeOut)
        {
            if (HttpStatusCode == System.Net.HttpStatusCode.OK)
                return;
            HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
            HttpStatusException = null;
            RestClientOptions options = new(Link)
            {
                UseDefaultCredentials = true,
                ThrowOnAnyError = true,
                Timeout = timeOut,
            };
            RestClient client = new(options);
            RestRequest request = new();
            try
            {
                //RestResponse response = await client.GetAsync(request).ConfigureAwait(continueOnCapturedContext);
                RestResponse response = client.GetAsync(request).ConfigureAwait(true).GetAwaiter().GetResult();
                HttpStatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                HttpStatusException = ex;
            }
        }

        #endregion
    }
}
