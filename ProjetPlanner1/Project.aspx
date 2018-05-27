<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Project.aspx.vb" Inherits="Project" %>
<%@ MasterType VirtualPath ="~/MasterPage.master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <fieldset>
    <h2 class=" form pink"><asp:label id="welcome" Runat="server" text="Welcome!"/></h2>
    <asp:dropdownlist ID="projectList"  runat ="server" onselectedindexchanged="projectChange" AutoPostBack="true" cssclass="inp"/><br/>
    </fieldset>
        <asp:gridview id="Grid" runat="server" onrowdatabound="Grid_RowDataBound" CssClass="Grid"></asp:gridview>
   
    </asp:Content>

