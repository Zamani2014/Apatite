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
		<div class="roundedTop"></div>
		<div class="framed">
			<h2>
				<xsl:value-of select="statuses/status[last()]/user/name"/>
			</h2>
			<img style="float: left; padding-right: 10px; padding-bottom: 10px;">
				<xsl:attribute name="src">
					<xsl:value-of select="statuses/status[last()]/user/profile_image_url"/>
				</xsl:attribute>
				<xsl:attribute name="alt">
					<xsl:value-of select="statuses/status[last()]/user/name"/>
				</xsl:attribute>
			</img>
			<xsl:value-of select="statuses/status[last()]/user/description"/>
			<br style="clear: both;"/>
			<br/>
			<a target="_blank">
				<xsl:attribute name="href">
					<xsl:value-of select="statuses/status[last()]/user/url"/>
				</xsl:attribute>
				<xsl:value-of select="$label_website"/>
			</a> | <xsl:value-of select="$label_twitterPage"/>:
			<a target="_blank">
				<xsl:attribute name="href">
					<xsl:text>http://twitter.com/</xsl:text>
					<xsl:value-of select="statuses/status[last()]/user/screen_name"/>
				</xsl:attribute>
				<xsl:value-of select="statuses/status[last()]/user/screen_name"/>
			</a> |
			<xsl:value-of select="$label_followers"/> <xsl:value-of select="statuses/status[last()]/user/followers_count"/> |
			<xsl:value-of select="$label_friends"/> <xsl:value-of select="statuses/status[last()]/user/friends_count"/>
			<p>
				<xsl:text disable-output-escaping="yes"><![CDATA[&nbsp;]]></xsl:text>
			</p>
			<h2>
				<xsl:value-of select="$label_tweets"/>
			</h2>
			<hr style="border: none; border-bottom: dashed 1px; height: 1px; overflow: hidden;"/>

			<xsl:for-each select="statuses/status">
				<xsl:value-of select="text" disable-output-escaping="yes"/>
				<br/>
				<span class="twitterDate">
					<xsl:value-of select="created_at" disable-output-escaping="yes"/>
				</span>
				<hr style="border: none; border-bottom: dashed 1px; height: 1px; overflow: hidden;"/>
			</xsl:for-each>
		</div>
		<div class="roundedBottom"></div>
	</xsl:template>
</xsl:stylesheet>