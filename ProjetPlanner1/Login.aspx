<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage2.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>


<asp:Content ID="Content2" ContentPlaceHolderID="content1" Runat="Server">
    <fieldset>
    <p class="form">Username:</p><asp:TextBox id="username" runat="server" Text="" cssclass="inp"/>
    <p class="form">Password:</p><asp:TextBox ID="password" runat="server" Text="" TextMode="Password" CssClass="inp" /></br>
    <asp:button ID="submit" runat="server" Text="Submit" OnClick ="Submit_button" CssClass="inp button" /> </br>
    <p><asp:Label ID="NotExists" runat="server" Text="" /></p>
        </fieldset>
</asp:Content>

