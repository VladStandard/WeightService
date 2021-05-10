<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="text" encoding="UTF-16" omit-xml-declaration="no"/>
  <xsl:template match="/">

    <xsl:text>
    ^XA</xsl:text>

    <xsl:text>
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
    </xsl:text>

    <xsl:variable name="length" select="100" />
    <xsl:variable name="width" select="80" />

    <xsl:text>
    ^LL</xsl:text>
    <xsl:value-of select="$length*11.8" />
    <xsl:text>
    ^PW</xsl:text>
    <xsl:value-of select="$width*11.8" />

    <!-- количество коробов -->
    <xsl:text>
    ^FO510,50
    ^CFE,64,54
    ^FB910,4,0,С,0
    </xsl:text>
    <xsl:text>^FH^FD</xsl:text>
    <xsl:value-of select="/ProductSeriesEntity/CountUnit"/>
    <xsl:text>^FS</xsl:text>

    <!-- штрихкод CODE128 -->
    <xsl:text>
    ^FO200,1000
    ^BCN,120,Y,N,N
    ^BY3
    </xsl:text>
    <xsl:text>^FD</xsl:text>
    <xsl:text>BOX</xsl:text>
    <xsl:value-of select="/ProductSeriesEntity/UUID"/>
    <xsl:text>^FS</xsl:text>


  </xsl:template>
</xsl:stylesheet>
