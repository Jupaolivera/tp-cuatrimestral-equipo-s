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
            {
                try
                {
                    TipoItemNegocio negocio = new TipoItemNegocio();
                    dgvTipos.DataSource = negocio.listar();
                    dgvTipos.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
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