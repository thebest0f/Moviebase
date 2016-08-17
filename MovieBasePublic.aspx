<%@ Page Title="" Language="C#" MasterPageFile="~/MovieBaseMasterPage.master" AutoEventWireup="true" CodeFile="MovieBasePublic.aspx.cs" Inherits="MovieBasePublic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:Panel ID="MoviePanels"  runat="server">

        </asp:Panel>
    </div>  

    <asp:HiddenField ID="hdnfldSelectedMovie" runat="server" />
</asp:Content>

