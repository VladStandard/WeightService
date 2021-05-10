-- ������� � ������. SYS.INDEXES
SELECT 
	 O.NAME [TABLE]
	,DDPS.ROW_COUNT [ROW_COUNT]
FROM SYS.INDEXES AS I
  INNER JOIN SYS.OBJECTS AS O ON I.OBJECT_ID = O.OBJECT_ID
  INNER JOIN SYS.DM_DB_PARTITION_STATS AS DDPS ON I.OBJECT_ID = DDPS.OBJECT_ID
  AND I.INDEX_ID = DDPS.INDEX_ID 
WHERE I.INDEX_ID < 2  AND O.IS_MS_SHIPPED = 0 ORDER BY O.NAME

-- ������� � ������. SYS.TABLES
SELECT T.NAME, S.ROW_COUNT FROM SYS.TABLES T
JOIN SYS.DM_DB_PARTITION_STATS S
ON T.OBJECT_ID = S.OBJECT_ID
AND T.TYPE_DESC = 'USER_TABLE'
AND T.NAME NOT LIKE '%DSS%'
AND S.INDEX_ID IN (0,1)
ORDER BY [NAME]
