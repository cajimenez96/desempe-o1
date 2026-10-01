<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmEventos.aspx.cs" Inherits="desempeño1.abmEventos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Gestión de Eventos</h2>
            
            <asp:Label ID="LabelBusqueda" runat="server" Text="Buscar por ID: "></asp:Label>
            <asp:TextBox ID="TextBoxBusquedaId" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" OnClick="BuscarEvento" />
            
            <br /><br />
            <hr />
            <br /><br />

            <asp:Label ID="LabelDescripcion" runat="server" Text="Descripción: "></asp:Label>
            <asp:TextBox ID="TextBoxDescripcion" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Label ID="LabelEscala" runat="server" Text="Escala (número): "></asp:Label>
            <asp:TextBox ID="TextBoxEscala" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Button ID="ButtonAlta" runat="server" Text="Crear evento" OnClick="AltaEvento" />
            <asp:Button ID="ButtonBaja" runat="server" Text="Eliminar evento" OnClick="EliminarEvento" />
            <asp:Button ID="ButtonModificar" runat="server" Text="Actualizar evento" OnClick="ActualizarEvento" />
            <br /><br />

            <asp:Label ID="LabelResultado" runat="server"></asp:Label>
            <br /><br />

            <asp:HyperLink ID="HyperLinkVolver" runat="server" NavigateUrl="Default.aspx">Volver al inicio</asp:HyperLink>
        </div>
    </form>
</body>
</html>
