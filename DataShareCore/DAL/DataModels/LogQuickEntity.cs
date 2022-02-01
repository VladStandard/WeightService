// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataShareCore.DAL.DataModels
{
    public class LogQuickEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; }
        public virtual string Scale { get; set; } = string.Empty;
        public virtual string Host { get; set; } = string.Empty;
        public virtual string App { get; set; } = string.Empty;
        public virtual string Version { get; set; } = string.Empty;
        public virtual string File { get; set; } = string.Empty;
        public virtual int Line { get; set; }
        public virtual string Member { get; set; } = string.Empty;
        public virtual string Icon { get; set; } = string.Empty;
        public virtual string Message { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public LogQuickEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(Uid)}: {Uid}. " +
                   $"{nameof(CreateDt)}: {CreateDt}. " +
                   $"{nameof(Scale)}: {Scale}. " +
                   $"{nameof(Host)}: {Host}. " +
                   $"{nameof(App)}: {App}. " +
                   $"{nameof(Version)}: {Version}. " +
                   $"{nameof(File)}: {File}. " +
                   $"{nameof(Line)}: {Line}. " +
                   $"{nameof(Member)}: {Member}. " +
                   $"{nameof(Icon)}: {Icon}. " +
                   $"{nameof(Message)}: {Message}. ";
        }

        public virtual bool Equals(LogQuickEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return 
                   Equals(Uid, entity.Uid) &&
                   Equals(CreateDt, entity.CreateDt) &&
                   Equals(Scale, entity.Scale) &&
                   Equals(Host, entity.Host) &&
                   Equals(App, entity.App) &&
                   Equals(Version, entity.Version) &&
                   Equals(File, entity.File) &&
                   Equals(Line, entity.Line) &&
                   Equals(Member, entity.Member) &&
                   Equals(Icon, entity.Icon) &&
                   Equals(Message, entity.Message);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LogQuickEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new LogQuickEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Uid, default(Guid)) &&
                   Equals(CreateDt, default(DateTime)) &&
                   Equals(Scale, default(string)) &&
                   Equals(Host, default(string)) &&
                   Equals(App, default(string)) &&
                   Equals(Version, default(string)) &&
                   Equals(File, default(string)) &&
                   Equals(Line, default(int)) &&
                   Equals(Member, default(string)) &&
                   Equals(Icon, default(string)) &&
                   Equals(Message, default(string));
        }

        public override object Clone()
        {
            return new LogQuickEntity
            {
                Uid = Uid,
                CreateDt = CreateDt,
                Scale = Scale,
                Host = Host,
                App = App,
                Version = Version,
                File = File,
                Line = Line,
                Member = Member,
                Icon = Icon,
                Message = Message,
            };
        }

        #endregion
    }
}
