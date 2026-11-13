<%@ Page Title="Tipos de ítem" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposItem.aspx.cs" Inherits="web_app.TiposItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tipos de ítem</h2>
    <p class="text-muted">Categorías de la carta (plato, bebida, postre): listado, alta, modificación y baja.</p>
    <asp:GridView ID="dgvTipos" runat="server" CssClass="table"
        AutoGenerateColumns="false" DataKeyNames="Id"
        OnSelectedIndexChanged="dgvTipos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
        </Columns>
    </asp:GridView>

    <asp:Button Text="Agregar Nuevo Tipo" runat="server" ID="btnAgregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
</asp:Content>