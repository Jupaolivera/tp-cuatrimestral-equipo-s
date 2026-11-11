using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace web_app
{
    public partial class Mesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MesaNegocio negocio = new MesaNegocio();
                dgvMesas.DataSource = negocio.listar();
                dgvMesas.DataBind();
            }
        }

        protected void dgvMesas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Asignar")
            {
                int idMesa = Convert.ToInt32(e.CommandArgument);
                // Cambiamos AsignarMozo.aspx por Asignaciones.aspx
                Response.Redirect("Asignaciones.aspx?id=" + idMesa);
            }
        }
    }
}