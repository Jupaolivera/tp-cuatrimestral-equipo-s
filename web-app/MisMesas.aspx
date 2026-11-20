<%@ Page Title="Mis mesas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisMesas.aspx.cs" Inherits="web_app.MisMesas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Mis mesas</h2>
    <p class="text-muted">Mesas que tenés asignadas para atender en el día de hoy.</p>

    <asp:GridView ID="dgvMisMesas" runat="server" CssClass="table table-striped table-hover table-bordered" AutoGenerateColumns="False" EmptyDataText="No tenés mesas asignadas para hoy.">
        <Columns>
            <asp:BoundField DataField="Numero" HeaderText="N° de Mesa" />
            <asp:BoundField DataField="Capacidad" HeaderText="Lugares" />
        </Columns>
    </asp:GridView>

</asp:Content>