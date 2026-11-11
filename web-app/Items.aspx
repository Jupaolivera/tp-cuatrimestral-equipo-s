<%@ Page Title="Ítems" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="web_app.Items" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ítems</h2>
    <p class="text-muted">Carta del restaurante: listado, alta, modificación y baja de ítems.</p>

    <asp:GridView ID="dgvItems" runat="server" CssClass="table"
        AutoGenerateColumns="false" DataKeyNames="Id"
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