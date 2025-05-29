namespace Anamnese.API.ORM.Model.PacientModel
{
    public class PacientResponseModel
    {
        public int PacientId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Uf { get; set; }
        public DateOnly Birth { get; set; }
        public string Gender { get; set; }
        public int ProfissionalId { get; set; }
        public string? MedicalSpeciality { get; set; }
    }
}
