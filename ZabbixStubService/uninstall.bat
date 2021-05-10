cd "C:\Windows\Microsoft.NET\Framework\v4.0.30319\"
installutil.exe /u "...\ZabbixStubService.exe"

netsh http delete urlacl url="http://+:18086/massa"
netsh http delete urlacl url="http://+:18086/status"
netsh http delete urlacl url="http://+:18086/sql"
netsh http delete urlacl url="http://+:18086/zebra"
