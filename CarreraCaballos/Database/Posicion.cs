using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarreraCaballos.Database
{
    public class Posicion
    {
        [Key]
        public long Id { get; set; }
        public short Numero { get; set; }
        [ForeignKey("Carrera")]
        public long CarreraId { get; set; }
        public virtual Carrera Carrera { get; set; }
        [ForeignKey("Caballo")]
        public long CaballoId { get; set; }
        public virtual Caballo Caballo { get; set; }
    }
}