<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentDashboard.aspx.cs" Inherits="ContentDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>This is a test page for content dashboard.</p>
            <asp:Button ID="btnTestConnection" runat="server" Text="Test Database Connection" OnClick="btnTestConnection_Click" />
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </form>
</body>
</html>
