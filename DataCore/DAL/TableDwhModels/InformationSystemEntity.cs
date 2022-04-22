// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableDwhModels
{
    public class InformationSystemEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string ConnectString1 { get; set; }
        public virtual string ConnectString2 { get; set; }
        public virtual string ConnectString3 { get; set; }
        public virtual int StatusId { get; set; }

        #endregion

        #region Constructor and destructor

        public InformationSystemEntity() : this(0)
        {
            //
        }

        public InformationSystemEntity(long id) : base(id)
        {
            Name = string.Empty;
            ConnectString1 = string.Empty;
            ConnectString2 = string.Empty;
            ConnectString3 = string.Empty;
            StatusId = 0;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. " +
            $"{nameof(ConnectString1)}: {ConnectString1}. " +
            $"{nameof(ConnectString2)}: {ConnectString2}. " +
            $"{nameof(ConnectString3)}: {ConnectString3}. " +
            $"{nameof(StatusId)}: {StatusId}. ";

        public virtual bool Equals(InformationSystemEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(ConnectString1, item.ConnectString1) &&
                   Equals(ConnectString2, item.ConnectString2) &&
                   Equals(ConnectString3, item.ConnectString3) &&
                   Equals(StatusId, item.StatusId);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((InformationSystemEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new InformationSystemEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Name, string.Empty) &&
                   Equals(ConnectString1, string.Empty) &&
                   Equals(ConnectString2, string.Empty) &&
                   Equals(ConnectString3, string.Empty) &&
                   Equals(StatusId, 0);
        }

        public override object Clone()
        {
            InformationSystemEntity item = (InformationSystemEntity)base.Clone();
            item.Name = Name;
            item.ConnectString1 = Name;
            item.ConnectString2 = Name;
            item.ConnectString3 = Name;
            item.StatusId = StatusId;
            return item;
        }

        #endregion
    }
}
