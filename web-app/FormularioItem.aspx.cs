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
    public partial class FormularioItem : System.Web.UI.Page
    {
        // Declaración explícita de controles para que el backend los reconozca sin archivo designer
        protected System.Web.UI.WebControls.TextBox txtNombre;
        protected System.Web.UI.WebControls.TextBox txtDescripcion;
        protected System.Web.UI.WebControls.TextBox txtPrecio;
        protected System.Web.UI.WebControls.TextBox txtStock;
        protected System.Web.UI.WebControls.DropDownList ddlTipo;
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
                    TipoItemNegocio tipoNegocio = new TipoItemNegocio();
                    ddlTipo.DataSource = tipoNegocio.listar();
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        itemNegocio negocio = new itemNegocio();
                        List<Item> lista = negocio.listar();

                        foreach (Item aux in lista)
                        {
                            if (aux.Id == id)
                            {
                                txtNombre.Text = aux.Nombre;
                                txtDescripcion.Text = aux.Descripcion;
                                txtPrecio.Text = aux.Precio.ToString();
                                txtStock.Text = aux.Stock.ToString();
                                ddlTipo.SelectedValue = aux.Tipo.Id.ToString();
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
                decimal precio;
                int stock;

                if (txtNombre.Text == "")
                {
                    lblMensaje.Text = "El nombre es obligatorio.";
                    return;
                }
                if (!decimal.TryParse(txtPrecio.Text, out precio) || precio < 0)
                {
                    lblMensaje.Text = "El precio debe ser un número mayor o igual a 0.";
                    return;
                }
                if (!int.TryParse(txtStock.Text, out stock) || stock < 0)
                {
                    lblMensaje.Text = "El stock debe ser un número entero mayor o igual a 0.";
                    return;
                }

                Item item = new Item();
                item.Nombre = txtNombre.Text;
                item.Descripcion = txtDescripcion.Text;
                item.Precio = precio;
                item.Stock = stock;
                item.Tipo = new TipoItem();
                item.Tipo.Id = int.Parse(ddlTipo.SelectedValue);

                itemNegocio negocio = new itemNegocio();

                if (Request.QueryString["id"] != null)
                {
                    item.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(item);
                }
                else
                {
                    negocio.agregar(item);
                }

                Response.Redirect("Items.aspx", false);
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
                itemNegocio negocio = new itemNegocio();
                negocio.eliminar(int.Parse(Request.QueryString["id"]));
                Response.Redirect("Items.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Items.aspx", false);
        }
    }
}