using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Desarrollador { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public int Numero_Votos { get; set; }
        public DateTime Fecha_Lanzamiento { get; set; }
        public ICollection<ApplicationUser>? Users { get; set; } = new List<ApplicationUser>();
    }
}
