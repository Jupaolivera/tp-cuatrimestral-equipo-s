<%@ Page Title="Detalle del Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioPedido.aspx.cs" Inherits="web_app.FormularioPedido" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row mb-3">
        <div class="col">
            <h2><asp:Label ID="lblTitulo" runat="server" Text="Pedido" /></h2>
            <p class="text-muted">Carga de ítems y cierre del pedido de la mesa.</p>
        </div>
    </div>

    <div class="row mb-4">
        <div class="col-md-6">
            <ul class="list-group">
                <li class="list-group-item d-flex justify-content-between">
                    <strong>Mesa:</strong>
                    <asp:Label ID="lblMesa" runat="server" />
                </li>
                <li class="list-group-item d-flex justify-content-between">
                    <strong>Mesero:</strong>
                    <asp:Label ID="lblMesero" runat="server" />
                </li>
                <li class="list-group-item d-flex justify-content-between">
                    <strong>Apertura:</strong>
                    <asp:Label ID="lblFechaApertura" runat="server" />
                </li>
                <li class="list-group-item d-flex justify-content-between">
                    <strong>Estado:</strong>
                    <asp:Label ID="lblEstado" runat="server" />
                </li>
                <li class="list-group-item d-flex justify-content-between">
                    <strong>Total:</strong>
                    <strong><asp:Label ID="lblTotal" runat="server" /></strong>
                </li>
            </ul>
        </div>
    </div>

    <asp:Panel ID="pnlAgregarItem" runat="server" CssClass="row mb-4">
        <div class="col-md-5">
            <h4>Agregar ítem al pedido</h4>
            <div class="mb-3">
                <label for="ddlItem" class="form-label">Ítem de la carta</label>
                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-select" />
            </div>
            <div class="mb-3">
                <label for="txtCantidad" class="form-label">Cantidad</label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="1" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-2" />

            <asp:Button ID="btnAgregarItem" runat="server" Text="Agregar ítem" CssClass="btn btn-primary" OnClick="btnAgregarItem_Click" />
        </div>
    </asp:Panel>

    <div class="row mb-3">
        <div class="col">
            <h4>Ítems consumidos</h4>
            <asp:GridView ID="dgvDetalles" runat="server" CssClass="table table-striped table-hover table-bordered"
                AutoGenerateColumns="False" OnRowCommand="dgvDetalles_RowCommand"
                EmptyDataText="Todavía no hay ítems cargados en este pedido.">
                <Columns>
                    <asp:BoundField HeaderText="Ítem" DataField="ItemNombre" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario" DataFormatString="{0:C}" />
                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminarItem" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm"
                                CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="row mt-3">
        <div class="col">
           <asp:Button ID="btnCerrarPedido" runat="server" Text="Cerrar pedido" CssClass="btn btn-danger" OnClick="btnCerrarPedido_Click" OnClientClick="return confirm('¿Seguro que querés cerrar el pedido? Una vez cerrado ya no se va a poder modificar.');" />
            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
        </div>
    </div>

</asp:Content>