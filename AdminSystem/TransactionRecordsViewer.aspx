<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsViewer.aspx.cs" Inherits="TransactionRecordsViewer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Details</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblTransactionID" runat="server" Text="Transaction ID:"></asp:Label><br />
        <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label><br />
        <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label><br />
        <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label><br />
        <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:"></asp:Label><br />
        <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label><br />
        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
    </form>
</body>
</html>
