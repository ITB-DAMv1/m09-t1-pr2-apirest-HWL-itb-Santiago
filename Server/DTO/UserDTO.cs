namespace Server.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<GameDTO> Games { get; set; } = new List<GameDTO>();
    }
}
