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
            if (!IsPostBack)
                cargar();
        }

        private void cargar()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                List<Usuario> lista = negocio.listar();

                string texto = txtFiltro.Text.Trim().ToLower();
                if (texto != "")
                    lista = lista.FindAll(x => x.Nombre.ToLower().Contains(texto) || x.Apellido.ToLower().Contains(texto) || x.Email.ToLower().Contains(texto));

                if (ddlPerfilFiltro.SelectedValue != "")
                    lista = lista.FindAll(x => x.Perfil.ToString() == ddlPerfilFiltro.SelectedValue);

                dgvUsuarios.DataSource = lista;
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            ddlPerfilFiltro.SelectedIndex = 0;
            cargar();
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