﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CategoriesPage.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebForm.CategoriesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Category</h1>
    </div>

    <div class="col-lg-12">
        <!-- Basic Card Example -->
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Category</h6>
            </div>

            <div class="card-body">
                <div class="row">
                    <asp:Literal ID="ltMessage" runat="server" />
                </div>
                <div class="row">
                    <div class="row mb-4">
                        <div class="col">
                            <label for="txtSearch" class="form-label">Search: </label>
                            <div class="row">
                                <div class="col">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col">
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-success" />
                                </div>
                            </div>

                        </div>
                        <div class="col">
                            <label for="txtCategoryName" class="form-label">Category Name: </label>
                            <div class="row">
                                <div class="col">
                                    <asp:TextBox ID="txtCategoryName" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col">
                                    <asp:Button Text="Add" ID="btnAdd" class="btn btn-primary" OnClick="btnAdd_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <asp:GridView ID="gvCategories" CssClass="table table-hover" ItemType="MitsubishiMotorsPartsECommerce.BLL.DTOs.CategoryDTO"
                        SelectMethod="GetAll" UpdateMethod="Update" DeleteMethod="Delete"
                        DataKeyNames="CategoryID,CategoryName" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Category Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Item.CategoryName %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# BindItem.CategoryName %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-success btn-sm" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton2" CssClass="btn btn-outline-default btn-sm" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-warning btn-sm" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton2" CssClass="btn btn-outline-danger btn-sm" runat="server" CausesValidation="False" CommandName="Delete"
                                        OnClientClick="return confirm('Are you sure to delete the category?')" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                </div>
            </div>
        </div>
</asp:Content>

