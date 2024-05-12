<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsDataEntry.aspx.cs" Inherits="TransactionRecordsDataEntry" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Entry</title>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <!-- Include jQuery from CDN -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

    <!-- Other scripts can follow here -->
</head>
<body>
    <form id="form1" runat="server">
        <!-- Add ScriptManager here -->
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
            <asp:DropDownList ID="ddlUsername" runat="server" DataTextField="Username" DataValueField="UserID" AutoPostBack="true"></asp:DropDownList>
            
            <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label>
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="Amount is required." Display="Dynamic" />
            
            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
            <asp:TextBox ID="txtTransactionDate" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="ceTransactionDate" runat="server" TargetControlID="txtTransactionDate" Format="yyyy-MM-dd" />
            
            <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:"></asp:Label>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server">
                <asp:ListItem Text="PayPal" Value="PayPal"></asp:ListItem>
                <asp:ListItem Text="Debit Card" Value="Debit Card"></asp:ListItem>
                <asp:ListItem Text="Credit Card" Value="Credit Card"></asp:ListItem>
            </asp:DropDownList>
            
            <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
            </asp:DropDownList>
            
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            
            <asp:GridView ID="gvTransactions" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
