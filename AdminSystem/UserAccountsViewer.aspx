<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccountsViewer.aspx.cs" Inherits="UserAccountsViewer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>User Accounts Viewer</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">User Account Viewer</h2>
            <div class="form-group">
                <asp:Label ID="lblUserID" runat="server" Text="User ID:" CssClass="control-label"></asp:Label>
                <asp:Label ID="lblUserIDValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblUsername" runat="server" Text="Username:" CssClass="control-label"></asp:Label>
                <asp:Label ID="lblUsernameValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="control-label"></asp:Label>
                <asp:Label ID="lblEmailValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblRole" runat="server" Text="Role:" CssClass="control-label"></asp:Label>
                <asp:Label ID="lblRoleValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
