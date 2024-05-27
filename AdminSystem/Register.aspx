<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="YourNamespace.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Register</h2>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />
            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
            <asp:DropDownList ID="ddlRole" runat="server">
                <asp:ListItem Text="User" Value="User"></asp:ListItem>
                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
            </asp:DropDownList><br />
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
        </div>
    </form>
</body>
</html>
