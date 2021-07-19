using System;

namespace DeviceControl.Core.DAL.DataModels
{
    public class LogSummaryEntity : BaseUidEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; }
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

        public virtual bool Equals(LogSummaryEntity entity)
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
            return Equals((LogSummaryEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new LogSummaryEntity());
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
            return new LogSummaryEntity
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
