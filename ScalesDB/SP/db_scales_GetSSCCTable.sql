CREATE PROCEDURE [db_scales].[GetSSCCTable]
	@GLN int ,
	@UnitType tinyint,
	@Count int
AS
BEGIN

	DECLARE @CurrentValue int;
	DECLARE @CurrentValueTable TABLE(CurrentValue int);  

	DECLARE @tbl TABLE (
		[SSCC]		varchar(36),
		[GLN]		int,
		[UnitID]	int,
		[UnitType]	tinyint,
		[SynonymSSCC] varchar(36),
		[Check]		int
	);

	MERGE db_scales.SSCCStorage WITH (HOLDLOCK, XLOCK) AS target  
		USING (SELECT @GLN) AS source (GLN)  
		ON (target.GLN = source.GLN)  
		WHEN MATCHED THEN
			UPDATE SET [COUNTER] = [COUNTER] + @Count  
		WHEN NOT MATCHED THEN  
			INSERT ([GLN], [COUNTER])  
			VALUES (source.GLN, @Count)  

		OUTPUT inserted.[COUNTER] INTO @CurrentValueTable;  

	SELECT TOP(1) @CurrentValue = CurrentValue FROM @CurrentValueTable;
	DECLARE @i int = @CurrentValue - @Count ;

	WHILE (@i < @CurrentValue) 
	BEGIN 
			
		DECLARE @sscc varchar(36) =  
			'' 
			+ CONVERT(varchar(1), @UnitType)
			+ LEFT(CONVERT(varchar,@GLN),9) 
			+ RIGHT('000000000000000'+CONVERT(varchar(7), @i),7);

		DECLARE @а1 int = 0, @а2 int = 0;
		DECLARE @ii int = 1;

		WHILE @ii < LEN(@sscc)+1 BEGIN
			IF (@ii%2 = 0) 
				SET @а1 = @а1 + CONVERT(int,SUBSTRING(@sscc,@ii,1));
			ELSE	
				SET @а2 = @а2 + CONVERT(int,SUBSTRING(@sscc,@ii,1));
			
			SET @ii = @ii + 1;
		END;	

		DECLARE @а0 int = RIGHT(10-CONVERT(int,(RIGHT(CONVERT(varchar(5),(@а1+@а2*3)),1))),1);
	
		INSERT INTO @tbl ([SSCC],[GLN],[UnitID],[UnitType],[SynonymSSCC],[Check]) 
		VALUES (
			'00' + @sscc + CONVERT(varchar(1), @а0)
			,@GLN
			,@i
			,@UnitType
			,'(00)' + @sscc + CONVERT(varchar(1), @а0)
			,@а0
			);

		SET @i = @i + 1;

	END;

	SELECT * FROM @tbl;

END

GO

GRANT EXECUTE ON [db_scales].[GetSSCCTable]
    TO  [db_scales_users]; 
GO