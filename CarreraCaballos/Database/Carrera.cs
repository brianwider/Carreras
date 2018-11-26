using System;
using System.Collections.Generic;

namespace CarreraCaballos.Database
{
    public class Carrera
    {
        public long Id { get; set; }
        public DateTime Inicio { get; set; }
        public virtual ICollection<Posicion> Posiciones { get; set; }
        public virtual ICollection<Caballo> Caballos { get; set; }
    }
}