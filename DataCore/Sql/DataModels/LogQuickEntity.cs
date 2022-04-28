// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.DataModels
{
    public class LogQuickEntity : BaseEntity
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

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Scale)}: {Scale}. " +
            $"{nameof(Host)}: {Host}. " +
            $"{nameof(App)}: {App}. " +
            $"{nameof(Version)}: {Version}. " +
            $"{nameof(File)}: {File}. " +
            $"{nameof(Line)}: {Line}. " +
            $"{nameof(Member)}: {Member}. " +
            $"{nameof(Icon)}: {Icon}. " +
            $"{nameof(Message)}: {Message}. ";

        public virtual bool Equals(LogQuickEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(CreateDt, item.CreateDt) &&
                   Equals(Scale, item.Scale) &&
                   Equals(Host, item.Host) &&
                   Equals(App, item.App) &&
                   Equals(Version, item.Version) &&
                   Equals(File, item.File) &&
                   Equals(Line, item.Line) &&
                   Equals(Member, item.Member) &&
                   Equals(Icon, item.Icon) &&
                   Equals(Message, item.Message);
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
            return base.EqualsDefault(IdentityName) &&
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

        public new virtual object Clone()
        {
            LogQuickEntity item = new()
            {
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
            item.Setup(((BaseEntity)this).CloneCast);
            return item;
        }

        public new virtual LogQuickEntity CloneCast => (LogQuickEntity)Clone();

        #endregion
    }
}
