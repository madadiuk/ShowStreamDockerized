<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="UserManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
</head>
<body class="usermanagement">
    <header>
        <div class="container">
            <div id="branding">
                <h1>User Management</h1>
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
                <div class="form-group">
                    <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                    <asp:DropDownList ID="ddlRole" runat="server">
                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
                    <asp:Button ID="btnEditUser" runat="server" Text="Edit User" OnClick="btnEditUser_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="message-label"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblUserID" runat="server" Text="User ID:"></asp:Label>
                    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                        OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit"
                        OnRowDeleting="gvUsers_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" />
                            <asp:BoundField DataField="Username" HeaderText="Username" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:TemplateField HeaderText="Role">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlRoleEdit" runat="server">
                                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
