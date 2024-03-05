namespace CatalogAPI.Models;

public class DoctorModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public virtual List<PacientModel> Patients { get; set; } = new List<PacientModel>();
}
