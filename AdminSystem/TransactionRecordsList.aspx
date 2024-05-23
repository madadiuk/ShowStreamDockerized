<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsList.aspx.cs" Inherits="TransactionRecordsList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction List</title>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/js/select2.min.js"></script>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                      OnPageIndexChanging="gvTransactions_PageIndexChanging"
                      OnRowEditing="gvTransactions_RowEditing"
                      OnRowUpdating="gvTransactions_RowUpdating"
                      OnRowCancelingEdit="gvTransactions_RowCancelingEdit"
                      OnRowDeleting="gvTransactions_RowDeleting"
                      OnRowDataBound="gvTransactions_RowDataBound"
                      DataKeyNames="TransactionID">
            <Columns>
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" ReadOnly="True" />
                <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="True" />
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount", "£{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date">
                    <ItemTemplate>
                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate", "{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTransactionDate" runat="server" Text='<%# Bind("TransactionDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="ceTransactionDate" runat="server" TargetControlID="txtTransactionDate" Format="yyyy-MM-dd" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Method">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Eval("PaymentMethod") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server" SelectedValue='<%# Bind("PaymentMethod") %>'></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# Bind("Status") %>'></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
