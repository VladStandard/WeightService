<?xml version="1.0" encoding="utf-16"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema">
<xsl:output method="text" encoding="UTF-16" omit-xml-declaration="no"/>
<xsl:template match="/">

<!-- Документ с описанием -->
<!-- ЧудоПечка 30x50 шт АРМ -->
<!-- http://10.0.204.49:8080/bin/view/Весовая%20платформа/Штрихкода/ШК%20шаблоны/# -->
<!-- Последние правки внесены 2022-08-16 -->

<!-- Переменные -->
<xsl:variable name="netWeight" select="/WeighingFactEntity/NetWeight"/>
<xsl:variable name="netWeightGr3" select="/WeighingFactEntity/NetWeightGrPretty3"/>
<xsl:variable name="netWeightKg2" select="/WeighingFactEntity/NetWeightKgPretty2"/>
<xsl:variable name="netWeightKg3" select="/WeighingFactEntity/NetWeightKgPretty3"/>
<xsl:variable name="netWeightKg1Dot3Eng" select="/WeighingFactEntity/NetWeightKgPretty1Dot3Eng"/>
<xsl:variable name="netWeightKg2Dot3Eng" select="/WeighingFactEntity/NetWeightKgPretty2Dot3Eng"/>
<xsl:variable name="netWeightKg1Dot3Rus" select="/WeighingFactEntity/NetWeightKgPretty1Dot3Rus"/>
<xsl:variable name="netWeightKg2Dot3Rus" select="/WeighingFactEntity/NetWeightKgPretty2Dot3Rus"/>

<!-- Начало этикетки -->
<xsl:text>^XA  ^CI28</xsl:text>
<!-- Шрифты принтера -->
<xsl:text>
^CWK,E:COURB.TTF
^CWL,E:COURBI.TTF
^CWM,E:COURBD.TTF
^CWN,E:COUR.TTF
^CWZ,E:ARIAL.TTF
^CWW,E:ARIALBI.TTF
^CWE,E:ARIALBD.TTF
^CWR,E:ARIALI.TTF

</xsl:text>
<!-- Стартовая позиция -->
<xsl:text>^LH0,30  </xsl:text>
<!-- Повернуть на 90° -->
<xsl:text>^FWR

</xsl:text>

<!-- Дата изготовления: метка -->
<xsl:text>
^FO300,40
^CFZ,50,26
^FB300,1,0,C,0
^FH^FDДата изготовления:
^FS
</xsl:text>

<!-- Дата изготовления: значение -->
<xsl:text>
^FO240,50
^CFK,56,40
^FB300,1,0,C,0
^FH^FD</xsl:text>
<xsl:variable name="dt" select="/WeighingFactEntity/ProductDate"/>
<xsl:value-of select="concat(substring($dt,9,2),'.',substring($dt,6,2),'.',substring($dt,1,4))"/>
<xsl:text>
^FS
</xsl:text>

<!-- Масса нетто: метка -->
<xsl:text>
^FO300,330
^CFZ,50,26
^FB300,1,0,C,0
^FH^FDМасса нетто:
^FS
</xsl:text>

<!-- Масса нетто: значение -->
<xsl:text>
^FO240,335
^CFK,56,40
^FB300,1,0,C,0
^FH^FD</xsl:text>
<xsl:variable name="nw" select="/WeighingFactEntity/PLU/NominalWeight"/>
<xsl:value-of select="substring(concat('00000',$nw),string-length($nw)+1,5)"/>
<xsl:text>
^FS
</xsl:text>

<!-- Bar Code  -->
<xsl:text>
^BY4
^FO70,135
^BER,120,Y,N
^FH^FD</xsl:text>
<!-- EAN13 -->
<xsl:variable name="ean" select="/WeighingFactEntity/PLU/EAN13"/>
<xsl:value-of select="substring(concat('000000000000',$ean),string-length($ean)+1,12)"/>
<xsl:text>^FS
</xsl:text>

<xsl:text>
^PQ1

^XZ</xsl:text>

</xsl:template>
</xsl:stylesheet>