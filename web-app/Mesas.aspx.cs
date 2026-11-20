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
                cargarMesas();
            }
        }

        private void cargarMesas()
        {
            try
            {
                MesaNegocio negocio = new MesaNegocio();
                dgvMesas.DataSource = negocio.listar();
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

                lblMensaje.Text = "";
                btnEliminar.Visible = true;
                pnlFormulario.Visible = true;
            }
        }

        protected void btnNuevaMesa_Click(object sender, EventArgs e)
        {
            hfIdMesa.Value = "";
            txtNumero.Text = "";
            txtCapacidad.Text = "";
            lblMensaje.Text = "";
            btnEliminar.Visible = false;
            pnlFormulario.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int numero;
                int capacidad;

                if (!int.TryParse(txtNumero.Text, out numero) || numero <= 0)
                {
                    lblMensaje.Text = "El número de mesa debe ser mayor a 0.";
                    return;
                }
                if (!int.TryParse(txtCapacidad.Text, out capacidad) || capacidad <= 0)
                {
                    lblMensaje.Text = "La capacidad debe ser mayor a 0.";
                    return;
                }

                Mesa m = new Mesa();
                m.Numero = numero;
                m.Capacidad = capacidad;

                MesaNegocio negocio = new MesaNegocio();

                if (hfIdMesa.Value != "")
                {
                    m.Id = int.Parse(hfIdMesa.Value);
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
                MesaNegocio negocio = new MesaNegocio();
                negocio.eliminar(int.Parse(hfIdMesa.Value));

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

        public string mozoDeHoy(object id)
        {
            int idMesa = (int)id;

            AsignacionMesaNegocio asigNegocio = new AsignacionMesaNegocio();
            List<AsignacionMesa> asignaciones = asigNegocio.listar();

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

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> usuarios = usuarioNegocio.listar();

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