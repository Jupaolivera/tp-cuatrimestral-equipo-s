using System;
using System.Collections.Generic;
using System.Web.UI;
using dominio;
using negocio;

namespace web_app
{
    public partial class MisMesas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = Session["usuario"] as Usuario;
                    if (usuario == null)
                        return;

                    AsignacionMesaNegocio asigNegocio = new AsignacionMesaNegocio();
                    List<AsignacionMesa> asignaciones = asigNegocio.listar();

                    MesaNegocio mesaNegocio = new MesaNegocio();
                    List<Mesa> todas = mesaNegocio.listar();

                    List<Mesa> misMesas = new List<Mesa>();
                    foreach (AsignacionMesa a in asignaciones)
                    {
                        if (a.Mesero.Id == usuario.Id && a.Fecha == DateTime.Today)
                        {
                            foreach (Mesa m in todas)
                            {
                                if (m.Id == a.Mesa.Id)
                                {
                                    misMesas.Add(m);
                                }
                            }
                        }
                    }

                    dgvMisMesas.DataSource = misMesas;
                    dgvMisMesas.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }
    }
}