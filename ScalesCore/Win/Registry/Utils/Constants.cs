// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable MissingXmlDoc
// ReSharper disable IdentifierTypo

namespace ScalesCore.Win.Registry.Utils
{
    public static class Constants
    {
        public static class ControlPanel
        {
            public static class Accessibility
            {
                public static string Get() => @"Control Panel\Accessibility";

                public static class KeyboardResponse
                {
                    public static string Get() => @"Control Panel\Accessibility\Keyboard Response";
                }
                
                public static class StickyKeys
                {
                    public static string Get() => @"Control Panel\Accessibility\StickyKeys";
                }
                
                public static class ToggleKeys
                {
                    public static string Get() => @"Control Panel\Accessibility\ToggleKeys";
                }
            }

            public static class Desktop
            {
                public static string Get() => @"Control Panel\Desktop";
            }
        }

        public static class Software
        {
            public static string Get() => @"SOFTWARE";

            public static class Classes
            {
                public static string Get() => @"SOFTWARE\Classes";

                public static class Exefile
                {
                    public static string Get() => @"SOFTWARE\Classes\exefile";

                    public static class Shell
                    {
                        public static string Get() => @"SOFTWARE\Classes\exefile\shell";
                        public static string Runas { get; } = @"SOFTWARE\Classes\exefile\shell\runas";
                    }
                }
            }

            public static class Microsoft
            {
                public static string Get() => @"SOFTWARE\Microsoft";

                public static class Windows
                {
                    public static string Get() => @"SOFTWARE\Microsoft\Windows";

                    public static class CurrentVersion
                    {
                        public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion";

                        public static class AppPaths
                        {
                            public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
                        }

                        public static class Applets
                        {
                            public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Applets";

                            public static class Regedit
                            {
                                public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Applets\Regedit";
                            }
                        }

                        public static class Explorer
                        {
                            public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer";
                            public static class AppKey
                            {
                                public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\AppKey";
                            }
                        }

                        public static class Policies
                        {
                            public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
                            public static class Explorer
                            {
                                public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer";
                            }
                            public static class System
                            {
                                public static string Get() => @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                            }
                        }
                    }
                }

                public static class WindowsNT
                {
                    public static string Get() => @"SOFTWARE\Microsoft\Windows NT";

                    public static class CurrentVersion
                    {
                        public static string Get() => @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";

                        public static class Winlogon
                        {
                            public static string Get() => @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
                        }
                        
                        public static class ProfileList
                        {
                            public static string Get() => @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList";
                        }
                    }
                }
            }

            public static class Policies
            {
                public static string Get() => @"SOFTWARE\Policies";

                public static class Microsoft
                {
                    public static string Get() => @"SOFTWARE\Policies\Microsoft";

                    public static class Windows
                    {
                        public static string Get() => @"SOFTWARE\Policies\Microsoft\Windows";

                        public static class ControlPanel
                        {
                            public static string Get() => @"SOFTWARE\Policies\Microsoft\Windows\Control Panel";
                            
                            public static class Desktop
                            {
                                public static string Get() => @"SOFTWARE\Policies\Microsoft\Windows\Control Panel\Desktop";
                            }
                        }
                        
                        public static class System
                        {
                            public static string Get() => @"SOFTWARE\Policies\Microsoft\Windows\System";
                        }
                    }
                }
            }
        }

        public static class Default
        {
            public static string Get() => @".DEFAULT";

            public static class KeyboardLayout
            {
                public static string Get() => @".DEFAULT\Keyboard Layout";

                public static class Preload
                {
                    public static string Get() => @".DEFAULT\Keyboard Layout\Preload";
                }
            }
        }

        public static class System
        {
            public static string Get() => @"SYSTEM";

            public static class CurrentControlSet
            {
                public static string Get() => @"SYSTEM\CurrentControlSet";
                
                public static class KeyboardLayout
                {
                    public static string Get() => @"SYSTEM\CurrentControlSet\Control\Keyboard Layout";
                }
                
                public static class Control
                {
                    public static string Get() => @"SYSTEM\CurrentControlSet\Control";
                    
                    public static class Lsa
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Control\Lsa";
                    }
                    
                    public static class SessionManager
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Control\Session Manager";
                        public static class Environment
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
                        }
                    }
                }

                public static class Services
                {
                    public static string Get() => @"SYSTEM\CurrentControlSet\Services";
                    
                    public static class Eventlog
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Services\eventlog";

                        public static class Security
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\Eventlog\Security";
                        }
                    }
                    
                    public static class Lanmanserver
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Services\Lanmanserver";

                        public static class Parameters
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\Lanmanserver\Parameters";
                        }
                    }
                    
                    public static class W32Time
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Services\W32Time";

                        public static class Config
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\W32Time\Config";
                        }
                        
                        public static class Parameters
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\W32Time\Parameters";
                        }
                        
                        public static class TimeProviders
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TimeProviders";
                            
                            public static class NtpClient
                            {
                                public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TimeProviders\NtpClient";
                            }
                            
                            public static class NtpServer
                            {
                                public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TimeProviders\NtpServer";
                            }
                            
                            public static class VMICTimeProvider
                            {
                                public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TimeProviders\VMICTimeProvider";
                                
                                public static class Parameters
                                {
                                    public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TimeProviders\VMICTimeProvider\Parameters";
                                }
                            }
                        }
                        public static class TriggerInfo
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\W32Time\TriggerInfo";
                            
                            public static class Number0
                            {
                                public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TriggerInfo\0";
                            }
                            
                            public static class Number1
                            {
                                public static string Get() => @"SYSTEM\CurrentControlSet\Services\w32time\TriggerInfo\1";
                            }
                        }
                    }
                    
                    public static class Tcpip
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Services\Tcpip";
                        
                        public static class Parameters
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\Eventlog\Parameters";
                        }
                    }
                    
                    public static class Tcpip6
                    {
                        public static string Get() => @"SYSTEM\CurrentControlSet\Services\Tcpip6";
                        
                        public static class Parameters
                        {
                            public static string Get() => @"SYSTEM\CurrentControlSet\Services\Eventlog\Parameters";
                        }
                    }
                }
            }
        }
    }
}
