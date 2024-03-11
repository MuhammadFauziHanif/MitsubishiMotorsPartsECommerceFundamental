<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductsPage.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebForm.ProductsPage" %>

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
                        <div class="col-lg-2">
                            <h4>Categories</h4>
                            <div class="list-group">
                                <asp:ListView ID="lvCategories" DataKeyNames="CategoryID"
                                    OnSelectedIndexChanging="lvCategories_SelectedIndexChanging"
                                    OnSelectedIndexChanged="lvCategories_SelectedIndexChanged" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" Text='<%# Eval("CategoryName") %>' CommandName="Select"
                                            runat="server" CssClass="list-group-item list-group-item-action" />
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" Text='<%# Eval("CategoryName") %>'
                                            runat="server" CssClass="list-group-item list-group-item-action active" />
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <a class="btn btn-outline-secondary w-100 mt-1" href="CategoriesPage.aspx">Manage Categories</a>
                            </div>
                            <br />
                            <button type="button" class="btn btn-primary w-100" data-bs-toggle="modal" data-bs-target="#myModal">
                                Add Product
                            </button>
                        </div>
                        <div class="col-lg-10">
                            <h4>Products</h4>
                            <table class="table table-hover" id="dataTable">
                                <asp:ListView ID="lvProducts" DataKeyNames="ProductID"
                                    OnSelectedIndexChanging="lvCategories_SelectedIndexChanging"
                                    OnSelectedIndexChanged="lvProducts_SelectedIndexChanged"
                                    OnItemCommand="lvProducts_ItemCommand"
                                    runat="server">
                                    <LayoutTemplate>
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Product Name</th>
                                                <th>Price</th>
                                                <th>Quantity</th>
                                                <th>Image</th>
                                                <th>&nbsp;</th>
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
                                            <td>
                                                <asp:LinkButton ID="lnkEdit" Text="Edit" CssClass="btn btn-outline-warning btn-sm"
                                                    CommandName="Select" CommandArgument="Edit" runat="server" />
                                                <asp:LinkButton ID="lnkDelete" Text="Delete" CssClass="btn btn-outline-danger btn-sm"
                                                    CommandName="Select" CommandArgument="Delete"
                                                    OnClientClick="return confirm('Are you sure you want to delete the product?')" runat="server" />
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

                <!-- Modal Add -->
                <div class="modal" id="myModal">
                    <div class="modal-dialog">
                        <div class="modal-content">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">Add Product</h4>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                    </div>

                                    <!-- Modal body -->
                                    <div class="modal-body">
                                        <div class="form-group">
                                            <label for="ddCategories">Category :</label>
                                            <asp:DropDownList ID="ddCategories" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtProductName">Product Name :</label>
                                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter Product Name" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtDescription">Description :</label>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"
                                                TextMode="MultiLine" placeholder="Enter Detail" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtPrice">Price :</label>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Price" type="number" min="0" step="any" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtStockQuantity">Stock Quantity :</label>
                                            <asp:TextBox ID="txtStockQuantity" runat="server" CssClass="form-control" placeholder="Enter Stock Quantity" type="number" min="0" step="1" />
                                        </div>
                                        <div class="form-group">
                                            <label for="fpPicAdd">Picture :</label>
                                            <asp:Label ID="Label1" Visible="false" runat="server" />
                                            <asp:FileUpload ID="fpPicAdd" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <!-- Modal footer -->
                                    <div class="modal-footer">
                                        <asp:Button Text="Submit" ID="btnSubmit" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnSubmit_Click" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </div>
                    </div>
                </div>

                <!-- Modal Edit -->
                <div class="modal" id="myModalEdit">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <!-- Modal Header -->
                            <div class="modal-header">
                                <h4 class="modal-title">Edit Product</h4>
                                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="txtProductIDEdit">Product ID :</label>
                                    <asp:TextBox ID="txtProductIDEdit" Enabled="false" runat="server" CssClass="form-control"
                                        placeholder="Enter Detail" />
                                </div>
                                <div class="form-group">
                                    <label for="ddCategoriesEdit">Category :</label>
                                    <asp:DropDownList ID="ddCategoriesEdit" CssClass="form-control" runat="server" />
                                </div>
                                <div class="form-group">
                                    <label for="txtProductNameEdit">ProductName :</label>
                                    <asp:TextBox ID="txtProductNameEdit" runat="server" CssClass="form-control" placeholder="Enter ProductName" />
                                </div>
                                <div class="form-group">
                                    <label for="txtDescriptionEdit">Description :</label>
                                    <asp:TextBox ID="txtDescriptionEdit" runat="server" CssClass="form-control"
                                        TextMode="MultiLine" placeholder="Enter Description" />
                                </div>
                                <div class="form-group">
                                    <label for="txtPriceEdit">Price :</label>
                                    <asp:TextBox ID="txtPriceEdit" runat="server" CssClass="form-control" placeholder="Enter Price" type="number" min="0" step="any" />
                                </div>
                                <div class="form-group">
                                    <label for="txtStockQuantityEdit">Stock Quantity :</label>
                                    <asp:TextBox ID="txtStockQuantityEdit" runat="server" CssClass="form-control" placeholder="Enter Stock Quantity" type="number" min="0" step="1" />
                                </div>
                                <div class="form-group">
                                    <label for="fpPicEdit">Picture :</label>
                                    <asp:Label ID="lblPic" Visible="false" runat="server" />
                                    <asp:FileUpload ID="fpPicEdit" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer">
                                <asp:Button Text="Edit" ID="btnEdit" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnEdit_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>
    </div>
        <asp:Literal ID="ltShowModal" runat="server" />
    <script>
        $(document).ready(function () {
            $('#myTable').DataTable();
        });
    </script>
</asp:Content>
