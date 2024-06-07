<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccountsViewer.aspx.cs" Inherits="UserAccountsViewer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Accounts Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>User Details</h2>
            <p>User ID: <asp:Label ID="lblUserID" runat="server" Text="N/A"></asp:Label></p>
            <p>Username: <asp:Label ID="lblUsername" runat="server" Text="N/A"></asp:Label></p>
            <p>Email: <asp:Label ID="lblEmail" runat="server" Text="N/A"></asp:Label></p>
            <p>Role: <asp:Label ID="lblRole" runat="server" Text="N/A"></asp:Label></p>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
