<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsEdit.aspx.cs" Inherits="TransactionRecordsEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Transaction</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Label ID="lblTransactionID" runat="server" Text="Transaction ID:"></asp:Label><br />
        <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label><br />
        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtTransactionDate" runat="server"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="ceTransactionDate" runat="server" TargetControlID="txtTransactionDate" Format="yyyy-MM-dd" /><br />
        <asp:DropDownList ID="ddlPaymentMethod" runat="server"></asp:DropDownList><br />
        <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList><br />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnReturn" runat="server" Text="Return to List" OnClick="btnReturn_Click" /><br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
