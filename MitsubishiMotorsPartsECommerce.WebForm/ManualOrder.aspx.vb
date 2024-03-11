Imports MitsubishiMotorsPartsECommerce.BLL
Imports MitsubishiMotorsPartsECommerce.BLL.DTOs

Public Class ManualOrder
    Inherits System.Web.UI.Page

    Dim _customerBLL As New CustomerBLL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim _customers = _customerBLL.GetAll()
            ddCustomers.DataSource = _customers
            ddCustomers.DataTextField = "FullNameWithID"
            ddCustomers.DataValueField = "CustomerID"
            ddCustomers.DataBind()

            Dim _productBLL As New MitsubishiMotorsPartsECommerce.BLL.ProductBLL
            Dim results = _productBLL.GetAll()
            lvProducts.DataSource = results
            lvProducts.DataBind()
        End If
    End Sub

    Protected Sub btnAddOrder_Click(sender As Object, e As EventArgs)
        Try
            Dim _salesOrderBLL As New MitsubishiMotorsPartsECommerce.BLL.SalesOrderBLL()
            Dim _salesOrderDto As New SalesOrderCreateDTO()
            _salesOrderDto.CustomerID = CInt(ddCustomers.SelectedValue)
            _salesOrderDto.LstProd = txtOrderString.Text

            _salesOrderBLL.Create(_salesOrderDto)

            ddCustomers.ClearSelection()
            txtOrderString.Text = String.Empty

            Dim _productBLL As New MitsubishiMotorsPartsECommerce.BLL.ProductBLL
            Dim results = _productBLL.GetAll()
            lvProducts.DataSource = results
            lvProducts.DataBind()

            ltMessage.Text = "<span class='alert alert-success'>Sales Order Successfully Created</span><br/><br/>"
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub
End Class