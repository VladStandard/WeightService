CREATE PROCEDURE [MDM].[ContragentMasterRowMake]
	@Id int
AS
BEGIN
	
    DECLARE @MasterID int;

    SELECT @MasterID = [MasterId]
    FROM [DW].[DimContragents]
    WHERE [ID] = @Id;

    IF @MasterID IS NOT NULL BEGIN
        RAISERROR ('Запись уже нормализована.',11,1);
        RETURN 0;
    END;

    INSERT INTO [DW].[DimContragents](
            [Marked]
           ,[Code]
           ,[Name]
           ,[FullName]
           ,[IsBuyer]
           ,[IsSupplier]
           ,[GLN]
           ,[GUID_Mercury]
           ,[INN]
           ,[KPP]
           ,[Comment]
           ,[Parents]
           ,[OKPO]
           ,[ContragentType]
           ,[ContactInfo]
           ,[ManagerID]
           ,[ConsolidatedClientID]
           ,[NumberDebtDays]
           ,[AmountDue]
           ,[DaysDeferment]
           ,[CommercialNetworkID]
           ,[CommercialNetworkName]
           ,[InformationSystemID]
           ,[NormalizationStatus]
           ,[CodeInIS]
           ,[RelevanceStatus]
           ,[CreateDate]
           ,[DLM]
           ,[StatusID]
            )
    SELECT [Marked]
          ,[Code]
          ,[Name]
          ,[FullName]
          ,[IsBuyer]
          ,[IsSupplier]
          ,[GLN]
          ,[GUID_Mercury]
          ,[INN]
          ,[KPP]
          ,[Comment]
          ,[Parents]
          ,[OKPO]
          ,[ContragentType]
          ,[ContactInfo]
          ,[ManagerID]
          ,[ConsolidatedClientID]
          ,[NumberDebtDays]
          ,[AmountDue]
          ,[DaysDeferment]
          ,[CommercialNetworkID]
          ,[CommercialNetworkName]
          ,7
          ,2
          ,[CodeInIS]
          ,1
          ,GETDATE()
          ,GETDATE()
          ,1
    FROM [DW].[DimContragents]
    WHERE [ID] = @Id;

    SELECT @MasterID = @@IDENTITY;

    UPDATE [DW].[DimContragents]
	SET 
		[NormalizationStatus] = 1
		,[RelevanceStatus] = 1
		,[MasterId] = @MasterId
	WHERE 
		[Id]  = @Id;

    UPDATE [DW].[DimContragents]
	SET 
		[MasterId] = @MasterId
	WHERE 
		[Id]  = @MasterId;

	RETURN 1;

END
GO