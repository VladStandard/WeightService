declare @OrderID int = NULL
declare @ScaleID varchar(255) = 'F1A90176-894E-11EA-9E4C-4CCC6A93A440'
declare @PLU int = 108
declare @NetWeight int = 70
declare @TareWeight int = 125
-- SaveWeighingFact
declare @SSCC varchar(50)
declare @WeithingDate datetime
declare @xmldata xml

execute [db_scales].[SetWeithingFact] @OrderID,@ScaleID,@PLU,@NetWeight,@TareWeight,@SSCC output,@WeithingDate output,@xmldata output

select @SSCC [SSCC], @WEITHINGDATE [WEITHINGDATE], convert(varchar(max), @XMLDATA) [XMLDATA]
