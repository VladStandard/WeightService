// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    public class LogEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Scale { get; set; } = string.Empty;
        public virtual HostEntity Host { get; set; } = new HostEntity();
        public virtual AppEntity App { get; set; } = new AppEntity();
        public virtual string Version { get; set; } = string.Empty;
        public virtual string File { get; set; } = string.Empty;
        public virtual int Line { get; set; }
        public virtual string Member { get; set; } = string.Empty;
        public virtual LogTypeEntity LogType { get; set; } = new LogTypeEntity();
        public virtual string Message { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public LogEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strHost = Host != null ? Host.Id.ToString() : "null";
            string? strApp = App != null ? App.Uid.ToString() : "null";
            string? strLogType = LogType != null ? LogType.Uid.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Scale)}: {Scale}. " +
                   $"{nameof(Host)}: {strHost}. " +
                   $"{nameof(App)}: {strApp}. " +
                   $"{nameof(Version)}: {Version}. " +
                   $"{nameof(File)}: {File}. " +
                   $"{nameof(Line)}: {Line}. " +
                   $"{nameof(Member)}: {Member}. " +
                   $"{nameof(LogType)}: {strLogType}. " +
                   $"{nameof(Message)}: {Message}. ";
        }

        public virtual bool Equals(LogEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Scale, entity.Scale) &&
                   Equals(Host, entity.Host) &&
                   Equals(App, entity.App) &&
                   Equals(Version, entity.Version) &&
                   Equals(File, entity.File) &&
                   Equals(Line, entity.Line) &&
                   Equals(Member, entity.Member) &&
                   Equals(LogType, entity.LogType) &&
                   Equals(Message, entity.Message);
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
            return Equals(new LogEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Host != null && !Host.EqualsDefault())
                return false;
            if (App != null && !App.EqualsDefault())
                return false;
            if (LogType != null && !LogType.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Scale, default(string)) &&
                   Equals(Version, default(string)) &&
                   Equals(File, default(string)) &&
                   Equals(Line, default(int)) &&
                   Equals(Member, default(string)) &&
                   Equals(Message, default(string));
        }

        public override object Clone()
        {
            return new LogEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Scale = Scale,
                Host = (HostEntity)Host.Clone(),
                App = (AppEntity)App.Clone(),
                Version = Version,
                File = File,
                Line = Line,
                Member = Member,
                LogType = (LogTypeEntity)LogType.Clone(),
                Message = Message,
            };
        }

        #endregion
    }
}
