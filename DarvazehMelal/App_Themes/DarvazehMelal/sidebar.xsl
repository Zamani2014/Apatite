<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns:content="http://purl.org/rss/1.0/modules/content/"
    exclude-result-prefixes="content">
  <xsl:output method="html" omit-xml-declaration="yes" indent="yes"/>

  <xsl:template match="/">
    <div class="rssChannel">
      <table border="0" cellpadding="0" cellspacing="0">
        <xsl:apply-templates select="rss/channel"/>
      </table>
    </div>
  </xsl:template>

  <xsl:template match="rss/channel">
    <xsl:variable name="link" select="link"/>
    <xsl:variable name="description" select="description"/>
    <xsl:variable name="title" select="title" />
    <tr>
      <td>
        <h2>
          <xsl:value-of select="title"/>
        </h2>
      </td>
    </tr>
    <tr>
      <td>
        <ul>
          <xsl:apply-templates select="item"/>
        </ul>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="item">
    <li class="item">
      <xsl:variable name="item_link" select="link"/>
      <div>
		  <xsl:choose>
			  <xsl:when test="link/@target='_blank'">
				  <a href="{$item_link}" target="_blank">
					  <xsl:value-of select="title"/>
				  </a>
			  </xsl:when>
			  <xsl:otherwise>
				  <a href="{$item_link}">
					  <xsl:value-of select="title"/>
				  </a>
			  </xsl:otherwise>
		  </xsl:choose>
        <small>
          <xsl:value-of select="pubDate"/>
        </small>
        <div class="entry">
          <xsl:value-of select="content:encoded"/>
        </div>
      </div>
    </li>
    <li class="sidebaritemseparator"/>
  </xsl:template>

</xsl:stylesheet>
