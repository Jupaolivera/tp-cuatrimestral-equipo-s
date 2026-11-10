using System;
using System.Collections.Generic;
// using System.Linq; // me lo agrego el visual no se que hace sedgun chates para consultas y coleciones de datos sera importante?
using dominio;

namespace negocio
{
    public class MesaNegocio
    {
        public List<Mesa> listar()
        {
            List<Mesa> lista = new List<Mesa>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // traigo las mesas
                datos.setearConsulta("select Id, Numero, Capacidad, Activo from Mesas where Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa m = new Mesa(); // le pongo m por mesaaa
                    m.Id = (int)datos.Lector["Id"];
                    m.Numero = (int)datos.Lector["Numero"];
                    m.Capacidad = (int)datos.Lector["Capacidad"];
                    m.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(m);
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

        public void agregar(Mesa m)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // le mando 1 directo en el insert para el activo
                datos.setearConsulta("insert into Mesas (Numero, Capacidad, Activo) values (@num, @cap, 1)");
                datos.setearParametro("@num", m.Numero);
                datos.setearParametro("@cap", m.Capacidad);

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

        public void modificar(Mesa mesaModificada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Mesas set Numero = @numero, Capacidad = @capacidad where Id = @id");
                datos.setearParametro("@numero", mesaModificada.Numero);
                datos.setearParametro("@capacidad", mesaModificada.Capacidad);
                datos.setearParametro("@id", mesaModificada.Id);

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
                // baja logica, actualizo el estado
                datos.setearConsulta("update Mesas set Activo = 0 where Id = @id");
                datos.setearParametro("@id", id);

                datos.ejecutarAccion(); //Manana es mi cunple pero tengop que trabajar maldita vida escrito el 10 de noviembre esta fila
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