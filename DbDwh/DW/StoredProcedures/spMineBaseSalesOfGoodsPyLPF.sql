CREATE PROCEDURE [DW].[spMineBaseSalesOfGoodsPyLPF]
	@ContragentID varbinary(16)		= 0xBA6D90E6BA17BDD711E2970540C117D6
	,@NomenclatureID varbinary(16)	= 0xA216A4BF0139EB1B11E908E8E96919CB 
	,@DeliveryPlaceID varbinary(16)  = 0x9401001E6722B44911E3414A88DE146B
	,@Show int = 0
AS
BEGIN
    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @Method nvarchar(20) = N'INTERPOLATE';
	DECLARE @InformationSystemID int = 1;

	DROP TABLE IF EXISTS #rsp
	CREATE TABLE #rsp 
	(
		[ContragentID]			varbinary(16),
		[NomenclatureID]		varbinary(16),
		[DeliveryPlaceID]		varbinary(16),
		[DateID]				int NOT NULL,
		--[Method]				nvarchar(20),
		[Qty]					decimal(15,3),
		[Price]					decimal(15,2),
		[Cost]					decimal(15,2)
	)

	INSERT INTO #rsp ([ContragentID],[NomenclatureID],[DeliveryPlaceID],[DateID],[Qty],[Price],[Cost])
	EXEC sp_execute_external_script  @language =N'Python',
	@script=N'
import pyodbc
import pandas as pd
import numpy as np
from scipy import interpolate
from scipy.interpolate import interp1d
from scipy import signal

df = InputDataSet
df = df.sort_index(ascending=False)

#отсекаем аномальные выбросы
q = df["Qty"].quantile(0.998)
m = df["Qty"].median()
df["Qty"]  = np.where(df["Qty"] >= q, m, df["Qty"])

if (df["Qty"].size > 10):
	b, a = signal.butter(2, 0.07, analog=False)
	df["Qty"]   = signal.filtfilt(b, a, df["Qty"])

columns = [''NOTMA'', ''TMA'']
df.drop(columns, inplace=True, axis=1)
df[''Cost''] = df[''Qty''] * df[''Price'']
OutputDataSet = df
	',

	@input_data_1 =N'
	SELECT 
		[ContragentID],[NomenclatureID],[DeliveryPlaceID],[DateID],
		SUM([Qty]) [Qty],
		SUM(NOTMA)  NOTMA,
		SUM(TMA) TMA,
		MAX(Price) Price
	FROM (
		SELECT 
			[ContragentID],[NomenclatureID],COALESCE(@DeliveryPlaceID,0x00) [DeliveryPlaceID],[DateID],
			CAST(SUM([Qty]) as float) [Qty],
			CASE WHEN ([BasePrice] =  [Price] OR [BasePrice] = 0) THEN (CAST(SUM([Qty]) as float)) ELSE NULL END AS NOTMA,
			CASE WHEN ([BasePrice] <> [Price]) THEN (CAST(SUM([Qty]) as float)) ELSE NULL END AS TMA,
			CAST(IIF(COALESCE(BasePrice,0)=0,Price,BasePrice) as float) Price
		FROM  [DW].[FactSalesOfGoods] 
		WHERE 
			[ContragentID] = @ContragentID 
			and [NomenclatureID] = @NomenclatureID 
			and ([DeliveryPlaceID]= @DeliveryPlaceID OR @DeliveryPlaceID is null)
		GROUP BY [ContragentID],[NomenclatureID]
		--,[DeliveryPlaceID]
		,[DateID],[BasePrice],[Price]
	) AS x	GROUP BY [ContragentID],[NomenclatureID],[DeliveryPlaceID],[DateID]
	ORDER BY [DateID]
	',
	@output_data_1_name = N'OutputDataSet',
	@params = N'@ContragentID varbinary(16),@NomenclatureID varbinary(16),@DeliveryPlaceID varbinary(16)',
	@ContragentID		= @ContragentID,
	@NomenclatureID		= @NomenclatureID,
	@DeliveryPlaceID	= @DeliveryPlaceID
	;

	IF @Show = 0 BEGIN

	MERGE [DW].[MineBaseSalesOfGoods] as target
	USING 
	( 
		SELECT 
		 ContragentID 
		,NomenclatureID
		,DeliveryPlaceID
		,DateID
        ,@Method
		,Qty
        ,Price
        ,Cost
        ,@DLM
		,@InformationSystemID
		FROM #rsp

	) 
	AS source 
	(
		 [ContragentID] 
		,[NomenclatureID]
		,[DeliveryPlaceID]
		,[DateID]
        ,[Method]
		,[Qty]
        ,[Price]
        ,[Cost]
        ,[DLM]
		,[InformationSystemID]
	)  
	ON 
	(
		target.[Method]				= source.[Method] 
		AND target.NomenclatureID	= source.NomenclatureID
		AND target.ContragentID		= source.ContragentID
		AND target.DeliveryPlaceID	= source.DeliveryPlaceID
		AND target.[DateID]			= source.[DateID]
	) 

	WHEN MATCHED 
	THEN UPDATE SET 
		[Qty]					= source.[Qty]
        ,[Price]				= source.[Price]
        ,[Cost]					= source.[Cost]
        ,[DLM]					= source.[DLM]

	WHEN NOT MATCHED BY TARGET THEN INSERT 
	(
		 [ContragentID] 
		,[NomenclatureID]
		,[DeliveryPlaceID]
		,[DateID]
		,[Method]
		,[Qty]
		,[Price]
		,[Cost]
		,[InformationSystemID]
	) 
	VALUES 
	(
     	 source.[ContragentID] 
     	,source.[NomenclatureID]
     	,source.[DeliveryPlaceID]
		,source.[DateID]
		,source.[Method]
		,source.[Qty]
		,source.[Price]
		,source.[Cost]
		,source.[InformationSystemID]
	);
	END 
	ELSE 
	BEGIN
		SELECT * FROM #rsp ORDER BY [DateID]

	END

	DROP TABLE IF EXISTS #rsp


RETURN 0
END