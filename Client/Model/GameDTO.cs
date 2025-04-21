namespace Client.Model
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Desarrollador { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public int Numero_Votos { get; set; }
        public DateTime Fecha_Lanzamiento { get; set; }
    }
}
