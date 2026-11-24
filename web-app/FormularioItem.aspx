<%@ Page Title="Ítem" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioItem.aspx.cs" Inherits="web_app.FormularioItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ítem</h2>

    <div class="row">
        <div class="col-md-5">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100" />
                <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="250" />
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="12" />
                <asp:RequiredFieldValidator ErrorMessage="El precio es obligatorio." ControlToValidate="txtPrecio" CssClass="text-danger" Display="Dynamic" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="El precio debe ser un número mayor o igual a 0, con hasta 2 decimales (por ejemplo 18,50)." ControlToValidate="txtPrecio"
                    ValidationExpression="^\s*\d{1,8}([.,]\d{1,2})?\s*$" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtStock" class="form-label">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" MaxLength="9" />
                <asp:RequiredFieldValidator ErrorMessage="El stock es obligatorio." ControlToValidate="txtStock" CssClass="text-danger" Display="Dynamic" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="El stock debe ser un número entero mayor o igual a 0." ControlToValidate="txtStock"
                    ValidationExpression="^\s*\d{1,9}\s*$" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo</label>
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select" />
            </div>

            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" CausesValidation="false" Visible="false" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>