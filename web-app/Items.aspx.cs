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
                    itemNegocio negocio = new itemNegocio();
                    dgvItems.DataSource = negocio.listar();
                    dgvItems.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
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