<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
   Inherits="System.Web.UI.Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/kindeditor-4.1.7/kindeditor-min.js" type="text/javascript"></script>
    <script src="Scripts/kindeditor-4.1.7/plugins/code/code.js" type="text/javascript"></script>
    <script src="Scripts/kindeditor-4.1.7/plugins/code/prettify.js" type="text/javascript"></script>
    <link href="Scripts/kindeditor-4.1.7/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            var id = $.getUrlParam('id');
            $.net.ArticleBLL.UpdateVisitTotal(id, function (data) { });
            $.net.ArticleBLL.DetailById(id, function (data) {
                $("#topics").setTemplateElement("template", null, { filter_data: false });
                $("#topics").processTemplate(data);
                prettyPrint();
                var d = $('#post-date').html();               
                var now = new Date(parseInt(d.substr(6)));
                $('#post-date').html(now.Format("yyyy-MM-dd hh:mm"));
            });           
        });
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <div id="topics">
    </div>
    <textarea id="template" style="display: none"> 
       <div class="post">
            <h1 class="postTitle">
                <a id="cb_post_title_url" class="postTitle2" href="#">
                    {$T.Subject} </a>
            </h1>
            <div class="clear">
            </div>
            <div class="postBody">
                <div id="cnblogs_post_body">
                   {$T.Content}                  
                </div>               
                <div class="clear">
                </div>
            </div>
            <div class="postDesc">
                posted @ <span id="post-date">{$T.PublishDate}</span> <a href='Default.aspx'>
                    gdjlc</a> 阅读({$T.VisitTotal}) 
                </div>
        </div>
    </textarea>
</asp:Content>
