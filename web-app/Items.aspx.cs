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
    public partial class Items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    TipoItemNegocio tipoNegocio = new TipoItemNegocio();
                    ddlTipoFiltro.DataSource = tipoNegocio.listar();
                    ddlTipoFiltro.DataValueField = "Id";
                    ddlTipoFiltro.DataTextField = "Descripcion";
                    ddlTipoFiltro.DataBind();
                    ddlTipoFiltro.Items.Insert(0, new ListItem("Todos los tipos", ""));

                    cargar();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        private void cargar()
        {
            try
            {
                itemNegocio negocio = new itemNegocio();
                List<Item> lista = negocio.listar();

                string texto = txtFiltro.Text.Trim().ToLower();
                if (texto != "")
                    lista = lista.FindAll(x => x.Nombre.ToLower().Contains(texto) || (x.Descripcion != null && x.Descripcion.ToLower().Contains(texto)));

                if (ddlTipoFiltro.SelectedValue != "")
                    lista = lista.FindAll(x => x.Tipo != null && x.Tipo.Id.ToString() == ddlTipoFiltro.SelectedValue);

                dgvItems.DataSource = lista;
                dgvItems.DataBind();
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
            ddlTipoFiltro.SelectedIndex = 0;
            cargar();
        }

        protected void dgvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvItems.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioItem.aspx?id=" + id, false);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioItem.aspx", false);
        }
    }
}