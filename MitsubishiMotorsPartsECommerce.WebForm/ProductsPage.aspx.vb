Imports MitsubishiMotorsPartsECommerce.BLL
Imports MitsubishiMotorsPartsECommerce.BLL.DTOs
Imports MitsubishiMotorsPartsECommerce.DAL

Public Class ProductsPage
    Inherits System.Web.UI.Page


#Region "Binding Data"


    Sub LoadDataCategories()
        Dim _categoryBLL As New MitsubishiMotorsPartsECommerce.BLL.CategoryBLL
        Dim results = _categoryBLL.GetAll()
        lvCategories.DataSource = results
        lvCategories.DataBind()

        ddCategories.DataSource = results
        ddCategories.DataTextField = "CategoryName"
        ddCategories.DataValueField = "CategoryID"
        ddCategories.DataBind()
    End Sub

    Sub LoadAllDataProducts()
        Dim _productBLL As New MitsubishiMotorsPartsECommerce.BLL.ProductBLL
        Dim results = _productBLL.GetAll()
        lvProducts.DataSource = results
        lvProducts.DataBind()
    End Sub

    Sub LoadDataProducts(categoryID As String)
        Dim _productBLL As New MitsubishiMotorsPartsECommerce.BLL.ProductBLL
        Dim results = _productBLL.GetProductByCategory(categoryID)
        lvProducts.DataSource = results
        lvProducts.DataBind()
    End Sub

    Sub GetEditArticle(id As Integer)
        Dim _categoryBLL As New CategoryBLL
        Dim _categories = _categoryBLL.GetAll()

        Dim _productBLL As New MitsubishiMotorsPartsECommerce.BLL.ProductBLL
        Dim _product = _productBLL.GetProductByID(id)


        Try
            ddCategoriesEdit.DataSource = _categories
            ddCategoriesEdit.DataTextField = "CategoryName"
            ddCategoriesEdit.DataValueField = "CategoryID"
            ddCategoriesEdit.DataBind()

            If _product IsNot Nothing Then
                txtProductIDEdit.Text = _product.ProductID
                txtProductNameEdit.Text = _product.ProductName
                txtDescriptionEdit.Text = _product.Description
                txtPriceEdit.Text = _product.Price
                txtStockQuantityEdit.Text = _product.StockQuantity
                ddCategoriesEdit.SelectedValue = _product.CategoryID
                lblPic.Text = _product.ImageUrl
            Else
                ltMessage.Text = "<span class='alert alert-danger'>Error: Product not found</span><br/><br/>"
            End If
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub

#End Region

#Region "Validate"
    Function CheckFileType(ByVal fileName As String) As Boolean
        Dim ext As String = IO.Path.GetExtension(fileName)
        Select Case ext.ToLower()
            Case ".gif"
                Return True
            Case ".png"
                Return True
            Case ".jpg"
                Return True
            Case ".jpeg"
                Return True
            Case ".bmp"
                Return True
            Case Else
                Return False
        End Select
    End Function
#End Region

#Region "Initialize"
    Sub InitializeFormAddProduct()
        txtProductName.Text = String.Empty
        txtDescription.Text = String.Empty
        txtPrice.Text = String.Empty
        txtStockQuantity.Text = String.Empty
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadDataCategories()
            LoadAllDataProducts()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablesorter", "$(document).ready( function () {
    $('#dataTable').DataTable();
} );", True)
        End If
    End Sub

    Protected Sub lvCategories_SelectedIndexChanging(sender As Object, e As ListViewSelectEventArgs)
        'Dim categoryid = lvCategories.DataKeys(e.NewSelectedIndex).Value
        'ltMessage.Text = categoryid.ToString()
    End Sub

    Protected Sub lvCategories_SelectedIndexChanged(sender As Object, e As EventArgs)
        'ViewState("CategoryID") = lvCategories.SelectedValue.ToString()
        'ltMessage.Text = lvCategories.SelectedValue.ToString()
        LoadDataCategories()
        LoadDataProducts(CInt(lvCategories.SelectedValue.ToString))
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            'rename upload file
            Dim fileName As String = Guid.NewGuid.ToString() & fpPicAdd.FileName
            If fpPicAdd.HasFile Then
                If CheckFileType(fileName) Then
                    Dim _path As String = Server.MapPath("~/Pics/")
                    fpPicAdd.SaveAs(_path & fileName)
                End If
            End If

            Dim _productBLL As New ProductBLL
            Dim _product As New ProductCreateDTO
            _product.CategoryID = CInt(ddCategories.SelectedValue)
            _product.ProductName = txtProductName.Text
            _product.Description = txtDescription.Text
            _product.Price = txtPrice.Text
            _product.StockQuantity = txtStockQuantity.Text
            _product.ImageUrl = fileName
            _productBLL.Insert(_product)

            lvProducts.DataBind()

            ltMessage.Text = "<span class='alert alert-success'>Product created successfully</span><br/><br/>"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseModalScript", "$('#myModal').modal('hide');", True)
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub


    Protected Sub lvProducts_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ViewState("Command") = "Delete" Then
            Try
                Dim _productBLL As New MitsubishiMotorsPartsECommerce.BLL.ProductBLL
                _productBLL.Delete(CInt(lvProducts.SelectedValue.ToString))
                lvProducts.DataBind()

                LoadAllDataProducts()
                ltMessage.Text = "<span class='alert alert-success'>Product deleted successfully</span><br/><br/>"
            Catch ex As Exception
                ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
            End Try
        ElseIf ViewState("Command") = "Edit" Then
            ltMessage.Text = "<span class='alert alert-success'>Edit</span><br/><br/>"
            GetEditArticle(CInt(lvProducts.SelectedValue.ToString))
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenModalScript", "$(window).on('load',function(){$('#myModalEdit').modal('show');})", True)
        End If
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        Try
            'rename upload file
            Dim fileName As String = Guid.NewGuid.ToString() & fpPicEdit.FileName
            If fpPicEdit.HasFile Then
                If CheckFileType(fileName) Then
                    Dim _path As String = Server.MapPath("~/Pics/")
                    fpPicEdit.SaveAs(_path & fileName)
                End If
            End If

            Dim _productBLL As New ProductBLL
            Dim _product As New ProductUpdateDTO
            _product.ProductID = CInt(txtProductIDEdit.Text)
            _product.CategoryID = CInt(ddCategoriesEdit.SelectedValue)
            _product.ProductName = txtProductNameEdit.Text
            _product.Description = txtDescriptionEdit.Text
            _product.Price = txtPriceEdit.Text
            _product.StockQuantity = txtStockQuantityEdit.Text
            _product.ImageUrl = If(fpPicEdit.HasFile, fileName, lblPic.Text)
            _productBLL.Update(_product)

            lvProducts.DataBind()

            LoadAllDataProducts()
            ltMessage.Text = "<span class='alert alert-success'>Product updated successfully</span><br/><br/>"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseModalScript", "$('#myModalEdit').modal('hide');", True)
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub

    Protected Sub lvProducts_ItemCommand(sender As Object, e As ListViewCommandEventArgs)
        'Dim lvDataItem As ListViewDataItem = CType(e.Item, ListViewDataItem)
        ViewState("Command") = e.CommandArgument
    End Sub

End Class