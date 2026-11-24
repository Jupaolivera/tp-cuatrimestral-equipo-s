<%@ Page Title="Ítems" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="web_app.Items" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ítems</h2>
    <p class="text-muted">Carta del restaurante: listado, alta, modificación y baja de ítems.</p>

    <asp:Panel runat="server" DefaultButton="btnBuscar" CssClass="row g-2 mb-3">
        <div class="col-md-5">
            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por nombre o descripción" />
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlTipoFiltro" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </asp:Panel>

    <asp:GridView ID="dgvItems" runat="server" CssClass="table table-striped table-hover"
        AutoGenerateColumns="false" DataKeyNames="Id" EmptyDataText="No se encontraron ítems."
        OnSelectedIndexChanged="dgvItems_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:C}" />
            <asp:BoundField HeaderText="Stock" DataField="Stock" />
            <asp:BoundField HeaderText="Tipo" DataField="TipoDescripcion" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
        </Columns>
    </asp:GridView>

    <asp:Button Text="Agregar Nuevo Ítem" runat="server" ID="btnAgregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />

</asp:Content>