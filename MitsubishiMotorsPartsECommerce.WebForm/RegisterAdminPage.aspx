<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RegisterAdminPage.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebForm.RegisterAdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Registration</h1>
        </div>

        <div class="col-lg-12">
            <!-- Basic Card Example -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Registration Page</h6>
                </div>
                <div class="card-body">
                    <asp:Literal ID="ltMessage" runat="server" />
                    <div class="form-group">
                        <label for="txtUsername">Username :</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtPassword">Password :</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email :</label>
                        <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" />
                    </div>
                    <%-- Dropdown to choose role as an admin or superadmin --%>
                    <div class="form-group">
                        <label for="ddlRole">Role :</label>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Admin" Value="admin" />
                            <asp:ListItem Text="Super Admin" Value="superadmin" />
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="btnRegistration" Text="Register" CssClass="btn btn-primary my-3" runat="server" OnClick="btnRegistration_Click" />
                </div>
            </div>

        </div>

    </div>
</asp:Content>
