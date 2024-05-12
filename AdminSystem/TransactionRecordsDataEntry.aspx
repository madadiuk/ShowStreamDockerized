<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionRecordsDataEntry.aspx.cs" Inherits="TransactionRecordsDataEntry" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Entry</title>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <!-- Include jQuery from CDN -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/js/select2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Add ScriptManager here -->
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
            <input type="hidden" id="usernameInput" name="usernameInput" style="width: 100%;" />

        
            <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label>
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="Amount is required." Display="Dynamic" />
            
            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
            <asp:TextBox ID="txtTransactionDate" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="ceTransactionDate" runat="server" TargetControlID="txtTransactionDate" Format="yyyy-MM-dd" />
            
            <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:"></asp:Label>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server">

            </asp:DropDownList>
            
            <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server">
                
            </asp:DropDownList>
            
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            
            <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="£{0:N2}" HtmlEncode="False" />
                    <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            $('#usernameInput').select2({
                ajax: {
                    url: 'UserSearch.ashx',
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            term: params.term // search term from the input
                        };
                    },
                    processResults: function (data) {
                        return {
                            results: data.map(user => ({
                                id: user.id,
                                text: user.text
                            }))
                        };
                    },
                    cache: true
                },
                placeholder: 'Search for a user',
                minimumInputLength: 1,
                allowClear: true
            });
        });
    </script>


</body>
</html>
