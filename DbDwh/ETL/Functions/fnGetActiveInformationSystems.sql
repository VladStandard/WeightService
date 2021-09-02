CREATE FUNCTION [ETL].[fnGetActiveInformationSystems()] ()
RETURNS @returntable TABLE
(
	[InformationSystemID] int
      ,[Name] nvarchar(255)
      ,[ConnectString1] nvarchar(2048)
      ,[ConnectString2] nvarchar(2048)
      ,[ConnectString3] nvarchar(2048)
)
AS
BEGIN


	INSERT @returntable
	SELECT [InformationSystemID]
      ,[Name]
      ,[ConnectString1]
      ,[ConnectString2]
      ,[ConnectString3]
	FROM [ETL].[InformationSystems]
	WHERE [StatusID] = 1

	RETURN
END
GO 


