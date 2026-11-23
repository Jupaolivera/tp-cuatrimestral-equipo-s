using System;
using System.IO;
using System.Web.UI;
using dominio;

namespace web_app
{
    public partial class SiteMaster : MasterPage
    {
        // Pantallas a las que se entra sin iniciar sesión
        private static readonly string[] publicas = { "login.aspx", "error.aspx" };

        // Pantallas que puede ver un mesero (perfil 2); el gerente ve todas
        private static readonly string[] delMesero = { "default.aspx", "mismesas.aspx", "pedidos.aspx", "formulariopedido.aspx" };

        protected void Page_Init(object sender, EventArgs e)
        {
            string pagina = Path.GetFileName(Page.AppRelativeVirtualPath).ToLower();
            if (Array.IndexOf(publicas, pagina) >= 0)
                return;

            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
                Response.Redirect("Login.aspx");

            if (usuario.Perfil == 2 && Array.IndexOf(delMesero, pagina) < 0)
            {
                Session.Add("error", "No tenés permiso para ver esa pantalla.");
                Response.Redirect("Error.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            bool conSesion = usuario != null;

            phMenu.Visible = conSesion;
            phSesion.Visible = conSesion;
            phAnonimo.Visible = !conSesion;
            phGerente.Visible = conSesion && usuario.Perfil == 1;
            phMesero.Visible = conSesion && usuario.Perfil == 2;

            if (conSesion)
                lblUsuario.Text = usuario.Nombre + " " + usuario.Apellido;
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}