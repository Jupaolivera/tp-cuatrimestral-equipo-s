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
    public partial class FormularioUsuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;

            try
            {
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                rfvClave.Enabled = id == "";

                if (id != "" && !IsPostBack)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    List<Usuario> lista = negocio.listar(id);
                    if (lista.Count == 0)
                    {
                        Response.Redirect("Usuarios.aspx", false);
                        return;
                    }
                    Usuario seleccionado = lista[0];

                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtApellido.Text = seleccionado.Apellido;
                    txtEmail.Text = seleccionado.Email;
                    ddlPerfil.SelectedValue = seleccionado.Perfil.ToString();

                    lblClaveAyuda.Visible = true;
                    btnInactivar.Visible = true;
                    if (!seleccionado.Activo)
                        btnInactivar.Text = "Reactivar";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                int id = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;

                if (negocio.existeEmail(txtEmail.Text.Trim(), id))
                {
                    lblError.Text = "Ya existe un usuario con ese email.";
                    return;
                }

                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text.Trim();
                usuario.Apellido = txtApellido.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.Perfil = int.Parse(ddlPerfil.SelectedValue);

                if (id != 0)
                {
                    usuario.Id = id;
                    usuario.Clave = txtClave.Text != "" ? txtClave.Text : negocio.listar(id.ToString())[0].Clave;
                    negocio.modificar(usuario);
                }
                else
                {
                    usuario.Clave = txtClave.Text;
                    negocio.agregar(usuario);
                }

                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                string id = Request.QueryString["id"];
                Usuario seleccionado = negocio.listar(id)[0];

                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}