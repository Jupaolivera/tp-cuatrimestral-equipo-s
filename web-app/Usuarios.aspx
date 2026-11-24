<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="web_app.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="mb-0">Usuarios y meseros</h2>
        <a href="~/FormularioUsuario" runat="server" class="btn btn-primary">Nuevo usuario</a>
    </div>

    <asp:Panel runat="server" DefaultButton="btnBuscar" CssClass="row g-2 mb-3">
        <div class="col-md-5">
            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Buscar por nombre, apellido o email" />
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlPerfilFiltro" runat="server" CssClass="form-select">
                <asp:ListItem Text="Todos los perfiles" Value="" />
                <asp:ListItem Text="Gerente" Value="1" />
                <asp:ListItem Text="Mesero" Value="2" />
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </asp:Panel>

    <asp:GridView ID="dgvUsuarios" runat="server" DataKeyNames="Id" AutoGenerateColumns="false"
        CssClass="table table-striped table-hover" EmptyDataText="No se encontraron usuarios."
        OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged" OnRowDataBound="dgvUsuarios_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:TemplateField HeaderText="Perfil">
                <ItemTemplate><%# nombrePerfil(Eval("Perfil")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate><%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %></ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
        </Columns>
    </asp:GridView>

</asp:Content>