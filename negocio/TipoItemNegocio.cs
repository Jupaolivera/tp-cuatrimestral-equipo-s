using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class TipoItemNegocio
    {
        public List<TipoItem> listar()
        {
            List<TipoItem> lista = new List<TipoItem>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM TiposItem");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoItem aux = new TipoItem();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}