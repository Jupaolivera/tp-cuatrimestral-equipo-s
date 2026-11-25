<%@ Page Title="Mesas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mesas.aspx.cs" Inherits="web_app.Mesas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mb-3">
        <div class="col">
            <h2>Mesas del Salón</h2>
            <p class="text-muted">Listado y administración de las mesas del restaurante.</p>
        </div>
    </div>

    <div class="row">
        <div class="col">
            <asp:Panel runat="server" DefaultButton="btnBuscar" CssClass="row g-2 mb-3">
                <div class="col-md-5">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por número de mesa" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                </div>
            </asp:Panel>

            <asp:GridView ID="dgvMesas" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False" EmptyDataText="No se encontraron mesas." OnRowCommand="dgvMesas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Numero" HeaderText="N° de Mesa" />
                    <asp:BoundField DataField="Capacidad" HeaderText="Lugares" />
                    <asp:TemplateField HeaderText="Mesero de hoy">
                        <ItemTemplate><%# mozoDeHoy(Eval("Id")) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:Button ID="btnAsignar" runat="server" Text="Abrir / Asignar" CssClass="btn btn-success btn-sm" CommandName="Asignar" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Button ID="btnEditar" runat="server" Text="Modificar" CssClass="btn btn-secondary btn-sm ms-1" CommandName="Modificar" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            <asp:Button ID="btnNuevaMesa" runat="server" Text="Agregar Nueva Mesa" CssClass="btn btn-primary mt-3" OnClick="btnNuevaMesa_Click" />
        </div>
    </div>

    <asp:Panel ID="pnlFormulario" runat="server" Visible="false" DefaultButton="btnGuardar" CssClass="row mt-4">
        <div class="col-md-5">
            <h4>Datos de la Mesa</h4>
            <asp:HiddenField ID="hfIdMesa" runat="server" />

            <div class="mb-3">
                <label for="txtNumero" class="form-label">N° de Mesa</label>
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="4" />
                <asp:RequiredFieldValidator ErrorMessage="El número de mesa es obligatorio." ControlToValidate="txtNumero" ValidationGroup="vgMesa" CssClass="text-danger" Display="Dynamic" runat="server" />
                <asp:RangeValidator ErrorMessage="El número de mesa debe ser un entero mayor a 0." ControlToValidate="txtNumero" ValidationGroup="vgMesa" Type="Integer" MinimumValue="1" MaximumValue="9999" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtCapacidad" class="form-label">Capacidad (Lugares)</label>
                <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control" MaxLength="3" />
                <asp:RequiredFieldValidator ErrorMessage="La capacidad es obligatoria." ControlToValidate="txtCapacidad" ValidationGroup="vgMesa" CssClass="text-danger" Display="Dynamic" runat="server" />
                <asp:RangeValidator ErrorMessage="La capacidad debe ser un entero entre 1 y 100." ControlToValidate="txtCapacidad" ValidationGroup="vgMesa" Type="Integer" MinimumValue="1" MaximumValue="100" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-2" />
            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" />

            <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" CssClass="btn btn-primary" ValidationGroup="vgMesa" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" CausesValidation="false" Visible="false" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
        </div>
    </asp:Panel>
</asp:Content>