using System;
using System.Collections.Generic;
using System.Globalization;
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
        protected System.Web.UI.WebControls.Button btnAceptar;
        protected System.Web.UI.WebControls.Button btnEliminar;
        protected System.Web.UI.WebControls.Button btnCancelar;
        protected System.Web.UI.WebControls.Label lblError;
        // true si el ítem está cargado en algún pedido abierto
        private bool itemEnPedidoAbierto(int idItem)
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            foreach (Pedido p in pedidoNegocio.listar())
            {
                if (p.Estado == 1)
                {
                    foreach (DetallePedido d in pedidoNegocio.listarDetalles(p.Id))
                    {
                        if (d.Item.Id == idItem)
                            return true;
                    }
                }
            }
            return false;
        }
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
                        if (itemEnPedidoAbierto(id))
                        {
                            lblError.Text = "Este ítem está en un pedido abierto. No se puede modificar ni eliminar hasta que el pedido se cierre.";
                            btnAceptar.Enabled = false;
                            btnEliminar.Enabled = false;
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
                if (!Page.IsValid)
                    return;
                if (Request.QueryString["id"] != null && itemEnPedidoAbierto(int.Parse(Request.QueryString["id"])))
                {
                    lblError.Text = "Este ítem está en un pedido abierto. No se puede modificar hasta que el pedido se cierre.";
                    return;
                }
                // El precio se acepta con punto o con coma como separador decimal
                decimal precio = decimal.Parse(txtPrecio.Text.Trim().Replace(',', '.'), CultureInfo.InvariantCulture);
                int stock = int.Parse(txtStock.Text.Trim());

                Item item = new Item();
                item.Nombre = txtNombre.Text.Trim();
                item.Descripcion = txtDescripcion.Text.Trim();
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
                int id = int.Parse(Request.QueryString["id"]);

                if (itemEnPedidoAbierto(id))
                {
                    lblError.Text = "Este ítem está en un pedido abierto. No se puede eliminar hasta que el pedido se cierre.";
                    return;
                }

                new itemNegocio().eliminar(id);
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