Public Class ProductSelectionPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Initialize the text boxes with empty strings
            txtQuantities1.Text = ""
            txtQuantities2.Text = ""
            txtQuantities3.Text = ""
            ' Initialize the label
            lblQuantities.Text = ""
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        ' Iterate through the CheckBoxList to get selected products and their quantities
        Dim selectedProducts As New List(Of String)()
        For Each item As ListItem In chkProducts.Items
            If item.Selected Then
                Dim quantityTextBoxId As String = "txtQuantities" & item.Value
                Dim quantityTextBox As TextBox = CType(Me.FindControl(quantityTextBoxId), TextBox)
                If quantityTextBox IsNot Nothing Then
                    Dim quantity As String = quantityTextBox.Text
                    selectedProducts.Add(item.Value & "-" & quantity)
                End If
            End If
        Next

        ' Join the selected products and quantities into a single string separated by commas
        Dim selectedProductsString As String = String.Join(",", selectedProducts)

        ' Display the selected products and quantities in the label
        lblQuantities.Text = "Selected Products and Quantities: " & selectedProductsString
    End Sub
End Class