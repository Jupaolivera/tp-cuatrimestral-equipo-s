using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class AsignacionMesaNegocio
    {
        public List<AsignacionMesa> listar()
        {
            List<AsignacionMesa> lista = new List<AsignacionMesa>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // traigo los datos de tabla las asignaciones
                datos.setearConsulta("select Id, Fecha, IdMesa, IdMesero from AsignacionesMesa");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    AsignacionMesa a = new AsignacionMesa();
                    a.Id = (int)datos.Lector["Id"];
                    a.Fecha = (DateTime)datos.Lector["Fecha"];
                    a.Mesa = new Mesa();
                    a.Mesa.Id = (int)datos.Lector["IdMesa"];
                    a.Mesero = new Usuario();
                    a.Mesero.Id = (int)datos.Lector["IdMesero"];

                    lista.Add(a);
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

        public void abrirMesa(AsignacionMesa asig)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // guardo m y md 
                datos.setearConsulta("insert into AsignacionesMesa (Fecha, IdMesa, IdMesero) values (@fecha, @mesa, @mozo)");
                datos.setearParametro("@fecha", asig.Fecha);
                datos.setearParametro("@mesa", asig.Mesa.Id);
                datos.setearParametro("@mozo", asig.Mesero.Id);
                datos.ejecutarAccion();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(AsignacionMesa asig)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // cambia el mozo si ya tenia uno anotado
                datos.setearConsulta("update AsignacionesMesa set IdMesero = @mozo where IdMesa = @mesa and Fecha = @fecha");
                datos.setearParametro("@fecha", asig.Fecha);
                datos.setearParametro("@mesa", asig.Mesa.Id);
                datos.setearParametro("@mozo", asig.Mesero.Id);
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