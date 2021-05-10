cd "C:\Windows\Microsoft.NET\Framework\v4.0.30319\"
installutil.exe "...\ZabbixStubService.exe"

netsh http show urlacl  -- просмотр списка прав доступа на url
netsh http add urlacl url="http://+:18086/massa" user=Все
netsh http add urlacl url="http://+:18086/status" user=Все
netsh http add urlacl url="http://+:18086/sql" user=Все
netsh http add urlacl url="http://+:18086/zebra" user=Все
