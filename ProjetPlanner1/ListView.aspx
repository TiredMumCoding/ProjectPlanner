<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListView.aspx.vb" Inherits="ListView" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <asp:Repeater ID="ParentRepeater" runat="server">
        <itemtemplate>
           <p class="pink"> <%# DataBinder.Eval(Container.DataItem, "DateWord") %></br>
            <asp:Repeater ID="ChildRepeater" runat="Server"
                datasource ='<%# Container.DataItem.Row.GetChildRows("myrelation")%>'> 
                <ItemTemplate>
                     <%# Container.DataItem("task") %> </br>
                </ItemTemplate>
                </asp:Repeater>
        </p></itemtemplate>
        </asp:Repeater>
    <asp:button ID="ReturnProj" runat="server" Text="Return to Project" OnClick="Return_Proj" CssClass="inp button" />
</asp:Content>

 