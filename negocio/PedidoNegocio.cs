using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class PedidoNegocio
    {
        // Todos los pedidos (gerente)
        public List<Pedido> listar()
        {
            return listar(0);
        }

        // idMesero = 0 trae todos; si no, solo los de ese mesero
        public List<Pedido> listar(int idMesero)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.IdMesa, P.IdMesero, P.FechaApertura, P.FechaCierre, P.Estado, P.Total, M.Numero, U.Nombre + ' ' + U.Apellido AS NombreMesero FROM Pedidos P INNER JOIN Mesas M ON M.Id = P.IdMesa INNER JOIN Usuarios U ON U.Id = P.IdMesero WHERE (@mesero = 0 OR P.IdMesero = @mesero) ORDER BY P.Id DESC");
                datos.setearParametro("@mesero", idMesero);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Mesa = new Mesa();
                    aux.Mesa.Id = (int)datos.Lector["IdMesa"];
                    aux.Mesero = new Usuario();
                    aux.Mesero.Id = (int)datos.Lector["IdMesero"];
                    aux.FechaApertura = (DateTime)datos.Lector["FechaApertura"];

                    if (!(datos.Lector["FechaCierre"] is DBNull))
                        aux.FechaCierre = (DateTime)datos.Lector["FechaCierre"];

                    aux.Estado = (int)datos.Lector["Estado"];
                    aux.Total = (decimal)datos.Lector["Total"];
                    aux.MesaNumero = (int)datos.Lector["Numero"];
                    aux.MeseroNombre = (string)datos.Lector["NombreMesero"];

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

        // Mesas asignadas hoy sin pedido abierto. idMesero = 0 trae todas (gerente)
        public List<Mesa> listarMesasParaAbrir(int idMesero)
        {
            List<Mesa> lista = new List<Mesa>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT M.Id, M.Numero FROM Mesas M INNER JOIN AsignacionesMesa A ON A.IdMesa = M.Id AND A.Fecha = CAST(GETDATE() AS DATE) WHERE M.Activo = 1 AND (@mesero = 0 OR A.IdMesero = @mesero) AND NOT EXISTS (SELECT 1 FROM Pedidos P WHERE P.IdMesa = M.Id AND P.Estado = 1)");
                datos.setearParametro("@mesero", idMesero);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa aux = new Mesa();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = (int)datos.Lector["Numero"];
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
        public void abrir(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Pedidos (IdMesa, IdMesero) SELECT IdMesa, IdMesero FROM AsignacionesMesa WHERE IdMesa = @mesa AND Fecha = CAST(GETDATE() AS DATE)");
                datos.setearParametro("@mesa", idMesa);
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
        public List<DetallePedido> listarDetalles(int idPedido)
        {
            List<DetallePedido> lista = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT D.Id, D.IdItem, I.Nombre, D.Cantidad, D.PrecioUnitario FROM DetallesPedido D INNER JOIN Items I ON I.Id = D.IdItem WHERE D.IdPedido = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido aux = new DetallePedido();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Item = new Item();
                    aux.Item.Id = (int)datos.Lector["IdItem"];
                    aux.Item.Nombre = (string)datos.Lector["Nombre"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
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
        public void agregarItem(int idPedido, int idItem, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE DetallesPedido SET Cantidad = Cantidad + @cant WHERE IdPedido = @pedido AND IdItem = @item; " +
                    "IF @@ROWCOUNT = 0 INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario) SELECT @pedido, Id, @cant, Precio FROM Items WHERE Id = @item; " +
                    "UPDATE Items SET Stock = Stock - @cant WHERE Id = @item; " +
                    "UPDATE Pedidos SET Total = (SELECT SUM(Cantidad * PrecioUnitario) FROM DetallesPedido WHERE IdPedido = @pedido) WHERE Id = @pedido;");
                datos.setearParametro("@pedido", idPedido);
                datos.setearParametro("@item", idItem);
                datos.setearParametro("@cant", cantidad);
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
        public void cerrar(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Pedidos SET Estado = 2, FechaCierre = GETDATE() WHERE Id = @id");
                datos.setearParametro("@id", idPedido);
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
