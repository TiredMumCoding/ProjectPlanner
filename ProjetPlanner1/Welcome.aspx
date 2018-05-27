<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage2.master" AutoEventWireup="false" CodeFile="Welcome.aspx.vb" Inherits="Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Menu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content1" Runat="Server">

    <fieldset>
    <div><p class="form">To create an account click here:</p> <asp:button id="createAccount" text="Create Account" runat ="server" onclick="Create_button" cssclass="inp button"/></div>
    <br />
    <div><p class="form">To login click here:</p> <asp:button id="login" text="Log In" runat="server" onclick="Login_button" cssclass="inp button" /></div>
    </fieldset>

</asp:Content>

