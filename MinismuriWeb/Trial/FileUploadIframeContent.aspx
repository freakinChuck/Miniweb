<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadIframeContent.aspx.cs" Inherits="MinismuriWeb.Trial.FileUploadIframeContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="/_Global/Master.css" rel="stylesheet" type="text/css" />
    <link href="/_Content/Content.css" rel="stylesheet" type="text/css" />

    <!-- Aus Web, wegen Button Bilder -->
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/themes/base/jquery-ui.css">
    <link href="/_Content/FileUpload/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        body
        {
            background:white !important;
        }
    </style>

</head>
<body>
    
    <div id="fileupload">
        <form action="/FileuploadHandler.ashx?Ziel=<%= Request["Ziel"] %>" method="post" enctype="multipart/form-data">
            <div class="fileupload-buttonbar">
                <label class="fileinput-button">
                    <span>Add files...</span>
                    <input id="file" type="file" name="files[]" multiple="multiple" />
                </label>
                <button type="submit" class="start">Upload starten</button>
                <button type="reset" class="cancel">Upload abbrechen</button>
                <button type="button" class="delete">Files löschen</button>
            </div>
        </form>
        <div class="fileupload-content" style="height:450px; overflow:auto">
            <table class="files"></table>
            <div class="fileupload-progressbar"></div>
        </div>
    </div>

<script id="template-upload" type="text/x-jquery-tmpl">
    <tr class="template-upload{{if error}} ui-state-error{{/if}}">
        <td class="preview"></td>
        <td class="name">${name}</td>
        <td class="size">${sizef}</td>
        {{if error}}
            <td class="error" colspan="2">Error:
                {{if error === 'maxFileSize'}}File is too big
                {{else error === 'minFileSize'}}File is too small
                {{else error === 'acceptFileTypes'}}Filetype not allowed
                {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                {{else}}${error}
                {{/if}}
            </td>
        {{else}}
            <td class="progress"><div></div></td>
            <td class="start"><button>Start</button></td>
        {{/if}}
        <td class="cancel"><button>Cancel</button></td>
    </tr>
</script>
<script id="template-download" type="text/x-jquery-tmpl">
    <tr class="template-download{{if error}} ui-state-error{{/if}}">
        {{if error}}
            <td></td>
            <td class="name">${namefdsa}</td>
            <td class="size">${sizef}</td>
            <td class="error" colspan="2">Error:
                {{if error === 1}}File exceeds upload_max_filesize (php.ini directive)
                {{else error === 2}}File exceeds MAX_FILE_SIZE (HTML form directive)
                {{else error === 3}}File was only partially uploaded
                {{else error === 4}}No File was uploaded
                {{else error === 5}}Missing a temporary folder
                {{else error === 6}}Failed to write file to disk
                {{else error === 7}}File upload stopped by extension
                {{else error === 'maxFileSize'}}File is too big
                {{else error === 'minFileSize'}}File is too small
                {{else error === 'acceptFileTypes'}}Filetype not allowed
                {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                {{else error === 'uploadedBytes'}}Uploaded bytes exceed file size
                {{else error === 'emptyResult'}}Empty file upload result
                {{else}}${error}
                {{/if}}
            </td>
        {{else}}
            <td class="preview">
                {{if Thumbnail_url}}
                    <a href="${url}" target="_blank"><img src="${Thumbnail_url}"></a>
                {{/if}}
            </td>
            <td class="name">
                <a href="${url}"{{if thumbnail_url}} target="_blank"{{/if}}>${Name}</a>
            </td>
            <td class="size">${Length}</td>
            <td colspan="2">File erfolgreich hochgeladen</td>
        {{/if}}
        <td class="delete">
            <button data-type="${delete_type}" data-url="${delete_url}">Delete</button>
        </td>
    </tr>
</script>

<script src="//ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/jquery-ui.min.js"></script>
<script src="//ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>

    <%--<script src="/_Content/Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="/_Content/Scripts/modernizr-2.0.6.min.js" type="text/javascript"></script>
    <script src="/_Content/FileUpload/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/_Content/FileUpload/jquery.tmpl.min.js" type="text/javascript"></script>--%>
    
    <script src="/_Content/FileUpload/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="/_Content/FileUpload/jquery.fileupload.js" type="text/javascript"></script>
    <script src="/_Content/FileUpload/jquery.fileupload-ui.js" type="text/javascript"></script>
    <script src="/_Content/FileUpload/FileUpload.js" type="text/javascript"></script>

</body>
</html>
