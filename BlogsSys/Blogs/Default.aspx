<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
     Inherits="System.Web.UI.Page" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<script type="text/javascript">
    var pageIndex = 1; //页面索引初始值 
    var pageSize = 10; //每页显示条数初始化
    $(function () {
        initTable(1, pageSize);
        $('#Pagination').pagination({
            pageSize: 10,
            pageNumber: 1,
            pageList: [5, 10, 15, 20],
            onSelectPage: function (pageNumber, pageSize) {
                $(this).pagination('loading');
                $(this).pagination('loaded');
                initTable(pageNumber, pageSize); 
            }
        });
        function initTable(pageIndex, pageSize) {
            $.net.ArticleBLL.GetPageData(pageIndex, pageSize, function (data) {
                $("#TmpContent").setTemplateElement("template", null, { filter_data: false });
                $("#TmpContent").processTemplate(data);
                $('#Pagination').pagination({
                    total: data.total
                });
                $('.jsondate').each(function () {
                    var d = $(this).html().trim();
                    var now = new Date(parseInt(d.substr(6)));                  
                    $(this).html(now.Format("yyyy年MM月dd日"));
                });
                $('.jsondate2').each(function () {
                    var d = $(this).html().trim();
                    var now = new Date(parseInt(d.substr(6)));
                    $(this).html(now.Format("yyyy-MM-dd hh:mm"));
                });
            });
        }      
        
    });   
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContentHolder">

    <div id="TmpContent">  
    </div> 
    <textarea id="template" style="display:none"> 
    {#foreach $T.rows as record}  
    <div class="day">
        <div class="dayTitle">
            <a href="#" class="jsondate">
                {$T.record.PublishDate}  </a>
        </div>
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
            posted @ <span class="jsondate2">{$T.record.PublishDate}</span> gdjlc 阅读(19) <a href="#" rel="nofollow">编辑</a></div>
        <div class="clear">
        </div>
    </div>
    {#/for}   
    </textarea>  
    <div class="topicListFooter">       
        <div id="Pagination" style="background:#F5F5F5;border:1px solid #ccc;"></div>  
    </div>    
</asp:Content>
