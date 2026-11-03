using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class itemNegocio
    {
        public List<Item> listar()
        {
            List<Item> lista = new List<Item>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Descripcion, Precio, Stock, IdTipoItem, Activo FROM Items");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Item aux = new Item();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];

                    // Asignamos el objeto Tipo y su Id correspondiente
                    aux.Tipo = new TipoItem();
                    aux.Tipo.Id = (int)datos.Lector["IdTipoItem"];

                    aux.Activo = (bool)datos.Lector["Activo"];

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