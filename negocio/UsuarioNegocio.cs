using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar(string id = "")
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select Id, Nombre, Apellido, Email, Clave, Perfil, Activo From Usuarios";
                if (id != "")
                    consulta += " Where Id = @id";
                consulta += " Order By Apellido, Nombre";

                datos.setearConsulta(consulta);
                if (id != "")
                    datos.setearParametro("@id", int.Parse(id));
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Clave = (string)datos.Lector["Clave"];
                    aux.Perfil = (int)datos.Lector["Perfil"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into Usuarios (Nombre, Apellido, Email, Clave, Perfil, Activo) values (@nombre, @apellido, @email, @clave, @perfil, 1)");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@clave", nuevo.Clave);
                datos.setearParametro("@perfil", nuevo.Perfil);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update Usuarios set Nombre = @nombre, Apellido = @apellido, Email = @email, Clave = @clave, Perfil = @perfil Where Id = @id");
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@clave", usuario.Clave);
                datos.setearParametro("@perfil", usuario.Perfil);
                datos.setearParametro("@id", usuario.Id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarLogico(int id, bool activo = false)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update Usuarios set Activo = @activo Where Id = @id");
                datos.setearParametro("@activo", activo);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeEmail(string email, int idExcluido = 0)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select count(*) From Usuarios Where Email = @email And Id <> @id");
                datos.setearParametro("@email", email);
                datos.setearParametro("@id", idExcluido);

                return datos.ejecutarAccionScalar() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}