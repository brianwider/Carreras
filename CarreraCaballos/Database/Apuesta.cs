using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarreraCaballos.Models;

namespace CarreraCaballos.Database
{
    public class Apuesta
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Caballo")]
        public long CaballoId { get; set; }
        public virtual Caballo Caballo { get; set; }
        public float Valor { get; set; }
        [ForeignKey("Carrera")]
        public long CarreraId { get; set; }
        public virtual Carrera Carrera { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}