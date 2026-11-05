<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="web_app.Error" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ocurrió un problema</h2>
    <p class="text-muted">No se pudo completar la operación.</p>
    <asp:Label ID="lblMensaje" CssClass="text-danger" runat="server" />
    <p class="mt-3"><a href="~/" runat="server">Volver al inicio</a></p>

</asp:Content>