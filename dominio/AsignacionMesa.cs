using System;

namespace dominio
{
    public class AsignacionMesa
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Mesa Mesa { get; set; }
        public Usuario Mesero { get; set; }
    }
}