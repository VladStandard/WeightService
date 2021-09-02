CREATE PROCEDURE [DW].[spConsolidateContragentsRebuild]
	@CurretntDate datetime
AS
BEGIN

	DECLARE @EndDateId int   = [DW].[fnDateTimeToInt](GETDATE());
	DECLARE @StartDateID int = [DW].[fnDateTimeToInt](DATEADD(year,-2, GETDATE()) );

	DECLARE 
		@INN varchar(20),
		@ID int,	
		@Name nvarchar(150),
		@ConsContName nvarchar(150),
		@ConsolidatedClientIDReal int,
		@ConsolidatedClientNameReal nvarchar(150),
		@ROWNUM int,
		@ConsContID int


	DECLARE CUR CURSOR FOR 
	SELECT 
		[INN],
		[ID],	
		[Name],
		COALESCE(NetName,[Name]) ConsContName,
		ConsolidatedClientID as ConsolidatedClientIDReal,
		ConsolidatedClientName as ConsolidatedClientNameReal,
		ROW_NUMBER() OVER (PARTITION BY INN ORDER BY ID) as ROWNUM
	FROM (
	  SELECT 
		c.INN
		,c.ID
		,c.[Name]
		,n.[ID]   NetID
		,n.[Name] NetName
		,cc.ConsolidatedClientID
		,cc.ConsolidatedClientName
	  FROM [DW].[DimContragents] c
	  LEFT JOIN [DW].[DimCommercialNetwork] n
	  ON c.CommercialNetworkID = n.CodeInIS
	  LEFT JOIN [DW].[DimConsolidateContragents] as cc
	  ON c.ConsolidatedClientID = cc.ConsolidatedClientID 
	  WHERE c.[CodeInIS] IN 
	  (
		SELECT DISTINCT [ContragentID] 
		FROM [DW].[FactSalesOfGoods] 
		WHERE  [DateID] between @StartDateID and @EndDateId
	  )
	  AND LEN(c.INN)>0
	 -- AND c.ConsolidatedClientID is null
	) as X
	ORDER BY INN


	OPEN CUR;
	FETCH NEXT FROM CUR INTO @INN,@ID,@Name,@ConsContName,@ConsolidatedClientIDReal,@ConsolidatedClientNameReal,@ROWNUM;
	WHILE @@FETCH_STATUS=0 BEGIN
	
		IF ((@ROWNUM = 1) AND (@ConsolidatedClientNameReal is null)) BEGIN
			INSERT INTO [DW].[DimConsolidateContragents] ([ConsolidatedClientName],[INN])  
					VALUES (@ConsContName,@INN);
			SELECT @ConsContID = @@IDENTITY;
		END;

		IF ((@ROWNUM = 1) AND (@ConsolidatedClientIDReal is not null)) BEGIN
			SELECT @ConsContID = @ConsolidatedClientIDReal;
		END;

		IF (@ROWNUM > 1) BEGIN
			DELETE [DW].[DimConsolidateContragents] 
			WHERE [ConsolidatedClientID] = @ConsolidatedClientIDReal AND COALESCE(ConsolidatedClientID,2147483647) <> @ConsContID;
		END;

		UPDATE [DW].[DimContragents]
		SET ConsolidatedClientID = @ConsContID
		WHERE ID = @ID AND COALESCE(ConsolidatedClientID,2147483647) <> @ConsContID;


		FETCH NEXT FROM CUR INTO @INN,@ID,@Name,@ConsContName,@ConsolidatedClientIDReal,@ConsolidatedClientNameReal,@ROWNUM;
	END
	CLOSE CUR;
	DEALLOCATE CUR;

END
GO