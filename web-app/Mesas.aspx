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
        <asp:TemplateField HeaderText="Acción">
            <ItemTemplate>
                <asp:Button ID="btnAsignar" runat="server" Text="Abrir / Asignar" CssClass="btn btn-success btn-sm" CommandName="Asignar" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
            
            <asp:Button ID="btnNuevaMesa" runat="server" Text="Agregar Nueva Mesa" CssClass="btn btn-primary mt-3" />
        </div>
    </div>
</asp:Content>