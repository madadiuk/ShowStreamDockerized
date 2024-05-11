<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsDataEntry.aspx.cs" Inherits="TransactionRecordsDataEntry" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Entry</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblUserId" runat="server" Text="User ID:"></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
            <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label>
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
            <asp:TextBox ID="txtTransactionDate" runat="server"></asp:TextBox>
            <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:"></asp:Label>
            <asp:TextBox ID="txtPaymentMethod" runat="server"></asp:TextBox>
            <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
            <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:GridView ID="gvTransactions" runat="server">
              
            </asp:GridView>
        </div>
    </form>
</body>
</html>
