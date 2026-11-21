using System;
using System.Data;

namespace negocio
{
    public class ReporteNegocio
    {
        public DataTable pedidosPorMesa(DateTime desde, DateTime hasta)
        {
            string consulta = "Select m.Numero As Mesa, Count(p.Id) As Pedidos, Sum(p.Total) As Total " +
                "From Pedidos p Inner Join Mesas m On m.Id = p.IdMesa " +
                "Where p.FechaApertura >= @desde And p.FechaApertura < @hasta " +
                "Group By m.Numero Order By m.Numero";

            return consultar(consulta, desde, hasta);
        }

        public DataTable pedidosPorMesero(DateTime desde, DateTime hasta)
        {
            string consulta = "Select u.Apellido + ', ' + u.Nombre As Mesero, Count(p.Id) As Pedidos, Sum(p.Total) As Total " +
                "From Pedidos p Inner Join Usuarios u On u.Id = p.IdMesero " +
                "Where p.FechaApertura >= @desde And p.FechaApertura < @hasta " +
                "Group By u.Apellido, u.Nombre Order By u.Apellido, u.Nombre";

            return consultar(consulta, desde, hasta);
        }

        // Las dos fechas son días completos: el rango incluye todo el día "hasta".
        private DataTable consultar(string consulta, DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametro("@desde", desde.Date);
                datos.setearParametro("@hasta", hasta.Date.AddDays(1));
                datos.ejecutarLectura();

                DataTable tabla = new DataTable();
                tabla.Load(datos.Lector);
                return tabla;
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
