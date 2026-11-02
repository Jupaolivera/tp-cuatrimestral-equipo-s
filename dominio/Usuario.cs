using System;

namespace dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public int Perfil { get; set; } // 1 gerente 2  mesero
        public bool Activo { get; set; }
    }
}