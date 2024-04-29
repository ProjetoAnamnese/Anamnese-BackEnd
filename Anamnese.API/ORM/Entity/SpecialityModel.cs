using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anamnese.API.ORM.Entity
{
    public class SpecialityModel
    {
        [Key]
        public string SpecialityCode { get; set; }
        public string SpecialityName { get; set; }

        //[ForeignKey("ProfissionalId")]        
        List< ProfissionalModel> Profissional { get; set; }

    }
}
