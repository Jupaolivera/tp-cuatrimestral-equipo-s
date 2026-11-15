using System;
using System.Web.UI;
using dominio;
using negocio;

namespace web_app
{
    public partial class Login : Page
    {
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.login(txtEmail.Text.Trim(), txtClave.Text);

                if (usuario == null)
                {
                    lblError.Text = "El email o la contraseña no son correctos.";
                    return;
                }

                Session.Add("usuario", usuario);
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
