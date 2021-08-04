
CREATE PROCEDURE [db_sscc].[GetSSCCxml]
	@GLN int = 460710023,
	@UnitType tinyint = 1,
	@Count int = 10,
	@xmldata xml OUTPUT
AS
BEGIN

	DECLARE @CurrentValue int;
	DECLARE @CurrentValueTable TABLE(CurrentValue int);  

	DECLARE @tbl TABLE (
		[SSCC]		varchar(36),
		[GLN]		varchar(9),
		[UnitID]	int,
		[UnitType]	tinyint,
		[SynonymSSCC] varchar(36),
		[Check]		int
	);

	MERGE db_sscc.SSCCStorage WITH (HOLDLOCK, XLOCK) AS target  
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
			+ RIGHT('000000000000000'+CONVERT(varchar(9), @GLN),9)
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
			,RIGHT('000000000000000'+CONVERT(varchar(9), @GLN),9)
			,@i
			,@UnitType
			,'(00)' + @sscc + CONVERT(varchar(1), @а0)
			,@а0
			);

		SET @i = @i + 1;

	END;

	SET @xmldata = (
      SELECT 
		[SSCC]	as '@SSCC',
		[GLN]	as '@GLN',
		[UnitID] as '@UnitID',
		[UnitType] as '@UnitType',
		[SynonymSSCC] as '@SynonymSSCC',
		[Check]  as '@Check'

      FROM  @tbl
      --FOR XML RAW('sscc'), TYPE
		FOR XML 
			PATH ('Item')
			,ROOT('SsccList')
			--,ELEMENTS XSINIL
			,BINARY BASE64 	  
		)

END
GO
GRANT EXECUTE
    ON OBJECT::[db_sscc].[GetSSCCxml] TO [db_scales_users]
    AS [scales_owner];

