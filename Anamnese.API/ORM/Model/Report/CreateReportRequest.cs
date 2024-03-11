namespace Anamnese.API.ORM.Model.Report
{
    public class CreateReportRequest
    {
        public string MedicalHistory { get; set; }
        public string CurrentMedications { get; set; }
        public bool CardiovascularIssues { get; set; }
        public bool Diabetes { get; set; }
        public bool FamilyHistoryCardiovascularIssues { get; set; }
        public bool FamilyHistoryDiabetes { get; set; }
        public string PhysicalActivity { get; set; }
        public bool Smoker { get; set; }
        public int AlcoholConsumption { get; set; }

        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Observations { get; set; }
    }
}
