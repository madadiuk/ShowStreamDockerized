<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccountsList.aspx.cs" Inherits="UserAccountsList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Accounts List</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
</head>
<body class="useraccountslist">
    <header>
        <div class="container">
            <div id="branding">
                <h1>User Accounts List</h1>
            </div>
            <nav>
                <ul>
                    <li><a href="UserAccountsList.aspx" class="btn">User Accounts List</a></li>
                    <li><a href="UserManagement.aspx" class="btn">User Management</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <div class="container">
        <div class="main-content">
            <form id="form1" runat="server">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                    OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit"
                    OnRowDeleting="gvUsers_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="User ID" />
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblMessage" runat="server" CssClass="message-label"></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
