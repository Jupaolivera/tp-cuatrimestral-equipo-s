<%@ Page Title="Tipo de ítem" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioTipoItem.aspx.cs" Inherits="web_app.FormularioTipoItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tipo de ítem</h2>

    <div class="row">
        <div class="col-md-5">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" />
                <asp:RequiredFieldValidator ErrorMessage="La descripción es obligatoria." ControlToValidate="txtDescripcion" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>

            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" CausesValidation="false" Visible="false" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>