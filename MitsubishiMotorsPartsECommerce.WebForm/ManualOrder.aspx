<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManualOrder.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebForm.ManualOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Products</h1>
        </div>

        <div class="col-lg-12">
            <!-- Basic Card Example -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Product Page</h6>
                </div>
                <div class="card-body">

                    <asp:Literal ID="ltMessage" runat="server" />
                    <div class="row">
                        <div class="col-lg-3">
                            <h4>Order Detail</h4>
                            <label for="ddCustomers">Customer: </label>
                            <asp:DropDownList ID="ddCustomers" runat="server"
                                AppendDataBoundItems="true"
                                CssClass="form-control">
                                <asp:ListItem Selected="True">-- Select Customer --</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <label for="orderString">Order String: </label>
                            <asp:TextBox ID="txtOrderString" runat="server" CssClass="form-control" />
                            <p>
                                Please input the order string with the following format:
                                <br />
                                ProductID-Quantity,ProductID-Quantity
                                <br />
                                for example: 1-1,2-1,3-2
                            </p>
                            <asp:Button CssClass="btn btn-primary" ID="btnAddOrder" runat="server" Text="Add Order" OnClick="btnAddOrder_Click" />
                        </div>
                        <div class="col-lg-9">
                            <h4>Products</h4>
                            <table class="table table-hover" id="dataTable">
                                <asp:ListView ID="lvProducts" DataKeyNames="ProductID"
                                    runat="server">
                                    <LayoutTemplate>
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Product Name</th>
                                                <th>Price</th>
                                                <th>Quantity</th>
                                                <th>Image</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </tbody>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ProductID") %></td>
                                            <td><%# Eval("ProductName") %></td>
                                            <td><%# string.Format("Rp {0:N0}", Eval("Price")) %></td>
                                            <td><%# Eval("StockQuantity") %></td>
                                            <td>
                                                <asp:Image ID="imgProduct" runat="server" Width="100"
                                                    ImageUrl='<%# If(Eval("ImageUrl").StartsWith("http"), Eval("ImageUrl"), Eval("ImageUrl", "~/Pics/{0}")) %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyItemTemplate>
                                        <tr>
                                            <td colspan="7">No records found</td>
                                        </tr>
                                    </EmptyItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    </div>
        <asp:Literal ID="ltShowModal" runat="server" />
</asp:Content>

