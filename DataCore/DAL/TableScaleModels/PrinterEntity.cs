// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Принтеры".
    /// </summary>
    public class PrinterEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; } = string.Empty;
        public virtual string Ip { get; set; } = string.Empty;
        public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
        public virtual short Port { get; set; }
        public virtual string Password { get; set; } = string.Empty;
        public virtual PrinterTypeEntity PrinterType { get; set; } = new PrinterTypeEntity();
        public virtual MacAddressEntity MacAddress { get; set; }
        public virtual string MacAddressValue
        {
            get => MacAddress.Value;
            set => MacAddress.Value = value;
        }
        public virtual bool PeelOffSet { get; set; }
        public virtual short DarknessLevel { get; set; }
        public virtual System.Net.HttpStatusCode HttpStatusCode { get; set; } = System.Net.HttpStatusCode.BadRequest;
        public virtual Exception? HttpStatusException { get; set; } = null;

        #endregion

        #region Constructor and destructor

        public PrinterEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
            MacAddress = new MacAddressEntity();
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strPrinterType = PrinterType != null ? PrinterType.Id.ToString() : "null";
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

        public virtual bool Equals(PrinterEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Name, entity.Name) &&
                   Equals(Ip, entity.Ip) &&
                   Equals(Port, entity.Port) &&
                   Equals(Password, entity.Password) &&
                   Equals(PrinterType, entity.PrinterType) &&
                   Equals(MacAddress, entity.MacAddress) &&
                   Equals(PeelOffSet, entity.PeelOffSet) &&
                   Equals(DarknessLevel, entity.DarknessLevel) &&
                   Equals(HttpStatusCode, entity.HttpStatusCode) &&
                   Equals(HttpStatusException, entity.HttpStatusException);
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
                   Equals(Name, default(string)) &&
                   Equals(Ip, default(string)) &&
                   Equals(Port, default(short)) &&
                   Equals(Password, default(string)) &&
                   Equals(PeelOffSet, default(bool)) &&
                   Equals(DarknessLevel, default(short)) &&
                   Equals(HttpStatusCode, System.Net.HttpStatusCode.BadRequest) &&
                   Equals(HttpStatusException, null);
        }

        public override object Clone()
        {
            return new PrinterEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Name = Name,
                Ip = Ip,
                Port = Port,
                Password = Password,
                PrinterType = (PrinterTypeEntity)PrinterType.Clone(),
                MacAddress = (MacAddressEntity)MacAddress.Clone(),
                PeelOffSet = PeelOffSet,
                DarknessLevel = DarknessLevel,
                HttpStatusCode = HttpStatusCode,
                HttpStatusException = HttpStatusException,
            };
        }

        public virtual async Task SetHttpStatusAsync(int timeOut = 100, bool continueOnCapturedContext = true)
        {
            HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
            HttpStatusException = null;
            RestSharp.RestClientOptions options = new(Link)
            {
                UseDefaultCredentials = true,
                ThrowOnAnyError = true,
                Timeout = timeOut,
            };
            RestSharp.RestClient client = new(options);
            RestRequest request = new();
            try
            {
                RestResponse response = await client.GetAsync(request).ConfigureAwait(continueOnCapturedContext);
                HttpStatusCode = response.StatusCode;
            }
            catch (System.Exception ex)
            {
                HttpStatusException = ex;
            }
        }

        #endregion
    }
}
