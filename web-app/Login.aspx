<%@ Page Title="Ingresar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="web_app.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-5">
            <h2 class="mb-3">Ingresar</h2>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" />
                <asp:RequiredFieldValidator ErrorMessage="Ingresá tu email." ControlToValidate="txtEmail" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtClave" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" MaxLength="100" />
                <asp:RequiredFieldValidator ErrorMessage="Ingresá tu contraseña." ControlToValidate="txtClave" CssClass="text-danger" Display="Dynamic" runat="server" />
            </div>

            <asp:Label ID="lblError" CssClass="text-danger d-block mb-3" runat="server" />

            <asp:Button Text="Ingresar" ID="btnIngresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" runat="server" />
        </div>
    </div>

</asp:Content>
