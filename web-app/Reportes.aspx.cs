using System;
using System.Globalization;
using System.Web.UI;
using negocio;

namespace web_app
{
    public partial class Reportes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDesde.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtHasta.Text = DateTime.Today.ToString("yyyy-MM-dd");
                cargarReportes();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            cargarReportes();
        }

        private void cargarReportes()
        {
            DateTime desde, hasta;
            if (!DateTime.TryParseExact(txtDesde.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out desde) ||
                !DateTime.TryParseExact(txtHasta.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out hasta))
            {
                lblError.Text = "Las fechas no son válidas.";
                return;
            }

            if (desde > hasta)
            {
                lblError.Text = "La fecha inicial no puede ser posterior a la final.";
                return;
            }

            lblError.Text = "";

            try
            {
                ReporteNegocio negocio = new ReporteNegocio();

                dgvPorMesa.DataSource = negocio.pedidosPorMesa(desde, hasta);
                dgvPorMesa.DataBind();

                dgvPorMesero.DataSource = negocio.pedidosPorMesero(desde, hasta);
                dgvPorMesero.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
