<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Aktuelles.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <h1>Termine</h1>
    <hr />


    <asp:Repeater ID="terminRepeater" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="background-image:url('/Images/Kalender.png'); width:85px; height:73px;text-align:center">
                    <b style="color:White; font-size:8pt; padding-top:10px; display:block;"><%# Eval("Monat") %></b>
                    <b style="color:Black; font-size:15pt; padding-top:5px; display:block"><%# Eval("Tag") %></b>
                </td>
                <td style="vertical-align:middle">
                    <p style="margin-left:20px; font-weight:bold"><%# Eval("Title") %></p>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>


</asp:Content>
