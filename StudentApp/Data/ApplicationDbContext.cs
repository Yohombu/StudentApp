using Microsoft.EntityFrameworkCore;
using StudentApp.Models.Entities;

namespace StudentApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Will recieve options as parameter and send it to the base class DB context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }
    }
}