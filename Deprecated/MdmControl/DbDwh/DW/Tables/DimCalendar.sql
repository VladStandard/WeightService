CREATE TABLE	[DW].[DimCalendar]
	(	[DateKey] INT primary key, 
		[Date] DATETIME,
		[FullDateUK] CHAR(10), -- Date in dd-MM-yyyy format
		[FullDateUSA] CHAR(10),-- Date in MM-dd-yyyy format
		[FullDateRU]  CHAR(10),-- Date in dd.MM.yyyy format
		[DayOfMonth] VARCHAR(2), -- Field will hold day number of Month
		[DaySuffix] VARCHAR(4), -- Apply suffix as 1st, 2nd ,3rd etc
		[DayName] VARCHAR(19), -- Contains name of the day, Sunday, Monday 
		[DayOfWeekUSA] CHAR(1),-- First Day Sunday=1 and Saturday=7
		[DayOfWeekUK] CHAR(1),-- First Day Monday=1 and Sunday=7
		[DayOfWeekInMonth] VARCHAR(2), --1st Monday or 2nd Monday in Month
		[DayOfWeekInYear] VARCHAR(2),
		[DayOfQuarter] VARCHAR(3),
		[DayOfYear] VARCHAR(3),
		[WeekOfMonth] VARCHAR(1),-- Week Number of Month 
		[WeekOfQuarter] VARCHAR(2), --Week Number of the Quarter
		[WeekOfYear] VARCHAR(2),--Week Number of the Year
		[Month] VARCHAR(2), --Number of the Month 1 to 12
		[MonthName] VARCHAR(9),--January, February etc
		[MonthOfQuarter] VARCHAR(2),-- Month Number belongs to Quarter
		[Quarter] CHAR(1),
		[QuarterName] VARCHAR(9),--First,Second..
		[Year] CHAR(4),-- Year value of Date stored in Row
		[YearName] CHAR(7), --CY 2012,CY 2013
		[MonthYear] CHAR(10), --Jan-2013,Feb-2013
		[MMYYYY] CHAR(6),
		[FirstDayOfMonth] DATE,
		[LastDayOfMonth] DATE,
		[FirstDayOfQuarter] DATE,
		[LastDayOfQuarter] DATE,
		[FirstDayOfYear] DATE,
		[LastDayOfYear] DATE,

		[IsWeekday] BIT,-- 0=Week End ,1=Week Day

		[IsHolidayUSA] BIT,-- Flag 1=National Holiday, 0-No National Holiday
		[HolidayUSA] VARCHAR(50),--Name of Holiday in US
		[IsHolidayUK] BIT Null,-- Flag 1=National Holiday, 0-No National Holiday
		[HolidayUK] VARCHAR(50), -- Null --Name of Holiday in UK

		[IsHolidayRU] BIT Null,-- Flag 1=National Holiday, 0-No National Holiday
		[HolidayRU] VARCHAR(50), --Null --Name of Holiday in RU

		[FiscalDayOfYear] VARCHAR(3),
		[FiscalWeekOfYear] VARCHAR(3),
		[FiscalMonth] VARCHAR(2), 
		[FiscalQuarter] CHAR(1),
		[FiscalQuarterName] VARCHAR(9),
		[FiscalYear] CHAR(4),
		[FiscalYearName] CHAR(7),
		[FiscalMonthYear] CHAR(10),
		[FiscalMMYYYY] CHAR(6),
		[FiscalFirstDayOfMonth] DATE,
		[FiscalLastDayOfMonth] DATE,
		[FiscalFirstDayOfQuarter] DATE,
		[FiscalLastDayOfQuarter] DATE,
		[FiscalFirstDayOfYear] DATE,
		[FiscalLastDayOfYear] DATE


	) on [DIMFileGroup]
GO

