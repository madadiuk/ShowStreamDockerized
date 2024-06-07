<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteError.aspx.cs" Inherits="DeleteError" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Error</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Delete Error</h2>
            <p>You must delete related movies before deleting this genre.</p>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
