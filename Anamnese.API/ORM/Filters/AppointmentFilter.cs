namespace Anamnese.API.ORM.Filters
{
    public class AppointmentFilter
    {
        public bool? IsCanceled { get; set; } = false;

        public DateTime? AppointmentDateTime { get; set; }
    }

}
