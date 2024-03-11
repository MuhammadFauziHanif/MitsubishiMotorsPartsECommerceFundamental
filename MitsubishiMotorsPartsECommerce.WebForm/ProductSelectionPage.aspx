<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductSelectionPage.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebForm.ProductSelectionPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Products</title>
    <script type="text/javascript">
        function toggleQuantityField(checkboxId, quantityId) {
            var checkbox = document.getElementById(checkboxId);
            var quantityField = document.getElementById(quantityId);

            if (checkbox.checked) {
                quantityField.disabled = false;
            } else {
                quantityField.disabled = true;
                quantityField.value = ''; // Clear the value when disabled
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Select Products and Quantities</h2>
            <asp:CheckBoxList ID="chkProducts" runat="server">
                <asp:ListItem Text="Product 1" Value="1" />
                <asp:ListItem Text="Product 2" Value="2" />
                <asp:ListItem Text="Product 3" Value="3" />
            </asp:CheckBoxList>

            <br />

            <asp:Label ID="lblQuantities" runat="server" Text="Quantities:"></asp:Label>
            <asp:TextBox ID="txtQuantities1" runat="server" ClientIDMode="Static" ></asp:TextBox>
            <asp:TextBox ID="txtQuantities2" runat="server" ClientIDMode="Static" ></asp:TextBox>
            <asp:TextBox ID="txtQuantities3" runat="server" ClientIDMode="Static" ></asp:TextBox>

            <br /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>