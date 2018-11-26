using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarreraCaballos.Database
{
    public class Caballo
    {
        [Key]
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Carrera> Carreras { get; set; }
        public virtual ICollection<Posicion> Posiciones { get; set; }
    }
}