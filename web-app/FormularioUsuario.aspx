<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="web_app.FormularioUsuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-3">Usuario</h2>

    <div class="row">
        <div class="col-md-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
                <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio." ControlToValidate="txtApellido" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" />
                <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" CssClass="text-danger" Display="Dynamic" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="El email no tiene un formato válido." ControlToValidate="txtEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtClave" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" MaxLength="100" />
                <asp:RequiredFieldValidator ID="rfvClave" ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtClave" CssClass="text-danger" Display="Dynamic" runat="server" />
                <asp:Label ID="lblClaveAyuda" Text="Dejala vacía para no cambiarla." CssClass="form-text" Visible="false" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlPerfil" class="form-label">Perfil</label>
                <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Mesero" Value="2" />
                    <asp:ListItem Text="Gerente" Value="1" />
                </asp:DropDownList>
            </div>

            <asp:Label ID="lblError" CssClass="text-danger d-block mb-3" runat="server" />

            <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server" />
            <a href="~/Usuarios" runat="server" class="btn btn-outline-secondary">Cancelar</a>
            <asp:Button Text="Inactivar" ID="btnInactivar" CssClass="btn btn-outline-danger" OnClick="btnInactivar_Click" CausesValidation="false" Visible="false" runat="server" />
        </div>
    </div>

</asp:Content>