<%@ Page Title="" Language="C#" MasterPageFile="~/MovieBaseMasterPage.master" AutoEventWireup="true" CodeFile="SingleMovie.aspx.cs" Inherits="SingleMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 runat="server" id= "h1MovieTitle"></h1>
    <h2 runat="server" id = "h2MovieYear"></h2>
    <h2 runat="server" id= "h2MovieDirector"></h2>
    <h4 runat="server" id = "h3MovieDescription"></h4>
</asp:Content>

