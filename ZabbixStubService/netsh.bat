URL
* http://localhost:18086/massa
* http://localhost:18086/sql
* http://localhost:18086/status
* http://localhost:18086/zebra

Install
cd "C:\Windows\Microsoft.NET\Framework\v4.0.30319\"
installutil.exe "...\ZabbixStubService.exe"
netsh http show urlacl  -- просмотр списка прав доступа на url
netsh http add urlacl url="http://+:18086/massa" user=Все
netsh http add urlacl url="http://+:18086/status" user=Все
netsh http add urlacl url="http://+:18086/sql" user=Все
netsh http add urlacl url="http://+:18086/zebra" user=Все

Show
netsh http show urlacl

Uninstall
cd "C:\Windows\Microsoft.NET\Framework\v4.0.30319\"
installutil.exe /u "...\ZabbixStubService.exe"
netsh http delete urlacl url="http://+:18086/massa"
netsh http delete urlacl url="http://+:18086/status"
netsh http delete urlacl url="http://+:18086/sql"
netsh http delete urlacl url="http://+:18086/zebra"
