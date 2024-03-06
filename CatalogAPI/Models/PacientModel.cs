using AnamneseAPI.Models;

namespace CatalogAPI.Models
{
    public class PacientModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Uf { get; set; }
        public string Birth { get; set; }
        public string Gender { get; set; }
        public int DoctorId { get; set; }
        public int ReportId { get; set; }        
        public DoctorModel Doctor { get; set; }        
        //public ReportModel Report { get; set; }        

        public virtual List<ReportModel> Patients { get; set; } = new List<ReportModel>();
    }
}
