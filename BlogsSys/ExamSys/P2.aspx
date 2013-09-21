<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="P2.aspx.cs" Inherits="ExamSys.Index" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
    <style type="text/css">
/*网易风格*/
.anpager .cpb {background:#1F3A87 none repeat scroll 0 0;border:1px solid #CCCCCC;color:#FFFFFF;font-weight:bold;margin:5px 4px 0 0;padding:4px 5px 0;}
.anpager a {background:#FFFFFF none repeat scroll 0 0;border:1px solid #CCCCCC;color:#1F3A87;margin:5px 4px 0 0;padding:4px 5px 0;text-decoration:none}
.anpager a:hover{background:#1F3A87 none repeat scroll 0 0;border:1px solid #1F3A87;color:#FFFFFF;}
#mytable {  
    padding: 0;
    margin: 0;  
    border-collapse:collapse;
}
td {
    border: 1px solid #C1DAD7;  
    background: #fff;
    font-size:15px;
    padding: 6px 6px 6px 12px;
    color: #4f6b72;
}
</style>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater runat="server" ID="rptList">
    <HeaderTemplate><table id="mytable"><tr><td>序号</td><td>主题</td><td>内容</td><td>日期</td></tr></HeaderTemplate>
    <ItemTemplate>
     <tr>
      <td><%#Eval("Id") %></td>
      <td><%#Eval("Subject") %></td>
      <td><%#Eval("Content") %></td>
      <td><%#Eval("PublishDate") %></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
    <br />
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="anpager" 
        CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页" 
        NextPageText="下一页" PrevPageText="上一页" onpagechanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
   <br /> <br />





    </div>
    </form>
</body>
</html>
