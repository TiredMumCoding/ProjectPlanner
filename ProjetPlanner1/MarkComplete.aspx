<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="MarkComplete.aspx.vb" Inherits="MarkComplete" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    
    <asp:GridView ID="Grid" runat="server" cssclass="Grid" />
        <fieldset>
    <p class ="form">Click to this task complete</p>
    <asp:Button ID="complete" runat="server" Text="Mark Complete" OnClick="Make_Complete" CssClass="inp button" /> <br/>
    <p class="form">Click to delete this task</p>
    <asp:Button ID="delete" runat="server" Text="Delete Task" OnClick ="Delete_Task" CssClass="inp button"/><br />
    <asp:Label ID="UpdateSuccessful" runat="server" Text="" />
    <asp:button ID="ReturnProj" runat="server" Text="Return to Project" OnClick="Return_Proj" CssClass="inp button" />
            </fieldset>
</asp:Content>

