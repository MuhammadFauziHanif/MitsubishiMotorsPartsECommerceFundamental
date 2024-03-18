Imports Microsoft.Extensions.Configuration

Namespace MitsubishiMotorsPartsECommerce.DAL
    Public Class Helper
        Public Shared Function GetConnectionString() As String
            If System.Configuration.ConfigurationManager.ConnectionStrings("MyDbConnectionString") Is Nothing Then
                Dim MyConfig = New ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
                Return MyConfig.GetConnectionString("MyDbConnectionString")
            End If
            Dim connString = System.Configuration.ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
            Return connString
            'return @"Data Source=ACTUAL;Initial Catalog=LatihanDb;Integrated Security=True;TrustServerCertificate=True";
            'return ConfigurationManager.AppSettings["MyDbConnectionString"];
        End Function
    End Class
End Namespace
