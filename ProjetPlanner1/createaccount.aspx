<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage2.master" AutoEventWireup="false" CodeFile="createaccount.aspx.vb" Inherits="createaccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Menu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content1" Runat="Server">
    <fieldset>
    <div><p class="form">Create a Username</p> <asp:textbox ID="create_username" runat="server" Text="" cssclass="inp" /></div>
    <p class="form">Create a Password</p><asp:textbox ID="create_password" runat="server" text="" TextMode="Password" cssclass="inp" /> <br>
    <asp:Button ID="submit_new" runat="server" Text="submit" OnClick ="submit_new_details" cssclass="inp button" /> <br />
    <asp:label id="notAvailable" runat="server" text="" />
        </fieldset>
</asp:Content>

