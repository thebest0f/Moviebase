<%@ Page Title="" Language="C#" MasterPageFile="~/MovieBaseMasterPage.master" AutoEventWireup="true" CodeFile="FullMovieList.aspx.cs" Inherits="FullMovieList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div id = "fullMovieList">
            <h1 runat="server" id = "genreLabel"></h1>
                <asp:Panel ID="FullMoviePanel" runat="server"></asp:Panel>
        </div>
        

</asp:Content>

