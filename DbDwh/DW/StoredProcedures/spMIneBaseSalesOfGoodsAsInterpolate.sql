CREATE PROCEDURE [DW].[spMIneBaseSalesOfGoodsAsInterpolate]
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
		[Qty]					decimal(15,3),
		[Price]					decimal(15,2),
		[Cost]					decimal(15,2),
		[Date]					datetime
	)

	INSERT INTO #rsp ([ContragentID],[NomenclatureID],[DeliveryPlaceID],[DateID],[Qty],[Price],[Cost],[Date])
	EXEC sp_execute_external_script  @language =N'Python',
	@script=N'
import pyodbc
import pandas as pd
import numpy as np
from scipy import interpolate
from scipy.interpolate import interp1d
from scipy import signal
from scipy.ndimage import gaussian_filter

df = InputDataSet
df = df.set_index("DateID")
df = df.sort_index(ascending=True)

ContragentID = df["ContragentID"].iloc[0]
NomenclatureID = df["NomenclatureID"].iloc[0]
DeliveryPlaceID = df["DeliveryPlaceID"].iloc[0]

#аномалиям кирдык
q = df["NOTMA"].quantile(0.9997)
df["NOTMA"]  = np.where(df["NOTMA"] >= q, df["NOTMA"].median(), df["NOTMA"])

# интерполяция
x = df["NOTMA"].tolist()
x = np.array(x)
not_nan = np.logical_not(np.isnan(x))
is_nan  = np.isnan(x)
index   = np.arange(len(x))
#---------------------------------
ff = interp1d(index[not_nan], x[not_nan],axis=0, fill_value="extrapolate",kind="linear")
y = ff(index[is_nan])
n = 0
for i in range(len(x)):
	if ( np.array(is_nan[i]) ):
		x[i] = y[n]
		n = n + 1
df["NOTMA"] = x.tolist()
df["NOTMA"] = gaussian_filter(np.ravel(df["NOTMA"]), sigma=.1)

#сделал из недель месяцы
df = df.groupby(["MntID"])["NOTMA","Qty","TMA"].sum()
df["NOTMA"] = gaussian_filter(np.ravel(df["NOTMA"]), sigma=1.2)
df = df.drop(["TMA","Qty"], axis=1)

#переподготовка массива данных
df.rename(columns={"NOTMA":"Qty"}, inplace=True)
df["DateID"] = df.index
df["ContragentID"] = ContragentID
df["NomenclatureID"] = NomenclatureID
df["DeliveryPlaceID"] = DeliveryPlaceID
df["Price"] = 0
df["Cost"] = 0
df["DateTime"] = df["DateID"].apply(lambda x: pd.to_datetime(str(x), format="%Y%m")).astype(str)
df["DateID"] = (df["DateID"]).astype(int) * 100 + 1
OutputDataSet = df[["ContragentID","NomenclatureID","DeliveryPlaceID","DateID","Qty","Price","Cost","DateTime"]]
',
@input_data_1 =N'
SELECT 
	 COALESCE([ContragentID], MAX(COALESCE([ContragentID],null)) OVER (ORDER BY [ContragentID] DESC ROWS BETWEEN UNBOUNDED PRECEDING AND 0 PRECEDING)) [ContragentID]
	,COALESCE([NomenclatureID], MAX(COALESCE([NomenclatureID],null)) OVER (ORDER BY [NomenclatureID] DESC ROWS BETWEEN UNBOUNDED PRECEDING AND 0 PRECEDING)) [NomenclatureID]
	,COALESCE([DeliveryPlaceID], MAX(COALESCE([DeliveryPlaceID],null)) OVER (ORDER BY [DeliveryPlaceID] DESC ROWS BETWEEN UNBOUNDED PRECEDING AND 0 PRECEDING)) [DeliveryPlaceID]
	,DateID
	,MntID
	,SUM([Qty])[Qty]
	,IIF(MAX(IsTmaMarker)=1,null,SUM([NOTMA])) [NOTMA]
	,SUM([TMA])TMA	 
FROM
(
SELECT  
 @ContragentID [ContragentID]
,@NomenclatureID [NomenclatureID]
,@DeliveryPlaceID [DeliveryPlaceID]
,[DateKey]
,[Year]+ right(''0000''+[WeekOfYear],2) DateID
,[Year]+ right(''0000''+[Month],2) MntID
,IsTmaMarker
,COALESCE([Qty],0) [Qty]
,COALESCE(NOTMA,0) NOTMA
,COALESCE(TMA,0)   TMA
FROM [DW].[DimCalendar] c
LEFT JOIN
(
SELECT
	[ContragentID],[NomenclatureID], [DeliveryPlaceID],[DateID],
	CAST(SUM([Qty]) as float) [Qty],
	CASE WHEN ([BasePrice] <> [Price] ) THEN 1 ELSE 0 END AS IsTmaMarker,
	CASE WHEN ([BasePrice] =  [Price] ) THEN (CAST(SUM([Qty]) as float)) ELSE NULL END AS NOTMA,
	CASE WHEN ([BasePrice] <> [Price]) THEN (CAST(SUM([Qty]) as float)) ELSE NULL END AS TMA
FROM  [DW].[FactSalesOfGoods] 
WHERE 
	[ContragentID] =@ContragentID 
	and [NomenclatureID] = @NomenclatureID
	and [DeliveryPlaceID]= @DeliveryPlaceID
GROUP BY [ContragentID],[NomenclatureID],[DeliveryPlaceID],[DateID],[BasePrice],[Price]
) AS W
ON DateId  = c.DateKey
WHERE 
	[Date] BETWEEN 
	DATEADD(month,-23, DATEADD(mm, DATEDIFF(mm, 0, getdate()), 0)) 
	AND 
	DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, getdate()) + 4, 0)) 
) as q
GROUP BY DateID,MntID,[ContragentID],[NomenclatureID],[DeliveryPlaceID]
ORDER BY DateID
	',
	@output_data_1_name = N'OutputDataSet',
	@params = N'@ContragentID varbinary(16),@NomenclatureID varbinary(16),@DeliveryPlaceID varbinary(16)',
	@ContragentID		= @ContragentID,
	@NomenclatureID		= @NomenclatureID,
	@DeliveryPlaceID	= @DeliveryPlaceID
	;

	IF @Show = 0 BEGIN

	DECLARE 
		@_ContragentID int
		,@_NomenclatureID int
		,@_DeliveryPlaceID int
		,@RegionID varbinary(16)
		,@_RegionID int


	select @_ContragentID = ID from [DW].[DimContragents] where [CodeInIS] = @ContragentID
	select @_NomenclatureID= ID from [DW].[DimNomenclatures] where [CodeInIS] = @NomenclatureID
	select @_DeliveryPlaceID = ID from [DW].[DimDeliveryPlaces] where [CodeInIS] = @DeliveryPlaceID
	select @RegionID =[RegionStoreID] from [DW].[DimDeliveryPlaces] where [CodeInIS] = @DeliveryPlaceID
	select top 1 @_RegionID = r.[ID] from [DW].[DimDeliveryPlaces] p 
			inner join [DW].[DimRegions] r on p.[RegionStoreID]=r.[CodeInIS]  
			where p.[CodeInIS] = @DeliveryPlaceID



	MERGE [DW].[MineBaseSalesOfGoods] as target
	USING 
	( 
		SELECT 
		ContragentID 
		,NomenclatureID
		,DeliveryPlaceID
		,DateID
		,Qty
        ,Price
        ,Cost
        ,@DLM
		,@InformationSystemID
		,[Date]
		FROM #rsp

	) 
	AS source 
	(
		 [ContragentID] 
		,[NomenclatureID]
		,[DeliveryPlaceID]
		,[DateID]
		,[Qty]
        ,[Price]
        ,[Cost]
        ,[DLM]
		,[InformationSystemID]
		,[Date]
	)  
	ON 
	(
		target.NomenclatureID		= source.NomenclatureID
		AND target.ContragentID		= source.ContragentID
		AND target.DeliveryPlaceID	= source.DeliveryPlaceID
		AND target.[DateID]			= source.[DateID]
	) 

	WHEN MATCHED 
	THEN UPDATE SET 
		[Qty]					= source.[Qty]
        ,[Price]				= source.[Price]
        ,[Cost]					= source.[Cost]
		,[RegionID]				= @RegionID
		,[_RegionID]			= @_RegionID
        ,[DLM]					= source.[DLM]

	WHEN NOT MATCHED BY TARGET THEN INSERT 
	(
		 [ContragentID] 
		,[NomenclatureID]
		,[DeliveryPlaceID]
		,[DateID]
		,[Qty]
		,[Price]
		,[Cost]
		,[InformationSystemID]
		,[_ContragentID] 
		,[_NomenclatureID]
		,[_DeliveryPlaceID]
		,[RegionID]
		,[_RegionID]
		,[_Date]


	) 
	VALUES 
	(
     	 source.[ContragentID] 
     	,source.[NomenclatureID]
     	,source.[DeliveryPlaceID]
		,source.[DateID]
		,source.[Qty]
		,source.[Price]
		,source.[Cost]
		,source.[InformationSystemID]
		,@_ContragentID
		,@_NomenclatureID
		,@_DeliveryPlaceID
		,@RegionID
		,@_RegionID
		,[Date]

	);



	END 
	ELSE 
	BEGIN
		SELECT * FROM #rsp ORDER BY [DateID]

	END

	DROP TABLE IF EXISTS #rsp


RETURN 0
END