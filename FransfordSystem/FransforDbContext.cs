using Microsoft.EntityFrameworkCore;
using FransfordSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


//Clase que sirve para conectar a la base integrando las tablas en models
namespace FransfordSystem
{
    public class FransforDbContext : IdentityDbContext<Usuario>
    {
        public FransforDbContext(DbContextOptions<FransforDbContext> options)
            : base(options) { 
        
        
        }


       
 
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Examen> Examen { get; set; }
        public DbSet<Descripcion> Descripcion { get; set; }
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<ReporteExamen> ReporteExamen { get; set; }

        public DbSet<Resultado> Resultado { get; set; }
        public DbSet<Asistencia> Asistencia { get; set; }
        public DbSet<Producto> Producto { get; set; }

        public DbSet<Cita> Cita { get; set; }







    }


}
