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

                dgvPedidos.DataSource = negocio.listar(idMesero);
                dgvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMesa.Items.Count == 0)
                {
                    lblMensaje.Text = "No hay mesas asignadas hoy sin pedido abierto.";
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.abrir(int.Parse(ddlMesa.SelectedValue));
                lblMensaje.Text = "";
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