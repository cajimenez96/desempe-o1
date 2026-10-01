<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="desempeño1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Caso Veterinaria</h2>
            <h3>Modulos ABM:</h3>
            <div>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="abmAnimales.aspx">Animales</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="abmEventos.aspx">Eventos</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="abmHistoriaClinica.aspx">Historia clinica</asp:HyperLink><br />

                <br />
                <hr />
                <br />

                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="consultaHistorial.aspx">Consultar historial</asp:HyperLink><br 

            </div>
        </div>
    </form>
</body>
</html>
