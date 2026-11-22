<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="web_app.Pedidos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Pedidos</h2>
<p class="text-muted">Abrí un pedido para una mesa asignada hoy o entrá a uno existente.</p>
    <div class="row mb-3">
        <div class="col-md-4">
            <label for="ddlMesa" class="form-label">Mesa</label>
            <asp:DropDownList ID="ddlMesa" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-4">
            <br />
            <asp:Button ID="btnAbrir" runat="server" Text="Abrir pedido" CssClass="btn btn-success" OnClick="btnAbrir_Click" />
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />

    <asp:GridView ID="dgvPedidos" runat="server" CssClass="table"
        AutoGenerateColumns="false" DataKeyNames="Id"
        OnSelectedIndexChanged="dgvPedidos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="N°" DataField="Id" />
            <asp:BoundField HeaderText="Mesa" DataField="MesaNumero" />
            <asp:BoundField HeaderText="Mesero" DataField="MeseroNombre" />
            <asp:BoundField HeaderText="Apertura" DataField="FechaApertura" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField HeaderText="Estado" DataField="EstadoTexto" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Ver" />
        </Columns>
    </asp:GridView>
</asp:Content>