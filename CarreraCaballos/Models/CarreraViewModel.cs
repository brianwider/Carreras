using System;
using System.Collections.Generic;
using CarreraCaballos.Database;

namespace CarreraCaballos.Models
{
    public class CarreraViewModel
    {
        public long Id { get; set; }
        public DateTime Inicio { get; set; }
        public List<Caballo> Caballos { get; set; }
        public List<long> CaballosSeleccionados { get; set; }
        public List<Caballo> CaballosSeleccionadosPresentacion{ get; set; }
    }
}