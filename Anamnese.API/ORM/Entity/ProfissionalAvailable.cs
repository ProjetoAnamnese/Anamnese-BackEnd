using System;
using System.Collections.Generic;

namespace Anamnese.API.ORM.Entity
{
    public class ProfessionalAvailable
    {
        public int ProfessionalAvailableId { get; set; }
        public int ProfessionalId { get; set; }
        public List<DayOfWeekAvailable> DaysOfWeek { get; set; } // Lista de dias da semana
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ProfissionalModel ProfessionalModel { get; set;}
    }

    public class DayOfWeekAvailable
    {
        public int DayOfWeekAvailableId { get; set; }
        public string DayOfWeek { get; set; }
    }
}
