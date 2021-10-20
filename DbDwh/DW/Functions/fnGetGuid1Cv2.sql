-- [DW].[fnGetGuid1Cv2]

DROP FUNCTION IF EXISTS [dbo].[Get_1C_GUID_From_IDRRef]
DROP FUNCTION IF EXISTS [DW].[fnGetGuid1Cv2]
GO

CREATE FUNCTION [DW].[fnGetGuid1Cv2] (@IDRRef binary(16))
-- Source: https://forum.infostart.ru/forum9/topic183950/
-- Description: получает ГУИД 1С (вида 9299c542-7d64-11e1-8dde-000c2989577c) из значения SQL binary поля IDRRef (вида 0x8DDE000C2989577C11E17D649299C542)
RETURNS nchar(36)
BEGIN
declare @IDRRef_String nchar(32);
SET @IDRRef_String = substring(sys.fn_sqlvarbasetostr(@IDRRef),3,32);
RETURN(
  substring(@IDRRef_String,25,8) + '-' +
  substring(@IDRRef_String,21,4) + '-' +
  substring(@IDRRef_String,17,4) + '-' +
  substring(@IDRRef_String,1,4) + '-' +
  substring(@IDRRef_String,5,12)
  );
END;
GO

GRANT EXECUTE ON [DW].[fnGetGuid1Cv2] TO [RenterRole]
GRANT EXECUTE ON [DW].[fnGetGuid1Cv2] TO [TerrasoftRole]
GO

-- CHECK
DECLARE @CodeInIs VARBINARY(16) = 0x80BB001E6722B44911E5EA8593805E79
DECLARE @etalon NVARCHAR(255) = N'93805e79-ea85-11e5-80bb-001e6722b449'
SELECT [DW].[fnGetGuid1Cv2] (@CodeInIs) [GUID_1C], @etalon [ETALON],
	CASE 
    	WHEN ([DW].[fnGetGuid1Cv2] (@CodeInIs) = @etalon) THEN 'TRUE'
    	ELSE 'FALSE'
    END [EQUALS]
