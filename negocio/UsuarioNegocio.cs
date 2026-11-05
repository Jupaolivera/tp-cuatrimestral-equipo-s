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
    }
}