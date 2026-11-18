<%@ Page Title="Asignación de mesas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Asignaciones.aspx.cs" Inherits="web_app.Asignaciones" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Asignación de mesa</h2>
    <p class="text-muted">Qué mesero atiende cada mesa en el día.</p>

    <div class="row">
        <div class="col-md-5">
            <div class="mb-3">
                <label for="ddlMesa" class="form-label">N° de Mesa</label>
                <asp:DropDownList ID="ddlMesa" runat="server" CssClass="form-select" />
            </div>
            <div class="mb-3">
                <label for="ddlMesero" class="form-label">Mesero</label>
                <asp:DropDownList ID="ddlMesero" runat="server" CssClass="form-select" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
            <br />

            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
        </div>
    </div>

</asp:Content>