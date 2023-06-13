:setvar id "ОП 003"
:setvar ip  '10.0.20.67'
:setvar port 9100
----------------------------------------------


USE [ScalesDB]
GO

DECLARE	@return_value INT

EXEC	@return_value = [db_scales].[ZplPipe]
		@ip = $(ip),
		@port = $(port),
		@zplCommand = N'
^XA
^CI28
^FWR
^LL1180
^PW944
^CWZ,E:COURB.TTF
^CWX,E:COUR.TTF
^CWY,E:ARIAL.TTF
^CWW,E:ARIALBL.TTF
^LH0,10

^FO20,20
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO100,20
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO180,20
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO260,20
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS


^FO20,300
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO100,300
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO180,300
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO260,300
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO20,580
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO100,580
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO180,580
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^FO260,580
^CFW,78,60
^FB400,1,0,L,0
^FH^FD$(id)^FS

^XZ
';

GO