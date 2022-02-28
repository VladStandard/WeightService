-- Update-2022-02-28
SELECT DISTINCT(PLU) FROM db_scales.PLU
--C = CHECK constraint
--D = DEFAULT (constraint or stand-alone)
--F = FOREIGN KEY constraint
--PK = PRIMARY KEY constraint
--UQ = UNIQUE CONSTRAINT
-- SQL Server 2012 through SQL Server 2014.
--SQ = Service queue
--TA = Assembly (CLR) DML trigger
--TF = SQL table-valued-function
--TR = SQL DML trigger
--TT = Table type
--U = Table (user-defined)
--UQ = UNIQUE constraint
--V = View
--X = Extended stored procedure
IF (OBJECT_ID('db_scales.PLU', 'U') IS NOT NULL) BEGIN
	SELECT * FROM sys.objects WHERE name LIKE '%PLU%' -- = OBJECT_ID('db_scales.PLU')
	
	ALTER TABLE [db_scales].[PLU] DROP CONSTRAINT [CN_PLU_PLU]
END
ALTER TABLE [db_scales].[PLU] ADD CONSTRAINT C_PLU_PLU CHECK([PLU] >= 0)
ALTER TABLE [db_scales].[PLU] ADD CONSTRAINT UQ_PLU_PLU UNIQUE([ScaleId], [Plu])
