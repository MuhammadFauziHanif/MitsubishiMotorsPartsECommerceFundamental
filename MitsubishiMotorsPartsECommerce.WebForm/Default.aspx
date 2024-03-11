<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="MitsubishiMotorsPartsECommerce.WebForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Mitsubishi Motors Parts E-Commerce Admin Platform</h1>
            <p class="lead">Welcome to the Mitsubishi Motors Parts E-Commerce Admin Platform. This platform provides you with the tools to efficiently manage your inventory of Mitsubishi automotive parts.</p>
        </section>

        <div class="row">
            <section class="col-md-6" aria-labelledby="manageProductsTitle">
                <h2 id="manageProductsTitle">Manage Products</h2>
                <p style="text-align: justify">
                    You can add new products, edit existing ones, update their descriptions, prices, and images, and ensure that your inventory is up-to-date and organized.
                </p>
                <p>
                    <a class="btn btn-primary" href="ProductsPage">Manage Products &raquo;</a>
                </p>
            </section>
            <section class="col-md-6" aria-labelledby="manageCategoriesTitle">
                <h2 id="manageCategoriesTitle">Manage Categories</h2>
                <p style="text-align: justify">
                    Keep your product listings organized by creating, editing, and deleting categories as needed. Ensure easy navigation for your customers by maintaining a well-structured category hierarchy.
                </p>
                <p>
                    <a class="btn btn-primary" href="CategoriesPage">Manage Categories &raquo;</a>
                </p>
            </section>
            <section class="col-md-6" id="createOrderManuallySection" aria-labelledby="createOrderManuallyTitle" runat="server">
                <h2 id="createOrderManuallyTitle">Create Order Manually</h2>
                <p style="text-align: justify">
                    You can create orders manually for your customers. This feature is useful when a customer calls in to place an order over the phone or in person.
                </p>
                <p>
                    <a class="btn btn-primary" href="ManualOrder">Create Order Manually &raquo;</a>
                </p>
             </section>
            <section class="col-md-6" id="registerAdminSection" aria-labelledby="registerAdminTitle" runat="server" visible="false">
                <h2 id="createAdminTitle">Register Admin</h2>
                <p style="text-align: justify">
                    You can register new administrators to help you manage your inventory. Make sure to give authorized personnel access to the platform to help you with the day-to-day operations.
                </p>
                <p>
                    <a class="btn btn-primary" href="RegisterAdminPage">Register Admin &raquo;</a>
                </p>
            </section>
        </div>

    </main>

</asp:Content>
