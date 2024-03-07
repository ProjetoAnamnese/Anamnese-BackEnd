using CatalogAPI.Models;

namespace AnamneseAPI.Models
{
    public class ReportModel
    {

        public int Id { get; set; }
        public int PacientId { get; set; }
        public DateTime ReportDateTime { get; set; }
        public string MedicalHistory { get; set; }
        public string CurrentMedications { get; set; }
        public bool CardiovascularIssues { get; set; }
        public bool Diabetes { get; set; }


        //Historico Familiar
        public bool FamilyHistoryCardiovascularIssues { get; set; }
        public bool FamilyHistoryDiabetes { get; set; }


        // Lifestyle        
        public string PhysicalActivity { get; set; }
        public bool Smoker { get; set; }
        public int AlcoholConsumption { get; set; }


        // Other relevant information
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Observations { get; set; }

        public PacientModel Pacient { get; set; }

    }
}
