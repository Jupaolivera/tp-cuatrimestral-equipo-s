using dominio;
using negocio;
using System;
using System.Collections.Generic;

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


        public void agregar(Item nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Items (Nombre, Descripcion, Precio, Stock, IdTipoItem, Activo) VALUES (@nombre, @descripcion, @precio, @stock, @idTipoItem, @activo)");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.setearParametro("@stock", nuevo.Stock);
                datos.setearParametro("@idTipoItem", nuevo.Tipo.Id);
                datos.setearParametro("@activo", true);
                datos.ejecutarAccion();
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
        public void modificar(Item item)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Items SET Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, Stock = @stock, IdTipoItem = @idTipoItem WHERE Id = @id");
                datos.setearParametro("@nombre", item.Nombre);
                datos.setearParametro("@descripcion", item.Descripcion);
                datos.setearParametro("@precio", item.Precio);
                datos.setearParametro("@stock", item.Stock);
                datos.setearParametro("@idTipoItem", item.Tipo.Id);
                datos.setearParametro("@id", item.Id);

                datos.ejecutarAccion();
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
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Items SET Activo = 0 WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
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