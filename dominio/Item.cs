using System;

namespace dominio
{
    public class Item
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public TipoItem Tipo { get; set; }
        public bool Activo { get; set; }
        public string TipoDescripcion
        {
            get { return Tipo != null ? Tipo.Descripcion : ""; }
        }
    }
}