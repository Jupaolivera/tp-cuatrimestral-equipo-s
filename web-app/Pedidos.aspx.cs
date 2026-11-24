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
    public partial class Pedidos : System.Web.UI.Page
    {
        // 0 = ve todo (gerente); si es mesero, su Id
        private int idMeseroFiltro()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            return usuario.Perfil == 2 ? usuario.Id : 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
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
                PedidoNegocio negocio = new PedidoNegocio();
                int idMesero = idMeseroFiltro();

                ddlMesa.Items.Clear();
                foreach (Mesa m in negocio.listarMesasParaAbrir(idMesero))
                {
                    ddlMesa.Items.Add(new ListItem("Mesa " + m.Numero, m.Id.ToString()));
                }
                btnAbrir.Enabled = ddlMesa.Items.Count > 0;

                cargarPedidos();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void cargarPedidos()
        {
            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> lista = negocio.listar(idMeseroFiltro());

                if (ddlEstadoFiltro.SelectedValue != "")
                    lista = lista.FindAll(x => x.Estado.ToString() == ddlEstadoFiltro.SelectedValue);

                string mesa = txtMesaFiltro.Text.Trim();
                if (mesa != "")
                    lista = lista.FindAll(x => x.MesaNumero.ToString().Contains(mesa));

                dgvPedidos.DataSource = lista;
                dgvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarPedidos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlEstadoFiltro.SelectedIndex = 0;
            txtMesaFiltro.Text = "";
            cargarPedidos();
        }

        protected void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMesa.Items.Count == 0)
                {
                    lblError.Text = "No hay mesas asignadas hoy sin pedido abierto.";
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.abrir(int.Parse(ddlMesa.SelectedValue));
                lblError.Text = "";
                cargar();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPedidos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioPedido.aspx?id=" + id, false);
        }
    }
}