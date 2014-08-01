<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Blog.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  

    <asp:ScriptManager runat="server" />
    
    <h1>News</h1>
    <hr />
    
    <asp:Repeater id="eintragRepeater" runat="server">
        <ItemTemplate>
            
            <div style="border-bottom:1px solid #aaa">

                <p style="font-weight:bold; text-decoration:underline; font-size:110%"><%# Eval("Titel") %></p>

                <%# Eval("Content") %>

                <div id="bildDiv" style="text-align:left" runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("ImagePath")) %>'>
                    <img src="<%# "/UploadData/Blogbilder/" + Eval("ImagePath") %>" alt='<%# Eval("Titel") %>' style="max-width:500px; max-height:400px;" />
                </div>

                <div style="text-align:right; font-size:80%">

                    Erstellt am <%# ((DateTime)Eval("Datum")).ToString("dd.MM.yyyy HH:mm") %> 

                    <span style="clear:both;"></span>
                </div>

            </div>

        </ItemTemplate>
    </asp:Repeater>



</asp:Content>
