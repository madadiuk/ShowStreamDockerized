<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="UserManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <style>
        .form-control {
            margin-bottom: 10px;
        }

        .btn {
            margin-right: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>User Management</h2>
            <div class="form-group form-control">
                <asp:Label ID="lblUserID" runat="server" Text="User ID:" AssociatedControlID="hfUserID" />
                <asp:HiddenField ID="hfUserID" runat="server" />
            </div>
            <div class="form-group form-control">
                <asp:Label ID="lblUsername" runat="server" Text="Username:" AssociatedControlID="txtUsername" />
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group form-control">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group form-control">
                <asp:Label ID="lblPassword" runat="server" Text="Password:" AssociatedControlID="txtPassword" />
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="form-group form-control">
                <asp:Label ID="lblRole" runat="server" Text="Role:" AssociatedControlID="ddlRole" />
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Admin" Value="Admin" />
                    <asp:ListItem Text="User" Value="User" />
                </asp:DropDownList>
            </div>
            <div class="form-group form-control">
                <asp:Button ID="btnAddUser" runat="server" Text="Add User" CssClass="btn btn-primary" OnClick="btnAddUser_Click" />
                <asp:Button ID="btnEditUser" runat="server" Text="Edit User" CssClass="btn btn-warning" OnClick="btnEditUser_Click" />
            </div>
            <div class="form-group">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowEditing="gvUsers_RowEditing" OnRowDeleting="gvUsers_RowDeleting" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" />
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
