CREATE PROCEDURE [db_scales].[SetNomenclature]

	@IDRRef				uniqueidentifier   ,
	@Code					nvarchar (30)	 ,
	@Marked					bit				 ,
	@Name					nvarchar (300)	 ,
	@NameFull				nvarchar (1024)	 ,
	@Description			nvarchar (1024)	 ,
	@Comment				nvarchar (1024)	 ,
	@Brand					varbinary(16)	 ,
	@GUID_Mercury			nvarchar (72)	 ,
	@NomenclatureType		varbinary(16)	 ,
	@VATRate				nvarchar (20)	 ,
	@ID						int OUTPUT

AS
BEGIN

	BEGIN TRAN;

	MERGE [db_scales].[Nomenclature] AS target  
    USING (
	SELECT 
		@IDRRef,
		@Code				,
		@Marked				,
		@Name				,
		@NameFull			,
		@Description		,
		@Comment			,
		@Brand				,
		@GUID_Mercury		,
		@NomenclatureType	,
		@VATRate			

	) AS source (

		[1CRRefID]			,
		[Code]				,
		[Marked]			,
		[Name]				,
		[NameFull]			,
		[Description]		,
		[Comment]			,
		[Brand]				,
		[GUID_Mercury]		,
		[NomenclatureType]	,
		[VATRate]			


	)  
    ON (target.IDRRef = source.[1CRRefID]) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			IDRRef			  = source.[1CRRefID]			  ,
			[Code]				  = source.[Code]				  ,
			[Marked]			  = source.[Marked]				  ,
			[Name]				  = source.[Name]				  ,
			[NameFull]			  = source.[NameFull]			  ,
			[Description]		  = source.[Description]		  ,
			[Comment]			  = source.[Comment]			  ,
			[Brand]				  = source.[Brand]				  ,
			[GUID_Mercury]		  = source.[GUID_Mercury]		  ,
			[NomenclatureType]	  = source.[NomenclatureType]	  ,
			[VATRate]			  = source.[VATRate]			  ,

			[ModifiedDate]	= GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			IDRRef			 ,
			[Code]				 ,
			[Marked]			 ,
			[Name]				 ,
			[NameFull]			 ,
			[Description]		 ,
			[Comment]			 ,
			[Brand]				 ,
			[GUID_Mercury]		 ,
			[NomenclatureType]	 ,
			[VATRate]			 
		)  
        VALUES 
		(
			source.[1CRRefID]			  ,
			source.[Code]				  ,
			source.[Marked]				  ,
			source.[Name]				  ,
			source.[NameFull]			  ,
			source.[Description]		  ,
			source.[Comment]			  ,
			source.[Brand]				  ,
			source.[GUID_Mercury]		  ,
			source.[NomenclatureType]	  ,
			source.[VATRate]			  

		) ;

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO

GRANT EXECUTE ON [db_scales].[SetNomenclature]
    TO  [db_scales_users]; 
GO
