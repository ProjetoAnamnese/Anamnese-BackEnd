namespace Anamnese.API.ORM.Model.PacientModel
{
    public class CreatePacientRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Uf { get; set; }
        public string Birth { get; set; }
        public string Gender { get; set; }
    }
}
