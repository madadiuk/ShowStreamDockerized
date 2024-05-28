<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="UserManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>

            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

            <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
            <asp:DropDownList ID="ddlRole" runat="server">
                <asp:ListItem Value="Admin">Admin</asp:ListItem>
                <asp:ListItem Value="User">User</asp:ListItem>
                <asp:ListItem Value="Guest">Guest</asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
            <asp:Button ID="btnEditUser" runat="server" Text="Edit User" OnClick="btnEditUser_Click" />

            <asp:HiddenField ID="hfUserID" runat="server" />

            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" OnRowEditing="gvUsers_RowEditing" OnRowDeleting="gvUsers_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="User ID" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Role" HeaderText="Role" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
