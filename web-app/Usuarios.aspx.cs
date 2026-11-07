using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace web_app
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    dgvUsuarios.DataSource = negocio.listar();
                    dgvUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioUsuario.aspx?id=" + id);
        }

        protected void dgvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Usuario usuario = (Usuario)e.Row.DataItem;
                if (!usuario.Activo)
                    e.Row.CssClass = "text-muted";
            }
        }

        public string nombrePerfil(object perfil)
        {
            return (int)perfil == 1 ? "Gerente" : "Mesero";
        }
    }
}