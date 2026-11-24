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

    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" />

    <asp:Panel runat="server" DefaultButton="btnBuscar" CssClass="row g-2 mb-3">
        <div class="col-md-3">
            <asp:DropDownList ID="ddlEstadoFiltro" runat="server" CssClass="form-select">
                <asp:ListItem Text="Todos los estados" Value="" />
                <asp:ListItem Text="Abierto" Value="1" />
                <asp:ListItem Text="Cerrado" Value="2" />
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtMesaFiltro" runat="server" CssClass="form-control" placeholder="Número de mesa" />
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </asp:Panel>

    <asp:GridView ID="dgvPedidos" runat="server" CssClass="table table-striped table-hover"
        AutoGenerateColumns="false" DataKeyNames="Id" EmptyDataText="No se encontraron pedidos."
        OnSelectedIndexChanged="dgvPedidos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="N°" DataField="Id" />
            <asp:BoundField HeaderText="Mesa" DataField="MesaNumero" />
            <asp:BoundField HeaderText="Mesero" DataField="MeseroNombre" />
            <asp:BoundField HeaderText="Apertura" DataField="FechaApertura" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField HeaderText="Estado" DataField="EstadoTexto" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Ver detalle" />
        </Columns>
    </asp:GridView>
</asp:Content>