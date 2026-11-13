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
    public partial class FormularioTipoItem : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox txtDescripcion;
        protected System.Web.UI.WebControls.Label lblMensaje;
        protected System.Web.UI.WebControls.Button btnAceptar;
        protected System.Web.UI.WebControls.Button btnEliminar;
        protected System.Web.UI.WebControls.Button btnCancelar;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        TipoItemNegocio negocio = new TipoItemNegocio();
                        List<TipoItem> lista = negocio.listar();

                        foreach (TipoItem aux in lista)
                        {
                            if (aux.Id == id)
                            {
                                txtDescripcion.Text = aux.Descripcion;
                            }
                        }

                        btnEliminar.Visible = true;
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
                if (txtDescripcion.Text == "")
                {
                    lblMensaje.Text = "La descripción es obligatoria.";
                    return;
                }

                TipoItem tipo = new TipoItem();
                tipo.Descripcion = txtDescripcion.Text;

                TipoItemNegocio negocio = new TipoItemNegocio();

                if (Request.QueryString["id"] != null)
                {
                    tipo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(tipo);
                }
                else
                {
                    negocio.agregar(tipo);
                }

                Response.Redirect("TiposItem.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoItemNegocio negocio = new TipoItemNegocio();
                negocio.eliminar(int.Parse(Request.QueryString["id"]));
                Response.Redirect("TiposItem.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("TiposItem.aspx", false);
        }
    }
}