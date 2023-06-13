USE [ScalesDB]
GO

DECLARE @ip nvarchar(max) = '10.0.20.67'
DECLARE @port int = 9100
DECLARE @zplCommand nvarchar(max)='^XA
^CI28
^CWK,E:COURB.TTF
^CWL,E:COURBI.TTF
^CWM,E:COURBD.TTF
^CWN,E:COUR.TTF
^CWZ,E:ARIAL.TTF
^CWW,E:ARIALBI.TTF
^CWE,E:ARIALBD.TTF
^CWR,E:ARIALI.TTF

^LH0,10
^FWR

^LL1180
^PW944

^FO510,50
^CFE,44,34
^FB910,4,0,J,0
^FH^FD_d0_98_d0_b7_d0_b4_d0_b5_d0_bb_d0_b8_d0_b5 _d0_ba_d0_be_d0_bb_d0_b1_d0_b0_d1_81_d0_bd_d0_be_d0_b5 _d0_b2_d0_b0_d1_80_d0_b5_d0_bd_d0_be_d0_b5. _d0_9f_d1_80_d0_be_d0_b4_d1_83_d0_ba_d1_82 _d0_bc_d1_8f_d1_81_d0_bd_d0_be_d0_b9 _d0_ba_d0_b0_d1_82_d0_b5_d0_b3_d0_be_d1_80_d0_b8_d0_b8 _d0_93. _d0_9a_d0_be_d0_bb_d0_b1_d0_b0_d1_81_d0_b0 _d0_b2_d0_b0_d1_80_d0_b5_d0_bd_d0_b0_d1_8f "_d0_9d_d0_b5_d0_b6_d0_bd_d0_b0_d1_8f _d1_81_d1_82_d0_b0_d0_bd_d0_b4_d0_b0_d1_80_d1_82" \&_d0_a2_d0_a3 10.13.14-007-91005552-2019 ^FS

^XZ';

-- TODO: исправить здесь | Set parameter values here.

EXECUTE [db_scales].[ZplPipe] 
   @ip
  ,@port
  ,@zplCommand
GO


