<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebFormApp._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Welome to Mitsubishi Motors Genuine Parts E-Commerce</h1>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LblHello" runat="server" Text="Hello ASP.NET"></asp:Label>
            <asp:TextBox ID="txtHello" runat="server" />
            <br />
            <asp:Label ID="LblResult" runat="server" Text="Result:"></asp:Label><br />
            <asp:Button ID="BtnHello" runat="server" Text="Say Hello! 👋" />
        </div>
    </form>
</body>
</html>
