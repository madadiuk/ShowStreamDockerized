<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamMainMenu.aspx.cs" Inherits="TeamMainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Team Main Menu</title>
    <link href="styles/stylesMenu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p> This is the team main menu</p>  
            <asp:Button ID="btnGoToTransactions" runat="server" Text="Go to Transaction Records List" OnClick="btnGoToTransactions_Click" />
            <asp:Button ID="btnGoToEpisodesManagement" runat="server" Text="Go to Episodes Management" OnClick="btnGoToEpisodesManagement_Click" /><br /><br />
            <asp:Button ID="btnGoToGenresManagement" runat="server" Text="Go to Genres Management" OnClick="btnGoToGenresManagement_Click" /><br /><br />
            <asp:Button ID="btnGoToUserManagement" runat="server" Text="Go to User Management" OnClick="btnGoToUserManagement_Click" />
        </div>
    </form>
</body>
</html>
