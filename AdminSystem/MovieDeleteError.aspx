<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MovieDeleteError.aspx.cs" Inherits="MovieDeleteError" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Error</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Delete Error</h2>
            <p>You must delete related video files before deleting this movie.</p>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
