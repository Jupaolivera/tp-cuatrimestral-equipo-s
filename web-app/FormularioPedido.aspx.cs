using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace web_app
{
    public partial class FormularioPedido : Page
    {
  
        private int idMeseroFiltro()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            return usuario.Perfil == 2 ? usuario.Id : 0;
        }

        // El pedido de la dirección tiene que existir, ser del mesero (si es mesero) y seguir abierto
        private bool pedidoAbiertoPropio()
        {
            int idPedido;
            if (!int.TryParse(Request.QueryString["id"], out idPedido))
                return false;

            PedidoNegocio negocio = new PedidoNegocio();
            foreach (Pedido p in negocio.listar(idMeseroFiltro()))
            {
                if (p.Id == idPedido)
                    return p.Estado == 1;
            }

            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Pedidos.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                cargar();
            }
        }

        private void cargar()
        {
            try
            {
                int idPedido;
                if (!int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    Response.Redirect("Pedidos.aspx", false);
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> lista = negocio.listar(idMeseroFiltro());

                Pedido pedidoActual = null;
                foreach (Pedido p in lista)
                {
                    if (p.Id == idPedido)
                    {
                        pedidoActual = p;
                    }
                }

                // Si no existe o pertenece a otro mesero, vuelvo al listado
                if (pedidoActual == null)
                {
                    Response.Redirect("Pedidos.aspx", false);
                    return;
                }

                lblTitulo.Text = "Pedido N° " + pedidoActual.Id;
                lblMesa.Text = "Mesa " + pedidoActual.MesaNumero;
                lblMesero.Text = pedidoActual.MeseroNombre;
                lblFechaApertura.Text = pedidoActual.FechaApertura.ToString("dd/MM/yyyy HH:mm");
                lblEstado.Text = pedidoActual.EstadoTexto;
                lblTotal.Text = pedidoActual.Total.ToString("C");

                dgvDetalles.DataSource = negocio.listarDetalles(idPedido);
                dgvDetalles.DataBind();

                itemNegocio itemNeg = new itemNegocio();
                List<Item> items = itemNeg.listar();

                ddlItem.Items.Clear();
                foreach (Item aux in items)
                {
                    ddlItem.Items.Add(new ListItem(aux.Nombre + " ($" + aux.Precio.ToString("0.00") + ") - Stock: " + aux.Stock, aux.Id.ToString()));
                }

                bool abierto = pedidoActual.Estado == 1;
                pnlAgregarItem.Visible = abierto;
                btnCerrarPedido.Visible = abierto;
                dgvDetalles.Columns[4].Visible = abierto;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAgregarItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                if (!pedidoAbiertoPropio())
                {
                    Response.Redirect("Pedidos.aspx", false);
                    return;
                }

                if (ddlItem.Items.Count == 0)
                {
                    lblError.Text = "No hay ítems disponibles en la carta.";
                    return;
                }

                int cantidad = int.Parse(txtCantidad.Text.Trim());
                int idPedido = int.Parse(Request.QueryString["id"]);
                int idItem = int.Parse(ddlItem.SelectedValue);

                itemNegocio itemNeg = new itemNegocio();
                List<Item> items = itemNeg.listar();

                Item seleccionado = null;
                foreach (Item aux in items)
                {
                    if (aux.Id == idItem)
                    {
                        seleccionado = aux;
                    }
                }

                if (seleccionado == null)
                {
                    lblError.Text = "El ítem seleccionado no está disponible.";
                    return;
                }

                if (cantidad > seleccionado.Stock)
                {
                    lblError.Text = "No hay stock suficiente. Stock disponible: " + seleccionado.Stock + ".";
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.agregarItem(idPedido, idItem, cantidad);

                txtCantidad.Text = "1";
                lblError.Text = "";
                cargar();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    if (!pedidoAbiertoPropio())
                    {
                        Response.Redirect("Pedidos.aspx", false);
                        return;
                    }

                    int idDetalle = Convert.ToInt32(e.CommandArgument);
                    PedidoNegocio negocio = new PedidoNegocio();

                    bool esDelPedido = false;
                    foreach (DetallePedido d in negocio.listarDetalles(int.Parse(Request.QueryString["id"])))
                    {
                        if (d.Id == idDetalle)
                            esDelPedido = true;
                    }
                    if (!esDelPedido)
                    {
                        Response.Redirect("Pedidos.aspx", false);
                        return;
                    }

                    negocio.eliminarItem(idDetalle);

                    lblError.Text = "";
                    cargar();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        protected void btnCerrarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pedidoAbiertoPropio())
                {
                    Response.Redirect("Pedidos.aspx", false);
                    return;
                }

                int idPedido = int.Parse(Request.QueryString["id"]);
                PedidoNegocio negocio = new PedidoNegocio();

                if (negocio.listarDetalles(idPedido).Count == 0)
                {
                    lblError.Text = "No se puede cerrar un pedido sin ítems.";
                    return;
                }

                negocio.cerrar(idPedido);

                Response.Redirect("Pedidos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pedidos.aspx", false);
        }
    }
}