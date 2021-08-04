CREATE PROCEDURE [db_scales].[XSLTTransform]
@xslInput NVARCHAR (MAX) NULL, @xmlInput NVARCHAR (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[XSLTTransform]

