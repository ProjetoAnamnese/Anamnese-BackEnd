namespace Anamnese.API.ORM.Model.ProfissionalModel
{
    public class ProfissionalAvailableRequest
    {
        public string DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
