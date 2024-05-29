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
    <!-- Link to the external CSS file -->
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="search-container">
            <label class="search-label" for="usernameInput">Username:</label>
            <button type="button" id="activateSearch" class="search-button">Find the User</button>
            <input type="hidden" id="usernameInput" name="usernameInput" class="search-input" />
            <div id="userNotification" class="user-notification" style="display:none;"></div>
            <asp:CustomValidator ID="cvUsername" runat="server" ErrorMessage="Username is required." Display="Dynamic" CssClass="error-message" OnServerValidate="ValidateUsername"></asp:CustomValidator>
        </div>

        <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label>
        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="Amount is required." Display="Dynamic" CssClass="error-message" />

        <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
        <asp:TextBox ID="txtTransactionDate" runat="server"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="ceTransactionDate" runat="server" TargetControlID="txtTransactionDate" Format="yyyy-MM-dd" />
        <asp:CustomValidator ID="cvTransactionDate" runat="server" ErrorMessage="Transaction Date cannot be empty or in the future." Display="Dynamic" CssClass="error-message" OnServerValidate="ValidateTransactionDate"></asp:CustomValidator>

        <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:"></asp:Label>
        <asp:DropDownList ID="ddlPaymentMethod" runat="server"></asp:DropDownList>

        <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
        <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnViewList" runat="server" Text="View Transaction List" OnClick="btnViewList_Click" CausesValidation="false" /><br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvTransactions_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="£{0:N2}" HtmlEncode="False" />
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:d}" />
                <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </form>
    <script>
        $(document).ready(function () {
            var userSearch = $('#usernameInput').select2({
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
                placeholder: 'Type to search and select a user', // Updated placeholder text
                minimumInputLength: 1,
                allowClear: true
            });

            $('#activateSearch').on('click', function () {
                userSearch.select2('open');
            });

            userSearch.on("select2:select", function (e) {
                var data = e.params.data;
                $('#userNotification').show().text('You have chosen: ' + data.text); // Shows selected user
            }).on("select2:unselect", function (e) {
                $('#userNotification').hide(); // Hides notification when user clears selection
            });
        });



    </script>


</body>
</html>
