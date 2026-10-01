<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmAnimales.aspx.cs" Inherits="desempeño1.abmAnimales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Gestión de Animales</h2>
            
            <asp:Label ID="Label3" runat="server" Text="Buscar por ID: "></asp:Label>
            <asp:TextBox ID="TextBoxBusquedaId" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" OnClick="BuscarAnimal" />
            
            <br /><br />
            <hr />
            <br /><br />

            <asp:Label ID="Label1" runat="server" Text="Nombre: "></asp:Label>
            <asp:TextBox ID="TextBoxNombre" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Label ID="Label2" runat="server" Text="Especie (código): "></asp:Label>
            <asp:TextBox ID="TextBoxEspecie" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Button ID="ButtonAlta" runat="server" Text="Crear animal" OnClick="AltaAnimal" />
            <asp:Button ID="ButtonBaja" runat="server" Text="Eliminar Animal" OnClick="EliminarAnimal" />
            <asp:Button ID="ButtonModificar" runat="server" Text="Actualizar animal" OnClick="ActualizarAnimal" />
            <br /><br />

            <!-- Etiqueta para informar resultados -->
            <asp:Label ID="LabelResultado" runat="server"></asp:Label>
            <br /><br />

            <!-- Enlace para volver al menú -->
            <asp:HyperLink ID="HyperLinkVolver" runat="server" NavigateUrl="Default.aspx">Volver al inicio</asp:HyperLink>
        </div>
    </form>
</body>
</html>
