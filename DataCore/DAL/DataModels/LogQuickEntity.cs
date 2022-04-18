// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.DataModels
{
    public class LogQuickEntity : BaseEntity<LogQuickEntity>
    {
        #region Public and private fields and properties

        public virtual string Scale { get; set; }
        public virtual string Host { get; set; }
        public virtual string App { get; set; }
        public virtual string Version { get; set; }
        public virtual string File { get; set; }
        public virtual int Line { get; set; }
        public virtual string Member { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Message { get; set; }

        #endregion

        #region Constructor and destructor

        public LogQuickEntity() : this(Guid.Empty)
        {
            //
        }

        public LogQuickEntity(Guid uid) : base(uid)
        {
            Scale = string.Empty;
            Host = string.Empty;
            App = string.Empty;
            Version = string.Empty;
            File = string.Empty;
            Line = 0;
            Member = string.Empty;
            Icon = string.Empty;
            Message = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
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
            return base.Equals(entity) &&
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
                   Equals(Scale, string.Empty) &&
                   Equals(Host, string.Empty) &&
                   Equals(App, string.Empty) &&
                   Equals(Version, string.Empty) &&
                   Equals(File, string.Empty) &&
                   Equals(Line, 0) &&
                   Equals(Member, string.Empty) &&
                   Equals(Icon, string.Empty) &&
                   Equals(Message, string.Empty);
        }

        public override object Clone()
        {
            LogQuickEntity item = (LogQuickEntity)base.Clone();
            item.Scale = Scale;
            item.Host = Host;
            item.App = App;
            item.Version = Version;
            item.File = File;
            item.Line = Line;
            item.Member = Member;
            item.Icon = Icon;
            item.Message = Message;
            return item;
        }

        #endregion
    }
}
