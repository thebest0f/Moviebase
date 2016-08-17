<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="login">

        <form runat="server">
            <fieldset>
                <legend>Please Login: </legend>
                <p runat="server">
                    <asp:TextBox ID="txtUsername" placeholder="Username" runat="server"></asp:TextBox>
                </p>
                <p runat="server">
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Password" ></asp:TextBox>
                </p>
                <p runat="server" id ="errorMessage">                  
                </p>
                <p runat="server">
                    <asp:Button ID="btnSubmitLoginCheck" runat="server" Text="Submit" OnClientClick="btnSubmitLoginCheck_Click" />
                </p>
            </fieldset>
        </form>

    </div>
</body>
</html>
