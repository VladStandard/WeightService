CREATE VIEW [db_scales].[vwProductionDynamics]
AS
SELECT        
    TOP (100) PERCENT 
    CAST(wf.WeithingDate AS date) AS WeithingDate, 
    wf.ScaleId, 
    wf.PluId, 
    plu.GoodsName, 
    ws.Name, 
    SUM(wf.NetWeight) AS NetWeightTotal, 
    SUM(wf.TareWeight) AS TareWeightTotal, 
    COUNT(wf.NetWeight) AS BoxCount

FROM            db_scales.WeithingFact AS wf INNER JOIN
                db_scales.Scales AS sc ON wf.ScaleId = sc.Id INNER JOIN
                db_scales.PLU AS plu ON wf.PluId = plu.Id INNER JOIN
                db_scales.WorkShop AS ws ON sc.WorkShopId = ws.Id
WHERE        (wf.WeithingDate > DATEADD(hour, - (24 * 7), GETDATE()))
GROUP BY wf.PluId, wf.ScaleId, CAST(wf.WeithingDate AS date), plu.GoodsName, ws.Name
GO
