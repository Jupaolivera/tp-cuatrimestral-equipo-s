<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="web_app.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="mb-0">Usuarios y meseros</h2>
        <a href="~/FormularioUsuario" runat="server" class="btn btn-primary">Nuevo usuario</a>
    </div>

    <asp:GridView ID="dgvUsuarios" runat="server" DataKeyNames="Id" AutoGenerateColumns="false"
        CssClass="table table-striped table-hover" EmptyDataText="No hay usuarios cargados."
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