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
            $.net.ArticleBLL.DetailById(id, function (data) {   
                $("#topics").setTemplateElement("template", null, { filter_data: false });
                $("#topics").processTemplate(data);
                prettyPrint();
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
                <a id="cb_post_title_url" class="postTitle2" href="http://www.cnblogs.com/gdjlc/p/3313713.html">
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
                    gdjlc</a> 阅读(<span id="post_view_count">...</span>) 
                <a href="http://www.cnblogs.com/gdjlc/admin/EditPosts.aspx?postid=3313713" rel="nofollow">
                    编辑</a></div>
        </div>
    </textarea>
</asp:Content>
