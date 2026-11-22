using System;

namespace dominio
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public Pedido Pedido { get; set; }
        public Item Item { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }
        public string ItemNombre
        {
            get { return Item != null ? Item.Nombre : ""; }
        }
    }
}