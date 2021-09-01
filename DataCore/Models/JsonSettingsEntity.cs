// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Configuration;

namespace DataCore.Models
{
    /// <summary>
    /// appsettings.json
    /// </summary>
    public class JsonSettingsEntity
    {
        public IConfiguration Configuration { get; }
        public string Server
        {
            get => Configuration["Sql:Server"];
            set => Configuration["Sql:Server"] = value;
        }
        public string Db
        {
            get => Configuration["Sql:Db"];
            set => Configuration["Sql:Db"] = value;
        }
        public bool Trusted
        {
            get => Convert.ToBoolean(Configuration["Sql:Trusted"]);
            set => Configuration["Sql:Trusted"] = value.ToString();
        }
        public string Username
        {
            get => Configuration["Sql:Username"];
            set => Configuration["Sql:Username"] = value;
        }
        public string Password
        {
            get => Configuration["Sql:Password"];
            set => Configuration["Sql:Password"] = value;
        }
        public bool IsDebug
        {
            get => Convert.ToBoolean(Configuration["IsDebug"]);
            set => Configuration["IsDebug"] = value.ToString();
        }
        public int SectionRowCount
        {
            get => Convert.ToInt32(Configuration["SectionRowCount"]);
            set => Configuration["SectionRowCount"] = value.ToString();
        }
        public int ItemRowCount
        {
            get => Convert.ToInt32(Configuration["ItemRowCount"]);
            set => Configuration["ItemRowCount"] = value.ToString();
        }
        public string ButtonWidth
        {
            get => Configuration["ButtonWidth"];
            set => Configuration["ButtonWidth"] = value;
        }
        public string ButtonHeight
        {
            get => Configuration["ButtonHeight"];
            set => Configuration["ButtonHeight"] = value;
        }

        public JsonSettingsEntity(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override string ToString()
        {
            string strTrusted = Trusted ? $"{nameof(Trusted)}: true." : $"{nameof(Username)}: {Username}. {nameof(Password)}: {Password}.";
            return $"{nameof(Server)}: {Server}. " + 
                   $"{nameof(Db)}: {Db}. " + 
                   $"{strTrusted} " + 
                   $"{nameof(IsDebug)}: {IsDebug}. " + 
                   $"{nameof(SectionRowCount)}: {SectionRowCount}. " + 
                   $"{nameof(ItemRowCount)}: {ItemRowCount}. ";
        }
    }
}
