using Anamnese.API.ORM.Entity;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.ORM.Context
{
    public class AnamneseDbContext : DbContext
    {        
            public AnamneseDbContext(DbContextOptions<AnamneseDbContext> options): base(options)
            {
            }          
            public DbSet<Profissional> Profissional { get; set; }
            
    }
    }
