using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace web_app
{
    public partial class TiposItem : System.Web.UI.Page
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
                TipoItemNegocio negocio = new TipoItemNegocio();
                List<TipoItem> lista = negocio.listar();

                string texto = txtFiltro.Text.Trim().ToLower();
                if (texto != "")
                    lista = lista.FindAll(x => x.Descripcion.ToLower().Contains(texto));

                dgvTipos.DataSource = lista;
                dgvTipos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            cargar();
        }

        protected void dgvTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvTipos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioTipoItem.aspx?id=" + id, false);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioTipoItem.aspx", false);
        }
    }
}