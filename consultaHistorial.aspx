<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="consultaHistorial.aspx.cs" Inherits="desempeño1.consultaHistorial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Historial Clínico por Animal</h2>

            <!-- Filtros de búsqueda -->
            <asp:Label ID="Label1" runat="server" Text="ID Animal: "></asp:Label>
            <asp:TextBox ID="TextBoxIdAnimal" runat="server"></asp:TextBox>
            
            &nbsp;&nbsp;o&nbsp;&nbsp;

            <asp:Label ID="Label2" runat="server" Text="Nombre Animal: "></asp:Label>
            <asp:TextBox ID="TextBoxNombreAnimal" runat="server"></asp:TextBox>

            <asp:Button ID="ButtonFiltrar" runat="server" Text="Filtrar" OnClick="FiltrarHistorial" />
            <br /><br />

            <!-- Mensajes al usuario -->
            <asp:Label ID="LabelResultado" runat="server"></asp:Label>
            <br /><br />

            <!-- Grilla para listar los eventos médicos del animal -->
            <asp:GridView ID="GridViewHistorial" runat="server">
            </asp:GridView>
            <br /><br />

            <asp:HyperLink ID="HyperLinkVolver" runat="server" NavigateUrl="Default.aspx">Volver al inicio</asp:HyperLink>
        </div>
    </form>
</body>
</html>
