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
        <asp:GridView ID="dgvMesas" runat="server" CssClass="table table-striped table-hover table-bordered" AutoGenerateColumns="False" OnRowCommand="dgvMesas_RowCommand">
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

    <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="row mt-4">
        <div class="col-md-5">
            <h4>Datos de la Mesa</h4>
            <asp:HiddenField ID="hfIdMesa" runat="server" />

            <div class="mb-3">
                <label for="txtNumero" class="form-label">N° de Mesa</label>
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCapacidad" class="form-label">Capacidad (Lugares)</label>
                <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
            <br />

            <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
        </div>
    </asp:Panel>
</asp:Content>