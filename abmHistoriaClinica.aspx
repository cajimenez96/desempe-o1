<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmHistoriaClinica.aspx.cs" Inherits="desempeño1.abmHistoriaClinica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Gestión de Historia Clínica</h2>
            
            <!-- Campo de búsqueda por ID del registro clínico -->
            <asp:Label ID="LabelBusqueda" runat="server" Text="Buscar por ID de registro: "></asp:Label>
            <asp:TextBox ID="TextBoxBusquedaId" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" OnClick="BuscarHistoria" />
            
            <br /><br />
            <hr />
            <br /><br />

            <!-- Campos de la historia clínica -->
            <asp:Label ID="LabelFecha" runat="server" Text="Fecha (DD-MM-AAAA): "></asp:Label>
            <asp:TextBox ID="TextBoxFecha" runat="server" TextMode="Date"></asp:TextBox>
            <br /><br />

            <asp:Label ID="LabelIdAnimal" runat="server" Text="ID Animal: "></asp:Label>
            <asp:TextBox ID="TextBoxIdAnimal" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Label ID="LabelIdEvento" runat="server" Text="ID Evento: "></asp:Label>
            <asp:TextBox ID="TextBoxIdEvento" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Label ID="LabelComentario" runat="server" Text="Comentario:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBoxComentario" runat="server" TextMode="MultiLine" Rows="4" Columns="40"></asp:TextBox>
            <br /><br />

            <!-- Botones ABM -->
            <asp:Button ID="ButtonAlta" runat="server" Text="Crear registro" OnClick="AltaHistoria" />
            <asp:Button ID="ButtonBaja" runat="server" Text="Eliminar registro" OnClick="EliminarHistoria" />
            <asp:Button ID="ButtonModificar" runat="server" Text="Actualizar registro" OnClick="ActualizarHistoria" />
            <br /><br />

            <!-- Etiqueta para informar resultados -->
            <asp:Label ID="LabelResultado" runat="server"></asp:Label>
            <br /><br />

            <asp:HyperLink ID="HyperLinkVolver" runat="server" NavigateUrl="Default.aspx">Volver al inicio</asp:HyperLink>
        </div>
    </form>
</body>
</html>
