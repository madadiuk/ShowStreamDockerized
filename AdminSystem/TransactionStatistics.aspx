<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionStatistics.aspx.cs" Inherits="TransactionStatistics" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Statistics</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h2>Transaction Statistics</h2>
        
        <asp:Label ID="lblTotalTransactions" runat="server" Text="Total Transactions:"></asp:Label><br />
        <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount:"></asp:Label><br />
        <asp:Label ID="lblAverageAmount" runat="server" Text="Average Amount:"></asp:Label><br />
        <asp:Label ID="lblUniqueUsers" runat="server" Text="Unique Users:"></asp:Label><br /><br />

        <asp:Button ID="btnReturnToList" runat="server" Text="Return to Transaction List" OnClick="btnReturnToList_Click" /><br /><br />
    </form>
</body>
</html>
