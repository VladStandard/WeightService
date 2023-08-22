# Converters
- [Image to GF](ZebraDesigner): rotate 90
- [Image to ZPL](http://www.jcgonzalez.com/img-to-zpl-online)
- [Text Cyrillic to Latin](https://www.branah.com/cyrillic-to-latin)
- [Text to Hex](https://online-toolz.com/tools/text-hex-convertor.php)
- [Text to Hex](Notepad++ -> Plugins -> Converter -> ASCII -> HEX)
- [XML tools](https://onlinexmltools.com/prettify-xml)
- [ZPL to print label](http://ip_address/dir -> Create script)
- [ZPL to virtual label](http://labelary.com/viewer.html)
- [Создание штрих-кодов GS1 с помощью сценария ZPL](https://supportcommunity.zebra.com/s/article/Creating-GS1-Barcodes-with-Zebra-Printers-for-Data-Matrix-and-Code-128-using-ZPL?language=ru)

## ZPL convert from Native into Cyrillic
1. Copy and paste ZPL into Notepad++.
2. Select data (Example: "31_37_2E_30_36_2E_32_30_32_32_0D_0A").
3. Ctrl + H:
	Find what: "_"
	Replace with: ""
	Search mode: Normal
	✓ In selection.
4. Plugins → Converter → HEX → ASCII.

## ZPL convert from Latin into Native
1. Copy and paste ZPL into Notepad++.
2. Select data (Example: "Data izgot.:").
3. Plugins → Converter → ASCII → HEX.
4. Select data (Example: "4461746120697A676F742E3A").
5. Ctrl + H:
	Find what: (..)
	Replace with: _\1
	Search mode: Regular expression
	✓ In selection.
6. Plugins → Converter → HEX → ASCII.
