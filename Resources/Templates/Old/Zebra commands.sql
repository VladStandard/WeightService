-- https://www.zebra.com/content/dam/zebra_new_ia/en-us/manuals/printers/common/programming/zpl-zbi2-pm-en.pdf
USE [ScalesDB]
GO

DECLARE @ip nvarchar(max) = '10.0.20.67'
DECLARE @port int = 9100
--DECLARE @zplCommand nvarchar(max) = '! U1 getvar "sensor.peeler"\n'
--DECLARE @zplCommand nvarchar(max) = '! U1 getvar "media.dynamic_length_calibration"\n'
DECLARE @zplCommand nvarchar(max) = '! U1 getvar "appl.link_os_version"\n'
SET @zplCommand = '! U1 getvar "appl.bootblock"\n'
SET @zplCommand = '! U1 getvar "appl.date"\n'
SET @zplCommand = '! U1 getvar "appl.name"\n'

SET @zplCommand = '! U1 getvar "device.printhead.odometer"\n'
SET @zplCommand = '! U1 getvar "device.printhead.test.detail"\n'

SET @zplCommand = '! U1 getvar "sensor.peel.gain"\n'
SET @zplCommand = '! U1 getvar "sensor.paper_supply"\n'
SET @zplCommand = '! U1 getvar "sensor.peeler"\n'
SET @zplCommand = '! U1 getvar "sensor.peel.brightness"\n'
SET @zplCommand = '! U1 getvar "sensor.peel.thold"\n'

SET @zplCommand = '! U1 getvar "sensor.head.temp"\n'
SET @zplCommand = '! U1 getvar "sensor.head.temp_celsius"\n'
SET @zplCommand = '! U1 getvar "sensor.head.temp_avg"\n'

SET @zplCommand = '! U1 getvar "sensor.gap.brightness"\n'
SET @zplCommand = '! U1 getvar "sensor.gap.gain"\n'
SET @zplCommand = '! U1 getvar "sensor.gap.offset"\n'
SET @zplCommand = '! U1 getvar "sensor.gap.thold"\n'

SET @zplCommand = '! U1 getvar "sensor.back_bar.offset"\n'
SET @zplCommand = '! U1 getvar "sensor.back_bar.cur"\n'
SET @zplCommand = '! U1 getvar "sensor.back_bar.ppr_out_thold"\n'
SET @zplCommand = '! U1 getvar "sensor.back_bar.brightness"\n'


SET @zplCommand = '! U1 getvar "print.tone"\n'

--Эта команда выключает или включает печать для совместимости с устаревшими версиями.
SET @zplCommand = '! U1 getvar "print.legacy_compatibility"\n'
--Возвращает количество открытий фиксатора печатающей головки
SET @zplCommand = '! U1 getvar "odometer.latch_open_count"\n'
--Возвращает количество этикеток, напечатанных с момента последнего сброса каждого сбрасываемого одометра.
SET @zplCommand = '! U1 getvar "odometer.user_label_count1"\n'
SET @zplCommand = '! U1 getvar "odometer.user_label_count2"\n'
--Эта команда устанавливает количество разрезов, выполненных резаком.
SET @zplCommand = '! U1 getvar "odometer.user_total_cuts"\n'
--Возвращает количество этикеток, напечатанных с момента последней команды установки одометра
SET @zplCommand = '! U1 getvar "odometer.user_label_count"\n'

--Эта команда возвращает общее количество этикеток, напечатанных за срок службы принтера.
--Возвращенное число не включает в себя подачу бумаги или калибровочные этикетки.
SET @zplCommand = '! U1 getvar "odometer.total_label_count"\n'
SET @zplCommand = '! U1 getvar "odometer.total_print_length"\n'
SET @zplCommand = '! U1 getvar "odometer.media_marker_count2"\n'
SET @zplCommand = '! U1 getvar "odometer.media_marker_count1"\n'
SET @zplCommand = '! U1 getvar "odometer.media_marker_count"\n'
--Отображает общее количество резов, выполненных резаком.
SET @zplCommand = '! U1 getvar "odometer.total_cuts"\n'

--This command returns the length of the last label printed or fed (in dots).
SET @zplCommand = '! U1 getvar "odometer.label_dot_length"\n'

--Эта настройка принтера относится к числу замененных головок одометра. Этот счетчик отслеживает, сколько дюймов и
--сантиметр прошел через принтер с момента последней замены головки
SET @zplCommand = '! U1 getvar "odometer.headnew"\n'
SET @zplCommand = '! U1 getvar "odometer.headclean"\n'

--Эта команда указывает скорость печати на носителе в дюймах в секунду (ips)
SET @zplCommand = '! U1 getvar "media.speed"\n'

SET @zplCommand = '! U1 getvar "media.printmode"\n'
SET @zplCommand = '! U1 getvar "media.part_number"\n'
-- Чтобы установить длину пропуска подачи в принтере:
SET @zplCommand = '! U1 getvar "media.feed_skip"\n'
--Эта команда включает или отключает динамическую калибровку длины. Это идентично первому параметру команда ^ XS - Динамическая калибровка длины
SET @zplCommand = '! U1 getvar "media.dynamic_length_calibration"\n'

--Позволяет пользователю настроить принтер так, чтобы он находил полосу с черными метками на передней или задней части носителя.
--ПРИМЕЧАНИЕ. Эта команда работает только с принтерами, имеющими передний датчик носителя.
SET @zplCommand = '! U1 getvar "media.bar_location"\n'

--Эта команда определяет, открыта или закрыта печатающая головка
SET @zplCommand = '! U1 getvar "head.latch"\n'

--Этот параметр позволяет записывать входные данные в режиме диагностики. Входной захват имеет три режима
--"print","run", and "off"". Режимы "print" И "run" могут использоваться для проверки данных, полученных принтером.
SET @zplCommand = '! U1 getvar "input.capture"\n'

SET @zplCommand = '! U1 getvar "head.darkness_switch"\n'

--Эта команда извлекает позицию отрыва
SET @zplCommand = '! U1 getvar "ezpl.tear_off"\n'
--Эта команда устанавливает положение метки дубля
SET @zplCommand = '! U1 getvar "ezpl.take_label"\n'
--Эта команда включает / выключает режим повторной печати.
SET @zplCommand = '! U1 getvar "ezpl.reprint_mode"\n'
--Эта команда устанавливает ширину печати этикетки.
SET @zplCommand = '! U1 getvar "ezpl.print_width"\n'

--"thermal trans" "direct thermal"
SET @zplCommand = '! U1 getvar "ezpl.print_method"\n'

--Эта команда устанавливает, что происходит с носителем при включении принтера. Эта команда похожа на команду ^MF
--Values
--• "calibrate"
--• "feed"
--• "length"
--• "no motion"
--• "short cal"
SET @zplCommand = '! U1 getvar "ezpl.power_up_action"\n'

--Эта команда определяет используемый тип носителя.Эта команда похожа на ^ MN
--Values
--• "continuous"
--• "gap/notch"
--• "mark"
SET @zplCommand = '! U1 getvar "ezpl.media_type"\n'

--Эта команда запускает последовательность ручной калибровки.
--! U1 setvar "ezpl.manual_calibration" ""
--! U1 do "ezpl.manual_calibration" ""

--Эта команда устанавливает пороговое значение выхода бумаги.
SET @zplCommand = '! U1 getvar "ezpl.label_sensor"\n'

--Эта команда устанавливает максимальную длину этикетки в дюймах.-Эта команда эквивалентна команде ^ ML
SET @zplCommand = '! U1 getvar "ezpl.label_length_max"\n'

--Эта команда устанавливает, что происходит с носителем после закрытия печатающей головки и извлечения принтера из Пауза.
--Values
--• "feed" = feed to the first web after sensor
--• "calibrate" = is used to force a label length measurement and adjust the media and ribbon sensor values.
--• "length" = is used to set the label length. Depending on the size of the label, the printer feeds one or more blank labels.
--• "no motion" = no media feed
--• "short cal" = short calibration
SET @zplCommand = '! U1 getvar "ezpl.head_close_action"\n'

--Возвращает разрешение печатающей головки в точках на дюйм в виде целого числа.
SET @zplCommand = '! U1 getvar "head.resolution.in_dpi"\n'

--Эта команда получает идентификатор принтера.
SET @zplCommand = '! U1 getvar "device.unique_id"\n'

--Эта команда устанавливает назначение вывода профиля датчика принтера.
SET @zplCommand = '! U1 getvar "device.sensor_profile"\n'
--Определяет, какой датчик носителя будет использоваться.Эта команда похожа на ^ JS
--Values
--• "reflective"
--• "transmissive"
--• "reflective" 
SET @zplCommand = '! U1 getvar "device.sensor_select"\n'

--Когда эта команда отправляется на принтер, принтер отправляет обратно три строки данных. Во избежание путаницы,
--хост печатает каждую строку в отдельной строке. Эта команда похожа на команду ~ HS
SET @zplCommand = '! U1 getvar "device.host_status"\n'

SET @zplCommand = '! U1 getvar "device.friendly_name"\n'

--Эта команда указывает принтеру прервать загрузку микропрограммы, если принтер не может получить какую-либо загрузку.
--данные в заданное количество секунд. Если установленное количество секунд превышено, загрузка будет прервана,
--и принтер автоматически перезагрузится. Эта команда предотвращает блокировку принтера в
--состояние загрузки, если связь с хостом прервана
--Values "0" through "65535"
--Default "0" ("0" disables this feature)
SET @zplCommand = '! U1 getvar "device.download_connection_timeout"\n'




SET @zplCommand = '! U1 setvar "odometer.user_label_count2" "5"\n'
SET @zplCommand = '! U1 setvar "odometer.user_label_count1" "1" \n'


SET @zplCommand = '! U1 setvar "odometer.user_label_count" "1"\n'
SET @zplCommand = '! U1 getvar "odometer.user_label_count"\n'





-- TODO: Set parameter values here.

EXECUTE [db_scales].[ZplPipe] 
   @ip
  ,@port
  ,@zplCommand
GO


