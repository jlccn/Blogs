<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
     Inherits="System.Web.UI.Page" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery.pager.js" type="text/javascript"></script>
    <link href="Styles/Pager.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var pageIndex = 1; //页面索引初始值 
    var pageSize = 3; //每页显示条数初始化
    var pagecount;
    var jsondata;
    $(function () {
        $.net.ArticleBLL.GetPageData(pageIndex, pageSize, function (data) {
            pagecount = Math.ceil(data.total / pageSize);  //向上取整，有小数，则整数部分加1
            $("#pager").pager({ pagenumber: 1, pagecount: pagecount, buttonClickCallback: PageClick });
            Go(1);           
        });
    });
    PageClick = function (pageclickednumber) {
        $("#pager").pager({ pagenumber: pageclickednumber, pagecount: pagecount, buttonClickCallback: PageClick });
        Go(pageclickednumber);         
    }
    function Go(index) {
        $.net.ArticleBLL.GetPageData(index, pageSize, function (data) {
            $("#TmpContent").setTemplateElement("template", null, { filter_data: false });
            $("#TmpContent").processTemplate(data);
            $('.jsondate').each(function () {
                var d = $(this).html();
                var now = new Date(parseInt(d.substr(6)));
                $(this).html(now.Format("yyyy年MM月dd日"));
            });
            $('.jsondate2').each(function () {
                var d = $(this).html();
                var now = new Date(parseInt(d.substr(6)));
                $(this).html(now.Format("yyyy-MM-dd hh:mm"));
            });
        });
    }
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContentHolder">
    <br />
    <div id="TmpContent">  
    </div> 
    <textarea id="template" style="display:none"> 
    {#foreach $T.rows as record}       
    <div class="day">
        {#if $T.record$first == false}
        <div class="dayTitle">
        </div>
        {#/if}
        <div class="postTitle">
            <a class="postTitle2"
                href="Detail.aspx?id={$T.record.Id}"> {$T.record.Subject}</a>
        </div>
        <div class="postCon">
            <div class="c_b_p_desc">
                {$T.record.Content}<a href="Detail.aspx?id={$T.record.Id}"
                    class="c_b_p_desc_readmore">阅读全文</a></div>
        </div>
        <div class="clear">
        </div>
        <div class="postDesc">
            posted @ <span class="jsondate2">{$T.record.PublishDate}</span> gdjlc 阅读({$T.record.VisitTotal})</div>
      
    </div>
    {#/for}   
    </textarea>  
    <div class="topicListFooter"> 
        <div id="pager" ></div>      
        <%--<div id="Pagination" style="background:#F5F5F5;border:1px solid #ccc;"></div> --%> 
    </div>    
</asp:Content>
