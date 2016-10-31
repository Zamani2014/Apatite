<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="html" omit-xml-declaration="yes" indent="yes"/>

	<!---parameters for localization-->
	<xsl:param name="label_website"/>
	<xsl:param name="label_twitterPage"/>
	<xsl:param name="label_followers"/>
	<xsl:param name="label_friends"/>
	<xsl:param name="label_tweets"/>

	<xsl:template match="/">
		<h2>
			<xsl:value-of select="statuses/status[last()]/user/name"/>
		</h2>
		<xsl:for-each select="statuses/status">
			<p class="twitterText">
				<xsl:value-of select="text" disable-output-escaping="yes"/>
				<br/>
				<span class="twitterDate">
					<xsl:value-of select="created_at" disable-output-escaping="yes"/>
				</span>
			</p>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>