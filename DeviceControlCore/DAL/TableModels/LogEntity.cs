using System;

namespace DeviceControlCore.DAL.TableModels
{
    public class LogEntity : BaseUidEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; }
        public virtual string Scale { get; set; }
        public virtual HostsEntity Host { get; set; }
        public virtual AppEntity App { get; set; }
        public virtual string Version { get; set; }
        public virtual string File { get; set; }
        public virtual int Line { get; set; }
        public virtual string Member { get; set; }
        public virtual LogTypeEntity LogType { get; set; }
        public virtual string Message { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strHost = Host != null ? Host.Id.ToString() : "null";
            var strApp = App != null ? App.Uid.ToString() : "null";
            var strLogType = LogType != null ? LogType.Uid.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDt)}: {CreateDt}. " +
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
                   Equals(CreateDt, entity.CreateDt) &&
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
                   Equals(CreateDt, default(DateTime)) &&
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
                Uid = Uid,
                CreateDt = CreateDt,
                Scale = Scale,
                Host = (HostsEntity)Host?.Clone(),
                App = (AppEntity)App?.Clone(),
                Version = Version,
                File = File,
                Line = Line,
                Member = Member,
                LogType = (LogTypeEntity)LogType?.Clone(),
                Message = Message,
            };
        }

        #endregion
    }
}
