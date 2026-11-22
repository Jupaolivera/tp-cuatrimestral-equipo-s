using System;
using System.Collections.Generic;

namespace dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public Mesa Mesa { get; set; }
        public Usuario Mesero { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; } 
        public int Estado { get; set; } // uno abierto dos cerrdo
        public decimal Total { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
        public int MesaNumero { get; set; }
        public string MeseroNombre { get; set; }
        public string EstadoTexto
        {
            get { return Estado == 1 ? "Abierto" : "Cerrado"; }
        }
    }
}