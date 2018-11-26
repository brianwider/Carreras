using System.Collections.Generic;
using CarreraCaballos.Database;

namespace CarreraCaballos.Models
{
    public class ApuestaViewModel
    {
        public long Id { get; set; }
        public Carrera Carrera { get; set; }
        public float Valor { get; set; }
        public long CaballoId { get; set; }
        public long CarreraId { get; set; }
        public List<Caballo> Caballos { get; set; }
    }
}