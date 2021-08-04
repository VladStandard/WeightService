CREATE VIEW [db_scales].[vwProdDynamicsHour]
	AS 
	SELECT
	CAST(wf.WeithingDate AS date) AS WeithingDate, 
	DATEPART(hour,wf.WeithingDate) AS WeithingHour, 
	sc.[Description],
	wf.ScaleId, 
	wf.PluId, 
	plu.GoodsName, 
	ws.Name, 
	SUM(wf.NetWeight) AS NetWeightTotal, 
	SUM(wf.TareWeight) AS TareWeightTotal, 
	COUNT(wf.NetWeight) AS BoxCount
FROM db_scales.WeithingFact AS wf 
INNER JOIN db_scales.Scales AS sc 
	ON wf.ScaleId = sc.Id 
INNER JOIN db_scales.PLU AS plu 
	ON wf.PluId = plu.Plu AND  wf.[ScaleId] =  plu.[ScaleId]
INNER JOIN db_scales.WorkShop AS ws 
	ON sc.WorkShopId = ws.Id
WHERE       
(wf.WeithingDate > DATEADD(hour, - (24 * 7), GETDATE()))
GROUP BY 
	wf.ScaleId,  
	wf.PluId,
	CAST(wf.WeithingDate AS date), 
	DATEPART(hour,wf.WeithingDate),
	plu.GoodsName, 
	ws.Name,
	sc.[Description]
GO
GRANT SELECT
    ON OBJECT::[db_scales].[vwProdDynamicsHour] TO [db_scales_users]
    AS [scales_owner];

