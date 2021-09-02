select t.object_id,
'INSERT INTO [VSDWH].'+QUOTENAME(sh.[name])+'.'+QUOTENAME(t.[name])+'('+x.[columns]+') 
SELECT '+x.[columns]+' FROM 
OPENQUERY ([SQLSRSP01.KOLBASA-VS.LOCAL\LEEDS], 
''SELECT '+x.[columns]+' FROM [VSDWH].'+QUOTENAME(sh.[name])+'.'+QUOTENAME(t.[name])+''') '

from sys.tables t
inner join sys.schemas sh 
on t.schema_id = sh.schema_id
cross apply (
	select STRING_AGG(QUOTENAME(name),',') as [columns]
	from sys.columns c 
	where c.object_id = t.object_id
	and is_identity = 0
	and max_length > 0
	group by c.object_id

) as x

where type = 'U'
and t.schema_id in (5,9)
order by sh.schema_id,t.object_id

