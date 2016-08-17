<%@ Page Title="" Language="C#" MasterPageFile="~/MovieBaseMasterPage.master" AutoEventWireup="true" CodeFile="MoviebaseAdminView.aspx.cs" Inherits="MoviebaseAdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="MainScriptManager" runat="server" />
    <asp:UpdatePanel ID="pnlUpdateDatabase" runat="server">
        <ContentTemplate>

            <h2>Admin Controls: </h2>
            <asp:DropDownList ID="ddlAdminControll" runat="server" OnSelectedIndexChange="ddlAdminControll_SelectedIndexChange" ClientIDMode="Static" AutoPostBack="True">
                <asp:ListItem Selected="True" Value="select">Select an Option</asp:ListItem>
                <asp:ListItem Value="aGenre">Add Genre</asp:ListItem>
                <asp:ListItem Value="aMovie">Add Movie</asp:ListItem>
                <asp:ListItem Value="aUser">Add User</asp:ListItem>
                <asp:ListItem Value="rGenre">Remove  Genre</asp:ListItem>
                <asp:ListItem Value="rMovie">Remove Movie</asp:ListItem>
                <asp:ListItem Value="rUser">Remove User</asp:ListItem>
            </asp:DropDownList>

            <!---------------------------------------------add genre control-->
            <Panel class="AdminControls" id="addGenre" runat="server">

                <h2>Add Genre</h2>

                <asp:Label ID="Label1" runat="server" Text="Genre: "></asp:Label>
                <asp:TextBox ID="txtGenreAdd" runat="server"></asp:TextBox>

                <asp:Button ID="btnSubmitNewGenre" runat="server" Text="Add" OnClientClick="BtnSubmitNewGenre_Click" AutoPostBack="True" />

            </Panel>
            <!---------------------------------------------add movie control-->
            <Panel class="AdminControls" id="addMovie" runat="server">

                <h2>Add Movie</h2>

                <asp:Label ID="Label2" runat="server" Text="Movie: "></asp:Label>
                <asp:TextBox ID="txtMovieAdd" runat="server"></asp:TextBox>

                <asp:Label ID="Label3" runat="server" Text="Genre: "></asp:Label>
                <asp:DropDownList ID="ddlGenre" runat="server" DataSourceID="sdsMoviebase" DataTextField="genre_name" DataValueField="genre_name"></asp:DropDownList>

                <asp:Button ID="btnSubmitNewMovie" runat="server" Text="Add" OnClientClick="btnSubmitNewMovie_Click" AutoPostBack="True" />

            </Panel>
            <!---------------------------------------------remove genre control-->
            <Panel class="AdminControls" id="remGenre" runat="server" >

                <h2>Remove Genre</h2> 

                <asp:Label ID="Label6" runat="server" Text="Genre: "></asp:Label>
                <asp:DropDownList ID="ddlRemoveGenre" runat="server"></asp:DropDownList>

                <asp:Button ID="btnRemoveGenre" runat="server" Text="Remove" OnClientClick="btnRemoveGenre_Click" AutoPostBack="True" />

            </panel>
            <!---------------------------------------------Remove Movie Controls-->
            <panel class="AdminControls" id="remMovie" runat="server">

                <h2>Remove Movie</h2>

                <asp:Label ID="Label4" runat="server" Text="Genre: "></asp:Label>
                <asp:DropDownList ID="ddlGenres" runat="server" OnSelectedIndexChanged="ddlGenres_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                <asp:Label ID="Label5" runat="server" Text="Movie: "></asp:Label>
                <asp:DropDownList ID="ddlMovies" runat="server" OnSelectedIndexChanged="ddlMovies_SelectedIndexChanged"></asp:DropDownList>

                <asp:Button ID="btnRemoveMovie" runat="server" Text="Remove" OnClientClick="btnRemoveMovie_Click" AutoPostBack="True" />

            </panel>

            <!---------------------------------------------Add Admin Users-->
            <panel class="AdminControls" id="addUser" runat="server">

                <h2>Add User Account</h2>

                <asp:Label ID="Label7" runat="server" Text="Username: "></asp:Label>
                <asp:TextBox ID="txtNewUser" runat="server"></asp:TextBox>

                <asp:Label ID="Label8" runat="server"  Text="Password: "></asp:Label>
                <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server"></asp:TextBox>

                <asp:Button ID="btnAddNewUser" runat="server" Text="Submit" OnClientClick="BtnAddUser_Click" AutoPostBack="True" />
            </panel>

          <!---------------------------------------------Remove Admin Users-->
            <panel class="AdminControls" id="removeUser" runat="server">
                <h2>Delete User Account</h2>
                <asp:DropDownList id = "ddlUsers" runat="server" AutoPostBack ="true"></asp:DropDownList>
                <asp:Button ID="btnRemoveUser" runat="server" Text="Submit" OnClientClick="BtnRemoveUser_Click" AutoPostBack="True" />
            </panel>

            <!---------------------------------------------admin grid views-->

            <h2>Database: </h2>

            <asp:GridView CssClass="gdvAdminGenre" ID="gdvUsers" runat="server" AutoGenerateColumns="False" DataSourceID="sdsUsers">
                <Columns>
                    <asp:BoundField DataField="username" HeaderText="Admin Users" SortExpression="username" />
                </Columns>
            </asp:GridView>


            <p></p>
            <asp:GridView CssClass="gdvAdminGenre" ID="gdvGenres" runat="server" AutoGenerateColumns="False" DataSourceID="sdsMoviebase">
                <Columns>
                    <asp:BoundField DataField="genre_name" HeaderText="Genre" SortExpression="genre_name" />
                </Columns>
            </asp:GridView>

            <p></p>

            <asp:GridView CssClass="gdvAdminMovie" ID="gdvMovies" runat="server" AutoGenerateColumns="False" DataSourceID="sdsMovieSource">
                <Columns>
                    <asp:BoundField DataField="movie_name" HeaderText="Movie" SortExpression="movie_name" />
                    <asp:BoundField DataField="genre_name" HeaderText="Genre" SortExpression="genre_name" />
                </Columns>
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <!---------------------------------------------Sql data sources-->
    <asp:SqlDataSource ID="sdsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:dbMoviebaseConnectionString %>" SelectCommand="SELECT [username] FROM [tblAdmins]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsMoviebase" runat="server" ConnectionString="<%$ ConnectionStrings:dbMoviebaseConnectionString %>" SelectCommand="SELECT [genre_name] FROM [tblGenres] ORDER BY [genre_name] ASC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsMovieSource" runat="server" ConnectionString="<%$ ConnectionStrings:dbMoviebaseConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT tblMovies.movie_name, tblGenres.genre_name
FROM tblMovies
INNER JOIN tblGenres
ON tblMovies.genre_ID = tblGenres.genre_ID
ORDER BY tblGenres.genre_name ASC;"></asp:SqlDataSource>

    <!---------------------------------------------hidden fields-->

</asp:Content>


