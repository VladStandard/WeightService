// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Logs".
    /// </summary>
    public class LogEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual HostEntity? Host { get; set; }
        public virtual AppEntity? App { get; set; }
        public virtual LogTypeEntity? LogType { get; set; }
        public virtual string Version { get; set; }
        public virtual string File { get; set; }
        public virtual int Line { get; set; }
        public virtual string Member { get; set; }
        public virtual string Message { get; set; }

        #endregion

        #region Constructor and destructor

        public LogEntity() : this(Guid.Empty)
        {
            //
        }

        public LogEntity(Guid uid) : base(uid)
        {
            Host = null;
            App = null;
            LogType = null;
            Version = string.Empty;
            File = string.Empty;
            Line = 0;
            Member = string.Empty;
            Message = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strHost = Host != null ? Host.IdentityId.ToString() : "null";
            string strApp = App != null ? App.IdentityUid.ToString() : "null";
            string strLogType = LogType != null ? LogType.IdentityUid.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Host)}: {strHost}. " +
                   $"{nameof(App)}: {strApp}. " +
                   $"{nameof(LogType)}: {strLogType}. " +
                   $"{nameof(Version)}: {Version}. " +
                   $"{nameof(File)}: {File}. " +
                   $"{nameof(Line)}: {Line}. " +
                   $"{nameof(Member)}: {Member}. " +
                   $"{nameof(Message)}: {Message}. ";
        }

        public virtual bool Equals(LogEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (Host != null && item.Host != null && !Host.Equals(item.Host))
                return false;
            if (App != null && item.App != null && !App.Equals(item.App))
                return false;
            if (LogType != null && item.LogType != null && !LogType.Equals(item.LogType))
                return false;
            return base.Equals(item) &&
                   Equals(Version, item.Version) &&
                   Equals(File, item.File) &&
                   Equals(Line, item.Line) &&
                   Equals(Member, item.Member) &&
                   Equals(Message, item.Message);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LogEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new());
        }

        public new virtual bool EqualsDefault()
        {
            if (Host != null && !Host.EqualsDefault())
                return false;
            if (App != null && !App.EqualsDefault())
                return false;
            if (LogType != null && !LogType.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(Version, string.Empty) &&
                   Equals(File, string.Empty) &&
                   Equals(Line, 0) &&
                   Equals(Member, string.Empty) &&
                   Equals(Message, string.Empty);
        }

        public new virtual object Clone()
        {
            LogEntity item = new();
            item.Host = Host?.CloneCast();
            item.App = App?.CloneCast();
            item.LogType = LogType?.CloneCast();
            item.Version = Version;
            item.File = File;
            item.Line = Line;
            item.Member = Member;
            item.Message = Message;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual LogEntity CloneCast() => (LogEntity)Clone();

        #endregion
    }
}
