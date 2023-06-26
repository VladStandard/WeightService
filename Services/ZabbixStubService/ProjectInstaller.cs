// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;

namespace ZabbixStubService;

[RunInstaller(true)]
public partial class ProjectInstaller : System.Configuration.Install.Installer
{
    public ProjectInstaller()
    {
        InitializeComponent();
    }
}