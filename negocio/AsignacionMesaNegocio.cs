using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class AsignacionMesaNegocio
    {
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
    }
}