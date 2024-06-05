<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsFilter.aspx.cs" Inherits="TransactionRecordsFilter" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Filter</title>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <!-- Include jQuery from CDN -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/js/select2.min.js"></script>
    <!-- Link to the external CSS file -->
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <h2>Filter Transactions</h2>
        
        <div>
            <label for="ddlPaymentMethodFilter">Payment Method:</label>
            <asp:DropDownList ID="ddlPaymentMethodFilter" runat="server">
                <asp:ListItem Text="All" Value="" />
                <asp:ListItem Text="PayPal" Value="PayPal" />
                <asp:ListItem Text="Debit Card" Value="Debit Card" />
                <asp:ListItem Text="Credit Card" Value="Credit Card" />
            </asp:DropDownList>
        </div>
        
        <div>
            <label for="ddlStatusFilter">Status:</label>
            <asp:DropDownList ID="ddlStatusFilter" runat="server">
                <asp:ListItem Text="All" Value="" />
                <asp:ListItem Text="Completed" Value="Completed" />
                <asp:ListItem Text="Pending" Value="Pending" />
                <asp:ListItem Text="Failed" Value="Failed" />
            </asp:DropDownList>
        </div>
        
        <div>
            <label for="txtDateFrom">Date From:</label>
            <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="ceDateFrom" runat="server" TargetControlID="txtDateFrom" Format="yyyy-MM-dd" />
        </div>
        
        <div>
            <label for="txtDateTo">Date To:</label>
            <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="ceDateTo" runat="server" TargetControlID="txtDateTo" Format="yyyy-MM-dd" />
        </div>
        
        <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" /><br /><br />
        <asp:Button ID="btnReturnToList" runat="server" Text="Return to Transaction List" OnClick="btnReturnToList_Click" /><br /><br />

        <asp:GridView ID="gvFilteredTransactions" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvFilteredTransactions_PageIndexChanging" OnRowCommand="gvFilteredTransactions_RowCommand" OnRowDeleting="gvFilteredTransactions_RowDeleting">
            <Columns>
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" ReadOnly="True" />
                <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="True" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="£{0:N2}" HtmlEncode="False" />
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:d}" />
                <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("TransactionID") %>'></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("TransactionID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
