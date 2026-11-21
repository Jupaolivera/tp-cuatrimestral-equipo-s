<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="web_app.Reportes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-3">Reportes</h2>

    <div class="row g-3 align-items-start mb-3">
        <div class="col-md-3">
            <label for="txtDesde" class="form-label">Desde</label>
            <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date" />
            <asp:RequiredFieldValidator ErrorMessage="Ingresá la fecha inicial." ControlToValidate="txtDesde" CssClass="text-danger" Display="Dynamic" runat="server" />
        </div>
        <div class="col-md-3">
            <label for="txtHasta" class="form-label">Hasta</label>
            <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date" />
            <asp:RequiredFieldValidator ErrorMessage="Ingresá la fecha final." ControlToValidate="txtHasta" CssClass="text-danger" Display="Dynamic" runat="server" />
        </div>
        <div class="col-md-3 pt-md-4 mt-md-2">
            <asp:Button Text="Filtrar" ID="btnFiltrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" runat="server" />
        </div>
    </div>

    <asp:Label ID="lblError" CssClass="text-danger d-block mb-3" runat="server" />

    <h4 class="mt-4">Pedidos por mesa</h4>
    <asp:GridView ID="dgvPorMesa" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
        EmptyDataText="No hay pedidos en ese rango de fechas.">
        <Columns>
            <asp:BoundField HeaderText="Mesa" DataField="Mesa" />
            <asp:BoundField HeaderText="Pedidos" DataField="Pedidos" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C2}" />
        </Columns>
    </asp:GridView>

    <h4 class="mt-4">Pedidos por mesero</h4>
    <asp:GridView ID="dgvPorMesero" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
        EmptyDataText="No hay pedidos en ese rango de fechas.">
        <Columns>
            <asp:BoundField HeaderText="Mesero" DataField="Mesero" />
            <asp:BoundField HeaderText="Pedidos" DataField="Pedidos" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C2}" />
        </Columns>
    </asp:GridView>

</asp:Content>
