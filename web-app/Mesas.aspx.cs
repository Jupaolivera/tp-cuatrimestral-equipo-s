using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace web_app
{
    public partial class Mesas : System.Web.UI.Page
    {
        // Se cargan una sola vez por listado, para mostrar el mesero de hoy de cada mesa
        private List<AsignacionMesa> asignaciones;
        private List<Usuario> usuarios;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarMesas();
            }
        }

        private void cargarMesas()
        {
            try
            {
                MesaNegocio negocio = new MesaNegocio();
                List<Mesa> lista = negocio.listar();

                string texto = txtFiltro.Text.Trim();
                if (texto != "")
                    lista = lista.FindAll(x => x.Numero.ToString().Contains(texto));

                asignaciones = new AsignacionMesaNegocio().listar();
                usuarios = new UsuarioNegocio().listar();

                dgvMesas.DataSource = lista;
                dgvMesas.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvMesas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idMesa = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Asignar")
            {
                Response.Redirect("Asignaciones.aspx?id=" + idMesa, false);
            }
            else if (e.CommandName == "Modificar")
            {
                MesaNegocio negocio = new MesaNegocio();
                List<Mesa> lista = negocio.listar();

                foreach (Mesa m in lista)
                {
                    if (m.Id == idMesa)
                    {
                        hfIdMesa.Value = m.Id.ToString();
                        txtNumero.Text = m.Numero.ToString();
                        txtCapacidad.Text = m.Capacidad.ToString();
                    }
                }

                lblError.Text = "";
                btnEliminar.Visible = true;
                pnlFormulario.Visible = true;
            }
        }

        protected void btnNuevaMesa_Click(object sender, EventArgs e)
        {
            hfIdMesa.Value = "";
            txtNumero.Text = "";
            txtCapacidad.Text = "";
            lblError.Text = "";
            btnEliminar.Visible = false;
            pnlFormulario.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                int numero = int.Parse(txtNumero.Text.Trim());
                int capacidad = int.Parse(txtCapacidad.Text.Trim());

                MesaNegocio negocio = new MesaNegocio();
                int idActual = hfIdMesa.Value != "" ? int.Parse(hfIdMesa.Value) : 0;
                Mesa existente = negocio.buscarPorNumero(numero);

                if (existente != null && existente.Id != idActual)
                {
                    if (existente.Activo)
                    {
                        lblError.Text = "Ya existe una mesa activa con ese número.";
                        return;
                    }
                    else if (idActual != 0)
                    {
                        lblError.Text = "Ese número pertenece a una mesa eliminada. Creala desde 'Agregar Nueva Mesa'.";
                        return;
                    }
                }

                Mesa m = new Mesa();
                m.Numero = numero;
                m.Capacidad = capacidad;

                if (idActual != 0)
                {
                    m.Id = idActual;
                    negocio.modificar(m);
                }
                else
                {
                    negocio.agregar(m);
                }

                pnlFormulario.Visible = false;
                cargarMesas();
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
                int idMesa = int.Parse(hfIdMesa.Value);

                PedidoNegocio pedidoNeg = new PedidoNegocio();
                foreach (Pedido p in pedidoNeg.listar())
                {
                    if (p.Mesa.Id == idMesa && p.Estado == 1)
                    {
                        lblMensaje.Text = "No se puede eliminar la mesa porque tiene un pedido abierto.";
                        return;
                    }
                }

                MesaNegocio negocio = new MesaNegocio();
                negocio.eliminar(idMesa);

                pnlFormulario.Visible = false;
                cargarMesas();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarMesas();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            cargarMesas();
        }

        public string mozoDeHoy(object id)
        {
            int idMesa = (int)id;

            int idMesero = 0;
            foreach (AsignacionMesa a in asignaciones)
            {
                if (a.Mesa.Id == idMesa && a.Fecha == DateTime.Today)
                {
                    idMesero = a.Mesero.Id;
                }
            }

            if (idMesero == 0)
                return "Sin asignar";

            foreach (Usuario u in usuarios)
            {
                if (u.Id == idMesero)
                {
                    return u.Nombre + " " + u.Apellido;
                }
            }

            return "Sin asignar";
        }
    }
}