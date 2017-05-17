<?xml version="1.0" encoding="utf-8" ?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"> 
	<xsl:output method="html" omit-xml-declaration="yes" indent="yes" /> 
	<xsl:key match="filter" name="thisTag" use="@tag" /> 
	<xsl:key match="scenarioInfo" name="thisFeature" use="@featureName" /> 
	
	<xsl:template match="/">
	  <html>
		  <body bgcolor="#F0F0F0">
			  <h2>Available Filters</h2>
			  <p>Note: The @design filter indicates scenarios that are being worked on and are not yet ready to run.</p>
			  <xsl:apply-templates/>
		  </body>
	  </html>
	</xsl:template>
		<xsl:template match="filters"> 
			<table width="100%" border="1" > 
				<tr bgcolor="#85ADAD"><td width="8%"><b>Tag Name</b></td><td><b>List of Scenarios</b></td></tr>
				<xsl:for-each select="filter[generate-id(.)=generate-id(key('thisTag', @tag)[1])]"> 
					<xsl:sort select="@tag"/>
						<tr bgcolor="#D1E0E0"> 
							<th colspan="2" align="left"> 
								<xsl:value-of select="@tag"/>
							</th>
						</tr> 
						<xsl:for-each select="key('thisTag',@tag)"> 
							<xsl:sort select="@scenarioName"/>
							<tr> 
								<td></td> 
								<td bgcolor="#F1F6F6"> <xsl:value-of select="@scenarioName"/> </td> 
							</tr> 
						</xsl:for-each> 
				</xsl:for-each>
			</table>    

		<h2>Feature and Scenario Descriptions</h2>
			<table width="100%" border="1"> 
				<tr bgcolor="#85ADAD"><td width="8%"><b>Feature Name and Description</b></td><td><b>Scenario Name</b></td><td><b>Scenario Description</b></td><td><b>Associated Test Cases</b></td></tr>
				<xsl:for-each select="scenarioInfo[generate-id(.)=generate-id(key('thisFeature', @featureName)[1])]">
					<xsl:sort select="@featureName"/>
						<tr bgcolor="#D1E0E0"> 
							<th colspan="4" align="left"> <pre>
								<xsl:value-of select="@featureNameAndDesc"/>
								</pre>
							</th>
						</tr> 
						<xsl:for-each select="key('thisFeature',@featureName)"> 
							<xsl:sort select="@featureName"/>
							<tr> 
								<td></td> 
								<td width="20%" bgcolor="#F1F6F6"> <xsl:value-of select="@scenarioName"/> </td> 
								<td bgcolor="#F1F6F6"><pre><xsl:value-of select="@scenarioDescription"/> </pre></td>
								<td bgcolor="#F1F6F6"><xsl:value-of select="@testCases"/></td>
							</tr> 
						</xsl:for-each> 
				</xsl:for-each>
			</table> 
	</xsl:template>
</xsl:stylesheet> 
