

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="pageHead"><title>
	Koders Code Search: BhoMain.cs - C#
</title><meta name="robots" content="noarchive,index,follow" /><meta name="GOOGLEBOT" content="noarchive,index,follow" /><link href="http://www.koders.com/search/KodersDescriptionOS1_1.xml"  title="Koders search" rel="search" type="application/opensearchdescription+xml" ><link href="http://www.koders.com/search/KodersDescriptionOS1_1Proj.xml"  title="Koders Projects search" rel="search" type="application/opensearchdescription+xml" ><link href="/skins/koders/styles.css" type="text/css" rel="stylesheet" /><!--[if IE]><link href="/skins/koders/iestyles.css" type="text/css" rel="stylesheet" /><![endif]-->
<link href="/Special/Stylesheets/koders.com.css" type="text/css" rel="stylesheet" /> <meta  name="ROBOTS"  content="NOARCHIVE"  /> 
		
	<link href="/skins/koders/home-page.css" type="text/css" rel="stylesheet" />

<script type="text/javascript" language="javascript">
//Copyright (c) 2005-2007 Koders.com,
//All Rights Reserved.
var anchors;
var symbolSegment = 0;
var max = 0;
	
function assignInitial() {
	var codeDiv = document.getElementById("CodeDiv");
	if(codeDiv != null) {
		anchors = codeDiv.getElementsByTagName("a");
		assignR(300);
	}
}

function assignR(max) {
	if(symbolSegment + max >= anchors.length) {
		max = anchors.length - symbolSegment; 
	}
	for(var i = 0; i < max; i++) {
		var temp = "";
		var index = symbolSegment +  i;
		var anchor = anchors[index];

		for(var k = 0; k < anchor.childNodes.length; k++) {
			if(anchor.childNodes[k].data == undefined) {
				temp = temp + anchor.childNodes[k].childNodes[0].data;
			}
			else {
				temp = temp + anchor.childNodes[k].data;
			}
		}
		
		anchors[index].setAttribute("href", "javascript:searchRef('" + temp + "')");
		anchors[index].setAttribute("title", "Search for references of '" + temp + "'");
	}
	
	if(symbolSegment + max < anchors.length) { 
		symbolSegment += max;
		this.setTimeout("assignR(50)", 200);		
	}
	
}

function searchRef(symbol) {
	location = homeUrl + "?s=" + symbol + "&scope=" + projectID + "&la=" + sourceLanguage;
}

function Hide(element) {
	if(element) {
		element.style.visibility = "hidden";
		element.style.display = "none";
	} 
}

function HideBanner() {
	var eleArray = document.getElementsByTagName('div');
	for(var i = 0; i < eleArray.length; i++) {
		if(!eleArray[i] || !eleArray[i].id) {
			continue;
		}

		var id = eleArray[i].id;
		if(id && id.indexOf("RightBanner") != -1){
			Hide(eleArray[i]);
								
			// hide all the iframes nested in the Banner
			var arrIframe = eleArray[i].getElementsByTagName('iframe');
			if(!arrIframe)
				return;
			
			for(var j = 0; j < arrIframe.length; j++) {
				Hide(arrIframe[j]);
			}
		}
	}
}

var dojo;
if(dojo) dojo.addOnLoad(assignInitial);
else window.onload = assignInitial;
</script>
</head>


<body>

	
 
    <form name="FRM" method="post" action="kv.aspx?fid=02F92772225FF8711957E789B2BAFEC8A9236DF0&amp;s=BHO" id="FRM">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUJMTYzMDIzMzAwD2QWBgIBD2QWAgIGDw8WBB4GRmlsZUlEAtTFwAUeCVByb2plY3RJRALH2QZkZAIFD2QWBAIDD2QWAmYPZBYIAgEPDxYCHgtOYXZpZ2F0ZVVybAUBL2QWAmYPDxYCHghJbWFnZVVybAVBaHR0cDovL21lZGlhLmJsYWNrZHVja3NvZnR3YXJlLmNvbS9rb2RlcnMvYmRzL2xvZ29fbWVkX3RhZ19iZC5naWZkZAIHDxAPFgIeC18hRGF0YUJvdW5kZ2QQFSINQWxsIExhbmd1YWdlcwxBY3Rpb25TY3JpcHQDQWRhA0FTUAdBU1AuTkVUCUFzc2VtYmxlcgFDAkMjA0MrKwVDb2JvbApDb2xkRnVzaW9uBkRlbHBoaQZFaWZmZWwGRXJsYW5nB0ZvcnRyYW4ESmF2YQpKYXZhU2NyaXB0A0pTUARMaXNwA0x1YQtNYXRoZW1hdGljYQZNYXRsYWIKT2JqZWN0aXZlQwRQZXJsA1BIUAZQcm9sb2cGUHl0aG9uBFJ1YnkGU2NoZW1lCVNtYWxsdGFsawNTUUwDVGNsAlZCBlZCLk5FVBUiASoMQWN0aW9uU2NyaXB0A0FkYQNBU1AHQVNQLk5FVAlBc3NlbWJsZXIBQwJDIwNDcHAFQ29ib2wKQ29sZEZ1c2lvbgZEZWxwaGkGRWlmZmVsBkVybGFuZwdGb3J0cmFuBEphdmEKSmF2YVNjcmlwdANKU1AETGlzcANMdWELTWF0aGVtYXRpY2EGTWF0bGFiCk9iamVjdGl2ZUMEUGVybANQSFAGUHJvbG9nBlB5dGhvbgRSdWJ5BlNjaGVtZQlTbWFsbHRhbGsDU1FMA1RjbAJWQgZWQi5ORVQUKwMiZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2RkAgkPEA8WAh8EZ2QQFR8MQWxsIExpY2Vuc2VzA0FGTARBTDIwA0FTTARBUFNMA0JTRANDUEwFRVBMMTAER1RQTANHUEwETEdQTAVJQk1QTARJT1NMBE1TQ0wETVNQTARNU1JMB01TVlNTREsETUlURAVNUEwxMAVNUEwxMQVOUEwxMAVOUEwxMQNPU0wEUEhQTARQU0ZMAlNMA1NQTANXM0MFV1hXTEwDWkxMA1pQTBUfASoDQUZMBEFMMjADQVNMBEFQU0wDQlNEA0NQTAVFUEwxMARHVFBMA0dQTARMR1BMBUlCTVBMBElPU0wETVNDTARNU1BMBE1TUkwHTVNWU1NESwRNSVREBU1QTDEwBU1QTDExBU5QTDEwBU5QTDExA09TTARQSFBMBFBTRkwCU0wDU1BMA1czQwVXWFdMTANaTEwDWlBMFCsDH2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dkZAILDw8WAh8CBTBodHRwOi8vd3d3LmtvZGVycy5jb20vY29ycC9zdXBwb3J0L2xpY2Vuc2UtaW5mby9kFgJmDxYCHgNzcmMFEy9pbWFnZXMvaW5mb18xNi5naWZkAgkPDxYCHgdWaXNpYmxlZ2QWCmYPDxYEHwMFFC9pbWFnZXMvZG93bmxvYWQuZ2lmHgdUb29sVGlwBRNEb3dubG9hZCBCaG9NYWluLmNzZGQCAg8WAh4LXyFJdGVtQ291bnQCARYCZg9kFgJmDxUFCkJob01haW4uY3MCQyMAAAhMT0M6IDM3MWQCAw8WAh8IAgEWAmYPZBYGZg8VAgA9L2luZm8uYXNweD9jPVByb2plY3RJbmZvJnBpZD1XUEc4VE5HU1JURUNYSFU0VFRTRDVOU0ZCRCZzPUJIT2QCAQ9kFgJmDxUBCk5YSUVIZWxwZXJkAgIPFQIJTnV4ZW81T3JnA3N2bmQCBQ9kFgJmD2QWBgIBDw8WAh8CBTcvaW5mby5hc3B4P2M9UHJvamVjdEluZm8mcGlkPVdQRzhUTkdTUlRFQ1hIVTRUVFNENU5TRkJEZBYCZg8PFgQfAwUSL2ltYWdlcy9mb2xkZXIuZ2lmHwcFKE51eGVvNU9yZ1xuXG54aWVoZWxwZXJcdHJ1bmtcTnhJRUhlbHBlclxkZAIDDw8WBB4EVGV4dAUeLi4ueGllaGVscGVyXHRydW5rXE54SUVIZWxwZXJcHwcFKE51eGVvNU9yZ1xuXG54aWVoZWxwZXJcdHJ1bmtcTnhJRUhlbHBlclxkZAIFDxYCHwgCCWQCBw8WAh8GaGQCBw8WAh8JBaMDPHNjcmlwdCB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiPg0KdmFyIGdhSnNIb3N0ID0gKCgiaHR0cHM6IiA9PSBkb2N1bWVudC5sb2NhdGlvbi5wcm90b2NvbCkgPyAiaHR0cHM6Ly9zc2wuIiA6ICJodHRwOi8vd3d3LiIpOw0KZG9jdW1lbnQud3JpdGUodW5lc2NhcGUoIiUzQ3NjcmlwdCBzcmM9JyIgKyBnYUpzSG9zdCArICJnb29nbGUtYW5hbHl0aWNzLmNvbS9nYS5qcycgdHlwZT0ndGV4dC9qYXZhc2NyaXB0JyUzRSUzQy9zY3JpcHQlM0UiKSk7DQo8L3NjcmlwdD4NCjxzY3JpcHQgdHlwZT0idGV4dC9qYXZhc2NyaXB0Ij4NCnZhciBwYWdlVHJhY2tlciA9IF9nYXQuX2dldFRyYWNrZXIoIlVBLTE0NTA0ODAtMyIpOw0KcGFnZVRyYWNrZXIuX2luaXREYXRhKCk7DQpwYWdlVHJhY2tlci5fdHJhY2tQYWdldmlldygpOw0KPC9zY3JpcHQ+DQpkGAEFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYBBRpLb2Rldmlld2VyMSREb3dubG9hZEJ1dHRvbg==" />


<script src="/ScriptResource.axd?d=MGrFr4RFqytxZinrzRW8UzygTzM7yLEbsL0CWXxTPxlGdGFMmFgXKm3IfhJUN9Tk0&amp;t=633638931120000000" type="text/javascript"></script>
<script src="/ws/1.0/account/mailinglist/default.asmx/js" type="text/javascript"></script>
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="left"  style="padding: 8px 10px 2px 10px;width:425px;">
                    
<table id="t1" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td style="white-space: nowrap;" nowrap="nowrap">
		    <a id="ctl03_Home" href="/"><img id="ctl03_imgLogo" hspace="0" vspace="6" src="http://media.blackducksoftware.com/koders/bds/logo_med_tag_bd.gif" height="32" width="141" border="0" /></a>
		</td>
		<td style="white-space: nowrap;" nowrap="nowrap">
		    <input name="ctl03$ctl00$s" type="text" maxlength="128" id="ctl03_ctl00_s" class="tbSearchSmall" />

		    <input type="submit" name="ctl03$btnSearch" value="Search" id="ctl03_btnSearch" class="btnSearch" /><input type="text" style="display:none;" /><br/>
			Filter:
			<select name="ctl03$Languages" id="ctl03_Languages">
	<option value="*">All Languages</option>
	<option value="ActionScript">ActionScript</option>
	<option value="Ada">Ada</option>
	<option value="ASP">ASP</option>
	<option value="ASP.NET">ASP.NET</option>
	<option value="Assembler">Assembler</option>
	<option value="C">C</option>
	<option value="C#">C#</option>
	<option value="Cpp">C++</option>
	<option value="Cobol">Cobol</option>
	<option value="ColdFusion">ColdFusion</option>
	<option value="Delphi">Delphi</option>
	<option value="Eiffel">Eiffel</option>
	<option value="Erlang">Erlang</option>
	<option value="Fortran">Fortran</option>
	<option value="Java">Java</option>
	<option value="JavaScript">JavaScript</option>
	<option value="JSP">JSP</option>
	<option value="Lisp">Lisp</option>
	<option value="Lua">Lua</option>
	<option value="Mathematica">Mathematica</option>
	<option value="Matlab">Matlab</option>
	<option value="ObjectiveC">ObjectiveC</option>
	<option value="Perl">Perl</option>
	<option value="PHP">PHP</option>
	<option value="Prolog">Prolog</option>
	<option value="Python">Python</option>
	<option value="Ruby">Ruby</option>
	<option value="Scheme">Scheme</option>
	<option value="Smalltalk">Smalltalk</option>
	<option value="SQL">SQL</option>
	<option value="Tcl">Tcl</option>
	<option value="VB">VB</option>
	<option value="VB.NET">VB.NET</option>

</select>&nbsp;<select name="ctl03$Licenses" id="ctl03_Licenses">
	<option value="*">All Licenses</option>
	<option value="AFL">AFL</option>
	<option value="AL20">AL20</option>
	<option value="ASL">ASL</option>
	<option value="APSL">APSL</option>
	<option value="BSD">BSD</option>
	<option value="CPL">CPL</option>
	<option value="EPL10">EPL10</option>
	<option value="GTPL">GTPL</option>
	<option value="GPL">GPL</option>
	<option value="LGPL">LGPL</option>
	<option value="IBMPL">IBMPL</option>
	<option value="IOSL">IOSL</option>
	<option value="MSCL">MSCL</option>
	<option value="MSPL">MSPL</option>
	<option value="MSRL">MSRL</option>
	<option value="MSVSSDK">MSVSSDK</option>
	<option value="MITD">MITD</option>
	<option value="MPL10">MPL10</option>
	<option value="MPL11">MPL11</option>
	<option value="NPL10">NPL10</option>
	<option value="NPL11">NPL11</option>
	<option value="OSL">OSL</option>
	<option value="PHPL">PHPL</option>
	<option value="PSFL">PSFL</option>
	<option value="SL">SL</option>
	<option value="SPL">SPL</option>
	<option value="W3C">W3C</option>
	<option value="WXWLL">WXWLL</option>
	<option value="ZLL">ZLL</option>
	<option value="ZPL">ZPL</option>

</select>&nbsp;<a id="ctl03_LicenseInfoLink" href="http://www.koders.com/corp/support/license-info/"><img src="/images/info_16.gif" id="ctl03_InfoImg" alt="InfoImg" height="16" width="16" border="0" /></a>
		</td>
		<td align="center" width="0px" valign="middle">
			
		</td>
	</tr>
</table>

                </td>
                <td align="right">
					<div class="kvHeaderLimiter">
                    

<script type="text/javascript" src="/scripts/toolbar.js"></script>
<link href="/skins/koders/toptoolbar.css" type="text/css" rel="stylesheet" />
<!-- {Begin Toolbar} -->
<div id="topToolbars">
<div class="topToolbar ttb_wider" style="width:463px; top: 0px; right: 0px;"
    onmouseover="Toolbar._onmouseover(event); " onmouseout="Toolbar._onmouseout(event); ">
    <div class="topToolbarCorner"></div>
    <div class="topToolbarInner">
        <div class="topToolbarButton" style="width:84px;cursor:pointer;cursor:hand" onclick="window.location='http://www.koders.com/corp/about/'">Company <span class="topToolbarDownArrow">&#9660;</span>
            <div class="topToolbarPopup" style="width:110px">
                <a href="http://corp.koders.com/corp/about/">About Koders</a>
                <a href="http://corp.koders.com/corp/about#Contact">Contact Us</a>
            </div>
        </div>
        <div class="topToolbarButton" style="width: 72px;cursor:pointer;cursor:hand" onclick="window.location='http://www.koders.com/info.aspx?c=tools'">Plugins <span class="topToolbarDownArrow">&#9660;</span>
            <div class="topToolbarPopup" style="width:110px">
                <a href="http://www.koders.com/info.aspx?c=tools#IDEs">
                    <img src="/images/toolbar-eclipse_icon.gif" alt="e" /> Eclipse</a>
                <a href="http://www.koders.com/info.aspx?c=tools#IDEs">
                    <img src="/images/toolbar-visual_icon.gif" alt="v" /> Visual Studio</a>
                <a href="http://www.koders.com/info.aspx?c=tools#Firefox">Browser</a>
                <a href="http://www.koders.com/info.aspx?c=tools#AddSearch">Widget APIs</a>
                <!--<a href="http://www.koders.com/blog/?p=36">Koders APIs</a>-->
            </div>
        </div>
        <div class="topToolbarButton" style="width: 76px;cursor:pointer;cursor:hand" onclick="window.location='http://corp.koders.com/corp/support'">Support <span class="topToolbarDownArrow">&#9660;</span>
            <div class="topToolbarPopup" style="width:160px">
                <a href="http://corp.koders.com/corp/support/">Support Overview</a>
                <a href="http://corp.koders.com/corp/support/code-search/getting-started/">Getting Started Guide</a>
                <a href="http://www.koders.com/info.aspx?c=feedback&product=kdc&type=bug">Feedback</a>
                <a href="http://corp.koders.com/corp/support/license-info/">License Info</a>
            </div>
        </div>
        <div class="topToolbarButton" style="width: 98px;cursor:pointer;cursor:hand" onclick="window.location='http://corp.koders.com/corp/community/'">Community <span class="topToolbarDownArrow">&#9660;</span>
            <div class="topToolbarPopup" style="width:150px">
		        <a href="http://corp.koders.com/corp/community/">Community Overview</a>
                <a href="http://www.koders.com/info.aspx?c=forms/SubmitProject">Add Projects</a>
                <!--<a href="http://www.koders.com/blog/">Blog</a>-->
                <a href="http://forums.koders.com/">Forums</a>
                <a href="http://corp.koders.com/zeitgeist/">Top Searches</a>
            </div>
        </div>
        <div class="topToolbarButton" style="width: 102px;cursor:pointer;cursor:hand" onclick="window.location='http://www.koders.com/info.aspx?page=MyAccount'">My Account <span class="topToolbarDownArrow">&#9660;</span>
            <div class="topToolbarPopup" style="width:98px">
                <a id="Sitetoolbar1_ctl00_toggleAccount" href="http://www.koders.com/info.aspx?page=MyAccount&amp;action=EditProfile">Edit Profile</a>
                <a id="Sitetoolbar1_ctl00_toggleLogin" href="http://www.koders.com/info.aspx?page=MyAccount&amp;action=Login">Logout</a>
            </div>
        </div>
    </div>
</div>
<br class="clear" />
</div>
<!-- {End Toolbar} -->
&nbsp;
                    </div>
                </td>
            </tr>
        </table>
        
        <div id="leaderboard" style="">
            <center>
				
<!-- Media Server Begin: 728x90 -->

<!-- Media Server End: 728x90 -->
               
            </center>
		</div>
        
        <div class="kodeViewerContDiv">
            
<!-- <div class="SidePadding" align="left"> -->
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td>
			<table id="Table2" cellspacing="5" width="100%" border="0">
				<tr>
					<td class="info_panel" valign="top">
						<table id="Table3" border="0">
							<tr>
								<td valign="middle" align="center" width="60">
									<input type="image" name="Kodeviewer1$DownloadButton" id="Kodeviewer1_DownloadButton" title="Download BhoMain.cs" src="/images/download.gif" border="0" />
									<a id="Kodeviewer1_LinkButton1" href="javascript:__doPostBack('Kodeviewer1$LinkButton1','')">download</a>
								</td>
								<td>
											<b>
												BhoMain.cs
											</b>
											<br>
											Language: <b>
												C#
											</b>
											<br>
											
											
											LOC: 371
											<br>
										</td>
							</tr>
						</table>
					</td>
					<td class="kodeviewer_info_panel" valign="top" width="200">
								<b>Project Info</b><br>
								<a title='' href='/info.aspx?c=ProjectInfo&pid=WPG8TNGSRTECXHU4TTSD5NSFBD&s=BHO'>
									<b>
										<span id="Kodeviewer1_ProjectView_ctl00_Label2">NXIEHelper</span></b></a>
								<br>
								Server:
								Nuxeo5Org
								<br>
								Type:
								svn
							
						
						
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>

<style type="text/css"> 
.RightBanner { Z-INDEX: 2; RIGHT: 25px; FLOAT: right; WIDTH: 160px; POSITION: absolute; HEIGHT: 600px } 
</style>

	<div id="Kodeviewer1_RightBanner" class="RightBanner">
<div style="MARGIN-BOTTOM: 2px" align="center">[<a style="FONT-SIZE: smaller" 
href="javascript:HideBanner();">Show Code</a>] </div>
<div>

<!-- Media Server Begin: 160x600 -->

<!-- Media Server End: 160x600 -->
<br/><br/><br/>
<!-- Media Server Begin: 160x602 -->

<!-- Media Server End: 160x602 -->
<br/><br/><br/>
<!-- Media Server Begin: 160x603 -->

<!-- Media Server End: 160x603 -->
</div>
<div style="MARGIN-TOP: 2px" align="center">[<a style="FONT-SIZE: smaller" 
href="javascript:HideBanner();">Show Code</a>] </div></div>

<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td height="100%">
			<table id="Table9" cellspacing="8" cellpadding="0" border="0" style="height:100%">
				<tr>
					<td valign="top">
						
						
<table id="Table1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
	cellspacing="0" cellpadding="3" border="0" width="100%">
	<tbody>
		<tr>
			<th class="exp_header" align="left">
				<a id="Kodeviewer1_ctl00_hyFicon" href="/info.aspx?c=ProjectInfo&amp;pid=WPG8TNGSRTECXHU4TTSD5NSFBD"><img id="Kodeviewer1_ctl00_ficon" title="Nuxeo5Org\n\nxiehelper\trunk\NxIEHelper\" src="/images/folder.gif" align="left" border="0" /></a>
				<b>
					<span id="Kodeviewer1_ctl00_Label1" title="Nuxeo5Org\n\nxiehelper\trunk\NxIEHelper\">...xiehelper\trunk\NxIEHelper\</span>
				</b>
				<br/>
			</th>
		</tr>
		<tr>
			<td class="exp_items">
				
						&nbsp;&nbsp;
					<a title="App.config" href="/noncode/fid056C55965B8BA3DB2D8E21EBDFD87F0C0E71860E.aspx?s=BHO">App.config<br></a>
						&nbsp;&nbsp;
					<a title="BhoMain.cs" href="/csharp/fid02F92772225FF8711957E789B2BAFEC8A9236DF0.aspx?s=BHO">BhoMain.cs<br></a>
						&nbsp;&nbsp;
					<a title="ConfigManager.cs" href="/csharp/fidCEA3FEA19C680E2D8EF7BC2F84070C11A7F397F8.aspx?s=BHO">ConfigManager.cs<br></a>
						&nbsp;&nbsp;
					<a title="LogForm.cs" href="/csharp/fidFB5BC1D56AD4AAF5C51931BCC69421CFF485606C.aspx?s=BHO">LogForm.cs<br></a>
						&nbsp;&nbsp;
					<a title="LogForm.Designer.cs" href="/csharp/fidA9BBFB7EF68BC3B7A8BF4E196CBF7E55313D3227.aspx?s=BHO">LogForm.Designer.cs<br></a>
						&nbsp;&nbsp;
					<a title="LogForm.resx" href="/xml/fid958B3D0C6E103821067773FF68949B083C29513E.aspx?s=BHO">LogForm.resx<br></a>
						&nbsp;&nbsp;
					<a title="Logger.cs" href="/csharp/fid1CA75C8A6301396459A83AD3E771842EA143D91E.aspx?s=BHO">Logger.cs<br></a>
						&nbsp;&nbsp;
					<a title="NxIEHelper.csproj" href="/noncode/fid749DE29A36D1D600D422ECF19694FA557C156E29.aspx?s=BHO">NxIEHelper.csproj<br></a>
						&nbsp;&nbsp;
					<a title="TransferOperation.cs" href="/csharp/fid7E048CDBBB5D2BCAE9666C0ABD694C5B9974CC07.aspx?s=BHO">TransferOperation.cs<br></a>
			</td>
		</tr>
	</tbody>
</table>
<br/>

						


						
					</td>
					<td valign="top">
						<div>
							<pre id='kodeViewerLineNumbers'><a href="#L1" name="L1">1</a><br/><a href="#L2" name="L2">2</a><br/><a href="#L3" name="L3">3</a><br/><a href="#L4" name="L4">4</a><br/><a href="#L5" name="L5">5</a><br/><a href="#L6" name="L6">6</a><br/><a href="#L7" name="L7">7</a><br/><a href="#L8" name="L8">8</a><br/><a href="#L9" name="L9">9</a><br/><a href="#L10" name="L10">10</a><br/><a href="#L11" name="L11">11</a><br/><a href="#L12" name="L12">12</a><br/><a href="#L13" name="L13">13</a><br/><a href="#L14" name="L14">14</a><br/><a href="#L15" name="L15">15</a><br/><a href="#L16" name="L16">16</a><br/><a href="#L17" name="L17">17</a><br/><a href="#L18" name="L18">18</a><br/><a href="#L19" name="L19">19</a><br/><a href="#L20" name="L20">20</a><br/><a href="#L21" name="L21">21</a><br/><a href="#L22" name="L22">22</a><br/><a href="#L23" name="L23">23</a><br/><a href="#L24" name="L24">24</a><br/><a href="#L25" name="L25">25</a><br/><a href="#L26" name="L26">26</a><br/><a href="#L27" name="L27">27</a><br/><a href="#L28" name="L28">28</a><br/><a href="#L29" name="L29">29</a><br/><a href="#L30" name="L30">30</a><br/><a href="#L31" name="L31">31</a><br/><a href="#L32" name="L32">32</a><br/><a href="#L33" name="L33">33</a><br/><a href="#L34" name="L34">34</a><br/><a href="#L35" name="L35">35</a><br/><a href="#L36" name="L36">36</a><br/><a href="#L37" name="L37">37</a><br/><a href="#L38" name="L38">38</a><br/><a href="#L39" name="L39">39</a><br/><a href="#L40" name="L40">40</a><br/><a href="#L41" name="L41">41</a><br/><a href="#L42" name="L42">42</a><br/><a href="#L43" name="L43">43</a><br/><a href="#L44" name="L44">44</a><br/><a href="#L45" name="L45">45</a><br/><a href="#L46" name="L46">46</a><br/><a href="#L47" name="L47">47</a><br/><a href="#L48" name="L48">48</a><br/><a href="#L49" name="L49">49</a><br/><a href="#L50" name="L50">50</a><br/><a href="#L51" name="L51">51</a><br/><a href="#L52" name="L52">52</a><br/><a href="#L53" name="L53">53</a><br/><a href="#L54" name="L54">54</a><br/><a href="#L55" name="L55">55</a><br/><a href="#L56" name="L56">56</a><br/><a href="#L57" name="L57">57</a><br/><a href="#L58" name="L58">58</a><br/><a href="#L59" name="L59">59</a><br/><a href="#L60" name="L60">60</a><br/><a href="#L61" name="L61">61</a><br/><a href="#L62" name="L62">62</a><br/><a href="#L63" name="L63">63</a><br/><a href="#L64" name="L64">64</a><br/><a href="#L65" name="L65">65</a><br/><a href="#L66" name="L66">66</a><br/><a href="#L67" name="L67">67</a><br/><a href="#L68" name="L68">68</a><br/><a href="#L69" name="L69">69</a><br/><a href="#L70" name="L70">70</a><br/><a href="#L71" name="L71">71</a><br/><a href="#L72" name="L72">72</a><br/><a href="#L73" name="L73">73</a><br/><a href="#L74" name="L74">74</a><br/><a href="#L75" name="L75">75</a><br/><a href="#L76" name="L76">76</a><br/><a href="#L77" name="L77">77</a><br/><a href="#L78" name="L78">78</a><br/><a href="#L79" name="L79">79</a><br/><a href="#L80" name="L80">80</a><br/><a href="#L81" name="L81">81</a><br/><a href="#L82" name="L82">82</a><br/><a href="#L83" name="L83">83</a><br/><a href="#L84" name="L84">84</a><br/><a href="#L85" name="L85">85</a><br/><a href="#L86" name="L86">86</a><br/><a href="#L87" name="L87">87</a><br/><a href="#L88" name="L88">88</a><br/><a href="#L89" name="L89">89</a><br/><a href="#L90" name="L90">90</a><br/><a href="#L91" name="L91">91</a><br/><a href="#L92" name="L92">92</a><br/><a href="#L93" name="L93">93</a><br/><a href="#L94" name="L94">94</a><br/><a href="#L95" name="L95">95</a><br/><a href="#L96" name="L96">96</a><br/><a href="#L97" name="L97">97</a><br/><a href="#L98" name="L98">98</a><br/><a href="#L99" name="L99">99</a><br/><a href="#L100" name="L100">100</a><br/><a href="#L101" name="L101">101</a><br/><a href="#L102" name="L102">102</a><br/><a href="#L103" name="L103">103</a><br/><a href="#L104" name="L104">104</a><br/><a href="#L105" name="L105">105</a><br/><a href="#L106" name="L106">106</a><br/><a href="#L107" name="L107">107</a><br/><a href="#L108" name="L108">108</a><br/><a href="#L109" name="L109">109</a><br/><a href="#L110" name="L110">110</a><br/><a href="#L111" name="L111">111</a><br/><a href="#L112" name="L112">112</a><br/><a href="#L113" name="L113">113</a><br/><a href="#L114" name="L114">114</a><br/><a href="#L115" name="L115">115</a><br/><a href="#L116" name="L116">116</a><br/><a href="#L117" name="L117">117</a><br/><a href="#L118" name="L118">118</a><br/><a href="#L119" name="L119">119</a><br/><a href="#L120" name="L120">120</a><br/><a href="#L121" name="L121">121</a><br/><a href="#L122" name="L122">122</a><br/><a href="#L123" name="L123">123</a><br/><a href="#L124" name="L124">124</a><br/><a href="#L125" name="L125">125</a><br/><a href="#L126" name="L126">126</a><br/><a href="#L127" name="L127">127</a><br/><a href="#L128" name="L128">128</a><br/><a href="#L129" name="L129">129</a><br/><a href="#L130" name="L130">130</a><br/><a href="#L131" name="L131">131</a><br/><a href="#L132" name="L132">132</a><br/><a href="#L133" name="L133">133</a><br/><a href="#L134" name="L134">134</a><br/><a href="#L135" name="L135">135</a><br/><a href="#L136" name="L136">136</a><br/><a href="#L137" name="L137">137</a><br/><a href="#L138" name="L138">138</a><br/><a href="#L139" name="L139">139</a><br/><a href="#L140" name="L140">140</a><br/><a href="#L141" name="L141">141</a><br/><a href="#L142" name="L142">142</a><br/><a href="#L143" name="L143">143</a><br/><a href="#L144" name="L144">144</a><br/><a href="#L145" name="L145">145</a><br/><a href="#L146" name="L146">146</a><br/><a href="#L147" name="L147">147</a><br/><a href="#L148" name="L148">148</a><br/><a href="#L149" name="L149">149</a><br/><a href="#L150" name="L150">150</a><br/><a href="#L151" name="L151">151</a><br/><a href="#L152" name="L152">152</a><br/><a href="#L153" name="L153">153</a><br/><a href="#L154" name="L154">154</a><br/><a href="#L155" name="L155">155</a><br/><a href="#L156" name="L156">156</a><br/><a href="#L157" name="L157">157</a><br/><a href="#L158" name="L158">158</a><br/><a href="#L159" name="L159">159</a><br/><a href="#L160" name="L160">160</a><br/><a href="#L161" name="L161">161</a><br/><a href="#L162" name="L162">162</a><br/><a href="#L163" name="L163">163</a><br/><a href="#L164" name="L164">164</a><br/><a href="#L165" name="L165">165</a><br/><a href="#L166" name="L166">166</a><br/><a href="#L167" name="L167">167</a><br/><a href="#L168" name="L168">168</a><br/><a href="#L169" name="L169">169</a><br/><a href="#L170" name="L170">170</a><br/><a href="#L171" name="L171">171</a><br/><a href="#L172" name="L172">172</a><br/><a href="#L173" name="L173">173</a><br/><a href="#L174" name="L174">174</a><br/><a href="#L175" name="L175">175</a><br/><a href="#L176" name="L176">176</a><br/><a href="#L177" name="L177">177</a><br/><a href="#L178" name="L178">178</a><br/><a href="#L179" name="L179">179</a><br/><a href="#L180" name="L180">180</a><br/><a href="#L181" name="L181">181</a><br/><a href="#L182" name="L182">182</a><br/><a href="#L183" name="L183">183</a><br/><a href="#L184" name="L184">184</a><br/><a href="#L185" name="L185">185</a><br/><a href="#L186" name="L186">186</a><br/><a href="#L187" name="L187">187</a><br/><a href="#L188" name="L188">188</a><br/><a href="#L189" name="L189">189</a><br/><a href="#L190" name="L190">190</a><br/><a href="#L191" name="L191">191</a><br/><a href="#L192" name="L192">192</a><br/><a href="#L193" name="L193">193</a><br/><a href="#L194" name="L194">194</a><br/><a href="#L195" name="L195">195</a><br/><a href="#L196" name="L196">196</a><br/><a href="#L197" name="L197">197</a><br/><a href="#L198" name="L198">198</a><br/><a href="#L199" name="L199">199</a><br/><a href="#L200" name="L200">200</a><br/><a href="#L201" name="L201">201</a><br/><a href="#L202" name="L202">202</a><br/><a href="#L203" name="L203">203</a><br/><a href="#L204" name="L204">204</a><br/><a href="#L205" name="L205">205</a><br/><a href="#L206" name="L206">206</a><br/><a href="#L207" name="L207">207</a><br/><a href="#L208" name="L208">208</a><br/><a href="#L209" name="L209">209</a><br/><a href="#L210" name="L210">210</a><br/><a href="#L211" name="L211">211</a><br/><a href="#L212" name="L212">212</a><br/><a href="#L213" name="L213">213</a><br/><a href="#L214" name="L214">214</a><br/><a href="#L215" name="L215">215</a><br/><a href="#L216" name="L216">216</a><br/><a href="#L217" name="L217">217</a><br/><a href="#L218" name="L218">218</a><br/><a href="#L219" name="L219">219</a><br/><a href="#L220" name="L220">220</a><br/><a href="#L221" name="L221">221</a><br/><a href="#L222" name="L222">222</a><br/><a href="#L223" name="L223">223</a><br/><a href="#L224" name="L224">224</a><br/><a href="#L225" name="L225">225</a><br/><a href="#L226" name="L226">226</a><br/><a href="#L227" name="L227">227</a><br/><a href="#L228" name="L228">228</a><br/><a href="#L229" name="L229">229</a><br/><a href="#L230" name="L230">230</a><br/><a href="#L231" name="L231">231</a><br/><a href="#L232" name="L232">232</a><br/><a href="#L233" name="L233">233</a><br/><a href="#L234" name="L234">234</a><br/><a href="#L235" name="L235">235</a><br/><a href="#L236" name="L236">236</a><br/><a href="#L237" name="L237">237</a><br/><a href="#L238" name="L238">238</a><br/><a href="#L239" name="L239">239</a><br/><a href="#L240" name="L240">240</a><br/><a href="#L241" name="L241">241</a><br/><a href="#L242" name="L242">242</a><br/><a href="#L243" name="L243">243</a><br/><a href="#L244" name="L244">244</a><br/><a href="#L245" name="L245">245</a><br/><a href="#L246" name="L246">246</a><br/><a href="#L247" name="L247">247</a><br/><a href="#L248" name="L248">248</a><br/><a href="#L249" name="L249">249</a><br/><a href="#L250" name="L250">250</a><br/><a href="#L251" name="L251">251</a><br/><a href="#L252" name="L252">252</a><br/><a href="#L253" name="L253">253</a><br/><a href="#L254" name="L254">254</a><br/><a href="#L255" name="L255">255</a><br/><a href="#L256" name="L256">256</a><br/><a href="#L257" name="L257">257</a><br/><a href="#L258" name="L258">258</a><br/><a href="#L259" name="L259">259</a><br/><a href="#L260" name="L260">260</a><br/><a href="#L261" name="L261">261</a><br/><a href="#L262" name="L262">262</a><br/><a href="#L263" name="L263">263</a><br/><a href="#L264" name="L264">264</a><br/><a href="#L265" name="L265">265</a><br/><a href="#L266" name="L266">266</a><br/><a href="#L267" name="L267">267</a><br/><a href="#L268" name="L268">268</a><br/><a href="#L269" name="L269">269</a><br/><a href="#L270" name="L270">270</a><br/><a href="#L271" name="L271">271</a><br/><a href="#L272" name="L272">272</a><br/><a href="#L273" name="L273">273</a><br/><a href="#L274" name="L274">274</a><br/><a href="#L275" name="L275">275</a><br/><a href="#L276" name="L276">276</a><br/><a href="#L277" name="L277">277</a><br/><a href="#L278" name="L278">278</a><br/><a href="#L279" name="L279">279</a><br/><a href="#L280" name="L280">280</a><br/><a href="#L281" name="L281">281</a><br/><a href="#L282" name="L282">282</a><br/><a href="#L283" name="L283">283</a><br/><a href="#L284" name="L284">284</a><br/><a href="#L285" name="L285">285</a><br/><a href="#L286" name="L286">286</a><br/><a href="#L287" name="L287">287</a><br/><a href="#L288" name="L288">288</a><br/><a href="#L289" name="L289">289</a><br/><a href="#L290" name="L290">290</a><br/><a href="#L291" name="L291">291</a><br/><a href="#L292" name="L292">292</a><br/><a href="#L293" name="L293">293</a><br/><a href="#L294" name="L294">294</a><br/><a href="#L295" name="L295">295</a><br/><a href="#L296" name="L296">296</a><br/><a href="#L297" name="L297">297</a><br/><a href="#L298" name="L298">298</a><br/><a href="#L299" name="L299">299</a><br/><a href="#L300" name="L300">300</a><br/><a href="#L301" name="L301">301</a><br/><a href="#L302" name="L302">302</a><br/><a href="#L303" name="L303">303</a><br/><a href="#L304" name="L304">304</a><br/><a href="#L305" name="L305">305</a><br/><a href="#L306" name="L306">306</a><br/><a href="#L307" name="L307">307</a><br/><a href="#L308" name="L308">308</a><br/><a href="#L309" name="L309">309</a><br/><a href="#L310" name="L310">310</a><br/><a href="#L311" name="L311">311</a><br/><a href="#L312" name="L312">312</a><br/><a href="#L313" name="L313">313</a><br/><a href="#L314" name="L314">314</a><br/><a href="#L315" name="L315">315</a><br/><a href="#L316" name="L316">316</a><br/><a href="#L317" name="L317">317</a><br/><a href="#L318" name="L318">318</a><br/><a href="#L319" name="L319">319</a><br/><a href="#L320" name="L320">320</a><br/><a href="#L321" name="L321">321</a><br/><a href="#L322" name="L322">322</a><br/><a href="#L323" name="L323">323</a><br/><a href="#L324" name="L324">324</a><br/><a href="#L325" name="L325">325</a><br/><a href="#L326" name="L326">326</a><br/><a href="#L327" name="L327">327</a><br/><a href="#L328" name="L328">328</a><br/><a href="#L329" name="L329">329</a><br/><a href="#L330" name="L330">330</a><br/><a href="#L331" name="L331">331</a><br/><a href="#L332" name="L332">332</a><br/><a href="#L333" name="L333">333</a><br/><a href="#L334" name="L334">334</a><br/><a href="#L335" name="L335">335</a><br/><a href="#L336" name="L336">336</a><br/><a href="#L337" name="L337">337</a><br/><a href="#L338" name="L338">338</a><br/><a href="#L339" name="L339">339</a><br/><a href="#L340" name="L340">340</a><br/><a href="#L341" name="L341">341</a><br/><a href="#L342" name="L342">342</a><br/><a href="#L343" name="L343">343</a><br/><a href="#L344" name="L344">344</a><br/><a href="#L345" name="L345">345</a><br/><a href="#L346" name="L346">346</a><br/><a href="#L347" name="L347">347</a><br/><a href="#L348" name="L348">348</a><br/><a href="#L349" name="L349">349</a><br/><a href="#L350" name="L350">350</a><br/><a href="#L351" name="L351">351</a><br/><a href="#L352" name="L352">352</a><br/><a href="#L353" name="L353">353</a><br/><a href="#L354" name="L354">354</a><br/><a href="#L355" name="L355">355</a><br/><a href="#L356" name="L356">356</a><br/><a href="#L357" name="L357">357</a><br/><a href="#L358" name="L358">358</a><br/><a href="#L359" name="L359">359</a><br/><a href="#L360" name="L360">360</a><br/><a href="#L361" name="L361">361</a><br/><a href="#L362" name="L362">362</a><br/><a href="#L363" name="L363">363</a><br/><a href="#L364" name="L364">364</a><br/><a href="#L365" name="L365">365</a><br/><a href="#L366" name="L366">366</a><br/><a href="#L367" name="L367">367</a><br/><a href="#L368" name="L368">368</a><br/><a href="#L369" name="L369">369</a><br/><a href="#L370" name="L370">370</a><br/><a href="#L371" name="L371">371</a><br/><a href="#L372" name="L372">372</a><br/><a href="#L373" name="L373">373</a><br/><a href="#L374" name="L374">374</a><br/><a href="#L375" name="L375">375</a><br/><a href="#L376" name="L376">376</a><br/><a href="#L377" name="L377">377</a><br/><a href="#L378" name="L378">378</a><br/><a href="#L379" name="L379">379</a><br/><a href="#L380" name="L380">380</a><br/><a href="#L381" name="L381">381</a><br/><a href="#L382" name="L382">382</a><br/><a href="#L383" name="L383">383</a><br/><a href="#L384" name="L384">384</a><br/><a href="#L385" name="L385">385</a><br/><a href="#L386" name="L386">386</a><br/><a href="#L387" name="L387">387</a><br/><a href="#L388" name="L388">388</a><br/><a href="#L389" name="L389">389</a><br/><a href="#L390" name="L390">390</a><br/><a href="#L391" name="L391">391</a><br/><a href="#L392" name="L392">392</a><br/><a href="#L393" name="L393">393</a><br/><a href="#L394" name="L394">394</a><br/><a href="#L395" name="L395">395</a><br/><a href="#L396" name="L396">396</a><br/><a href="#L397" name="L397">397</a><br/><a href="#L398" name="L398">398</a><br/><a href="#L399" name="L399">399</a><br/><a href="#L400" name="L400">400</a><br/><a href="#L401" name="L401">401</a><br/><a href="#L402" name="L402">402</a><br/><a href="#L403" name="L403">403</a><br/><a href="#L404" name="L404">404</a><br/><a href="#L405" name="L405">405</a><br/><a href="#L406" name="L406">406</a><br/><a href="#L407" name="L407">407</a><br/><a href="#L408" name="L408">408</a><br/><a href="#L409" name="L409">409</a><br/><a href="#L410" name="L410">410</a><br/><a href="#L411" name="L411">411</a><br/><a href="#L412" name="L412">412</a><br/><a href="#L413" name="L413">413</a><br/><a href="#L414" name="L414">414</a><br/><a href="#L415" name="L415">415</a><br/><a href="#L416" name="L416">416</a><br/><a href="#L417" name="L417">417</a><br/><a href="#L418" name="L418">418</a><br/><a href="#L419" name="L419">419</a><br/><a href="#L420" name="L420">420</a><br/><a href="#L421" name="L421">421</a><br/><a href="#L422" name="L422">422</a><br/><a href="#L423" name="L423">423</a><br/><a href="#L424" name="L424">424</a><br/><a href="#L425" name="L425">425</a><br/><a href="#L426" name="L426">426</a><br/><a href="#L427" name="L427">427</a><br/><a href="#L428" name="L428">428</a><br/><a href="#L429" name="L429">429</a><br/><a href="#L430" name="L430">430</a><br/><a href="#L431" name="L431">431</a><br/><a href="#L432" name="L432">432</a><br/><a href="#L433" name="L433">433</a><br/><a href="#L434" name="L434">434</a><br/><a href="#L435" name="L435">435</a><br/><a href="#L436" name="L436">436</a><br/><a href="#L437" name="L437">437</a><br/><a href="#L438" name="L438">438</a><br/><a href="#L439" name="L439">439</a><br/><a href="#L440" name="L440">440</a><br/><a href="#L441" name="L441">441</a><br/><a href="#L442" name="L442">442</a><br/><a href="#L443" name="L443">443</a><br/><a href="#L444" name="L444">444</a><br/><a href="#L445" name="L445">445</a><br/><a href="#L446" name="L446">446</a><br/><a href="#L447" name="L447">447</a><br/><a href="#L448" name="L448">448</a><br/><a href="#L449" name="L449">449</a><br/><a href="#L450" name="L450">450</a><br/><a href="#L451" name="L451">451</a><br/><a href="#L452" name="L452">452</a><br/><a href="#L453" name="L453">453</a><br/><a href="#L454" name="L454">454</a><br/><a href="#L455" name="L455">455</a><br/><a href="#L456" name="L456">456</a><br/><a href="#L457" name="L457">457</a><br/><a href="#L458" name="L458">458</a><br/><a href="#L459" name="L459">459</a><br/><a href="#L460" name="L460">460</a><br/><a href="#L461" name="L461">461</a><br/><a href="#L462" name="L462">462</a><br/><a href="#L463" name="L463">463</a><br/><a href="#L464" name="L464">464</a><br/><a href="#L465" name="L465">465</a><br/><a href="#L466" name="L466">466</a><br/><a href="#L467" name="L467">467</a><br/></pre>
						</div>
					</td>
					<td valign="top" onmouseup="javascript:LogReuse();">
						<div id="CodeDiv">
							<pre><span class='k'>using</span> <a class='r'>System</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Collections</a>.<a class='r'>Generic</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Runtime</a>.<a class='r'>InteropServices</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Text</a>;
<span class='k'>using</span> <a class='r'>SHDocVw</a>;
<span class='k'>using</span> <a class='r'>mshtml</a>;
<span class='k'>using</span> <a class='r'>COMInterop</a> = <a class='r'>System</a>.<a class='r'>Runtime</a>.<a class='r'>InteropServices</a>.<a class='r'>ComTypes</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Windows</a>.<a class='r'>Forms</a>;
<span class='k'>using</span> <a class='r'>Microsoft</a>.<a class='r'>Win32</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>IO</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Diagnostics</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Collections</a>;
<span class='k'>using</span> <a class='r'>System</a>.<a class='r'>Configuration</a>;

<span class='k'>namespace</span> <a class='r'>NxIEHelperNS</a>
{
	[<a class='r'>ComVisible</a>(<span class='k'>true</span>),
	<a class='r'>ClassInterface</a>(<a class='r'>ClassInterfaceType</a>.<a class='r'>None</a>),
	<a class='r'>Guid</a>(<span class='str'>&quot;8E61D540-4EC6-4915-8BE6-1F86DA78F6E5&quot;</span>)]
	<span class='k'>public</span> <span class='k'>class</span> <a class='r'><span class='s0'>Bho</span>Main</a> : <a class='r'>IObjectWithSite</a>, <a class='r'>IDocHostUIHandler</a>, <a class='r'>NxIEHelperNS</a>.<a class='r'>IDropTarget</a>
	{
		#<a class='r'>region</a> <a class='r'>constants</a>

		<span class='k'>private</span> <span class='k'>const</span> <span class='k'>int</span> <a class='r'>MAX_PATH</a> = 260;
		<span class='k'>private</span> <span class='k'>const</span> <span class='k'>int</span> <a class='r'>MAX_FILE_LENGTH</a> = 1 &lt;&lt; 30;

		<span class='k'>private</span> <span class='k'>const</span> <span class='k'>int</span> <a class='r'>Ok</a> = 0;
		<span class='k'>private</span> <span class='k'>const</span> <span class='k'>int</span> <a class='r'>Error</a> = 1;
		<span class='k'>private</span> <span class='k'>const</span> <span class='k'>string</span> <a class='r'>pluginActivatorTag</a> = <span class='str'>&quot;Nx5PluginEnabled&quot;</span>;
		<span class='k'>private</span> <span class='k'>const</span> <span class='k'>string</span> <a class='r'>pluginStatusElement</a> = <span class='str'>&quot;nx5firefoxhelperStatus&quot;</span>;

		#<a class='r'>endregion</a>

		#<a class='r'>region</a> <a class='r'>properties</a>
		
		<span class='k'>public</span> <span class='k'>bool</span> <a class='r'>pluginEnabled</a> = <span class='k'>false</span>;
		<span class='k'>public</span> <span class='k'>bool</span> <a class='r'>PluginEnabled</a>
		{
			<span class='k'>get</span>
			{
				<span class='c'>// static check -&gt; requires enable OnDocumentComplete event
</span>				<span class='c'>//return pluginEnabled;
</span>
				<span class='c'>// dinamic check
</span>				<span class='k'>return</span> <a class='r'>IsPluginEnabled</a>();
			}
		}

		<span class='k'>private</span> <span class='k'>bool</span> <a class='r'>IsPluginEnabled</a>()
		{
			<span class='k'>bool</span> <a class='r'>retval</a> = <span class='k'>false</span>;
			<span class='k'>try</span>
			{
				<span class='k'>if</span> (<a class='r'>myBrowser</a> != <span class='k'>null</span>)
				{
					<a class='r'>IHTMLDocument3</a> <a class='r'>document</a> = <a class='r'>myBrowser</a>.<a class='r'>Document</a> <span class='k'>as</span> <a class='r'>IHTMLDocument3</a>;
					<a class='r'>IHTMLElement</a> <a class='r'>pluginSwitchElem</a> = <a class='r'>document</a>.<a class='r'>getElementById</a>(<a class='r'>pluginActivatorTag</a>);
					<a class='r'>retval</a> = (<a class='r'>pluginSwitchElem</a> != <span class='k'>null</span> <span class='c'>/*&amp;&amp; (string)pluginSwitchElem.getAttribute(&quot;value&quot;) == &quot;true&quot; */</span> );
					<span class='k'>if</span> (<a class='r'>retval</a>)
						<a class='r'>Logger</a>.<a class='r'>ShowForm</a>();
				}
			}
			<span class='k'>catch</span> (<a class='r'>Exception</a> <a class='r'>ex</a>)
			{
				<a class='r'>Logger</a>.<a class='r'>LogText</a>(<span class='str'>&quot;Exception: {0} -&gt; {1}&quot;</span>, <a class='r'>ex</a>.<a class='r'>Message</a>, <a class='r'>ex</a>.<a class='r'>StackTrace</a>);
			}
			<span class='k'>return</span> <a class='r'>retval</a>;
		}

		#<a class='r'>endregion</a>
		
		#<a class='r'>region</a> <span class='k'>private</span> <a class='r'>vars</a>
		
		<span class='k'>private</span> <a class='r'>IWebBrowser2</a> <a class='r'>myBrowser</a>;
		<span class='k'>private</span> <a class='r'>IDocHostUIHandler</a> <a class='r'>defaultDocHandler</a>;
		<span class='k'>private</span> <a class='r'>IDropTarget</a> <a class='r'>defaultDropTarget</a>;
		#<a class='r'>endregion</a>

		#<a class='r'>region</a> <a class='r'><span class='s0'>BHO</span></a> <a class='r'>registration</a>

		<span class='k'>private</span> <span class='k'>static</span> <span class='k'>string</span> <a class='r'><span class='s0'>Bho</span>RegistryKey</a>
		{
			<span class='k'>get</span>
			{
				<span class='k'>string</span> <a class='r'>parentKey</a> = @<span class='str'>&quot;SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects\&quot;;
				Attribute guidAttr = Attribute.GetCustomAttribute(typeof(<span class='s0'>Bho</span>Main), typeof(GuidAttribute));
				string guidValue = &quot;</span>{<span class='str'>&quot; + (guidAttr as GuidAttribute).Value + &quot;</span>}<span class='str'>&quot;;
				return parentKey + guidValue;
			}
		}
		[ComRegisterFunction]
		public static void Register<span class='s0'>BHO</span>(Type t)
		{
			Registry.LocalMachine.CreateSubKey(<span class='s0'>Bho</span>RegistryKey);
		}

		[ComUnregisterFunction]
		public static void Unregister<span class='s0'>BHO</span>(Type t)
		{
			Registry.LocalMachine.DeleteSubKey(<span class='s0'>Bho</span>RegistryKey);
		}

		#endregion

		#region constructor

		public <span class='s0'>Bho</span>Main()
		{
			myBrowser = null;
			defaultDocHandler = null;
			defaultDropTarget = null;
		} 
		#endregion

		#region IObjectWithSite Members

		public void SetSite(object pUnkSite)
		{
			try
			{
				if (pUnkSite == null)
				{
					myBrowser = null;
				}
				else
				{
					myBrowser = pUnkSite as IWebBrowser2;
					DWebBrowserEvents2_Event ev = myBrowser as DWebBrowserEvents2_Event;
					//ev.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(ev_DocumentComplete);
					ev.StatusTextChange += new DWebBrowserEvents2_StatusTextChangeEventHandler(ev_StatusTextChange);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void GetSite(ref Guid riid, out object ppvSite)
		{
			ppvSite = null;
		}

		#endregion

		#region browser event handlers

		void ev_StatusTextChange(string Text)
		{
			try
			{
				IHTMLDocument2 doc = myBrowser.Document as IHTMLDocument2;
				if (doc != null)
				{
					IOleObject oleObj = doc as IOleObject;
					if (oleObj != null)
					{
						ICustomDoc customDoc = doc as ICustomDoc;
						IOleClientSite clientSite = null;
						oleObj.GetClientSite(ref clientSite);
						if (customDoc != null &amp;&amp; clientSite != null)
						{
							defaultDocHandler = clientSite as IDocHostUIHandler;
							customDoc.SetUIHandler(this);
						}
					}
				}
			}
			catch
			{
				defaultDocHandler = null;
				defaultDropTarget = null;
			}
		}

		void ev_DocumentComplete(object pDisp, ref object URL)
		{
			pluginEnabled = IsPluginEnabled();
			//try 
			//{				
			//    IHTMLDocument3 document = myBrowser.Document as IHTMLDocument3;
			//    IHTMLElement pluginSwitchElem = document.getElementById(pluginActivatorTag);
			//    if (pluginSwitchElem != null)
			//        pluginEnabled= true;
			//} 
			//catch (Exception ex)
			//{
			//}
		}

		#endregion

		#region IDropTarget implementation

		public void DragEnter(COMInterop.IDataObject pDataObj, System.Int32 grfKeyState, System.Int32 ptX, System.Int32 ptY, ref DROPEFFECTS pdwEffect)
		{
			Logger.LogText(&quot;</span><a class='r'>DragEnter</a><span class='str'>&quot;);
			if (defaultDropTarget != null)
				defaultDropTarget.DragEnter(pDataObj, grfKeyState, ptX, ptY, ref pdwEffect);
		}

		public void DragLeave()
		{
			Logger.LogText(&quot;</span><a class='r'>DragLeave</a><span class='str'>&quot;);
			if (defaultDropTarget != null)
				defaultDropTarget.DragLeave();
		}
		
		public void DragOver(System.Int32 grfKeyState, System.Int32 ptX, System.Int32 ptY, ref DROPEFFECTS pdwEffect)
		{
			Logger.LogText(&quot;</span><a class='r'>DragOver</a><span class='str'>&quot;);
			if (defaultDropTarget != null)
				defaultDropTarget.DragOver(grfKeyState, ptX, ptY, ref pdwEffect);
		}

		public void Drop(COMInterop.IDataObject pDataObj, System.Int32 grfKeyState, System.Int32 ptX, System.Int32 ptY, ref DROPEFFECTS pdwEffect)
		{
			Logger.LogText(&quot;</span><a class='r'>DragEnter</a><span class='str'>&quot;);
			if (!PluginEnabled)
			{
				Logger.LogText(&quot;</span><a class='r'>Plugin</a> <a class='r'>NOT</a> <a class='r'>enabled</a><span class='str'>&quot;);
				if (defaultDropTarget != null)
					defaultDropTarget.Drop(pDataObj, grfKeyState, ptX, ptY, ref pdwEffect);
				return;
			}

			try
			{
				COMInterop.STGMEDIUM td = new COMInterop.STGMEDIUM();
				td.tymed = COMInterop.TYMED.TYMED_HGLOBAL;
				COMInterop.FORMATETC fr = new COMInterop.FORMATETC();
				fr.cfFormat = 15;
				fr.ptd = IntPtr.Zero;
				fr.dwAspect = COMInterop.DVASPECT.DVASPECT_CONTENT;
				fr.lindex = -1;
				fr.tymed = COMInterop.TYMED.TYMED_HGLOBAL;
				pDataObj.GetData(ref fr, out td);

				ArrayList filesList = new ArrayList();
				ArrayList folderList = new ArrayList();


				uint filesCount = Win32Api.DragQueryFile((uint)td.unionmember.ToInt32(), 0xFFFFFFFF, null, 0);
				if (filesCount &gt; 0)
				{
					int totalFilesCount = 0;
					IHTMLDocument2 htmlDocument = myBrowser.Document as IHTMLDocument2;
					for (uint i = 0; i &lt; filesCount; i++)
					{
						StringBuilder sb = new StringBuilder(MAX_PATH);
						Win32Api.DragQueryFile((uint)td.unionmember.ToInt32(), i, sb, MAX_PATH);
						string fileOrFolderName = sb.ToString();

						if (Directory.Exists(fileOrFolderName))
						{
							folderList.Add(fileOrFolderName);
							totalFilesCount += ComputeFolders_fileCount(fileOrFolderName);
						}
						else if (File.Exists(fileOrFolderName))
						{
							filesList.Add(fileOrFolderName);
							totalFilesCount++;
						}
					}

					Logger.LogText(&quot;</span><a class='r'>Initiate</a> <a class='r'>transfer</a><span class='str'>&quot;);
					string scriptCode = string.Format(&quot;</span><a class='r'>initTransfert</a>({0},<span class='k'>null</span>)<span class='str'>&quot;, totalFilesCount);
					htmlDocument.parentWindow.execScript(scriptCode, &quot;</span><a class='r'>JavaScript</a><span class='str'>&quot;);
					

					TransferOperation mainOperation = new TransferOperation(null,
																			this,
																			(string[])filesList.ToArray( typeof(string)),
																			(string[])folderList.ToArray(typeof(string)),
																			&quot;</span><span class='str'>&quot;,
																			&quot;</span><span class='str'>&quot;,
																			htmlDocument);
					TransferOperation.crtOperation = mainOperation;
					mainOperation.ContinueOpertation();

					//CComBSTR redirectUrl;
					//redirectUrl.AppendBSTR(m_url.bstrVal);
					//redirectUrl.AppendBSTR(CComBSTR(&quot;</span>?<a class='r'>portal_status_message</a>=<a class='r'>psm_file</a>(<a class='r'>s</a>)<a class='r'>_uploaded</a><span class='str'>&quot;) );
					//redirectUrl.AppendBSTR(m_location);
					//m_spWebBrowser2-&gt;Navigate(redirectUrl,&amp;vtEmpty,&amp;vtEmpty,&amp;vtEmpty,&amp;vtEmpty);
				}
				if (defaultDropTarget != null)
					defaultDropTarget.DragLeave();
			}
			catch (Exception ex)
			{
				Logger.LogText(&quot;</span><a class='r'>Exception</a>: {0} -&gt; {1}<span class='str'>&quot;, ex.Message, ex.StackTrace);
			}
		}

		
		#endregion
	
		#region IDocHostUIHandler Members

		uint  IDocHostUIHandler.ShowContextMenu(uint dwID, ref tagPOINT ppt, object pcmdtReserved, object pdispReserved)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.ShowContextMenu( dwID, ref ppt, pcmdtReserved, pdispReserved);
 			throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.GetHostInfo(ref DOCHOSTUIINFO pInfo)
		{
			
			if (defaultDocHandler!=null)
				defaultDocHandler.GetHostInfo( ref pInfo);
			pInfo.dwFlags = pInfo.dwFlags &amp; (~(uint)DOCHOSTUIFLAG.DOCHOSTUIFLAG_DIALOG);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.ShowUI(uint dwID, ref object pActiveObject, ref object pCommandTarget, ref object pFrame, ref object pDoc)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.ShowUI( dwID, ref pActiveObject, ref pCommandTarget, ref pFrame, ref pDoc);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.HideUI()
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.HideUI();
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.UpdateUI()
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.UpdateUI();
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.EnableModeless(int fEnable)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.EnableModeless(fEnable);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.OnDocWindowActivate(int fActivate)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.OnDocWindowActivate(fActivate);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.OnFrameWindowActivate(int fActivate)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.OnFrameWindowActivate(fActivate);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.ResizeBorder(ref tagRECT prcBorder, int pUIWindow, int fRameWindow)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.ResizeBorder(ref prcBorder, pUIWindow, fRameWindow);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		uint  IDocHostUIHandler.TranslateAccelerator(ref tagMSG lpMsg, ref Guid pguidCmdGroup, uint nCmdID)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.TranslateAccelerator(ref lpMsg, ref pguidCmdGroup, nCmdID);
			throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		void  IDocHostUIHandler.GetOptionKeyPath(ref string pchKey, uint dw)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.GetOptionKeyPath(ref pchKey, dw);
			//throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		int IDocHostUIHandler.GetDropTarget(IDropTarget pDropTarget, out IDropTarget ppDropTarget)
		{
			if (defaultDocHandler != null)
			{
				defaultDocHandler.GetDropTarget(pDropTarget, out ppDropTarget);
				defaultDropTarget = ppDropTarget as IDropTarget;
			}
			ppDropTarget = this;
			return Ok;
		}

		void IDocHostUIHandler.GetExternal([MarshalAs(UnmanagedType.IDispatch)] out object ppDispatch)
		{
			if (PluginEnabled)
			{
				ppDispatch = new CustomMethods();
			}
			else
				if (defaultDocHandler != null)
					defaultDocHandler.GetExternal(out ppDispatch);
				else
					ppDispatch = null;

		}

		uint  IDocHostUIHandler.TranslateUrl(uint dwTranslate, string pchURLIn, ref string ppchURLOut)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.TranslateUrl(dwTranslate, pchURLIn, ref ppchURLOut);
			throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		IDataObject  IDocHostUIHandler.FilterDataObject(IDataObject pDO)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.FilterDataObject(pDO);
			throw new Exception(&quot;</span><a class='r'>The</a> <a class='r'>method</a> <a class='r'>or</a> <a class='r'>operation</a> <span class='k'>is</span> <a class='r'>not</a> <a class='r'>implemented</a>.<span class='str'>&quot;);
		}

		#endregion

		#region private func
		
		private int ComputeFolders_fileCount(string folderPath)
		{
			int fileCount = 0;
			foreach (string subFolderPath in Directory.GetDirectories(folderPath))
				fileCount += ComputeFolders_fileCount(subFolderPath);
			return fileCount + Directory.GetFiles(folderPath).Length;
		} 
		#endregion

		public void DisplayHtmlStatus(string statusText, params object[] parameters)
		{
			try
			{
				if (!ConfigManager.DisableHtmlStatus)
				{
					IHTMLDocument3 doc = myBrowser.Document as IHTMLDocument3;
					doc.getElementById(&quot;</span><a class='r'>nx5firefoxhelperStatus</a><span class='str'>&quot;).setAttribute(&quot;</span><a class='r'>label</a><span class='str'>&quot;, string.Format(statusText, parameters), 0 /*case insensitive*/);
				}
			}
			catch{}
		}
	}

	public class CustomMethods : IJSCallback
	{
		#region ICustomMethods Members

		public void LogText([MarshalAs(UnmanagedType.BStr)] string textToLog)
		{
			Logger.LogText(&quot;</span><a class='r'>JScript</a>: {0}&quot;, <a class='r'>textToLog</a>);
		}

		<span class='k'>public</span> <span class='k'>void</span> <a class='r'>FileTransferCallback</a>()
		{
			<a class='r'>TransferOperation</a>.<a class='r'>crtOperation</a>.<a class='r'>FileCallback</a>();
		}

		<span class='k'>public</span> <span class='k'>void</span> <a class='r'>FolderCreateCallback</a>(<span class='k'>object</span> <a class='r'>folderId</a>)
		{
			<a class='r'>TransferOperation</a>.<a class='r'>crtOperation</a>.<a class='r'>FolderCallback</a>(<a class='r'>folderId</a> <span class='k'>as</span> <span class='k'>string</span>);
		}

		#<a class='r'>endregion</a>
	}
}
</pre>
						</div>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>


<script type="text/javascript">
var jumpLine = "20";
if(jumpLine && jumpLine != "-1" && !document.location.hash){
	if(document.attachEvent){
		document.attachEvent("onreadystatechange", function(){
			if(document.readyState != "complete") return;
			var numbers = document.all["kodeViewerLineNumbers"];
			var targetA = numbers.children.tags("a")[jumpLine - 1];
			targetA.scrollIntoView();
			document.body.scrollLeft = 0;
		});
	} else if(window.addEventListener){
		window.addEventListener("load", function(){
			document.location.replace(document.location + "#L" + jumpLine);
		}, false);
	}
}
	
var homeUrl = "/"; 
var projectID = "WPG8TNGSRTECXHU4TTSD5NSFBD";
var sourceLanguage = "C%23";
var IsLogged = false;

// only logs once
function LogReuse() {
	// dynamic image loading
	if(!IsLogged) {
		//alert("logging");
		var	fileImg	= new Image();
		var fileImgSrc = "/kv.aspx?fid=02F92772225FF8711957E789B2BAFEC8A9236DF0&s=BHO&mode=cp";
		//alert("fileImgSrc = " + fileImgSrc);
		fileImg.src	= fileImgSrc;
		IsLogged = true;
	} else {
		//alert("already logged!");
	}
	return true;
}
</script>


            
        </div>
        
        
<!-- Footer -->

<div style="CLEAR: both" align="center" id="FooterDiv">
	
        <div align="center" id="footer">
            <div class="row_1">
    		    <a href="http://www.koders.com/" title="Home">Home</a> |
                <a href="http://corp.koders.com/corp/about/" title="About Koders">Company</a> | 
                <a href="http://www.koders.com/info.aspx?c=tools" title="Download Plugins">Downloads</a> | 
                <a href="http://corp.koders.com/corp/support/" title="Support resources">Support</a> | 
                <a href="http://corp.koders.com/corp/community/" title="Community">Community</a> | 
                <a href="http://www.koders.com/corp/about/#Legal" title="Legal">Legal</a> | 
                <a href="http://corp.koders.com/corp/about#Contact" title="Contact us">Contact Us</a> |
   		        <a href="http://www.koders.com/corp/sitemap/" title="Koders Site Map">Site Map</a>    
	        </div>
            <div class="row_2">
                <span class="copy">
                    Copyright &copy; 2008 <a href="http://www.blackducksoftware.com/" target="_blank" title="Black Duck Software, Inc.">Black Duck Software</a>. 
                    All rights reserved.
                </span>            
                <span class="feed">
                    <a href="http://www.koders.com/blog/?feed=rss2" title="Subscribe!">RSS Feed</a>

                    <img width="12px" height="12px" hspace="0" vspace="0" src="/images/rss_sm.gif" alt="Koders Rss Feeds" />                    
                </span>
            </div>
            <br />
            <br />
            <a id="KodersFooter_ctl00_hplWorkingTime" title="0.00\0.02\0.00\0.00\0.00\0.00\0.00\0.02\0.05\0.00s"><font color="graytext">processed in: 0.08s</font></a>
        </div>

</div>

    

<script type="text/javascript">
//<![CDATA[
Sys.Application.initialize();
//]]>
</script>
</form>
    
    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
var pageTracker = _gat._getTracker("UA-1450480-3");
pageTracker._initData();
pageTracker._trackPageview();
</script>

</body>
</html>
