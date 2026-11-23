using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace web_app
{
    public partial class Asignaciones : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    MesaNegocio mesaNegocio = new MesaNegocio();
                    ddlMesa.DataSource = mesaNegocio.listar();
                    ddlMesa.DataValueField = "Id";
                    ddlMesa.DataTextField = "Numero";
                    ddlMesa.DataBind();

                    // cargo meseros que esten regritrados uno y dos son prueba cambiar a futuro    
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    List<Usuario> usuarios = usuarioNegocio.listar();

                    foreach (Usuario u in usuarios)
                    {
                        if (u.Perfil == 2 && u.Activo)
                        {
                            ddlMesero.Items.Add(new ListItem(u.Nombre + " " + u.Apellido, u.Id.ToString()));
                        }
                    }

                    if (Request.QueryString["id"] != null)
                    {
                        int idMesa = int.Parse(Request.QueryString["id"]);
                        ddlMesa.SelectedValue = idMesa.ToString();
                        ddlMesa.Enabled = false;

                        // si la mesa ya tiene mozo hoy lo dejo seleccionado
                        AsignacionMesaNegocio asigNegocio = new AsignacionMesaNegocio();
                        List<AsignacionMesa> lista = asigNegocio.listar();

                        foreach (AsignacionMesa a in lista)
                        {
                            if (a.Mesa.Id == idMesa && a.Fecha == DateTime.Today)
                            {
                                ddlMesero.SelectedValue = a.Mesero.Id.ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int idMesa = int.Parse(ddlMesa.SelectedValue);
                int idMesero = int.Parse(ddlMesero.SelectedValue);

                AsignacionMesa asig = new AsignacionMesa();
                asig.Fecha = DateTime.Today;
                asig.Mesa = new Mesa();
                asig.Mesa.Id = idMesa;
                asig.Mesero = new Usuario();
                asig.Mesero.Id = idMesero;

                AsignacionMesaNegocio negocio = new AsignacionMesaNegocio();
                List<AsignacionMesa> lista = negocio.listar();

                bool existeHoy = false;
                foreach (AsignacionMesa a in lista)
                {
                    if (a.Mesa.Id == idMesa && a.Fecha == DateTime.Today)
                    {
                        existeHoy = true;
                    }
                }


                if (existeHoy)
                {
                    negocio.modificar(asig);
                }
                else
                {
                    negocio.abrirMesa(asig);
                }

                Response.Redirect("Mesas.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mesas.aspx", false);
        }
    }
}