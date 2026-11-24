<%@ Page Title="Tipos de ítem" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposItem.aspx.cs" Inherits="web_app.TiposItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tipos de ítem</h2>
    <p class="text-muted">Categorías de la carta (plato, bebida, postre): listado, alta, modificación y baja.</p>

    <asp:Panel runat="server" DefaultButton="btnBuscar" CssClass="row g-2 mb-3">
        <div class="col-md-5">
            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por descripción" />
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </asp:Panel>

    <asp:GridView ID="dgvTipos" runat="server" CssClass="table table-striped table-hover"
        AutoGenerateColumns="false" DataKeyNames="Id" EmptyDataText="No se encontraron tipos de ítem."
        OnSelectedIndexChanged="dgvTipos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
        </Columns>
    </asp:GridView>

    <asp:Button Text="Agregar Nuevo Tipo" runat="server" ID="btnAgregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
</asp:Content>