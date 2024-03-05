namespace AnamneseAPI.Services.Pacient.Models
{
    public class CreatePacientRequest
    {
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Uf { get; set; }
        public string Birth { get; set; }
        public string Gender { get; set; }
    }
}
