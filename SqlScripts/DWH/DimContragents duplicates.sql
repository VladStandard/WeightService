-- DimContragents duplicates
SELECT
	DC1.*
FROM (SELECT
		DC.CodeInIS
	FROM DW.DimContragents DC
	GROUP BY DC.CodeInIS
	HAVING COUNT(DC.Code) > 1) DC
LEFT JOIN DW.DimContragents DC1 ON DC.CodeInIS = DC1.CodeInIS
ORDER BY DC.CodeInIS, DC1.InformationSystemID
