CREATE VIEW [DW].[vwCurrentPlannedCost]
AS 

SELECT 
	 [Marked]
	,[Posted]
	,[DocNum]
	,[DocDate]
	,[Price]
	,[NomenclatureId]
	,[NomenclatureName]
	,[ID]
FROM (
	SELECT 
		RANK() OVER (PARTITION BY [NomenclatureId] ORDER BY [DocDate] DESC, [ID]) R
		,[ID]
		,[Marked]
		,[Posted]
		,[DocNum]
		,[DocDate]
		,[NomenclatureInIS]
		,[UnitInIS]
		,[Price]
		,[NomenclatureId]
		,[NomenclatureName]
		,[UnitId]
		,[UnitName]
		,[StatusID]
		,[InformationSystemID]
		,[CodeInIS]
		,[CHECKSUMM]
		,[LineNo100]
	FROM [DW].[FactPlannedCost]
	WHERE Marked = 0 AND Posted = 1
) as x
WHERE R=1
GO

GRANT SELECT ON [DW].[vwCurrentPlannedCost] TO [OLAPReaderRole]
GO
