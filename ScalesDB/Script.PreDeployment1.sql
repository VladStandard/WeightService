/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO
--sp_configure 'clr strict security', 0;
--GO
--RECONFIGURE;
--GO

ALTER DATABASE [$(DatabaseName)] SET TRUSTWORTHY ON; 
GO

BEGIN TRY
    CREATE ASSEMBLY [System.Drawing]
    FROM 'C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\System.Drawing.dll'
    WITH PERMISSION_SET = UNSAFE
END TRY
BEGIN CATCH  
    PRINT ERROR_MESSAGE();
END CATCH 

GO
