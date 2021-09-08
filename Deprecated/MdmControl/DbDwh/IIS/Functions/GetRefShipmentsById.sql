CREATE FUNCTION [IIS].[GetRefShipmentsById]
(
	@ID bigint
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @jsonVariable NVARCHAR(MAX);	

	SET @jsonVariable = (
	SELECT * FROM 
	(
		SELECT @ID as "id", @ID as "idc"

		--SELECT @ID as "id", null as "idc"
		--UNION ALL
		--select  null as "id", doc.[ID] as "idc" from [DW].[DocJournal] doc
		--inner join [DW].[FactReturns] ss
		--ON ss.[CodeInIS] = doc.[CodeInIS] AND ss.InformationSystemID = doc.InformationSystemID
		--WHERE ss.[_SalesCodeID] = @ID
		--GROUP BY doc.[ID]

	) AS XXX FOR JSON PATH);

	RETURN @jsonVariable;

END
GO